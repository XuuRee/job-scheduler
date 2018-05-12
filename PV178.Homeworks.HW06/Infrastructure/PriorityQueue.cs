using System;
using PV178.Homeworks.HW06.Jobs;

namespace PV178.Homeworks.HW06.Infrastructure
{
    public class PriorityQueue : IPriorityQueue
    {
        private const int AboveAverageMaxStarvationLevel = 4;
        private const int NormalMaxStarvationLevel = 8;
        private const int BelowAverageMaxStarvationLevel = 12;

        // TODO add some class members here

        public void Enqueue(BaseJob job)
        {
            // TODO

            throw new NotImplementedException();
        }

        public BaseJob Dequeue()
        {
            // TODO

            throw new NotImplementedException();
        }

        public int GetScheduledJobsCount()
        {
            // TODO

            throw new NotImplementedException();
        }
    }
}
