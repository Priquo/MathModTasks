using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    /// <summary>
    /// Класс для решения задачи комивояжера
    /// </summary>
    class Сommivoyageur
    {
        string readPath; 
        string savingPath;
        int[,] time; //Массив, содержащий введённые значения
        int[] f = new int[0]; //Массив, содержащий длины путей
        int uzli; //Длина строк и столбцов массива time
        string[] puti = new string[0]; // Массив, содержащий полученные пути

        /// <summary>
        /// Метод, записывающий путь файла, из которого берутся данные и в который записываются результаты
        /// </summary>
        /// <param name="readPath">Путь файла, из которого берутся данные</param>
        /// <param name="savingPath">Путь файла, в который записываются результаты</param> 
        public Сommivoyageur(string readPath, string savingPath)
        {
            this.readPath = readPath;
            this.savingPath = savingPath;
        }
        /// <summary>
        /// Метод для поиска совпадений среди строки
        /// </summary>
        /// <param name="pi">Номер рассматриваемого пути</param>
        /// <param name="j">Номер столбца в рассматриваемой строке</param>
        /// <returns>Возвращает True, если совпадений нет и False, если есть</returns>
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
        /// <summary>
        /// Метод, в котором вызывается поиск и подсчёт длин путей
        /// </summary>
        void CalculatePaths()
        {
            for (int i = 0; i < uzli; i++) //точка отправления - передаем в метод для поиска путей
            {
                Array.Resize(ref puti, puti.Length + 1); //Увеличение размера массива путей
                Array.Resize(ref f, f.Length + 1); //Увеличение размера массива длины путей
                puti[puti.Length - 1] = $"{i + 1}"; //Запись в начало пути стартового элемента
                puti = Schet(puti.Length - 1, i); // Добавление нового пути
            }

        }
        /// <summary>
        /// Основной вычислительный метод, в котором ведётся подсчёт длин путей и добавление их пунктов и длин в массивы
        /// </summary>
        /// <param name="pi">Номер рассматриваемого пути</param>
        /// <param name="i1">Номер рассматриваемой строки</param>
        /// <returns>Возвращает изменённый массив путей</returns>
        string[] Schet(int pi, int i1)
        {
            int min = time[i1, 0], mi2 = 0;
            if (puti[pi].Length != time.GetLength(0) * 2 - 1) //Проверка, что количество узлов в пути ниже количества узлов в массиве (длина строк и столбцов массива умножается на 2, потому что в пятх также считаются '-' между узлами)
            {
                for (int j = 0; j < time.GetLength(0); j++) //Просмотр строки в поиске элемента, который не был записан в пути
                {
                    if (time[i1, j] != 0) 
                    {
                        if (PoiskSovpad(pi, j + 1))
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
                        if (PoiskSovpad(pi, j + 1)) 
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
                            mi2 = j; //Запись индекса ещё одного минимума, встречающегося в строке
                            Array.Resize(ref puti, puti.Length + 1); //Увеличение размера массива путей
                            Array.Resize(ref f, f.Length + 1);//Увеличение размера массива длины путей
                            pi = f.Length - 1;//Индексу присваивается значение последнего индекса длины путей
                            puti[pi] = s;//В путь, по последнему индексу массива длины путей записывается временная строка
                            f[pi] = ff;//В массив длин путей, по последнему индексу массива длины путей записывается временная переменная с длиной
                        }
                }
            }
            else    //Когда доходит до последнего узла он берет первый узел в пути и добавляет его в конец
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
        /// <summary>
        /// Основной метод, в котором читаются данные из файла, вызывается вычисляющий метод, и результаты записываются в файл
        /// </summary>
        public void Calculate()
        {
            ReadSaveData.ReadData(readPath, ref time);
            uzli = time.GetLength(0);
            CalculatePaths();
            ReadSaveData.WriteToFile(savingPath, uzli, puti, f);
        }
    }
}
