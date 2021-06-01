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
        List<int[]> var;
        bool[,] checkMin;
        public Stank(string path)
        {
            List<string[]> ls = DataWorkerCSV.ReadCSV("data.csv");
            n = ls.Count;            
            data = new int[n, n];
            checkMin = new bool[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    data[i, j] = Convert.ToInt32(ls[i][j]);
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

    }
}
