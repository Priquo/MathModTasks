using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class PotentialMethod
    {
        int[] whogive, whoget, minData;
        int[,] mainData;        
        int n, m;
        MinDistrib md;
        public PotentialMethod(string path)
        {
            List<string[]> newdata = DataWorkerCSV.ReadCSV(path);
            m = newdata.Count - 1;
            n = newdata.First().Length - 1;
            whogive = new int[m];
            whoget = new int[n];
            mainData = new int[n, m];
            for (int i = 0; i < newdata.Count; i++)
            {
                for (int j = 0; j < newdata.First().Length; j++)
                {
                    if (i != 0)
                    {
                        whogive[i - 1] = Convert.ToInt32(newdata[i][0]);
                        if (j != 0)
                        {
                            whoget[j - 1] = Convert.ToInt32(newdata[0][j]);
                            mainData[i - 1, j - 1] = Convert.ToInt32(newdata[i][j]);
                        }
                    }
                }
            }
        }
        public void DataReaderConverter(string path)
        {
            
            //MinDistrib md = new MinDistrib(mainData, whogive, whoget, m, n);
            //md.MinDistribute();
            //for (int i = 0; i < n; i++)
            //{
            //    for (int j = 0; j < m; j++)
            //        Console.Write(md.distrMatric[i, j] + "\t");
            //    Console.WriteLine();
            //}
            //Console.WriteLine(md.CountNotNullElement);
        }
        public void MainSolution()
        {
            MinDistrib md = new MinDistrib(mainData, whogive, whoget, m, n);
            md.MinDistribute();
            if (md.CountNotNullElement != m + n - 1)
            {
                FindMin();
                md.distrMatric[minData[1], minData[2]] = -1;
            }

        }
        void FindMin()
        {
            int min = mainData[0, 0];
            minData = new int[3];
            for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            if (min > mainData[i, j] && mainData[i, j] != 0 && min != 0)
            {
                min = mainData[i, j];
                minData[0] = min;
                minData[1] = i;
                minData[2] = j;
            }                
        }
    }
}
