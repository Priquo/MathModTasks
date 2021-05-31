using System;
using System.Collections.Generic;
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
        static public void WriteToCSV(string path)
        {

        }
    }
}
