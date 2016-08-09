using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multibot_starter
{
    class Program
    {
        string pathfolder = "C:\\Users\\lh_fi\\Desktop\\multibot\\bot_folder";
        public static List<string> GetDirectoryPath()
        {
            string path_main = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            return Directory.GetDirectories(path_main).ToList();
        }

        public static List<Process> ProcessStarter(List<string> folders)
        {
            List<Process> process_list = new List<Process>();
            foreach (string folder in folders)
            {
                Directory.SetCurrentDirectory(folder);
                string fileName = folder + "\\NecroBot.exe";
                Console.WriteLine(fileName);
                ProcessStartInfo psi = new ProcessStartInfo(fileName)
                {
                    //CreateNoWindow = true,
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    //UseShellExecute = false,
                    //RedirectStandardOutput = true
                };

                Process process = Process.Start(psi);
                process_list.Add(process);
            }
            return process_list;
        }

        public static void Visualizer(List<Process> processes)
        {
            while (true)
            {
                Console.Clear();
                List<string> stringbuilder = new List<string>();
                foreach (Process process in processes)
                {
                    /*string lastLine = null;
                    while (!process.StandardOutput.EndOfStream)
                    {
                        lastLine = process.StandardOutput.ReadLine();
                    }*/
                    string text = String.Format("Title: {0}\n", process.MainWindowTitle);
                    stringbuilder.Add(text);
                }
                foreach (string testo in stringbuilder)
                {
                    Console.Write(testo);
                }
                System.Threading.Thread.Sleep(5000);
            }
        }

        static void Main(string[] args)
        {
            List<string> test = GetDirectoryPath();
            List<Process> processlist = ProcessStarter(test);
            Console.ReadKey();
            Console.CancelKeyPress += delegate {
                foreach (Process process in processlist)
                {
                    Console.WriteLine(String.Format("Killing process {0} with pid {1} | Title: {2}", process.Handle, process.Id, process.MainWindowTitle));
                    process.Kill();
                    Console.WriteLine("Process killed.");
                }
            };
            Visualizer(processlist);

            while (true) { }
            
            Console.ReadKey();
        }
    }
}
