using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2_Worker
{
    abstract class Worker : IComparable
    {
       public string name; 
       protected double rate;

        /// <summary>
        /// Абстрактный класс всех работников
        /// </summary>
        /// <param name="name"> Имя работника</param>
        /// <param name="rate"> Ставка работника</param>
        protected Worker(string name, double rate)
        {
            this.name = name;
            this.rate = rate;
        }

        public int CompareTo(object obj)
        {
            if (((Worker)obj).MonthSalary() > MonthSalary())
                return 1;
            if (((Worker)obj).MonthSalary() < MonthSalary())
                return -1;
            return 0;
        }

        public abstract double MonthSalary();
    }
}
