using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryLib
{
    public class Memory
    {
        public const string kernel_lib = "kernel32.dll";
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [DllImport(kernel_lib, SetLastError = true)]
        public static extern IntPtr OpenProcess(
                 ProcessAccessFlags processAccess,
                 bool bInheritHandle,
                 int processId);
        public static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
        {
            return OpenProcess(flags, false, proc.Id);
        }

        [DllImport(kernel_lib, SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        [DllImport(kernel_lib, SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out, MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        [DllImport(kernel_lib, SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            IntPtr lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        [DllImport(kernel_lib, SetLastError = true)]
        public static extern bool WriteProcessMemory(
                  IntPtr hProcess,
                  IntPtr lpBaseAddress,
                  byte[] lpBuffer,
                  Int32 nSize,
                  out IntPtr lpNumberOfBytesWritten);

        [DllImport(kernel_lib, SetLastError = true)]
        public static extern bool WriteProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          [MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
          int dwSize,
          out IntPtr lpNumberOfBytesWritten);

        public Memory(string processName)
        {
            IntPtr pHandle = OpenProcess(Process.GetProcessesByName(processName)[0], ProcessAccessFlags.All);
        }
    }
}