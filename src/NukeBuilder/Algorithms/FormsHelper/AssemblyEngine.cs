using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NukeBuilder.Algorithms.FormsHelper
{
    internal class AssemblyEngine
    {
        public static string ExtractAndWriteVersionInfo(string sourceFilePath, string targetFilePath)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(sourceFilePath);

            string versionInfoText = $"{fileVersionInfo.InternalName ?? "N/A"}\n" +
                                     $"{fileVersionInfo.FileDescription ?? "N/A"}\n" +
                                     $"{fileVersionInfo.CompanyName ?? "N/A"}\n" +
                                     $"{fileVersionInfo.ProductName ?? "N/A"}\n" +
                                     $"{fileVersionInfo.LegalCopyright ?? "N/A"}\n" +
                                     $"{fileVersionInfo.LegalTrademarks ?? "N/A"}\n" +
                                     $"{fileVersionInfo.FileMajorPart}.{fileVersionInfo.FileMinorPart}.{fileVersionInfo.FileBuildPart}.{fileVersionInfo.FilePrivatePart}\n" +
                                     $"{fileVersionInfo.ProductMajorPart}.{fileVersionInfo.ProductMinorPart}.{fileVersionInfo.ProductBuildPart}.{fileVersionInfo.ProductPrivatePart}";

            try
            {
                File.WriteAllText(targetFilePath, versionInfoText);
                return targetFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing version info: {ex.Message}");
                return null; 
            }
        }

    }
}
