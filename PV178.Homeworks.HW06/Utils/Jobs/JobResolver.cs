using System;
using PV178.Homeworks.HW06.Enums;
using PV178.Homeworks.HW06.Jobs;

namespace PV178.Homeworks.HW06.Utils.Jobs
{
    /// <summary>
    /// Simple class which handles baseJob resolving
    /// </summary>
    public static class JobResolver
    {
        /// <summary>
        /// Creates job instance according to given jobType (by using JobBuilder)
        /// </summary>
        /// <param name="jobType">Type of the created job</param>
        /// <param name="priority">Priority of created job</param>
        /// <param name="customArguments">Arguments for created job</param>
        /// <returns>Created (and initialized) job instance</returns>
        public static BaseJob Resolve(JobType jobType, JobPriority priority = JobPriority.Normal, string customArguments = "")
        {
            // jobType recognize jobtype here in resolve function.
            JobBuilder builder = new JobBuilder(jobType);
            builder.SetPriority(priority);
            builder.SetJobArguments(customArguments);
            return builder.GetResult();
        }
    }
}