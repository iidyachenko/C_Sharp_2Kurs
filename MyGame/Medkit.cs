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
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        /// <summary>
        /// Расчет траектории движения звезды
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
