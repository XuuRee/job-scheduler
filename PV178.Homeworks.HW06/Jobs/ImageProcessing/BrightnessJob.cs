using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using PV178.Homeworks.HW06.Content;


namespace PV178.Homeworks.HW06.Jobs.ImageProcessing
{
    public class BrightnessJob : BaseJob
    {
        public const int DefaultBrightnessAdjustment = 30;

        public const int MaxBrightness = 100;

        public const int MinBrightness = -100;

        public int BrightnessChange { get; set; }
        public string ImagePath { get; set; }

        public BrightnessJob(long id) : base(id)
        {
            BrightnessChange = DefaultBrightnessAdjustment;
            ImagePath = Paths.Image01;
        }

        public override void InitJobArguments(string input)
        {
            string[] line = input.Trim().Split();

            string path = null; int? change = null;
            Helpers.Parse(line, ref change, ref path);      //Helpers.Parse(out change, out path, line);

            if (change.HasValue)
            {
                BrightnessChange = AssignRightBrightnessValue(change.Value);
            }
            if (path != null && File.Exists(path))
            {
                ImagePath = path;
            }
        }

        protected override void DoWork(IProgress<string> progress, CancellationToken cancellationToken)
        {
            Bitmap bmp = new Bitmap(ImagePath); 
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            int parts = Environment.ProcessorCount;
            List<Task> tasks = new List<Task>();
            Tuple<int, int>[] limits = new Tuple<int, int>[parts];

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

            progress.Report($"Starting job with id {this.Id}... 0 %");
            int percent_progress = 20;
            InitTupleLimits(limits, bytes, parts);

            for (int i = 0; i < limits.Length; i++)
            {
                int j = i;      // dont need
                int start = limits[j].Item1; int end = limits[j].Item2;
                tasks.Add(Task.Run(() => {
                    for (int counter = start; counter < end; counter += 1)
                    {
                        int brightness = rgbValues[counter] + BrightnessChange;
                        rgbValues[counter] = Helpers.ConvertToByte(brightness);
                    }
                }).ContinueWith(x => {
                    progress.Report($"Job is in process...\t {percent_progress} %");
                    percent_progress += 20;
                }));
            }
            Task.WaitAll(tasks.ToArray());

            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bmp.UnlockBits(bmpData);
            bmp.Save(Paths.GetOutputImageFullName(this.Id, "brightness"));

            progress.Report($"Ending job with id {this.Id}... 100 %");
            SwitchToFinishedState($"Changed brightness by {BrightnessChange} point(s)");
        }

        private void InitTupleLimits(Tuple<int, int>[] limits, int bytes, int parts)
        {
            int onePart = bytes / parts;
            int start = 0; int end = onePart;
            for (int i = 0; i < parts; i++)
            {
                limits[i] = Tuple.Create(start, end);
                start = end;
                if (end + onePart < bytes)
                {
                    end += onePart;
                }
                else
                {
                    end = bytes - 1;
                }
            }
        }

        /*
        private void Parse(out int? change, out string path, string[] line)
        {
            int result;
            path = null; change = null;
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
        */
        private int AssignRightBrightnessValue(int change)
        {
            if (change > MaxBrightness)
            {
                return MaxBrightness;
            }
            if (change < MinBrightness)
            {
                return MinBrightness;
            }
            return change;
        }
    }
}
