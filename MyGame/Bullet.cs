using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Bullet: BaseObject
    {
        /// <summary>
        /// Конструктор пуль
        /// </summary>
        /// <param name="pos">Позиция пули</param>
        /// <param name="dir">Скорость</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Рисуем пулю
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            
        }

        /// <summary>
        /// Пуля движется слева направо
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width) Pos.X = 0;
        }

    }
}
