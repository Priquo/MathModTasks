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
        int n, m, summ;
        int[] whogive, whoget, minData, U, V;
        int[,] delta;
        bool optimSolution = false;
        bool[,] elemChecked;
        List<int[]> maxDelta = new List<int[]>();        
        Element[,] mainData;    
        MinDistrib md;
        public PotentialMethod(string path)
        {
            List<string[]> newdata = ReadSaveData.ReadData(path);
            n = newdata.Count - 1;
            m = newdata.First().Length - 1;
            whogive = new int[n];
            whoget = new int[m];
            V = new int[n];
            U = new int[m];
            elemChecked = new bool[n, m];
            mainData = new Element[n, m];
            
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
        void CheckVir(MinDistrib mg)
        {
            if (mg.CountNotNullElement != m + n - 1)
            {
                FindMin(1);
                while (true)
                {
                    if (mainData[minData[1], minData[2]].Delivery == 0)
                    {
                        mainData[minData[1], minData[2]].Delivery = -1;
                        mg.CountNotNullElement++;
                        break;
                    }
                    else FindMin(1);
                }
            }
        }
        public void MainSolution()
        {            
            md = new MinDistrib(mainData, whogive, whoget, m, n);
            while (md.MinDistribute(ref mainData) != m + n - 1)
            {
                CheckVir(md);
            }
            while (optimSolution == false)
            {
                // заполняет изначально массивы с потенциалами, чтобы потом перезаписать на нормальные
                V = new int[n];
                for (int i = 0; i < V.Length; i++)
                {
                    V[i] = 99999;
                }
                U = new int[m];
                for (int i = 0; i < U.Length; i++)
                {
                    U[i] = 99999;
                }
                //нахождение макс. тарифа в заполненых ячейках
                double MaxCost = 0;
                int maxV = 0;
                int maxU = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (mainData[i, j].Delivery != 0 && mainData[i, j].Value > MaxCost)
                        {
                            MaxCost = mainData[i, j].Value;
                            maxV = i;
                            maxU = j;
                        }
                    }
                }
                //расчет потенциалов 
                V[maxV] = 0;
                U[maxU] = mainData[maxV, maxU].Value - V[maxV];
                for (int sania = 0; sania < m; sania++) // саню не трогать!!!
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < m; j++)
                            if (mainData[i, j].Delivery != 0 && (V[i] == 99999 || U[j] == 99999)) //проверяется, является ли поставка пустой и не записаны ли для этой ячейки нормальные потенциалы
                            {
                                if (V[i] == 99999 && U[j] == 99999) //если обе такие, считать нечего
                                    continue;
                                if (V[i] != 99999)
                                {
                                    for (int k = 0; k < m; k++)
                                        if (mainData[i, k].Delivery != 0)
                                            U[k] = mainData[i, k].Value - V[i];
                                }
                                if (U[j] != 99999)
                                    for (int k = 0; k < n; k++)
                                        if (mainData[k, j].Delivery != 0) V[k] = mainData[k, j].Value - U[j];

                            }
                maxDelta.Clear();
                maxDelta.Add(new int[] { 0, 0, 0 });
                delta = new int[n, m];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        if (mainData[i, j].Delivery == 0)
                        {
                            delta[i, j] = U[j] + V[i] - mainData[i, j].Value;
                            if (delta[i, j] > maxDelta[0][0])
                            {
                                maxDelta.RemoveAt(0);
                                maxDelta.Add(new int[] { delta[i, j], i, j });
                            }
                        }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (delta[i, j] > 0) { optimSolution = false; break; }
                        else optimSolution = true;
                    }
                    if (optimSolution == false) break;
                }
                if (optimSolution == false)
                {
                    double MaxCostInRowWithDelta = 0;
                    int MaxCostInRowWithDeltaI = 0, MaxCostInRowWithDeltaJ = 0;
                    //находим ячейку с товаром и макс. тарифом в строке с макс. дельта
                    //maxDelta[0][1] - возвращает индекс строки максимальной дельты
                    //maxDelta[0][2] - возвращает индекс столбца максимальной дельты
                    for (int j = 0; j < m; j++)
                    {
                        if (mainData[maxDelta[0][1], j].Delivery != 0 && mainData[maxDelta[0][1], j].Value > MaxCostInRowWithDelta)
                        {
                            MaxCostInRowWithDelta = mainData[maxDelta[0][1], j].Value;
                            MaxCostInRowWithDeltaI = maxDelta[0][1];
                            MaxCostInRowWithDeltaJ = j;
                        }
                    }
                    //находим ячейку с товаром и макс. тарифом в столбце с макс. дельта
                    double MaxCostInCOLUMNWithDelta = 0;
                    int MaxCostInColumnWithDeltaI = 0, MaxCostInColumnWithDeltaJ = 0;

                    for (int i = 0; i < n; i++)
                    {
                        if (mainData[i, maxDelta[0][2]].Delivery != 0 && mainData[i, maxDelta[0][2]].Value > MaxCostInCOLUMNWithDelta)
                        {
                            MaxCostInCOLUMNWithDelta = mainData[i, maxDelta[0][2]].Value;
                            MaxCostInColumnWithDeltaI = i;
                            MaxCostInColumnWithDeltaJ = maxDelta[0][2];
                        }
                    }
                    //находим, сколько товара мы можем переместить
                    double MaxAmountWeCanAfford;
                    if (mainData[MaxCostInColumnWithDeltaI, MaxCostInColumnWithDeltaJ].Delivery > mainData[MaxCostInRowWithDeltaI, MaxCostInRowWithDeltaJ].Delivery)
                    {
                        MaxAmountWeCanAfford = mainData[MaxCostInRowWithDeltaI, MaxCostInRowWithDeltaJ].Delivery;
                    }
                    else
                    {
                        MaxAmountWeCanAfford = mainData[MaxCostInColumnWithDeltaI, MaxCostInColumnWithDeltaJ].Delivery;
                    }
                    //перемещаем товар
                    mainData[MaxCostInRowWithDeltaI, MaxCostInRowWithDeltaJ].Delivery -= (int)MaxAmountWeCanAfford;
                    mainData[maxDelta[0][1], maxDelta[0][2]].Delivery += (int)MaxAmountWeCanAfford;
                    mainData[MaxCostInColumnWithDeltaI, MaxCostInColumnWithDeltaJ].Delivery -= (int)MaxAmountWeCanAfford;
                    mainData[MaxCostInColumnWithDeltaI, MaxCostInRowWithDeltaJ].Delivery += (int)MaxAmountWeCanAfford;
                }
                else
                {
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < m; j++)
                            summ += mainData[i, j].Delivery * mainData[i, j].Value;
                    break;
                }
            }
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
        // оставьте в покое метод... он мне где-то нужен был
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
