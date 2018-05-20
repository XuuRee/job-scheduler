using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV178.Homeworks.HW06.Jobs
{
    public static class Helpers
    {
        public static byte ConvertToByte(int subpixel)
        {
            if (subpixel > 255)
            {
                return 255;
            }
            if (subpixel < 0)
            {
                return 0;
            }
            return Convert.ToByte(subpixel);
        }

        //public static void Parse(string[] line, out int? change, out string path)

        public static void Parse(string[] line, ref int? change, ref string path)
        {
            int result;
            //path = null; change = null;
            foreach (var item in line)
            {
                if (int.TryParse(item, out result))
                {
                    change = result;
                }
                else
                {
                    path = item;
                }
            }
        }
    }
}
