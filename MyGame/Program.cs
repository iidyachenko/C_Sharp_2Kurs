using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запуск стартовой формы
            Form1 form1 = new Form1();
            SplashScreen.Init(form1);
            form1.ShowDialog();
        }
    }
}
