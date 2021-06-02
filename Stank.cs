using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class Stank
    {
        int n;
        int[] minData = new int[3];
        int[,] data;
        List<int[]> vari = new List<int[]>();
        bool[,] checkMin;
        bool[] checkI;
        bool[] checkJ;
        public Stank(string path)
        {
            List<string[]> ls = DataWorkerCSV.ReadCSV(path);
            n = ls.Count;
            checkI = new bool[n];
            checkJ = new bool[n];
            data = new int[n, n];
            checkMin = new bool[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    data[i, j] = Convert.ToInt32(ls[i][j]);
                    checkMin[i, j] = false;
                }                  
            
        }
        public void MainSolution()
        {
            while (AllTrue(checkMin) == true)
            {
                FindMin();
                int[] temp = new int[n];
                //List<int[]> minTemp = new List<int[]>();
                //minTemp.Add(minData);
                for (int i = 0; i < n; i++)
                {
                    temp[i] = data[minData[1], minData[2]];
                    checkI[minData[1]] = true;
                    checkJ[minData[2]] = true;
                    FindMin(1);
                }
                vari.Add(temp);
                checkI = new bool[n];
                checkJ = new bool[n];
            }
            ShowList();
        }
        void ShowList()
        {
            for (int i = 0; i < vari.Count; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(vari[i][j] + "\t");
                Console.WriteLine();
            }                
        }
        bool AllTrue(bool[,] arr)
        {
            bool flag = true;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    flag = flag && arr[i, j] ;  
                }
            return !flag;
        }
        public void FindMin()
        {
            DefaultMin();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (minData[0] > data[i, j] && checkMin[i, j] == false)
                    {
                        minData[0] = data[i, j];
                        minData[1] = i;
                        minData[2] = j;
                    }
            checkMin[minData[1], minData[2]] = true;
        }        
        public void FindMin(int k)
        {            
            DefaultMin(1);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (minData[0] > data[i, j] && checkI[i] == false && checkJ[j] == false)
                    {
                        minData[0] = data[i, j];
                        minData[1] = i;
                        minData[2] = j;
                    }
        }
        void DefaultMin()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (checkMin[i, j] == false)
                    {
                        minData[0] = data[i, j];
                        minData[1] = i;
                        minData[2] = j;
                    }
        }
        void DefaultMin(int k)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (checkI[i] == false && checkJ[j] == false)
                    {
                        minData[0] = data[i, j];
                        minData[1] = i;
                        minData[2] = j;
                    }
        }
    }
}
