using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AssemblyVersionSetter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool replaceFromFront = false;
            bool trace = false;
            string version = "";

            string path = Directory.GetCurrentDirectory();
            string name = new DirectoryInfo(path).Name.ToLower();
            if (name == "bin") path = Directory.GetParent(path).FullName;
            if (name == "debug" || name == "release") path = Directory.GetParent(path).Parent.FullName;

            for (int index = 0; index < args.Length; index++)
            {
                var arg = args[index];
                switch (arg.ToLower())
                {
                    case "-s":
                        replaceFromFront = true;
                        break;
                    case "-t":
                        trace = true;
                        break;
                    case "-e":
                        replaceFromFront = false;
                        break;
                    default:
                        if (string.IsNullOrEmpty(version)) version = arg;
                        else path = arg;
                        break;
                }
            }

            if (string.IsNullOrEmpty(version))
            {
                Console.WriteLine("Error: version must be specified\n");
                DisplayUsage();
                Environment.Exit(1);
            }
            else
            {
                var parts = version.Split('.');
                if (parts.Length < 2)
                {
                    Console.WriteLine("Error: version must be at least 2 numbers\n");
                    DisplayUsage();
                    Environment.Exit(1);
                }
                if (parts.Length > 4)
                {
                    Console.WriteLine("Error: version must be less than 4 numbers\n");
                    DisplayUsage();
                    Environment.Exit(2);
                }
                foreach (var part in parts)
                {
                    int i;
                    if (!int.TryParse(part, out i))
                    {
                        Console.WriteLine("Error: version must be numbers and there must have between 2 - 4 numbers\n");
                        DisplayUsage();
                        Environment.Exit(3);                        
                    }
                }

                FindAndReplace(path, version, replaceFromFront, trace);
            }

            Environment.Exit(1);
        }

        private static void FindAndReplace(string path, string version, bool fromFront, bool trace)
        {
            const string strRegex = @"AssemblyFileVersion\(\""(.*)\""\)";
            const RegexOptions regoptions = RegexOptions.None;
            var regex = new Regex(strRegex, regoptions);

            path = new DirectoryInfo(path).FullName;

            if (trace) Console.WriteLine("looking in {0}", path);
            var files = new List<string>(Directory.GetFiles(path, "*.cs"));
            if (Directory.Exists(path + "\\Properties"))
            {
                if (trace) Console.WriteLine("looking in {0}", path + "\\Properties");
                files.AddRange(Directory.GetFiles(path + "\\Properties", "*.cs"));
            }

            foreach (var fileName in files)
            {
                if (trace) Console.WriteLine("reading {0}", fileName);

                string buffer;
                using (var textFile = File.OpenText(fileName))
                {
                    buffer = textFile.ReadToEnd();
                }

                foreach (Match match in regex.Matches(buffer))
                {
                    if (match.Success)
                    {

                        var current = match.Groups[1].Value;
                        if (trace) Console.WriteLine("found {0}", current);

                        var currentParts = new List<string>(current.Split('.'));
                        var versionParts = new List<string>(version.Split('.'));

                        while(currentParts.Count<4)
                            currentParts.Add("0");

                        for (var i = 0; i < versionParts.Count; i++)
                        {
                            if (fromFront) currentParts[i] = versionParts[i];
                            else currentParts.RemoveAt(currentParts.Count-1);
                        }
                        if (!fromFront) currentParts.AddRange(versionParts);

                        version = string.Join(".", currentParts);
                        version = match.Groups[0].Value.Replace(match.Groups[1].Value, version);

                        buffer = buffer.Replace(match.Groups[0].Value, version);

                        var attrs = File.GetAttributes(fileName);
                        if ((attrs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            File.SetAttributes(fileName, attrs & ~FileAttributes.ReadOnly);
                        }
                        
                        File.WriteAllText(fileName,buffer);
                    }
                }
            }

            Environment.Exit(4);
        }

        private static void DisplayUsage()
        {
            var type = typeof (Program);
            Console.WriteLine(GetStringResource(type.Assembly, type.Namespace + ".USAGE.txt"));
        }

        public static string GetStringResource(Assembly assembly, string resourceName)
        {
            var resourceBuilder = new StringBuilder();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        resourceBuilder.Append(reader.ReadToEnd());
                    }
            }

            return resourceBuilder.ToString();
        }
    }
}
