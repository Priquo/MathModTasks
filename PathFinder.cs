using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModTasks
{
    class PathFinder
    {
        string readPath;
        string savePath;
        public PathFinder(string readPath, string savePath)
        {
            this.readPath = readPath;
            this.savePath = savePath;
        }

        List<Activity> activities = new List<Activity>(); //Список всех работ (в графике это дуги)
        List<Path> pathes = new List<Path>(); //Список всех путей

        int FindStartingPos() //Метод для поиска начальной точки
        {
            foreach (Activity activity in activities) //Если нет таких дуг, которые бы входили в данную точку, то она начальная.
                if (activities.Where(x => x.eventEnd == activity.eventStart).Count() == 0) return activity.eventStart;
            return -1;
        }

        int FindEndingPos() //Метод для поиска конечной точки
        {
            foreach (Activity activity in activities) //Если нет таких дуг, которые бы исходили из данной точки, то она конечная.
                if (activities.Where(x => x.eventStart == activity.eventEnd).Count() == 0) return activity.eventEnd;
            return -1;
        }

        void CalculatePathes() //Метод подсчета путей
        {
            foreach (Activity activity in activities.Where(x => x.eventStart == FindStartingPos())) //Сначала в список путей заносятся все начальные дуги
            {
                pathes.Add(new Path { path = activity.eventStart + "--" + activity.eventEnd, lastPoint = activity.eventEnd, length = activity.time });
            }
            for (int i = 0; i < pathes.Count; i++) //Затем программа начинает обход по всем записанным путям (в ходе выполнения цикла их количество пополняется)
            {
                foreach (Activity activity in activities.Where(x => x.eventStart == pathes[i].lastPoint)) //В список путей заносятся новые пути, которые исходят из проверяемого в данных момент
                {
                    //Таким образом в список заносятся все промежуточные пути, зато работает
                    pathes.Add(new Path { path = pathes[i].path + "--" + activity.eventEnd, lastPoint = activity.eventEnd, length = pathes[i].length + activity.time });
                }
            }
        }

        Path FindCriticalPath() //Метод поиска критического пути
        {
            int maxLength = 0;
            foreach (Path path in pathes.Where(x => x.lastPoint == FindEndingPos())) //Проверяет все пути, конечная точка которых совпадает с концом сети
            {
                if (path.length > maxLength) maxLength = path.length; //Вычисляет самый длинный путь из представленных
            }
            Path criticalPath = pathes.Find(x => x.length == maxLength); //И возвращает его
            return criticalPath;
        }

        Path FindMinimalPath() //Метод поиска минимального пути
        {
            int minLength = int.MaxValue;
            foreach (Path path in pathes.Where(x => x.lastPoint == FindEndingPos()))
            {
                if (path.length < minLength) minLength = path.length; //То же самое, но ищет минимальный
            }
            Path minimalPath = pathes.Find(x => x.length == minLength);
            return minimalPath;
        }


        public void CalculateCriticalPath()
        {
            ReadSaveData.ReadData(readPath, ref activities);
            CalculatePathes();
            var criticalPath = FindCriticalPath();
            ReadSaveData.WriteToFile(savePath, criticalPath);
            pathes.Clear();
        }
        public void CalculateMinimalPath()
        {
            ReadSaveData.ReadData(readPath, ref activities);
            CalculatePathes();
            var minimalPath = FindMinimalPath();
            ReadSaveData.WriteToFile(savePath, minimalPath);
            pathes.Clear();
        }
    }
}