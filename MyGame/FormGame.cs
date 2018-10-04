using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            e.Cancel = MessageBox.Show("Хотите закрыть окно?", "Закрытие окна", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No;
            if (e.Cancel == false)
            { Environment.Exit(0); }
        }
    }
}
