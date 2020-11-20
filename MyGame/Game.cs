using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static Image newImage = Image.FromFile(@"Images\Meteor.png");
        public static BaseObject[] _objs;
        public static Star[] _star;
        public static Sputnic s;

        static Game()
        {
        }

        //Инициализация начального состояния формы для игры
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;

            // Предоставляем доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
           
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            //Загружаем параметры отображения
            Load();

            //Начальное состояние - пустой черный экран
            Clear();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

        }

        public static void Clear()
        {
            Buffer.Graphics.Clear(Color.FromArgb(0, 0, 64));
            Buffer.Render();
        }

        public static void Load()
        {
            //По Х случайное появление точки
            Random r = new Random();
            int starSize;
            _objs = new BaseObject[5];
            _star = new Star[30];

            //Создаем метеоры
            for (int i = 0; i < _objs.Length; i++)
                _objs[i] = new BaseObject(new Point(r.Next(0, 600), i * 20), new Point(-i-15, -i-15), new
                Size(10, 10));

            //Создаем звезды. Хотим что бы звезды имели случайный размер и место появления
            for (int i = 0; i < _star.Length; i++)
            {
                starSize = r.Next(4, 10);
                _star[i] = new Star(new Point(r.Next(0, 600), i * 20), new Point(i, 0), new Size(starSize, starSize));
            }

            //Создаем спутник
            s = new Sputnic(new Point(100, 100), new Point(10, 10), new Size(20, 20));
        }

        //Метод для рисования массива объектов
        public static void Draw(BaseObject[] objs)
        {
            // Проверяем вывод графики
            foreach (BaseObject obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        // двигаем объекты
        public static void Update(BaseObject[] objs)
        {
            foreach (BaseObject obj in objs)
                obj.Update();           
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Clear();
            Draw(_objs);
            Draw(_star);
            Update(_objs);
            Update(_star);
            s.Draw();
            s.Update();
        }

        private static void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Если нужно чтобы форма не закрывалась:
            e.Cancel = false;
        }
    }
}
