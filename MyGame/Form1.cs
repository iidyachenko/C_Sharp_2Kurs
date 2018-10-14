using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
//            FormClosing += Form1_FormClosing;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            Form form = new FormGame();
            form.Width = 1000;
            form.Height = 600;
            Game.NewGameClear();
            Game.Init(form);
            form.ShowDialog();
            //Application.Restart();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
     
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = MessageBox.Show("Хотите закрыть окно?", "Закрытие окна", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No;
           // Application.Restart();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool ExitFlag = MessageBox.Show("Хотите закрыть окно?", "Закрытие окна", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No;
            if (ExitFlag == false)
            { Environment.Exit(0); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader recR = new StreamReader("Record.txt");
            MessageBox.Show("Текущий рекорд в игре: "+ recR.ReadLine());
            recR.Close();
        }
    }
}
