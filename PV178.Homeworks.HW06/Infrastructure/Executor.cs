using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PV178.Homeworks.HW06.Jobs;

namespace PV178.Homeworks.HW06.Infrastructure
{
    public delegate void Delegate(object sender, BaseJob job);

    public class Executor : IExecutor
    {
        public event EventHandler<BaseJob> JobDone;
        public static bool IsExecutorFree { get; set; }

        Action<String> print = (message) => Debug.WriteLine(message);
        public Stopwatch StopWatch { get; set; }

        public BaseJob ProcessJob { get; set; }

        public Executor()
        {
            StopWatch = new Stopwatch();
        }

        public void CancelCurrentJob()
        {
            StopWatch.Stop();
            ProcessJob.ExecutionTime = StopWatch.Elapsed.Ticks;
            ProcessJob.SwitchToCancelledState();
            ProcessJob = null;
        }

        public bool CanStartNewJob()
        {
            return ProcessJob == null ? true : false;
        }
        
        public Task ExecuteJob(BaseJob baseJob)                         //async
        {
            ProcessJob = baseJob;
            StopWatch.Start();
            Progress<string> progress = new Progress<string>(print);
            Task task = Task.Run(() => {                                //await
                 baseJob.Execute(progress, CancellationToken.None);
            });
            task.Wait();
            StopWatch.Stop();
            ProcessJob.ExecutionTime = StopWatch.Elapsed.Ticks;
            JobDone?.Invoke(this, ProcessJob);
            ProcessJob = null;
            return task;
        }
        
    }
}
