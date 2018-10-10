using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Medkit : BaseObject
    {
        /// <summary>
        /// Создания звезд
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Скорость</param>
        /// <param name="size">размер</param>
        public Medkit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Отображения звезды
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.Battery, Pos.X, Pos.Y);


        }

        /// <summary>
        /// Расчет траектории движения звезды
        /// </summary>
        public override void Update()
        {
            Random r = new Random();
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0)
            {
                Pos.X = Game.Width;
                Pos.Y = r.Next(2, 600);
            }
        }
    }
}
