﻿using System;
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
    public partial class FormGame : Form
    {

        public FormGame()
        {
            FormClosing += FormGame_FormClosing;
            
        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
           /// e.Cancel = MessageBox.Show("Хотите закрыть окно?", "Закрытие окна", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No;
            if (e.Cancel == false)
            {

                Game.Buffer.Dispose();
                Game._timer.Stop();
                Game._timer.Tick -= Game.Timer_Tick;
                Game.sw.Close();
                Game.recW = new StreamWriter("Record.txt");
                Game.recW.WriteLine(Game.CurRec);
                Game.recW.Close();
                
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormGame
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "FormGame";
            this.ResumeLayout(false);

        }
    }
}
