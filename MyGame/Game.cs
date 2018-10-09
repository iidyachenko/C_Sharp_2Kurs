using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static Image newImage = Image.FromFile(@"Images\Meteor.png");
        public static Image N5 = Image.FromFile(@"Images\N5.PNG");
        public static BaseObject[] _objs;
        public static Star[] _star;
        public static Sputnic s;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        private static Ship _ship;
        public static Timer _timer = new Timer { Interval = 100 };
        public static int score = 0;


        static Game()
        {
        }

        /// <summary>
        /// Инициализация начального состояния формы для игры
        /// </summary>
        /// <param name="form">Форма для отображения</param>
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;

            // Предоставляем доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы

            if (form.Width > 1000 || form.Width < 0 || form.Height > 1000 || form.Height < 0)
                throw new ArgumentOutOfRangeException("Некорректный размер окна игры");
            Width = form.Width;
            Height = form.Height;

            //Вызов обработчика события нажатия клавиши
            form.KeyDown += Form_KeyDown;

            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            //Загружаем параметры отображения
            Load();

            //Начальное состояние - пустой черный экран
            Clear();

            _timer.Start();
            _timer.Tick += Timer_Tick;

            Ship.MessageDie += Finish;
        }

        /// <summary>
        /// Очистка экрана
        /// </summary>
        public static void Clear()
        {
            Buffer.Graphics.Clear(Color.FromArgb(0, 0, 64));
            Buffer.Graphics.DrawString("Score:" + score, SystemFonts.StatusFont, Brushes.White, 200, 0);
            Buffer.Render();
        }

        /// <summary>
        /// Загрузка начальных параметров объектов
        /// </summary>
        public static void Load()
        {
            //По Х случайное появление точки
            Random rnd = new Random();
            int starSize;
            _objs = new BaseObject[5];
            _star = new Star[30];

            _bullet = new Bullet(new Point(0, 300), new Point(25, 0), new Size(4, 1));

            _asteroids = new Asteroid[8];
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 30);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(40, 40));
            }

            //Создаем звезды. Хотим что бы звезды имели случайный размер и место появления
            for (int i = 0; i < _star.Length; i++)
            {
                starSize = rnd.Next(4, 10);
                _star[i] = new Star(new Point(rnd.Next(0, 600), i * 20), new Point(i, 0), new Size(starSize, starSize));
            }

            //Создаем спутник
            s = new Sputnic(new Point(100, 100), new Point(10, 10), new Size(20, 20));
                 //Создаем Корабль

            _ship = new Ship(new Point(10, 300), new Point(5, 5), new Size(10, 10));
    }

        /// <summary>
        /// Метод для рисования массива объектов
        /// </summary>
        /// <param name="objs">Оюъекты для рисования</param>
        public static void Draw(BaseObject[] objs)
        {
            // Проверяем вывод графики
            foreach (BaseObject obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// двигаем объекты
        /// </summary>
        /// <param name="objs">Массив объектов</param>
        public static void Update(BaseObject[] objs)
        {
            foreach (BaseObject obj in _asteroids)
            {
                obj.Update();
                if (obj.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    obj.Pos.X = Game.Width;
                    _bullet.Pos.X = 0;
                    score++;
                }

                if (obj.Collision(_ship))
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    var rnd = new Random();
                    _ship?.EnergyLow(rnd.Next(10, 50));
                    obj.Pos.X = Game.Width;
                    _bullet.Pos.X = 0;
                    if (_ship.Energy <= 0) _ship?.Die();
                }

            }

            foreach (BaseObject obj in _star)
            {
                obj.Update();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {

                Clear();
                _ship.Draw();
                Draw(_asteroids);
                Draw(_star);
                _bullet.Draw();
                Update(_asteroids);
                Update(_star);
                _bullet.Update();
                s.Draw();
                s.Update();
        }


    private static void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Если нужно чтобы форма не закрывалась:
            e.Cancel = false;
        }

        /// <summary>
        /// Обработка нажатия клавиш для управления кораблем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(25, 0), new Size(4, 1));
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        public static void NewGameClear()
        {
            if (_bullet != null)
            {
                _bullet.Pos.X = 0;
                _bullet.Pos.Y = 0;
                _bullet.Dir.X = 0;
                _bullet.Dir.Y = 0;
            }
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}
