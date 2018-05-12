using System;
using System.Threading.Tasks;
using PV178.Homeworks.HW06.Jobs;

namespace PV178.Homeworks.HW06.Infrastructure
{
    /// <summary>
    /// Responsible for baseJob execution, note that only single job can be executed at the time
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// Decides, whether new job can be started right now
        /// </summary>
        /// <returns>True if new baseJob can be started</returns>
        bool CanStartNewJob();

        /// <summary>
        /// Executes given job
        /// </summary>
        /// <param name="baseJob">Job to execute</param>
        Task ExecuteJob(BaseJob baseJob);

        /// <summary>
        /// Cancels currently running job
        /// </summary>
        void CancelCurrentJob();

        /// <summary>
        /// Signals job completition
        /// </summary>
        event EventHandler<BaseJob> JobDone;
    }
}