using PV178.Homeworks.HW06.Jobs;

namespace PV178.Homeworks.HW06.Infrastructure
{
    /// <summary>
    /// Priority wise queue for scheduling jobs
    /// </summary>
    public interface IPriorityQueue
    {
        /// <summary>
        /// Adds baseJob to queue
        /// </summary>
        /// <param name="baseJob">BaseJob to enqueue</param>
        void Enqueue(BaseJob baseJob);

        /// <summary>
        /// Picks next baseJob from the queue
        /// </summary>
        /// <returns>Next baseJob if queue is not empty, otherwise returns null</returns>
        BaseJob Dequeue();

        /// <summary>
        /// Gets number of currently scheduled jobs within the queue
        /// </summary>
        /// <returns>Number of currently scheduled jobs</returns>
        int GetScheduledJobsCount();
    }
}