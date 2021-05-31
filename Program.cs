using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            PotentialMethod p = new PotentialMethod();
            p.DataReaderConverter("potdata.csv");
            Console.ReadKey();
        }
    }
}
