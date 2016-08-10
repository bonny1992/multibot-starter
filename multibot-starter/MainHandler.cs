using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multibot_starter
{
    class MainHandler
    {
        public class ParsedString
        {
            public string Username;
            public string Runtime;
            public string Level;
            public string ExpMin;
        }

        string pathfolder = "C:\\Users\\test\\Desktop\\multibot\\bot_folder"; //unused
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
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Minimized,
                    //UseShellExecute = false,
                    //RedirectStandardOutput = true
                };

                Process process = Process.Start(psi);
                process_list.Add(process);
            }
            return process_list;
        }

        public static void Visualizer_getall()
        {
            List<string> stringbuilder = new List<string>();
            Process[] all_process = Process.GetProcessesByName("NecroBot");
            foreach (Process process in all_process)
            {
                if (process.MainWindowTitle.Split(' ').Length > 1)
                {
                    ParsedString testo = new ParsedString();
                    testo.Username = process.MainWindowTitle.Split(' ')[2];
                    testo.Runtime = process.MainWindowTitle.Split(' ')[5];
                    testo.Level = process.MainWindowTitle.Split(' ')[8].ToString();
                    testo.ExpMin = String.Format("{0} {1}", process.MainWindowTitle.Split(' ')[14], process.MainWindowTitle.Split(' ')[15].Substring(0, process.MainWindowTitle.Split(' ')[15].Length - 1));
                    string text = String.Format("Username: {0} Runtime: {1} Level: {2} Exp needed: {3}\n",
                        testo.Username,
                        testo.Runtime,
                        testo.Level,
                        testo.ExpMin);
                    stringbuilder.Add(text);
                }
                else
                {
                    string text = String.Format("Title: {0}\n", process.MainWindowTitle);
                    stringbuilder.Add(text);
                }
            }
            Console.Clear();
            foreach (string testo in stringbuilder)
            {
                Console.Write(testo);
            }
            Console.WriteLine("Press CTRL+C to close this process and all NecroBots handled by this.");
            System.Threading.Thread.Sleep(1000);
        }

        static void Main(string[] args)
        {
            Console.Title = "Bot manager by Bonny1992";
            List<string> test = GetDirectoryPath();
            List<Process> processlist = ProcessStarter(test);
            Console.CancelKeyPress += delegate {
                foreach (Process process in processlist)
                {
                    Console.WriteLine(String.Format("Killing process {0} with pid {1} | Title: {2}", process.Handle, process.Id, process.MainWindowTitle));
                    try
                    {
                        process.Kill();
                        Console.WriteLine("Process killed.");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(String.Format("Process not killed, reason: {0}", e.Message));
                    }
                }
            };
            Console.Title = String.Format("Bot manager by Bonny1992 - Bot managed: {0}", processlist.Count);
            while (true)
                Visualizer_getall();

            while (true) { }
            
            Console.ReadKey();
        }
    }
}
