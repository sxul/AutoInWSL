using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace aiw
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                // 参数转换
                if (args.Length == 0) return 2;
                var argsb = new StringBuilder();
                foreach (var arg in args)
                {

                    var match = Regex.Match(arg, "([a-zA-Z]):(.*)");
                    if (match.Success)
                    {
                        var letter = match.Result("$1").ToLower();
                        var path = match.Result("$2").Replace("\\", "/");
                        if (!path.StartsWith("/")) path = $"/{path}";
                        argsb.Append(match.Result($"/mnt/{letter}{path}"));
                    }
                    else
                    {
                        var path = arg.Replace("\\", "/");
                        argsb.Append(path);
                    }
                    argsb.Append(" ");
                }

                // 进入WSL执行
                var p = new Process
                {
                    StartInfo =
                    {
                        FileName = "bash.exe",
                        Arguments = argsb.ToString().Trim(),
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    },
                    EnableRaisingEvents = true
                };


                p.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        Console.WriteLine(e.Data);
                    }
                };
                p.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        Console.WriteLine(e.Data);
                    }
                };
                p.Start();

                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                p.WaitForExit();
                p.Close();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }
    }
}
