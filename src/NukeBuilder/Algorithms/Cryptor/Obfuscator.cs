using dnlib.DotNet.Emit;
using dnlib.DotNet;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NukeBuilder.Algorithms.Cryptor
{
    internal class Obfuscator
    {
        private static Random random = new Random();
        public static void PerformObfuscation(string outputFile)
        {
            string directory = Path.GetDirectoryName(outputFile);
            string originalFileName = Path.GetFileName(outputFile);
            string moduleNew = Path.Combine(directory, $"tmp_{originalFileName}");
            try
            {
                File.Copy(outputFile, moduleNew, overwrite: true);
                using (ModuleDef module = ModuleDefMD.Load(moduleNew))
                {
                    RenameProtector.Execute(module);
                    JunkMethods.Execute(module, RandomUtils.RandomNumber(100, 250), RandomUtils.RandomNumber(50, 95), 8);
                    module.Write(outputFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Obfuscation failed: {ex.Message}\nFailed method: {ex.TargetSite}");
            }
            finally
            {
                File.Delete(moduleNew);
            }
        }
        public static class RandomUtils
        {
            public static string RandomString(int length)
            {
                const string chars =
                    "abcdefghijklmnopqrstuvwxyz" +
                    "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                    "0123456789" +
                    "!@#$%^&*()_-+={[}]|:;<,>.?" +
                    "牡マキグナルファ系路克瑞大阪市立学鎰命科ャマ能力ϒ人は妻スティ要 望 通り玉宏¥サ丹谷Ѫ灯影伝鶐";

                char[] result = new char[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = chars[random.Next(chars.Length)];
                }

                return new string(result.OrderBy(_ => random.Next()).ToArray());
            }

            public static int RandomNumber(int minValue, int maxValue)
            {
                if (minValue >= maxValue)
                    throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");

                return random.Next(minValue, maxValue);
            }

            public static string RandomChar() => "!@#$%^&*()_-+={[}]|:;<,>.?"[new Random().Next(28)].ToString();

        }

        public class RenameProtector
        {
            public static int count_xxx = 0;

            public static void Execute(ModuleDef module)
            {
                try
                {
                    module.Name = RandomUtils.RandomString(7);

                    foreach (var type in module.Types)
                    {
                        if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface)
                            continue;

                        count_xxx++;
                        type.Name = RandomUtils.RandomString(40);
                        type.Namespace = "";

                        foreach (var property in type.Properties)
                        {
                            count_xxx++;
                            property.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var field in type.Fields)
                        {
                            count_xxx++;
                            field.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var eventDef in type.Events)
                        {
                            count_xxx++;
                            eventDef.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var method in type.Methods)
                        {
                            if (method.IsConstructor) continue;
                            count_xxx++;
                            method.Name = RandomUtils.RandomString(40);

                            foreach (var param in method.ParamDefs)
                            {
                                count_xxx++;
                                param.Name = RandomUtils.RandomString(40);
                            }

                            if (method.HasBody)
                            {
                                foreach (var local in method.Body.Variables)
                                {
                                    count_xxx++;
                                    local.Name = RandomUtils.RandomString(40);
                                }

                                foreach (var instr in method.Body.Instructions)
                                {
                                    if (instr.OpCode == OpCodes.Ldloc || instr.OpCode == OpCodes.Stloc)
                                    {
                                        var localVar = instr.Operand as Local;
                                        if (localVar != null && localVar.Name != null)
                                        {
                                            count_xxx++;
                                            localVar.Name = RandomUtils.RandomString(40);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during renaming: {ex.Message}");
                }
            }
        }
        public class JunkMethods
        {
            public static void Execute(ModuleDef module, int junkClasses, int junkMethodsPerClass, int junkInstructionsPerMethod)
            {
                try
                {
                    for (int i = 0; i < junkClasses; i++)
                    {
                        var junkType = new TypeDefUser(RandomUtils.RandomString(10), module.CorLibTypes.Object.TypeDefOrRef);
                        module.Types.Add(junkType);

                        for (int j = 0; j < junkMethodsPerClass; j++)
                        {
                            var junkMethod = new MethodDefUser(RandomUtils.RandomString(10), MethodSig.CreateStatic(module.CorLibTypes.Void), MethodAttributes.Public | MethodAttributes.Static)
                            {
                                Body = new CilBody { KeepOldMaxStack = true }
                            };

                            junkMethod.Body.Instructions.Add(OpCodes.Ret.ToInstruction());

                            JunkInstructionInserter.AddJunkInstructions(junkMethod, junkInstructionsPerMethod);

                            junkType.Methods.Add(junkMethod);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during junking: {ex}");
                }
            }
        }
        public class JunkInstructionInserter
        {
            public static void AddJunkInstructions(MethodDef method, int junkCount)
            {
                if (junkCount <= 0)
                    return;

                var instructions = method.Body.Instructions;
                var randomInstructions = GenerateRandomInstructions(junkCount);

                foreach (var junkInstr in randomInstructions)
                {
                    instructions.Insert(instructions.Count - 1, junkInstr);
                }
            }

            private static IEnumerable<Instruction> GenerateRandomInstructions(int count)
            {
                var junkInstructions = new List<Instruction>();

                for (int i = 0; i < count; i++)
                {
                    var randomOpCode = GetRandomOpCode();

                    Instruction junkInstr;
                    if (randomOpCode == OpCodes.Ldc_I4)
                    {
                        junkInstr = Instruction.Create(randomOpCode, random.Next(1, 100));
                    }
                    else if (randomOpCode.OperandType == OperandType.InlineNone) 
                    {
                        junkInstr = Instruction.Create(randomOpCode);
                    }
                    else
                    {
                        continue;
                    }

                    junkInstructions.Add(junkInstr);
                }

                return junkInstructions;
            }

            private static OpCode GetRandomOpCode()
            {
                var opCodes = new[]
                {
                    OpCodes.Nop,
                    OpCodes.Ldc_I4,
                    OpCodes.Add,
                    OpCodes.Sub,
                    OpCodes.Mul,
                    OpCodes.Div,
                    OpCodes.Call,
                    OpCodes.Ret
                };

                return opCodes[random.Next(opCodes.Length)];
            }
        }
    }
}