using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ControlDisplayInputSource
{
    internal class Helper
    {
        public static string AppName => "ControlDisplayInputSource";
        public static string AppVersion => GetVersionFromProject();
        public static string AppTitle => $"{AppName} {AppVersion} (by MBODM 11/2024)";
        public static string ExeName => $"{GetAssemblyNameFromProject().ToLower()}.exe";
        public static string ExeDescription => $"{ExeName} is a tiny Windows CLI tool, using DDC/CI to control the input source of a computer's display.";

        public static void ExitWithSuccess(bool showUsage)
        {
            if (showUsage)
            {
                Console.WriteLine();
                Console.WriteLine(ExeDescription);
                Console.WriteLine();
                Console.WriteLine("Usage:");
                Console.WriteLine();
                Console.WriteLine($"  {ExeName} --get        Get the display's acutal DDC VCP60 value (as a number between 1 and 65535)");
                Console.WriteLine($"  {ExeName} --set X      Set the display's acutal DDC VCP60 value (X: a number between 1 and 65535)");
                Console.WriteLine($"  {ExeName} --cap        Get the display's supported VCP60 values (via the DDC capabilities string)");
                Console.WriteLine();
                Console.WriteLine("Notes:");
                Console.WriteLine();
                Console.WriteLine("  - If your display does not support DDC/CI, this tool will not work.");
                Console.WriteLine("  - If you are not sure, check the display's user guide, to find out.");
                Console.WriteLine("  - The DDC VCP60 value defines which display input source is active.");
                Console.WriteLine();
                Console.WriteLine("Have a look at https://github.com/mbodm/cdis for more information");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Have a nice day.");
            }

            Environment.Exit(0);
        }

        public static void ExitWithError(int exitCode, string errorMessage)
        {
            if (exitCode < 1 || exitCode > 254)
            {
                throw new ArgumentOutOfRangeException(nameof(exitCode));
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException($"'{nameof(errorMessage)}' cannot be null or whitespace.", nameof(errorMessage));
            }

            Console.WriteLine();
            Console.WriteLine($"Error: {errorMessage}");

            Environment.Exit(exitCode);
        }

        public static string GetVCP60ValuesAsNumbersFromCapabilitiesString(string capabilitiesString)
        {
            if (string.IsNullOrWhiteSpace(capabilitiesString) || !capabilitiesString.Contains("60(") || capabilitiesString.Contains("60()"))
            {
                return string.Empty;
            }

            var vcp60HexValuesString = capabilitiesString.
                Split(new string[] { "60(" }, StringSplitOptions.RemoveEmptyEntries).
                Last().Trim().
                Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries).
                First().Trim();
            if (string.IsNullOrEmpty(vcp60HexValuesString))
            {
                return string.Empty;
            }

            var hexValues = vcp60HexValuesString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var decValues = new List<uint>();
            foreach (var hex in hexValues)
            {
                if (uint.TryParse(hex, NumberStyles.HexNumber, null, out uint decValue))
                {
                    decValues.Add(decValue);
                }
            }

            var decString = string.Join(" ", decValues).TrimEnd();

            return decString;
        }

        private static string GetVersionFromProject()
        {
            // For Console Apps this seems to be the most simple way, in .NET 5 or later.
            // It's the counterpart of the "Version" entry, declared in the .csproj file.

            return Assembly.
                GetEntryAssembly()?.
                GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.
                InformationalVersion ?? "0.0.0";
        }

        private static string GetAssemblyNameFromProject()
        {
            // Not the project's name shall be used as exe/assembly file name, when published.
            // It's the counterpart of the "AssemblyName" entry, declared in the .csproj file.

            return Assembly.
                GetEntryAssembly()?.
                GetName()?.
                Name ?? "UNKNOWN";
        }
    }
}
