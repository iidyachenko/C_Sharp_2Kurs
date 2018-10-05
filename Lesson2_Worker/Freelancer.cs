using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2_Worker
{
    class Freelancer : Worker
    {
        /// <summary>
        /// Работник по найму
        /// </summary>
        /// <param name="name">Имя работника</param>
        /// <param name="rate">Ставка за час</param>
        public Freelancer(string name, int rate):base(name, rate)
        {

        }

        public override double MonthSalary()
        {
            return 20.8 * 8 * rate;
        }
    }
}
