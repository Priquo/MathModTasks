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
        /// <summary>
        /// Класс для оптимального распределения m-го количества работ среди n-го количества станков
        /// </summary>
        /// <param name="path">Путь к файлу с исходными данными</param>
        public Stank(string path)
        {
            // данные в файле должны выглядеть как таблица, у которой значения ячеек разделены с помощью ";". НИКАКИХ ПРОБЕЛОВ!!!!!!
            List<string[]> ls = ReadSaveData.ReadData(path);
            n = ls.Count;
            checkI = new bool[n];
            checkJ = new bool[n];
            data = new int[n, n];
            checkMin = new bool[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    data[i, j] = Convert.ToInt32(ls[i][j]);            
        }
        /// <summary>
        /// Находит оптимальное распределение работ между станками
        /// </summary>
        public void MainSolution()
        {
            //пока все минимумы не будут использованы, цикл продолжается
            while (AllTrue(checkMin) == true)
            {
                //находит минимум во всей матрице
                FindMin();
                List<int[]> temp = new List<int[]>();
                for (int i = 0; i < n; i++)
                {
                    //добавляет первый минимум и потом "вырезает" его индексы, чтобы они не использовались для нахождения других минимумов
                    temp.Add(new int[] {data[minData[1],minData[2]], minData[1], minData[2] });
                    checkI[minData[1]] = true;
                    checkJ[minData[2]] = true;
                    FindMin(1);
                }
                //добавляется вариант распределения, после чего минимум ищется снова, но другой
                vari.Add(temp);
                checkI = new bool[n];
                checkJ = new bool[n];
            }
            //подсчет суммы всех вариантов и нахождение минимальной
            summ = new int[vari.Count];
            for (int i = 0; i < vari.Count; i++)
                for (int j = 0; j < n; j++)
                    summ[i] += vari[i][j][0];            
            int min = FindMin(summ);
            //подготовка результатов для записи в файл
            int k = 2;
            string[] m = new string[n+2];
            m[0] = "Оптимальное распределение имеет временные затраты: " + min;
            m[1] = "Распределение станков следующее";
            for (int i = 0; i < n; i++)
            {
                m[k] = vari[Array.IndexOf(summ, min)][i][0] + ";" + vari[Array.IndexOf(summ, min)][i][1] + ";" + vari[Array.IndexOf(summ, min)][i][2];
                k++;
            }
            ReadSaveData.WriteToFile("result.csv", m);
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
        //данный минимум используется в начале, checkMin записывает, использовался ли минимум (тру если да) 
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
        //данный минимум используется для "вырезания" использованных индексов с помощью массивов checkI и checkJ для индексов i и j соответственно
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
        //этот минимум нужен для нахождения минимальной суммы распределения работ у станков
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
        //задает значение минимуму перед нахождением самого минимума в матрице в перегрузке метода FindMin()
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
        //задает значение минимуму перед нахождением самого минимума в матрице в перегрузке метода FindMin(int k)
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
