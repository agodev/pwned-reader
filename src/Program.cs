using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using CommandLine;

namespace pwned_reader
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts));
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            var watch = Stopwatch.StartNew();
            var spinner = new ConsoleSpinner();
            spinner.Start();
            
            string hashedPass = HashPassword(opts.Password);

            using (StreamReader str = File.OpenText(opts.InputFile))
            {
                var line = String.Empty;
                var notFound = true;

                while ((line = str.ReadLine()) != null && notFound)
                {
                    notFound = !line.Contains(hashedPass);
                }

                spinner.Clear();

                if (notFound)
                {
                    Console.WriteLine("Lucky you!! Password has not been pwned");
                }
                else
                {
                    Console.WriteLine($"You have been Pwnd, {line.Split(":")[1]} times!!!");
                }
            }

            Console.WriteLine($"Elasped time: {watch.Elapsed.ToString()}");
        }

        private static string HashPassword(string password)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            return HexStringFromBytes(hashBytes);
        }

        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString().ToUpper();
        }
    }
}
