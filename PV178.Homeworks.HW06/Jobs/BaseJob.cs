using System;
using System.Runtime.Remoting;
using System.Threading;
using PV178.Homeworks.HW06.Enums;

namespace PV178.Homeworks.HW06.Jobs
{
    /// <summary>
    /// Represents generic work unit with all related parameters
    /// </summary>
    public abstract class BaseJob
    {
        protected BaseJob(long id)
        {
            Id = id;
        }

        #region Properties

        /// <summary>
        /// Unique Identifier
        /// </summary>
        public long Id { get; }

        public static explicit operator BaseJob(ObjectHandle v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Current state of the baseJob
        /// </summary>
        public JobState State { get; private set; } = JobState.Created;

        private JobPriority priority = JobPriority.Unspecified;
        /// <summary>
        /// Priority of the baseJob
        /// </summary>
        public JobPriority Priority
        {
            get { return priority; }
            internal set
            {
                if (priority == JobPriority.Unspecified)
                {
                    priority = value;
                }
            }
        }

        /// <summary>
        /// BaseJob starvation indicator (indicates for how long the baseJob has been waiting in the queue)
        /// </summary>
        public int StarvationLevel { get; private set; } 
        
        /// <summary>
        /// More detailed information about baseJob current state.
        /// </summary>
        public string JobStatus { get; private set; } = string.Empty;

        private long executionTime = -1;
        /// <summary>
        /// Approximate job execution time
        /// </summary>
        public long ExecutionTime
        {
            get
            {
                return executionTime;
            }
            internal set
            {
                if (value > -1)
                {
                    executionTime = value;
                }
            }
        }

        #endregion

        #region StateModifiers

        /// <summary>
        /// Switches baseJob to "InProgress" state
        /// </summary>
        private void SwitchToInProgressState()
        {
            State = JobState.InProgress;
            JobStatus = $"{ToShortString()} is running...";
        }

        /// <summary>
        /// Switches baseJob to "Cancelled" state
        /// </summary>
        internal void SwitchToCancelledState()
        {
            State = JobState.Cancelled;
            JobStatus = $"{ToShortString()} was cancelled by user...";
        }

        /// <summary>
        /// Switches baseJob to "Faulted" state
        /// </summary>
        /// <param name="reason">Exception message</param>
        internal void SwitchToFaultedState(string reason = null)
        {
            State = JobState.Faulted;
            JobStatus = $"{ToShortString()} has faulted due to: {reason ?? "unknown reason"}";
        }

        /// <summary>
        /// Switches baseJob to "Finished" state
        /// </summary>
        /// <param name="result">Description of the baseJob output</param>
        protected void SwitchToFinishedState(string result)
        {
            State = JobState.Finished;
            JobStatus = result;
        }

        #endregion

        /// <summary>
        /// Initializes baseJob argument (if it has not been initialized yet)
        /// </summary>
        /// <param name="input">baseJob arguments, such as filepaths, ...</param>
        public abstract void InitJobArguments(string input);

        /// <summary>
        /// Performs the work associated with corresponding baseJob type
        /// </summary>
        /// <param name="progress">Progress reporter</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract void DoWork(IProgress<string> progress, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the baseJob
        /// </summary>
        /// <param name="progress">Progress reporter</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public void Execute(IProgress<string> progress, CancellationToken cancellationToken)
        {
            if (State != JobState.Created)
            {
                throw new InvalidOperationException("BaseJob has been already started");
            }
            SwitchToInProgressState();
            DoWork(progress, cancellationToken);
        }

        /// <summary>
        /// Increments baseJob starvation level
        /// </summary>
        public void IncreaseStarvationLevel()
        {
            StarvationLevel++;
        }

        public override string ToString()
        {
            return $"ID: {Id}, {State} {GetType().Name}, with priority: {Priority} (starvation level: {StarvationLevel})";
        }

        private string ToShortString()
        {
            return $"ID: {Id}, with priority: {Priority}";
        }
    }
}
