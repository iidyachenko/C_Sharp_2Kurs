using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2_Worker
{
    class Employee : Worker
    {
        /// <summary>
        /// Работник на зарплате
        /// </summary>
        /// <param name="name">Имя работника</param>
        /// <param name="rate">Ставка за месяц</param>
        public Employee(string name, int rate) :base(name,rate)
        {

        }

        public override double MonthSalary()
        {
            return rate;
        }
    }
}
