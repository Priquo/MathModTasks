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

        List<Activity> activities = new List<Activity>();
        List<Path> pathes = new List<Path>();

        int FindStartingPos()
        {
            foreach (Activity activity in activities)
                if (activities.Where(x => x.eventEnd == activity.eventStart).Count() == 0) return activity.eventStart;
            return -1;
        }

        int FindEndingPos()
        {
            foreach (Activity activity in activities)
                if (activities.Where(x => x.eventStart == activity.eventEnd).Count() == 0) return activity.eventEnd;
            return -1;
        }

        void CalculatePathes()
        {
            foreach (Activity activity in activities.Where(x => x.eventStart == FindStartingPos()))
            {
                pathes.Add(new Path { path = activity.eventStart + "--" + activity.eventEnd, lastPoint = activity.eventEnd, length = activity.time });
            }
            for (int i = 0; i < pathes.Count; i++)
            {
                foreach (Activity activity in activities.Where(x => x.eventStart == pathes[i].lastPoint))
                {
                    pathes.Add(new Path { path = pathes[i].path + "--" + activity.eventEnd, lastPoint = activity.eventEnd, length = pathes[i].length + activity.time });
                }
            }
        }

        Path FindCriticalPath()
        {
            int maxLength = 0;
            foreach (Path path in pathes.Where(x => x.lastPoint == FindEndingPos()))
            {
                if (path.length > maxLength) maxLength = path.length;
            }
            Path criticalPath = pathes.Find(x => x.length == maxLength);
            return criticalPath;
        }

        Path FindMinimalPath()
        {
            int minLength = int.MaxValue;
            foreach (Path path in pathes.Where(x => x.lastPoint == FindEndingPos()))
            {
                if (path.length < minLength) minLength = path.length;
            }
            Path minimalPath = pathes.Find(x => x.length == minLength);
            return minimalPath;
        }


        public void CalculateCriticalPath()
        {
            ReadSaveData.ReadData("file.csv", ref activities);
            CalculatePathes();
            var criticalPath = FindCriticalPath();
            ReadSaveData.WriteToFile(savePath, criticalPath);
            pathes.Clear();
        }
        public void CalculateMinimalPath()
        {
            ReadSaveData.ReadData("file.csv", ref activities);
            CalculatePathes();
            var minimalPath = FindMinimalPath();
            ReadSaveData.WriteToFile(savePath, minimalPath);
            pathes.Clear();
        }
    }
}