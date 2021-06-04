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
            //PotentialMethod p = new PotentialMethod("potdata4.csv");
            //p.MainSolution();
            //Stank st = new Stank("data1.csv");
            //st.MainSolution();
            //Simplex S = new Simplex(ReadSaveData.StringListConverter(ReadSaveData.ReadData("sipm.csv")));
            //S.MakeResult();
            //var pf = new PathFinder("file.csv", "reshenie.txt");
            //pf.CalculateCriticalPath();
            Сommivoyageur commivoyageur = new Сommivoyageur("Commivoyazh.csv", "reshenie.txt");
            commivoyageur.Calculate();
        }
    }
}
