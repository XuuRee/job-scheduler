using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PV178.Homeworks.HW06.Jobs;
using PV178.Homeworks.HW06.Enums;

namespace PV178.Homeworks.HW06.Utils.Jobs
{
    public interface IJobBuilder
    {
        void SetJobArguments(string input);
        void SetPriority(JobPriority priority);
        BaseJob GetResult();
    }
}
