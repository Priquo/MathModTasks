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
        int[] summ;
        int[,] data;
        List<List<int[]>> vari = new List<List<int[]>>();
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
                    data[i, j] = Convert.ToInt32(ls[i][j]);            
        }
        public void MainSolution()
        {
            while (AllTrue(checkMin) == true)
            {
                FindMin();
                List<int[]> temp = new List<int[]>();
                //List<int[]> minTemp = new List<int[]>();
                //minTemp.Add(minData);
                for (int i = 0; i < n; i++)
                {
                    temp.Add(new int[] {data[minData[1],minData[2]], minData[1], minData[2] });
                    checkI[minData[1]] = true;
                    checkJ[minData[2]] = true;
                    FindMin(1);
                }
                vari.Add(temp);
                checkI = new bool[n];
                checkJ = new bool[n];
            }
            summ = new int[vari.Count];
            for (int i = 0; i < vari.Count; i++)
                for (int j = 0; j < n; j++)
                    summ[i] += vari[i][j][0];
            //ShowList();
            int min = FindMin(summ);
            int k = 2;
            string[] m = new string[n+2];
            m[0] = "Оптимальное распределение имеет временные затраты: " + min;
            m[1] = "Распределение станков следующее";
            //Console.Write("Оптимальное распределение имеет временные затраты: {0};\nРаспределение станков следующее\n", min);
            for (int i = 0; i < n; i++)
            {
                m[k] = vari[Array.IndexOf(summ, min)][i][0] + ";" + vari[Array.IndexOf(summ, min)][i][1] + ";" + vari[Array.IndexOf(summ, min)][i][2];
                k++;
            }
            //Console.Write(vari[Array.IndexOf(summ, min)][i][0] + ": " + vari[Array.IndexOf(summ, min)][i][1] + ", " + vari[Array.IndexOf(summ, min)][i][2] + "\t");
            DataWorkerCSV.WriteToCSV("result.csv", m);
        }
        //void ShowList()
        //{
        //    for (int i = 0; i < vari.Count; i++)
        //    {
        //        for (int j = 0; j < n; j++)
        //            Console.Write("\t" + vari[i][j][0] + ": " + vari[i][j][1] + ", " + vari[i][j][2] + "\t");
        //        Console.WriteLine();
        //    }                
        //}
        void MakeSum()
        {
            
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
        void FindMin()
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
        void FindMin(int k)
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
        int FindMin(int[] ar)
        {
            int min = ar[0];
            for (int i = 0; i < n; i++)
                if (min > ar[i])
                {
                    min = ar[i];
                }
            return min;
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
