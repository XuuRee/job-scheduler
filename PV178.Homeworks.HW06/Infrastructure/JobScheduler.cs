using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PV178.Homeworks.HW06.Enums;
using PV178.Homeworks.HW06.Jobs;
using PV178.Homeworks.HW06.Utils.Output;

namespace PV178.Homeworks.HW06.Infrastructure
{
    /// <summary>
    /// Responsible for scheduling jobs
    /// </summary>
    public static class JobScheduler
    {
        /// <summary>
        /// Priority wise job queue
        /// </summary>
        private static readonly IPriorityQueue PriorityQueue = new PriorityQueue();

        /// <summary>
        /// Job executor
        /// </summary>
        private static readonly IExecutor Executor;

        static JobScheduler()
        {
            Executor = new Executor();
            Executor.JobDone += Executor_JobDone;
            AppDomain.CurrentDomain.ProcessExit += ProcessExit;
        }

        /// <summary>
        /// De facto static class destructor used for closing log writer
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private static void ProcessExit(object sender, EventArgs e)
        {
            Executor.JobDone -= Executor_JobDone;
            AppDomain.CurrentDomain.ProcessExit -= ProcessExit;
            LogHelper.CloseLogWriter();
        }

        private static void Executor_JobDone(object sender, BaseJob job)
        {
            if (job.State == JobState.Created || job.State == JobState.InProgress)
            {
                throw new InvalidOperationException("Job is not completed yet");
            }
            var log = $"{job.State} job: {job.JobStatus ?? string.Empty}, ID: {job.Id}";
            var executionTime = $" in {job.ExecutionTime} ms.";
            Debug.WriteLine(log + executionTime + Environment.NewLine);
            LogHelper.WriteLog(log);
            
            // TODO perform some operation/s here
        }

        /// <summary>
        /// Schedules given job within PriorityQueue
        /// </summary>
        /// <param name="jobs">Jobs to schedule</param>
        public static void ScheduleJobs(params BaseJob[] jobs)
        {
            foreach (var job in jobs)
            {
                var log = $"Scheduling {job?.GetType()?.Name?.Replace("Job", string.Empty)} job (ID: {job.Id}) with {job.Priority} priority.";
                Debug.WriteLine(log);
                LogHelper.WriteLog(log);
                PriorityQueue.Enqueue(job);
            }
            
            // TODO perform some operation/s here
            while (!AllJobsHaveFinished())
            {
                if (Executor.CanStartNewJob())
                {
                    var job = PriorityQueue.Dequeue();
                    Executor.ExecuteJob(job);
                }
            }
        }

        /// <summary>
        /// Cancels currently running job
        /// </summary>
        public static void CancelCurrentJob()
        {
            Executor.CancelCurrentJob();
        }

        /// <summary>
        /// Check if all scheduled jobs have finished
        /// </summary>
        /// <returns>True if all scheduled jobs have finished, otherwise false</returns>
        public static bool AllJobsHaveFinished()
        {
            return PriorityQueue.GetScheduledJobsCount() == 0 ? true : false;
        }
    }
}
