using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    /// <summary>
    /// Класс для оптимального распределения m-го количества работ среди n-го количества станков
    /// </summary>
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
        /// Инициализирует объект класса Stank с чтением исходных данных по указанному пути path
        /// </summary>
        /// <param name="path">Путь к файлу с исходными данными</param>
        public Stank(string path)
        {
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
                    FindMinInNotUsedElements();
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
        /// <summary>
        /// Устанавливает флаг проверки использованных элементов в матрице. Если хотя бы один элемент будет равен false, метод вернет true
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Нахождение минимального элемента среди неиспользованных элементов матрицы и запись их (использованные) как true в булевой матрице checkMin + 1 перегрузка
        /// </summary>
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
        /// <summary>
        /// Нахождение минимального элемента среди элементов матрицы, где индексы не совпадают с использованными ранее с помощью массивов checkI и checkJ для индексов i и j соответственно
        /// </summary>
        void FindMinInNotUsedElements()
        {            
            DefaultMinInNotUsedElements();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (minData[0] > data[i, j] && checkI[i] == false && checkJ[j] == false)
                    {
                        minData[0] = data[i, j];
                        minData[1] = i;
                        minData[2] = j;
                    }
        }
        /// <summary>
        /// Нахождение минимальной суммы среди всех полученных вариантов распределения работ между станками
        /// </summary>
        /// <param name="ar">Последовательность всех вариантов распределения работ между станками</param>
        /// <returns></returns>
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
        /// <summary>
        /// Задает значение минимуму перед нахождением самого минимума в матрице для метода FindMin()
        /// </summary>
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
        /// <summary>
        /// Задает значение минимуму перед нахождением самого минимума в матрице для метода FindMinInNotUsedElements()
        /// </summary>
        void DefaultMinInNotUsedElements()
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
