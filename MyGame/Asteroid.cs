using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс астероидов
    /// </summary>
    class Asteroid : BaseObject
    {
      /// <summary>
      /// Создаем астероиды
      /// </summary>
      /// <param name="pos">Позиция</param>
      /// <param name="dir">Смещение</param>
      /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Рисуем астероиды
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Game.newImage, Pos.X, Pos.Y);
        }

        /// <summary>
        /// Двигаем астероиды
        /// </summary>
        public override void Update()
        {
            {
                Pos.X = Pos.X + Dir.X;
                Pos.Y = Pos.Y + Dir.Y;
                if (Pos.X < 0) Dir.X = -Dir.X;
                if (Pos.X > Game.Width) Dir.X = -Dir.X;
                if (Pos.Y < 0) Dir.Y = -Dir.Y;
                if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
            }
        }
    }
}
