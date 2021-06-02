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
            double[] result = new double[2];
            double[,] table_result;
            Simplex S = new Simplex(DataWorkerCSV.StringListConverter(DataWorkerCSV.ReadCSV("sipm.csv")));
            table_result = S.Calculate(result);
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    Console.Write(table_result[i, j] + "\t");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Решение:");
            Console.WriteLine("X[1] = " + result[0]);
            Console.WriteLine("X[2] = " + result[1]);
            Console.ReadKey();
        }
    }
}
