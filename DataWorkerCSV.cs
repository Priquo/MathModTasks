using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    static class DataWorkerCSV
    {        
        static public List<string[]> ReadCSV(string path)
        {
            List<string[]> data = new List<string[]>();
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8, true))
                {
                    while (sr.EndOfStream != true)
                    {
                        string[] str = sr.ReadLine().Split(';');
                        data.Add(str);
                    }
                }
                return data;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ошибка считывания данных!");
                return data;
            }
        }
        static public void WriteToCSV(string path, string[] messege)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var s in messege)
                {
                    sw.WriteLine(s + ";");
                }
            }
        }
        static public double[,] StringListConverter(List<string[]> list)
        {
            double[,] d = new double[list.Count, list.First().Length];
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list.First().Length; j++)
                    d[i, j] = Convert.ToDouble(list[i][j]);
            return d;
        }
    }
}
