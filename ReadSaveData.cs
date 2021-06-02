using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatModelPraktika
{
    public struct Item : IComparable<Item>
    {
        public int number, aTime, bTime;

        public override string ToString()
        {
            return aTime + " " + bTime;
        }

        public int CompareTo(Item item)
        {
            if (aTime <= bTime)
            {
                if (aTime > item.aTime)
                    return 1;
                if (aTime < item.aTime)
                    return -1;
            }
            else
            {
                if (bTime > item.bTime)
                    return 1;
                if (bTime < item.bTime)
                    return -1;
            }
            return 0;
        }
    }

    struct Activity
    {
        public int eventStart, eventEnd, time;
    }
    struct Path
    {
        public string path;
        public int lastPoint, length;
    }

    static class ReadSaveData
    {
        public static void ReadData(string path, ref List<Activity> activities)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                string[] str = line.Split(';');
                activities.Add(new Activity { eventStart = Convert.ToInt32(str[0]), eventEnd = Convert.ToInt32(str[1]), time = Convert.ToInt32(str[2]) });
            }
        }

        public static void ReadData(string path, ref List<Item> items)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            var lines = File.ReadAllLines(path);
            int i = 1;
            foreach (var line in lines)
            {
                string[] str = line.Split(';');
                items.Add(new Item { number = i, aTime = Convert.ToInt32(str[0]), bTime = Convert.ToInt32(str[1]) });
                i++;
            }
        }

        public static void WriteToFile(string path, List<Item> items, List<int> prostoi, List<Item> optimalItems, List<int> optimalProstoi)
        {
            if (!File.Exists(path)) File.Create(path).Close();
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine("Введенная матрица имеет вид:");
                    sw.WriteLine("Номер\ta\tb");
                    foreach (Item item in items)
                    {
                        sw.WriteLine("{0}\t{1}\t{2}", item.number, item.aTime, item.bTime);
                    }
                    sw.WriteLine("Время простоя второй машины при первичном порядке равно:");
                    sw.WriteLine(prostoi.Max() + "\n——");
                    sw.WriteLine("Оптимальная перестановка имеет следующий вид:");
                    sw.WriteLine("Номер\ta\tb");
                    foreach (Item item in optimalItems)
                    {
                        sw.WriteLine("{0}\t{1}\t{2}", item.number, item.aTime, item.bTime);
                    }
                    sw.WriteLine("Время простоя при оптимальной перестановке равно:");
                    sw.WriteLine(optimalProstoi.Max());
                }
            }
            catch
            {
                Console.WriteLine("Не удалось записать данные в файл!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public static void WriteToFile(string path, Path savingPath)
        {
            if (!File.Exists(path)) File.Create(path).Close();
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine("Найденный путь имеет вид:\n" + savingPath.path + "\nЕго длина составляет: " + savingPath.length);
                }
            }
            catch
            {
                Console.WriteLine("Не удалось записать данные в файл!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
