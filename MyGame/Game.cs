using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

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
        public static Image Battery = Image.FromFile(@"Images\battery.PNG");
        public static BaseObject[] _objs;
        public static Star[] _star;
        public static Sputnic s;
        private static Bullet _bullet;
        private static Ship _ship;
        public static Timer _timer = new Timer { Interval = 100 };
        public static int score = 0;
        public static int CurRec;
        public static int AsterCount = 2;
        public static int mspeed = 15;
        public static Medkit _medkit;
        public static event Action<string> _log;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static List<Asteroid> _lasteroids = new List<Asteroid>();
        public static Random rnd = new Random();
        public static StreamWriter sw = new StreamWriter("Log.txt");
        public static StreamReader recR;
        public static StreamWriter recW;


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
 //           _log += stat;

            sw.WriteLine("Начало записи лога");

            recR = new StreamReader("Record.txt");
            CurRec = Convert.ToInt32(recR.ReadLine());
            recR.Close();


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

            _bullet = new Bullet(new Point(0, 300), new Point(50, 0), new Size(10, 2));
            _bullets.Add(_bullet);

            //Создаем звезды. Хотим что бы звезды имели случайный размер и место появления
            for (int i = 0; i < _star.Length; i++)
            {
                starSize = rnd.Next(4, 10);
                _star[i] = new Star(new Point(rnd.Next(0, 600), i * 20), new Point(i, 0), new Size(starSize, starSize));
            }

            //Создаем астероиды
            CreateAsteroid(AsterCount,mspeed);

            //Создаем спутник
            s = new Sputnic(new Point(100, 100), new Point(10, 10), new Size(20, 20));
                 //Создаем Корабль

            _ship = new Ship(new Point(10, 300), new Point(5, 5), new Size(30, 10));

            _medkit = new Medkit(new Point(Game.Width, rnd.Next(0, 600)), new Point(10, 0), new Size(20, 20));

            
    }
        /// <summary>
        /// Создаем астероиды
        /// </summary>
        /// <param name="n">Количество астероидов</param>
        public static void CreateAsteroid(int n, int mspeed)
        {
            for (int i = 0; i < n; i++)
            {
                int r = rnd.Next(mspeed - 10, mspeed);
                _lasteroids.Add(new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(40, 40)));
            }
        }
        
        /// <summary>
        /// Метод для рисования массива объектов
        /// </summary>
        /// <param name="objs">Оюъекты для рисования</param>
        public static void Draw(BaseObject[] objs)
        {
            // Проверяем вывод графики
            foreach (BaseObject obj in objs)
                obj?.Draw();
            Buffer.Render();
        }
        
        public static void Draw(List<Bullet> objs)
        {
            // Проверяем вывод графики
            foreach (Bullet obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Draw(List<Asteroid> objs)
        {
            // Проверяем вывод графики
            foreach (Asteroid obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// двигаем объекты
        /// </summary>
        /// <param name="objs">Массив объектов</param>
        public static void Update(BaseObject[] objs)
        {
            foreach (BaseObject obj in _star)
            {
                obj.Update();
            }
        }

        /// <summary>
        /// Обновляем объекты на экране
        /// </summary>
        public static void Update()
        {
            foreach (Bullet b in _bullets) b.Update();

            for (var i = 0; i < _lasteroids.Count; i++)
            {
                if (_lasteroids[i] == null) continue;

                _lasteroids[i].Update();

                if (_lasteroids[i].Collision(_ship))
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    var rnd = new Random();
                    _ship?.EnergyLow(rnd.Next(10, 50));
                    _lasteroids[i].Pos.X = Width;
                    _lasteroids[i].Pos.Y = Height;
                    _log += ShipHit;
                    if (_ship.Energy <= 0) _ship?.Die();
                }

                for (int j = 0; j < _bullets.Count; j++)
                    if (_lasteroids[i] != null && _bullets[j].Collision(_lasteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        _bullets.RemoveAt(j);
                        _lasteroids.RemoveAt(i);
                        score++;
                        if(score > CurRec)
                        {
                            CurRec = score;
                        }
                        _log += Hit;
                        j--;
                        if (_lasteroids.Count == 0)
                        {
                            AsterCount++;
                            mspeed++;
                            CreateAsteroid(AsterCount,mspeed);
                        }
                        break;
                    }

                if (_medkit.Collision(_ship))
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    _ship?.EnergyHigh(25);
                    _medkit.Pos.X = Game.Width;
                    _medkit.Pos.Y = rnd.Next(0, Game.Height);
                }
            }

        }

        public static void Timer_Tick(object sender, EventArgs e)
        {

                Clear();
                _medkit.Draw();              
                _ship.Draw();
                Draw(_star);
                Draw(_bullets);
                Draw(_lasteroids);
                //Update(_lasteroids);
                Update(_star);
                Update();
                s.Draw();
                s.Update();
                _medkit.Update();
            if (_log != null)
            {
                _log("Статус");
                _log -= Hit;
                _log -= ShipHit;
            }
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
                _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(50, 0), new Size(10, 2)));
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.P) _timer.Stop();
            if (e.KeyCode == Keys.Enter) _timer.Start();
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

        public static void stat(string message)
        {
            Console.WriteLine(DateTime.Now + " " + message + " энергия корабля: " + _ship.Energy);
            sw.WriteLine(DateTime.Now + " " + message + " энергия корабля: " + _ship.Energy);
        }

        public static void Hit(string message)
        {
            Console.WriteLine(DateTime.Now + " " + message + " Взорвали астероид: счет " + score);
            sw.WriteLine(DateTime.Now + " " + message + " Взорвали астероид: счет " + score);
        }

        public static void ShipHit(string message)
        {
            Console.WriteLine(DateTime.Now + " В корабль попали энергия корабля: " + _ship.Energy);
            sw.WriteLine(DateTime.Now + " В корабль попали энергия корабля: " + _ship.Energy);
        }
    }
}
