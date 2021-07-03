using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    /// <summary>
    /// Производит решение задачи симплекс-методом
    /// </summary>
    public class Simplex
    {
        string savePath;
        double[,] table; //симплекс таблица

        int m, n;

        List<int> basis; //список базисных переменных
        /// <summary>
        /// Инициализирует новый экземпляр класса Simplex и преобразует исходные данные для обработки
        /// </summary>
        /// <param name="savePath">Путь для сохранения файла</param>
        /// <param name="source">Симплекс таблица без базисных переменных</param>
        public Simplex(string savePath, double[,] source)
        {
            this.savePath = savePath;
            m =source.GetLength(0);
            n = source.GetLength(1);
            table = new double[m, n + m - 1];
            basis = new List<int>();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j < n)
                        table[i, j] = source[i, j];
                    else
                        table[i, j] = 0;
                }
                //выставляем коэффициент 1 перед базисной переменной в строке
                if ((n + i) < table.GetLength(1))
                {
                    table[i, n + i] = 1;
                    basis.Add(n + i);
                }
            }

            n = table.GetLength(1);
        }

        /// <summary>
        /// Вычисление симплекс-метода в массив
        /// </summary>
        /// <param name="result">Массив полученных значений Х</param>
        /// <returns></returns>
        double[,] Calculate(double[] result)
        {
            int mainCol, mainRow; //ведущие столбец и строка

            while (!IsItEnd())
            {
                mainCol = findMainCol();
                mainRow = findMainRow(mainCol);
                basis[mainRow] = mainCol;

                double[,] new_table = new double[m, n];

                for (int j = 0; j < n; j++)
                    new_table[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];

                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                        new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                }
                table = new_table;
            }

            //заносим в result найденные значения X
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = table[k, 0];
                else
                    result[i] = 0;
            }

            return table;
        }
        /// <summary>
        /// Вычисляет результаты методом Calculate() и записывает результаты в файл
        /// </summary>
        public void MakeResult()
        {
            double[] result = new double[2];
            double[,] table_result;
            table_result = Calculate(result);
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    Console.Write(table_result[i, j] + "\t");
                Console.WriteLine();
            }
            ReadSaveData.WriteToFile(savePath, result);
        }
        /// <summary>
        /// Осуществляет проверку строки оценок симплекс-таблицы на наличие положительных значений. Если их нет, возвращает false.
        /// </summary>
        /// <returns></returns>
        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }
        /// <summary>
        /// Находит разрешающий столбец симплекс-таблицы
        /// </summary>
        /// <returns></returns>
        private int findMainCol()
        {
            int mainCol = 1;

            for (int j = 2; j < n; j++)
                if (table[m - 1, j] < table[m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }
        /// <summary>
        /// Находит разрешающую строку симплекс-таблицы
        /// </summary>
        /// <param name="mainCol">Разрешающий столбец симплекс-таблицы</param>
        /// <returns></returns>
        private int findMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i = 0; i < m - 1; i++)
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }

            for (int i = mainRow + 1; i < m - 1; i++)
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }


    }
}