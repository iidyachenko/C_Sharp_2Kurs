using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Конструктор спутника
    /// </summary>
    class Sputnic:BaseObject
    {
        public Sputnic(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Рисуем спутник ввиде элепса и линий(антен)
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Firebrick, Pos.X + Size.Width, Pos.Y + Size.Height, Pos.X + 2*Size.Width, Pos.Y + 2*Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Firebrick, Pos.X + Size.Width, Pos.Y, Pos.X + 2*Size.Width, Pos.Y - Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Firebrick, Pos.X + Size.Width, Pos.Y + Size.Width/2, Pos.X + 2 * Size.Width, Pos.Y + Size.Width / 2);
            Game.Buffer.Render();
        }

        /// <summary>
        /// Двигаем спутник по диагонали, если пропадет в вверху - появится внизу
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            if (Pos.Y < 0) Pos.Y = Game.Height + Size.Height;
        }
    }
}
