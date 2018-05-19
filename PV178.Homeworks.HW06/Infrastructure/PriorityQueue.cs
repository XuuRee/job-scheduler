using System;
using System.Collections.Generic;
using System.Linq;
using PV178.Homeworks.HW06.Enums;
using PV178.Homeworks.HW06.Jobs;

namespace PV178.Homeworks.HW06.Infrastructure
{
    public class PriorityQueue : IPriorityQueue
    {
        public List<BaseJob> queue = new List<BaseJob>();
        public BaseJobComparer comparer = new BaseJobComparer();

        public void Enqueue(BaseJob job)
        {
            lock (queue)    // need lock?
            { 
                queue.ForEach(x => x.IncreaseStarvationLevel());
                queue.Add(job);
                queue.Sort(comparer);
            }
        }

        public BaseJob Dequeue()
        {
            lock (queue)    // need lock?
            {
                if (GetScheduledJobsCount() == 0)
                {
                    return null;
                }
                BaseJob inTurn = queue.First();
                queue.RemoveAt(0);
                return inTurn;
            }
        }

        public int GetScheduledJobsCount()
        {
            lock (queue)    // need lock?
            {
                return queue.Count();
            }
        }

        // my method, remove from project
        public void Iterate()
        {
            foreach (BaseJob item in queue)
            {
                Console.WriteLine("ID: {0}, Priority: {1}, StarvationLevel: {2}", 
                    item.Id, item.Priority, item.StarvationLevel);
            }
        }
    }

    public class BaseJobComparer : IComparer<BaseJob>
    {
        private const int AboveAverageMaxStarvationLevel = 4;
        private const int NormalMaxStarvationLevel = 8;
        private const int BelowAverageMaxStarvationLevel = 12;
        
        private bool IsJobStarvation(BaseJob job)
        {
            if (job.Priority == JobPriority.Normal && 
                job.StarvationLevel >= NormalMaxStarvationLevel)
            {
                return true;
            }

            if (job.Priority == JobPriority.AboveAverage && 
                job.StarvationLevel >= AboveAverageMaxStarvationLevel)
            {
                return true;
            }

            if (job.Priority == JobPriority.BelowAverage && 
                job.StarvationLevel >= BelowAverageMaxStarvationLevel)
            {
                return true;
            }

            return false;
        }

        public int Compare(BaseJob first, BaseJob second)
        {
            bool firstStarvation = IsJobStarvation(first);
            bool secondStarvation = IsJobStarvation(second);

            // opacne? why?
            if (firstStarvation && secondStarvation)
            {
                return second.StarvationLevel.CompareTo(first.StarvationLevel);
            }
            if (firstStarvation && !secondStarvation)
            {
                return -1;      // opacne? hraje roli poradi enum?
            }

            if (!firstStarvation && secondStarvation)
            {
                return 1;       // opacne?
            }

            return first.Priority.CompareTo(second.Priority);
        }
    }
}
