using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class SplashBaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Объекты для заставки
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public SplashBaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Рисуем сзвездочки
        /// </summary>
        public virtual void Draw()
        {
            SplashScreen.Buffer.Graphics.DrawLine(Pens.Aqua, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            SplashScreen.Buffer.Graphics.DrawLine(Pens.Aqua, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        /// <summary>
        /// Звездочки должны раззлетаться по окружности
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.X < 0 || Pos.X>400)
            {
                Pos.X = 200;
                Pos.Y = 200;
            }
            if (Pos.Y < 0 || Pos.Y > 400)
            {
                Pos.Y = 200;
                Pos.X = 200;
            }
        }
    }
}
