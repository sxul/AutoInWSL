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
                // convert arguments
                if (args.Length == 0) return 2;
                var argsb = new StringBuilder();
                foreach (var arg in args)
                {
                    var argLiunx = arg;

                    // do not convert normal argument
                    if (!argLiunx.StartsWith("-") && !argLiunx.StartsWith("/"))
                    {
                        argLiunx = argLiunx.Replace("\\\\", "\\").Replace("\\", "/");
                    }

                    // Only match A-Z dirver letter
                    var match = Regex.Match(argLiunx, "^([a-zA-Z]{1}):(.*)");
                    if (match.Success)
                    {
                        var letter = match.Result("$1").ToLower();
                        var path = match.Result("$2");
                        if (!path.StartsWith("/")) path = $"/{path}";
                        argLiunx = match.Result($"/mnt/{letter}{path}");

                    }

                    // 处理linux下的特殊符号
                    argLiunx = Regex.Replace(argLiunx, "([!$^&()=[\\]{}';, `|\\\\*?\"<>])", "\\$1");

#if DEBUG
                    Console.WriteLine($"{arg} -> {argLiunx}");
#endif
                    argsb.Append($"{argLiunx} ");
                }

                // 进入WSL执行
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "bash.exe",
                        Arguments = argsb.ToString().Trim(),
                        RedirectStandardError = false,
                        RedirectStandardInput = false,
                        RedirectStandardOutput = false,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    },
                    EnableRaisingEvents = true
                };

                process.Start();

                process.WaitForExit();
                process.Close();
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
