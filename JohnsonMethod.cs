using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MathModTasks
{
    class JohnsonMethod
    {
        string readPath;
        string savePath;
        List<Item> items = new List<Item>(); //Список предметов, загруженный из файла
        List<Item> optimalItems = new List<Item>(); //Список предметов после оптимального распределения

        List<int> prostoi = new List<int>(); //Список простоев станков
        List<int> optimalProstoi = new List<int>(); //Список простоев станков после оптимального распределения

        public JohnsonMethod(string readPath, string savePath)
        {
            this.readPath = readPath;
            this.savePath = savePath;
        }

        void SortElements() //Метод сортировки элементов по массиву
        {
            List<Item> aList = new List<Item>(); //Первый временный список, для дальнейшего распределения
            List<Item> bList = new List<Item>(); //Второй временный список, для дальнейшего распределения
            foreach (Item item in items)
            {
                if (item.aTime <= item.bTime) //Если время на первом станке <=, чем на втором, то идет добавление в первый временный список
                {
                    aList.Add(item);
                }
                else bList.Add(item); //В ином случае записывается во второй временный список
            }
            aList.Sort(); //Данный список сортируется в порядке возрастания времени на первом станке (см. структуру)
            bList.Sort(); //Данный список сортируется в порядке возрастания времени на втором станке (см. структуру)
            bList.Reverse(); //Далее данный список переворачивается для дальнейшего объединения
            foreach (Item item in aList) optimalItems.Add(item); //Все данные из первого списка записываются в новый список
            foreach (Item item in bList) optimalItems.Add(item); //Все данные из второго списка записываются в новый список
        }

        void FindOptimal(List<Item> items, List<int> prostoi)
        {
            int count = 0; //Временная переменная для подсчета простоев
            for (int i = 0; i < items.Count; i++)
            {
                if (i != 0) //Если это не первый элемент в первом списке
                    count += (items[i].aTime - items[i - 1].bTime); //То вычисляется по этой формуле
                else //Если первый, то просто записывается
                    count += items[i].aTime;
                prostoi.Add(count); //И это записывается в лист простоев
            }
        }

        public void Calculate()
        {
            ReadSaveData.ReadData(readPath, ref items);
            SortElements();
            FindOptimal(items, prostoi);
            FindOptimal(optimalItems, optimalProstoi);
            ReadSaveData.WriteToFile(savePath, items, prostoi, optimalItems, optimalProstoi);
        }

    }
}
