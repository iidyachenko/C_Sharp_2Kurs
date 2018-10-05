using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    //Объекты для заставки
    class SplashBaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public SplashBaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        // Рисуем сзвездочки
        public virtual void Draw()
        {
            SplashScreen.Buffer.Graphics.DrawLine(Pens.Aqua, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            SplashScreen.Buffer.Graphics.DrawLine(Pens.Aqua, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        //Звездочки должны раззлетаться по окружности
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
