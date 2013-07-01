using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AssemblyVersionFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool shortVersion = false;
            bool tailVersion = false;
            bool scanDirectory = true;

            string path = Directory.GetCurrentDirectory();
            string name = new DirectoryInfo(path).Name.ToLower();
            if (name == "bin") path = Directory.GetParent(path).FullName;
            if (name == "debug" || name=="release") path = Directory.GetParent(path).Parent.FullName;

            foreach (var arg in args)
            {
                switch (arg.ToLower())
                {
                    case "-s":
                        shortVersion = true;
                        tailVersion = false;
                        break;
                    case "-b":
                        shortVersion = false;
                        tailVersion = true;
                        break;
                    case "-a":
                        scanDirectory = false;
                        break;
                    default:
                        path = arg;
                        break;
                }
            }

            string version = scanDirectory ? ScanDirectory(path) : ReadAssembly(path);

            if (!string.IsNullOrEmpty(version))
            {
                if (shortVersion)
                {
                    var parts = new List<string>(version.Split('.'));
                    while (parts.Count > 2) parts.RemoveAt(parts.Count - 1);
                    version = string.Join(".", parts.ToArray());
                }
                if (tailVersion)
                {
                    var parts = new List<string>(version.Split('.'));
                    while (parts.Count > 2) parts.RemoveAt(0);
                    version = string.Join(".", parts.ToArray());                    
                }

                Console.WriteLine(version);
                Environment.Exit(0);
                return;
            }

            Environment.Exit(1);
        }

        private static string ReadAssembly(string path)
        {
            if (File.Exists(path))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(path);
                var productVersion = versionInfo.ProductVersion;
                return productVersion;
            }
            Environment.Exit(3);
            return null;
        }

        private static string ScanDirectory(string path)
        {
            const string strRegex = @"AssemblyFileVersion\(\""(.*)\""\)";
            const RegexOptions regoptions = RegexOptions.None;
            var regex = new Regex(strRegex, regoptions);

            var files = new List<string>(Directory.GetFiles(path,"*.cs"));
            if (Directory.Exists(path + "\\Properties"))
            {
                files.AddRange(Directory.GetFiles(path + "\\Properties", "*.cs"));
            }

            foreach (var fileName in files)
            {
                string buffer = File.OpenText(fileName).ReadToEnd();

                foreach (Match match in regex.Matches(buffer))
                {
                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }
                }
            }

            Environment.Exit(2);
            return null;
        }
    }
}
