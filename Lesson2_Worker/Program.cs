using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2_Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[5];

            workers[0]  = new Freelancer("Mike", 100);
            workers[1]  = new Freelancer("Bill", 150);
            workers[2] = new Employee("Igor", 50000);
            workers[3] = new Freelancer("Tom", 50);
            workers[4] = new Employee("Ivan", 10000);


            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine($"За месяц {workers[i].name} получил: {workers[i].MonthSalary()}$");
            }

            Console.WriteLine("Сортируем массив");
            Array.Sort(workers);

            foreach (var w in workers)
            {
                Console.WriteLine($"За месяц {w.name} получил: {w.MonthSalary()}$");
            }

            Console.ReadKey();
        }
    }
}
