using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.FxCop.Sdk;

namespace SPDisposeCheckRules.Rules
{
    internal class SPDisposeCheck : SPDisposeCheckBaseRule
    {
        private const string SECTION_TERMINATOR = "----------------------------------------------------------";

        public SPDisposeCheck()
            : base("SPDisposeCheck")
        {
        }

        public override ProblemCollection Check(ModuleNode moduleNode)
        {
            AssemblyNode containingAssembly = moduleNode.ContainingAssembly;
            var startInfo = new ProcessStartInfo("\"" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SPDisposeCheck.exe\"", "\"" + containingAssembly.Location + "\"");
            startInfo.UseShellExecute = false;
            startInfo.ErrorDialog = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            try
            {
                StreamReader standardOutput = Process.Start(startInfo).StandardOutput;
                string s1 = standardOutput.ReadToEnd();
                standardOutput.Close();
                if (!s1.Contains("Total Found: 0"))
                {
                    using (StringReader stringReader = new StringReader(s1))
                    {
                        string str1 = string.Empty;
                        string str2 = string.Empty;
                        string str3 = string.Empty;
                        string str4 = string.Empty;
                        string str5 = string.Empty;
                        string s2 = string.Empty;
                        string str6 = string.Empty;
                        string str7 = string.Empty;
                        string str8;
                        while ((str8 = stringReader.ReadLine()) != null)
                        {
                            if (str8.Contains("ID: SPDisposeCheckID"))
                            {
                                str1 = str8.Replace("ID: ", "");
                                str2 = string.Empty;
                                str3 = string.Empty;
                                str4 = string.Empty;
                                str5 = string.Empty;
                                s2 = string.Empty;
                                str6 = string.Empty;
                                str7 = string.Empty;
                            }
                            if (str8.Contains("Module: "))
                                str2 = str8.Replace("Module: ", "");
                            if (str8.Contains("Method: "))
                                str3 = str8.Replace("Method: ", "");
                            if (str8.Contains("Statement: "))
                                str4 = str8.Replace("Statement: ", "");
                            if (str8.Contains("Source: "))
                                str5 = str8.Replace("Source: ", "");
                            if (str8.Contains("Line: "))
                                s2 = str8.Replace("Line: ", "");
                            if (str8.Contains("Notes: "))
                                str6 = str8.Replace("Notes: ", "");
                            if (str8 == SECTION_TERMINATOR && str1.Contains("SPDisposeCheckID"))
                            {
                                if (s2 != string.Empty)
                                    Problems.Add(new Problem(GetResolution(new object[4]
                  {
                    str1,
                    str6,
                    str3,
                    str4
                  }), str2, int.Parse(s2)));
                                else
                                    this.Problems.Add(new Problem(GetResolution(new object[4]
                  {
                    str1,
                    str6,
                    str3,
                    str4
                  }), containingAssembly.Location, 0));
                                str1 = string.Empty;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return this.Problems;
        }
    }
}
