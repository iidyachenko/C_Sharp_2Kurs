using System;
using System.Collections.Generic;

namespace Lesson4_Collection_and_LINQ
{
    class Program

    {
        public static List<int> ilist;
        public static Dictionary<int, int> result;
        public static Dictionary<int, int> tresult;

        /// <summary>
        /// Генератор значений для коллекции
        /// </summary>
        public static void Generator()
        {
            ilist = new List<int>();
            result = new Dictionary<int, int>();
            Random r = new Random();
            for(int i =0; i<20; i++)
            {
                ilist.Add(r.Next(1, 10));
                if(i<10)result.Add(i, 0);
            }
        }

        private static IDictionary<T,int> GetUniques<T>(ICollection<T> list)
        {
            // Для отслеживания элементов используйте словарь 
            Dictionary<T, int> found = new Dictionary<T, int>();
            //List<T> tlist = new List<T>();
            foreach (T val in list)
            {
                foreach (T val2 in list)
                    if (val.Equals(val2))
                        {
                        if (found.ContainsKey(val)==false)
                        {
                            found.Add(val, 0);
                            found[val]++;

                        }
                        else found[val]++;  
                        }
            }
            return found;
        }

        /// <summary>
        /// Отображаем итоговый словарь
        /// </summary>
        /// <param name="dict">Словарь для отображения</param>
        public static void Show(Dictionary<int,int> dict)
        {
            foreach (KeyValuePair<int,int> pair in dict)
            {
                Console.WriteLine(pair.Key + " " + pair.Value);
            }
        }

        public static void Show(List<int> ilist)
        {
            foreach (int l in ilist)
            {
                Console.Write(l + " ");
            }
        }

        static void Main(string[] args)
        {
            Generator();
            Show(ilist);
            

            //Подсчет для целочисленной коллекции
            foreach(int obj in ilist)
            {
                result[obj]++;
            }

            Console.WriteLine();
            Console.WriteLine("Количество повторений для целочисленной коллекции");
            Show(result);

            Console.WriteLine("Количество повторений для обобщенной коллекции");
            foreach (var pair in GetUniques(ilist))
            {
                Console.WriteLine(pair.Key + " " + pair.Value);
            };


            Console.ReadKey();
        }

    }
}
