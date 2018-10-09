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
    
    /// <summary>
    /// Абстрактный класс, основа всех графических объектов
    /// </summary>
    abstract class BaseObject:ICollision
    {
        public delegate void Message();

        public Point Pos;
        public Point Dir;
        public Size Size;
       
        /// <summary>
        /// Конструктор объектов
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;

            //Проверка на максимально допустимую скорость объекта
            try
            {
                if (dir.X > 25 || dir.Y > 25)
                throw new MyException("Превышение максимальной скорости");
            }
            catch (MyException)
            {
                Dir.X = 15;
                Dir.Y = 15;

            }
        }

        public Rectangle Rect => new Rectangle(Pos, Size);

        /// <summary>
        /// Расчет столкновения
        /// </summary>
        /// <param name="o">объект столкновения</param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        /// <summary>
        /// Рисуем объект
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.newImage, Pos.X, Pos.Y);
        }

        /// <summary>
        /// Задаем траекторию полета
        /// </summary>
        public abstract void Update();
      

    }

}
