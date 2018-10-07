using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Каласс для создания базовых объектов, в нашем случае метеоритов

namespace MyGame
{
    abstract class BaseObject:ICollision
    {
        public Point Pos;
        public Point Dir;
        public Size Size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
           
            try
            {
                if (dir.X > 10 || dir.Y > 10)
                throw new MyException("Превышение максимальной скорости");
            }
            catch (MyException)
            {
                Dir.X = 10;
                Dir.Y = 10;

            }
        }

        public Rectangle Rect => new Rectangle(Pos, Size);

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        //Рисуем метеорит
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.newImage, Pos.X, Pos.Y);
        }

        // Задаем траекторию полета
        public abstract void Update();
      

    }

}
