using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV178.Homeworks.HW06.Jobs
{
    public static class Helpers
    {
        public static byte ConvertToByte(int contrast)
        {
            if (contrast > 255)
            {
                return 255;
            }
            if (contrast < 0)
            {
                return 0;
            }
            return Convert.ToByte(contrast);
        }

        public static void ShowProgress(IProgress<string> progress, int index, int part)
        {
            if (index == part)  // float, double?
            {
                progress.Report("Job is in process...\t 25 %");
            }
            if (index == 2 * part)
            {
                progress.Report("Job is in process...\t 50 %");
            }
            if (index == 3 * part)
            {
                progress.Report("Job is in process...\t 75 %");
            }
        }
    }
}
