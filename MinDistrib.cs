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
        Element[,] mainData1;
        int m, n;
        public int CountNotNullElement = 0;
        //public int CountNotNullElement
        //{
        //    get { return count; }
        //    set { count = value; }
        //}
        public MinDistrib(Element[,] mainData, int[] whoGive, int[] whoGet, int m, int n)
        {          
            this.whoGive = whoGive;
            this.whoGet = whoGet;
            this.m = m;
            this.n = n;
            mainData1 = new Element[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    mainData1[i, j].Value = mainData[i, j].Value;
        }
        public int MinDistribute(ref Element[,] distrMatric)
        {            
            int[] min = FindMin();
            int i = 0, j = 0;
            while (min[0] != 0)
            {
                try
                {
                    //если элемент массива поставщиков равен нулю, i прибавляется, и ищется новый минимум в матрице
                    if (whoGive[min[1]] == 0) { i++; min = FindMin(); }
                    //если элемент массива покупателей равен нулю, j прибавляется, и ищется новый минимум в матрице
                    else if (whoGet[min[2]] == 0) { j++; min = FindMin(); }
                    //если элементы массива поставщиков и покупателей равны нулю, i и j прибавляются, и ищется новый минимум в матрице
                    else if (whoGive[min[1]] == 0 && whoGet[min[2]] == 0) { i++; j++; min = FindMin(); }
                    //в остальных случаях вычисляется распеределение поставок
                    else
                    {
                        //из массива поставщиков и покупателей сравнивается, какой элемент меньше, он и будет добавлен как поставка
                        distrMatric[min[1], min[2]].Delivery = FindMinElement(whoGive[min[1]], whoGet[min[2]]);
                        whoGive[min[1]] -= distrMatric[min[1], min[2]].Delivery;
                        whoGet[min[2]] -= distrMatric[min[1], min[2]].Delivery;
                        min = FindMin();
                        //переменная, которая записывает количество поставок (не пустых ячеек доставки)
                        CountNotNullElement++;
                    }
                }
                catch
                {

                }
            }
            return CountNotNullElement;
        }
        public int[] FindMin()
        {
            int min = mainData1[0, 0].Value;
            int[] minData = new int[3];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {                    
                    if (min >= mainData1[i, j].Value && mainData1[i, j].Value != 0 && min != 0)
                    {
                        min = mainData1[i, j].Value;
                        minData[0] = min;
                        minData[1] = i;
                        minData[2] = j;
                    }
                    else if (min == 0 && min < mainData1[i, j].Value)
                    {
                        min = mainData1[i, j].Value;
                        minData[0] = min;
                        minData[1] = i;
                        minData[2] = j;
                    }
                }
            //использованная ячейка с минимальными затратами обнуляется, чтобы она пропускалась при нахождении минимума
            mainData1[minData[1], minData[2]].Value = 0;
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
