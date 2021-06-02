using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class Stank
    {
        int n, min;
        int[] minData = new int[3];
        int[,] data;
        List<int[]> vari;
        bool[,] checkMin;
        bool[] checkI;
        bool[] checkJ;
        public Stank(string path)
        {
            List<string[]> ls = DataWorkerCSV.ReadCSV("data.csv");
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
            while (!AllTrue(checkMin))
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
            }
        }
        bool AllTrue(bool[,] arr)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (checkMin[i, j] == false) return true;
                    else return false;
            return true;
        }
        public void FindMin()
        {

            minData[0] = data[0, 0];
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
            minData[0] = data[0, 0];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (minData[0] > data[i, j] && checkI[i] != true && checkJ[j] != true)
                    {
                        minData[0] = data[i, j];
                        minData[1] = i;
                        minData[2] = j;
                    }
        }

    }
}
