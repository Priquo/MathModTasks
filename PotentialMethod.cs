using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    struct Element
    {
        public int Delivery { get; set; }
        public int Value { get; set; }
        public static int FindMinElement(int a, int b)
        {
            if (a > b) return b;
            if (a == b) { return a; }
            else return a;
        }
    }
    class PotentialMethod
    {
        int n, m;
        int[] whogive, whoget, minData, U, V;
        int[,] delta;
        bool optimSolution = true;
        bool[,] elemChecked;
        List<int[]> maxDelta;        
        Element[,] mainData;    
        MinDistrib md;
        public PotentialMethod(string path)
        {
            List<string[]> newdata = DataWorkerCSV.ReadCSV(path);
            m = newdata.Count - 1;
            n = newdata.First().Length - 1;
            whogive = new int[n];
            whoget = new int[m];
            V = new int[n];
            U = new int[m];
            elemChecked = new bool[n, m];
            mainData = new Element[n, m];
            delta = new int[n, m];
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
                            mainData[i - 1, j - 1].Value = Convert.ToInt32(newdata[i][j]);
                        }
                    }
                }
            }
        }
        // раньше был метод, но теперь часть конструктора. по итогу использую для отладки
        //public void DataReaderConverter(string path)
        //{

        //    MinDistrib md = new MinDistrib(mainData, whogive, whoget, m, n);
        //    md.MinDistribute();
        //    for (int i = 0; i < n; i++)
        //    {
        //        for (int j = 0; j < m; j++)
        //            Console.Write(md.distrMatric[i, j] + "\t");
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine(md.CountNotNullElement);
        //}
        public void MainSolution()
        {
            MinDistrib md = new MinDistrib(mainData, whogive, whoget, m, n);
            md.MinDistribute(ref mainData);
            if (md.CountNotNullElement != m + n - 1)
            {
                FindMin(1);
                while (true)
                {                    
                    if (mainData[minData[1], minData[2]].Delivery == 0)
                    {
                        mainData[minData[1], minData[2]].Delivery = -1;
                        break;
                    }
                    else FindMin(1);
                }                
            }
            for (int i = 0; i < n; i++)           
            for (int j = 0; j < m; j++)
            {
                if (mainData[i, j].Delivery == 0)
                {
                    delta[i, j] = U[j] + V[i] - mainData[i, j].Value;
                    if (delta[i, j] > maxDelta[0][0])
                    {
                        maxDelta.RemoveAt(0);
                        maxDelta.Add(new int[] { delta[i, j], i, j});
                    }                    
                }
            }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (delta[i, j] > 0) { optimSolution = false; break; }
                    else optimSolution = true;
            //FindUV(U, V, mainData);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(mainData[i, j].Value + "/" + mainData[i, j].Delivery + "\t");
                Console.WriteLine();
            }
            for (int i = 0; i < n; i++) Console.Write(U[i] + "\t");
            Console.WriteLine();
            for (int i = 0; i < n; i++) Console.Write(V[i] + "\t");

        }        
        void FindUV()
        {
            bool[] U1 = new bool[m];
            U1[0] = true;
            U[0] = 0;
            bool[] V1 = new bool[n];
            while (!(AllTrue(V1) && AllTrue(U1)))
            {

            }
        }
        // взяла код из какого-то древнего проекта. надеюсь, работает
        // криво работает
        //void FindUV(int[] U, int[] V, Element[,] HelpMatr)
        //{
        //    //для проверки вычислена ли Ui Vi будем использовать массив boolean'ов
        //    //даже 2 массива. в одном признак того вычислена ли соответствующий потенциал
        //    //во втором прошлись ли мы по строке/строчке этого потенциала
        //    //алгоритм позволит за конечное число итераций вычислить все потенциалы. ура.
        //    bool[] U1 = new bool[m];
        //    U1[0] = true;
        //    U[0] = 0;
        //    bool[] U2 = new bool[m];
        //    bool[] V1 = new bool[n];
        //    bool[] V2 = new bool[n];
        //    //V[BSize - 1] = 0;
        //    //V1[BSize - 1] = true;
        //    // пока все элементы массивов V1 и U1 не будут равны true
        //    while (!(AllTrue(V1) && AllTrue(U1)))
        //    {
        //        int i = -1;
        //        int j = -1;
        //        for (int i1 = n - 1; i1 >= 0; i1--)
        //            if (V1[i1] && !V2[i1]) i = i1;
        //        for (int j1 = m - 1; j1 >= 0; j1--)
        //            if (U1[j1] && !U2[j1]) j = j1;

        //        if ((j == -1) && (i == -1))
        //            for (int i1 = n - 1; i1 >= 0; i1--)
        //                if (!V1[i1] && !V2[i1])
        //                {
        //                    i = i1;
        //                    V[i] = 0;
        //                    V1[i] = true;
        //                    break;
        //                }
        //        if ((j == -1) && (i == -1))
        //            for (int j1 = m - 1; j1 >= 0; j1--)
        //                if (!U1[j1] && !U2[j1])
        //                {
        //                    j = j1;
        //                    U[j] = 0;
        //                    U1[j] = true;
        //                    break;
        //                }

        //        if (i != -1)
        //        {
        //            for (int j1 = 0; j1 < m; j1++)
        //            {
        //                if (!U1[j1]) U[j1] = HelpMatr[j1, i].Value - V[i];
        //                if (U[j1] == U[j1]) U1[j1] = true;
        //            }
        //            V2[i] = true;
        //        }

        //        if (j != -1)
        //        {
        //            for (int i1 = 0; i1 < n; i1++)
        //            {
        //                if (!V1[i1]) V[i1] = HelpMatr[j, i1].Value - U[j];
        //                if (V[i1] == V[i1]) V1[i1] = true;
        //            }
        //            U2[j] = true;
        //        }

        //    }
        //    int rt = 0;
        //}
        private bool AllTrue(bool[] arr)
        {
            return Array.TrueForAll(arr, delegate (bool x) { return x; });
        }
        void FindMin()
        {
            int min = mainData[0, 0].Value;
            minData = new int[3];
            for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            if (min > mainData[i, j].Value && mainData[i, j].Value != 0 && min != 0)
            {
                min = mainData[i, j].Value;
                minData[0] = min;
                minData[1] = i;
                minData[2] = j;
            }                
        }
        void FindMin(int k)
        {
            int min = mainData[0, 0].Value;
            minData = new int[3];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (min > mainData[i, j].Value && mainData[i, j].Value != 0 && min != 0 && mainData[i, j].Delivery == 0 && elemChecked[i, j] == false)
                    {
                        min = mainData[i, j].Value;
                        minData[0] = min;
                        minData[1] = i;
                        minData[2] = j;
                    }
        }
    }
}
