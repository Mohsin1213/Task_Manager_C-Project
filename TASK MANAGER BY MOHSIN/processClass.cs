using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace TASK_MANAGER_BY_MOHSIN
{
    class processClass
    {
        private double Tpro = 0;
        private double Tthread = 0;
        private double TMem = 0;
        private int Count = 0;
        private Process[] processes;
        public void AddProcesses(ListView lv,StatusBar sb)
        {
            processes = Process.GetProcesses();
            if (processes.Length != Tpro)
            {
                Tpro = 0;
                Tthread = 0;
                TMem = 0;
                Count = 0;
                lv.Items.Clear();
                foreach (Process p in Process.GetProcesses())
                {
                    try
                    {
                        double mem = (p.WorkingSet64 / 1024);
                        string[] ProcessesInfo = new string[] { p.ProcessName, p.Id.ToString(), p.StartTime.ToShortTimeString(), mem.ToString() + " KB", p.Threads.Count.ToString() };
                        ListViewItem lvi = new ListViewItem(ProcessesInfo);
                        lv.Items.Add(lvi);
                        Tthread += p.Threads.Count;
                        TMem += mem;
                        Count++;
                    }
                    catch { }
                }
                Tpro = processes.Length;
            }
            sb.Panels[0].Text = "Total Processes: " + Count.ToString();
            sb.Panels[1].Text = "Total Memory Used: " + TMem.ToString() + " KB";
            sb.Panels[2].Text = "Total Threads: " + Tthread.ToString();
            
        }
        public void StartNewProcess(string name)
        {
            try {
                Process p = new Process();
                p.StartInfo.FileName = name;
                p.Start();
                StreamWriter sw = new StreamWriter(@"Logs.txt", true);
               // p.StartInfo.FileName = "notepad.exe";
                sw.WriteLine(p.ProcessName);
                sw.WriteLine(DateTime.Now.ToShortDateString());
                sw.WriteLine(DateTime.Now.ToShortTimeString());
                sw.WriteLine("Started");
                sw.Close();
            
                }
            catch
            {
                MessageBox.Show("Something Goes Wrong");
            }
        }
        public void KillProcess(int id)
        {
            try
            {
                Process p = Process.GetProcessById(id);
                StreamWriter sw = new StreamWriter(@"Logs.txt",true);
                sw.WriteLine(p.ProcessName.ToString());
                sw.WriteLine(DateTime.Now.ToShortDateString());
                sw.WriteLine(DateTime.Now.ToShortTimeString());
                sw.WriteLine("Killed");
                sw.Close();
                p.Kill();
            }
            catch { }
        }

        public void GetHistory(DataGridView dgv)
        {
            
            if (File.Exists(@"Logs.txt"))
            {
                dgv.Rows.Clear();
                string[] History = File.ReadAllLines("Logs.txt");
                for (int i = 0; i < History.Length; i += 4)
                {
                    dgv.Rows.Add(History[i], History[i + 1], History[i + 2], History[i + 3]);
                }
            }
            
        }

    }
}
