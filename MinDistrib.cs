using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class MinDistrib
    {
        int[] whoGive, whoGet;
        int[,] mainData;
        int m, n, count;
        public int[,] distrMatric;
        public int CountNotNullElement
        {
            get { return count; }
            set { count = value; }
        }
        public MinDistrib(int[,] mainData, int[] whoGive, int[] whoGet, int m, int n)
        {
            this.mainData = mainData;
            this.whoGive = whoGive;
            this.whoGet = whoGet;
            this.m = m;
            this.n = n;
        }
        public void MinDistribute()
        {
            distrMatric = new int[n, m];
            int[] min = FindMin();
            int i = 0, j = 0;
            while (min[0] != 0)
            {
                try
                {
                    if (whoGive[min[1]] == 0) { i++; min = FindMin(); }
                    else if (whoGet[min[2]] == 0) { j++; min = FindMin(); }
                    else if (whoGive[min[1]] == 0 && whoGet[min[2]] == 0) { i++; j++; min = FindMin(); }
                    else
                    {
                        distrMatric[min[1], min[2]] = FindMinElement(whoGive[min[1]], whoGet[min[2]]);
                        whoGive[min[1]] -= distrMatric[min[1], min[2]];
                        whoGet[min[2]] -= distrMatric[min[1], min[2]];
                        min = FindMin();
                        CountNotNullElement++;
                    }
                }
                catch
                {

                }
            }
        }
        public int[] FindMin()
        {
            int min = mainData[0, 0];
            int[] minData = new int[3];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (min >= mainData[i, j] && mainData[i, j] != 0 && min != 0)
                    {
                        min = mainData[i, j];
                        minData[0] = min;
                        minData[1] = i;
                        minData[2] = j;
                    }
                    else if (min == 0 && min < mainData[i, j])
                    {
                        min = mainData[i, j];
                        minData[0] = min;
                        minData[1] = i;
                        minData[2] = j;
                    }
                }                    
            mainData[minData[1], minData[2]] = 0;
            return minData;
        }
        static int FindMinElement(int a, int b)
        {
            if (a > b) return b;
            else if (a == b) { return a; }
            else return a;
        }
    }
}
