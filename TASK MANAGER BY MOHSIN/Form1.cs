using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TASK_MANAGER_BY_MOHSIN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        processClass PC = new processClass();
        private void Form1_Load(object sender, EventArgs e)
        {
            //PC.AddProcesses(listView1,statusBar1);
        }

        private void endProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void endProcessToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count >= 1)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text.ToString());
                PC.KillProcess(id);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PC.AddProcesses(listView1,statusBar1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                timer1.Start();
            }
            else if (e.TabPage == tabPage2)
            {
                timer1.Stop();
                PC.GetHistory(dataGridView1);
            }

        }

        private void txtBox1_Click(object sender, EventArgs e)
        {
            if (txtBox1.Text == "Start New Tast")
            {
                txtBox1.ForeColor = Color.Black;
                txtBox1.Text = "";
            }
        }

        private void btnAddProcess_Click(object sender, EventArgs e)
        {
            PC.StartNewProcess(txtBox1.Text);
            txtBox1.ForeColor = Color.LightGray;
            txtBox1.Text = "Start New Tast";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
             OpenFileDialog ofd = new OpenFileDialog();
             DialogResult dr = ofd.ShowDialog();
             if (dr == DialogResult.OK)
             {
                    txtBox1.ForeColor = Color.Black;
                    txtBox1.Text = ofd.FileName;
             }
             timer1.Start();
        }

        private void txtBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
