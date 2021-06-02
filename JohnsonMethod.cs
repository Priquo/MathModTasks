using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MatModelPraktika
{
    class JohnsonMethod
    {
        string readPath;
        string savePath;
        List<Item> items = new List<Item>();
        List<Item> optimalItems = new List<Item>();

        List<int> prostoi = new List<int>();
        List<int> optimalProstoi = new List<int>();

        public JohnsonMethod(string readPath, string savePath)
        {
            this.readPath = readPath;
            this.savePath = savePath;
        }

        void SortElements()
        {
            List<Item> aList = new List<Item>();
            List<Item> bList = new List<Item>();
            foreach (Item item in items)
            {
                if (item.aTime <= item.bTime)
                {
                    aList.Add(item);
                }
                else bList.Add(item);
            }
            aList.Sort();
            bList.Sort();
            bList.Reverse();
            foreach (Item item in aList) optimalItems.Add(item);
            foreach (Item item in bList) optimalItems.Add(item);
        }

        void FindOptimal(List<Item> items, List<int> prostoi)
        {
            int count = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (i != 0)
                    count += (items[i].aTime - items[i - 1].bTime);
                else
                    count += items[i].aTime;
                prostoi.Add(count);
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
