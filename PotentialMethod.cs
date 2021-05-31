﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class PotentialMethod
    {
        int[] whogive;
        int[] whoget;
        int[,] mainData;
        public void DataReaderConverter(string path)
        {
            List<string[]> newdata = DataWorkerCSV.ReadCSV(path);
            int m = newdata.Count - 1;
            int n = newdata.First().Length - 1;
            whogive = new int[m];
            whoget = new int[n];
            mainData = new int[n, m];
            for (int i = 0; i < newdata.Count; i++)
            {
                for (int j = 0; j < newdata.First().Length; j++)
                {
                    if (i != 0) 
                    {
                        whogive[i-1] = Convert.ToInt32(newdata[i][0]);                        
                        if (j != 0)
                        {
                            whoget[j-1] = Convert.ToInt32(newdata[0][j]);
                            mainData[i-1,j-1] = Convert.ToInt32(newdata[i][j]);
                        }                            
                    }                    
                }                
            }
            for (int i = 0; i < whogive.Length; i++)
            {
                Console.Write(whogive[i] + "\t");
            }
            for (int i = 0; i < whogive.Length; i++)
            {
                Console.Write(whoget[i] + "\t");
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(mainData[i, j] + "\t");
                Console.WriteLine();
            }
        }
    }
}