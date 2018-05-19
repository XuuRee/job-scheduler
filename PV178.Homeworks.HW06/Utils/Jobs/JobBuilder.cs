using PV178.Homeworks.HW06.Jobs.ImageProcessing;
using PV178.Homeworks.HW06.Jobs;
using PV178.Homeworks.HW06.Enums;
using System;
using System.Reflection;
using System.Threading;
using System.Linq;

namespace PV178.Homeworks.HW06.Utils.Jobs
{
    /// <summary>
    /// Base class for building jobs of various types
    /// </summary>
    public class JobBuilder : IJobBuilder
    {
        public BaseJob _job { get; set; }

        public JobBuilder(JobType type)
        {
            long id = JobIdAssigner.AssignId();
            string name = type.ToString() + "Job";
            _job = CreateInstance(name, id);
        }

        public void SetJobArguments(string input)
        {
            _job.InitJobArguments(input);
        }

        public void SetPriority(JobPriority priority)
        {
            _job.Priority = priority;
        }

        public BaseJob GetResult()
        {
            return _job;
        }

        private static BaseJob CreateInstance(string className, long id)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .First(t => t.Name == className);
            
            return (BaseJob) Activator.CreateInstance(type, id);
        }
    }
}
