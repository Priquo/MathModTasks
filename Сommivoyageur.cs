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
        int uzli = 6;
        int[,] time;
        int[] f = new int[0];
        string[] puti = new string[0];

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

        void CalculatePathes()
        {
            for (int i = 0; i < uzli; i++) //точка отправления - передаем в метод для поиска путей
            {
                Array.Resize(ref puti, puti.Length + 1);
                Array.Resize(ref f, f.Length + 1);
                puti[puti.Length - 1] = $"{i + 1}";
                puti = Schet(puti.Length - 1, i, 0);
            }

        }

        string[] Schet(int pi, int i1, int i2)
        {
            int min = time[i1, i2], mi2 = i2;
            if (puti[pi].Length != time.GetLength(0) * 2 - 1)
            {
                for (int j = 0; j < time.GetLength(0); j++)
                {
                    if (time[i1, j] != 0)
                    {
                        if (PoiskSovpad(pi, j + 1))
                        {
                            min = time[i1, j];
                            mi2 = j;
                            break;
                        }
                    }
                }
                for (int j = 0; j < time.GetLength(0); j++)
                {
                    if (time[i1, j] != 0)
                    {
                        if (PoiskSovpad(pi, j + 1))
                        {
                            if (min > time[i1, j] || (min == time[i1, j] && j == mi2))
                            {
                                min = time[i1, j];
                                mi2 = j;

                            }
                        }
                    }
                }
                for (int j = i2; j < time.GetLength(0); j++) //0
                {
                    if (PoiskSovpad(pi, j + 1))
                        if (min == time[i1, j] && j != mi2)
                        {
                            string s = puti[pi];
                            int ff = f[pi];
                            puti[pi] += $"-{mi2 + 1}";
                            f[pi] += min;
                            puti = Schet(pi, mi2, 0);
                            mi2 = j;
                            Array.Resize(ref puti, puti.Length + 1);
                            Array.Resize(ref f, f.Length + 1);
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
            puti = Schet(pi, mi2, 0);
            return puti;
        }

        public void Calculate()
        {
            ReadSaveData.ReadData(readPath, uzli, ref time);
            CalculatePathes();
            ReadSaveData.WriteToFile(savingPath, uzli, puti, f);
        }
    }
}
