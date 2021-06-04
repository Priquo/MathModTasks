using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class Сommivoyageur
    {
        string readPath; 
        string savingPath;
        int[,] time; //Массив, содержащий введённые значения
        int[] f = new int[0]; //Массив, содержащий длины путей
        int uzli; //Длина строк и столбцов массива time
        string[] puti = new string[0]; // Массив, содержащий полученные пути

        public Сommivoyageur(string readPath, string savingPath)
        {
            this.readPath = readPath;
            this.savingPath = savingPath;
        }

        bool PoiskSovpad(int pi, int j)
        {
            bool sch = true; //совпадений нет
            foreach (string s in puti[pi].Split('-'))
            {
                if (Convert.ToInt32(s) == j)
                {
                    sch = false; //есть
                    break;
                }
            }
            return sch;
        }

        void CalculatePaths()
        {
            for (int i = 0; i < uzli; i++) //точка отправления - передаем в метод для поиска путей
            {
                Array.Resize(ref puti, puti.Length + 1); //Увеличение размера массива путей
                Array.Resize(ref f, f.Length + 1); //Увеличение размера массива длины путей
                puti[puti.Length - 1] = $"{i + 1}"; //Запись в конец пути стартового элемента (с которого начинается путь)
                puti = Schet(puti.Length - 1, i); //Добавление нового пути
            }

        }

        string[] Schet(int pi, int i1)
        {
            int min = time[i1, 0], mi2 = 0;
            if (puti[pi].Length != time.GetLength(0) * 2 - 1) //Проверка, что количество узлов в пути ниже количества узлов в массиве длина строк и столбцов массива умножается на 2, потому что в пятх также считаются '-' между узлами
            {
                for (int j = 0; j < time.GetLength(0); j++) //Просмотр строки в поиске элемента, который не был записан в пути
                {
                    if (time[i1, j] != 0) 
                    {
                        if (PoiskSovpad(pi, j + 1)) //Метод поиска совпадений в рассматриваемом пути, возвращает false, если в пути этот узел уже использовался
                        {
                            min = time[i1, j]; //Первый элемент строки, не встречавшийся до этого в пути, берётся за минимум
                            mi2 = j; //Запись индекса элемента
                            break;
                        }
                    }
                }
                for (int j = 0; j < time.GetLength(0); j++) //Просмотр строки в поисках элемента строки который меньше ранее записанного первого элемента, не встречающегося в пути
                {
                    if (time[i1, j] != 0)
                    {
                        if (PoiskSovpad(pi, j + 1)) //Проверяет на совпадения в пути
                        {
                            if (min > time[i1, j] || (min == time[i1, j] && j == mi2)) //Если элемент меньше минимума или равен ему и не является им же, то он является новым минимумом и его индекс записывается
                            {
                                min = time[i1, j];
                                mi2 = j;

                            }
                        }
                    }
                }
                for (int j = 0; j < time.GetLength(0); j++) //Добавление узлов в путь и расстояния между ними
                {
                    if (PoiskSovpad(pi, j + 1))
                        if (min == time[i1, j] && j != mi2) //Если нашёл элемент, равный минимуму, но не являющийся им
                        {
                            string s = puti[pi]; //Запись во временную переменную расматриваемого пути
                            int ff = f[pi]; //Запись во временную переменную длину расматриваемого пути 
                            puti[pi] += $"-{mi2 + 1}"; //Добавление в путь узла, в котором находится минимум строки
                            f[pi] += min; //Добавление к длине пути минимум строки
                            puti = Schet(pi, mi2); //Рассчёт дальнейшего пути, начиная с узла, в котором находится минимум
                            mi2 = j;
                            Array.Resize(ref puti, puti.Length + 1); //Увеличение размера массива путей
                            Array.Resize(ref f, f.Length + 1);//Увеличение размера массива длины путей
                            pi = f.Length - 1;
                            puti[pi] = s;
                            f[pi] = ff;
                        }
                }
            }
            else
            {
                foreach (string s in puti[pi].Split('-'))
                {
                    mi2 = Convert.ToInt32(s);

                    min = time[i1, mi2 - 1];
                    break;
                }
                puti[pi] += $"-{mi2}";
                f[pi] += min;
                return puti;
            }
            puti[pi] += $"-{mi2 + 1}";
            f[pi] += min;
            puti = Schet(pi, mi2);
            return puti;
        }

        public void Calculate()
        {
            ReadSaveData.ReadData(readPath, ref time);
            uzli = time.GetLength(0);
            CalculatePaths();
            ReadSaveData.WriteToFile(savingPath, uzli, puti, f);
        }
    }
}
