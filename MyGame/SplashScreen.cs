using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static SplashBaseObject[] _objs;

        static SplashScreen()
        {
        }

        /// <summary>
        /// Инициализация начального состояния формы для игры
        /// </summary>
        /// <param name="form">Форма для экрана меню</param>
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

        /// <summary>
        /// Загрузка стартовых параметров заставки
        /// </summary>
        public static void Load()
        {
            //По Х случайное появление точки
            Random r = new Random();

            _objs = new SplashBaseObject[360];
            double a;
            //Создаем звезды, летящие звезды должны создавать окружность
            for (int i = 0; i < _objs.Length; i++)
            {
                a = i ;
                _objs[i] = new SplashBaseObject(new Point(200, 200), new Point( - 10*Convert.ToInt32(Math.PI*Math.Sin(a)), - 10*Convert.ToInt32(Math.PI * Math.Cos(a))), new
                Size(10, 10));
            }
        }

        /// <summary>
        /// Таймер заставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Clear();
            Draw(_objs);
            Update(_objs);
        }

        /// <summary>
        /// Очищаем заставку
        /// </summary>
        public static void Clear()
        {
            Buffer.Graphics.Clear(Color.FromArgb(0, 0, 64));
            Buffer.Render();
        }

        /// <summary>
        ///Метод для рисования массива объектов
        /// </summary>
        /// <param name="objs">массив объектов</param>
        public static void Draw(SplashBaseObject[] objs)
        {
            // Проверяем вывод графики
            foreach (SplashBaseObject obj in objs)
                obj.Draw();
            Buffer.Render();

        }


        /// <summary>
        /// двигаем объекты заставки
        /// </summary>
        /// <param name="objs">массив объектов</param>
        public static void Update(SplashBaseObject[] objs)
        {
            foreach (SplashBaseObject obj in objs)
                obj.Update();

        }
    }
}
