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
            //PotentialMethod p = new PotentialMethod("potdata.csv");
            //p.MainSolution();
            //Stank st = new Stank("data1.csv");
            //st.MainSolution();
            Simplex S = new Simplex(DataWorkerCSV.StringListConverter(DataWorkerCSV.ReadCSV("sipm.csv")));
            S.MakeResult();
            Console.ReadKey();
        }
    }
}
