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
    public class ContrastJob : BaseJob
    {
        public const int DefaultContrastAdjustment = 10;

        public const int MaxContrast = 50;

        public const int MinContrast = -50;

        public int ContrastChange { get; set; }
        public string ImagePath { get; set; }

        public ContrastJob(long id) : base(id)
        {
            ContrastChange = DefaultContrastAdjustment;
            ImagePath = Paths.Image01;
        }

        public override void InitJobArguments(string input)
        {
            string[] line = input.Trim().Split();

            string path = null; int? change = null;
            Helpers.Parse(line, ref change, ref path);
            //Helpers.Parse(out change, out path, line);

            if (change.HasValue)
            {
                ContrastChange = AssignRightContrastValue(change.Value);
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

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            int parts = bytes / 5;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);
            
            double change = Math.Pow(((100 + ContrastChange) / 100.0), 2);
            progress.Report($"Starting job with id {this.Id}... 0 %");
            Parallel.For(0, rgbValues.Length, index => {
                ShowProgress(progress, index, parts);
                int contrast = Convert.ToInt32(((rgbValues[index] / 255.0 - 0.5) * change + 0.5) * 255);
                rgbValues[index] = Helpers.ConvertToByte(contrast);
            });

            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bmp.UnlockBits(bmpData);
            bmp.Save(Paths.GetOutputImageFullName(this.Id, "contrast"));

            progress.Report($"Ending job with id {this.Id}... 100 %");
            SwitchToFinishedState($"Changed contrast by {ContrastChange} point(s)");
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

        private int AssignRightContrastValue(int change)
        {
            if (change > MaxContrast)
            {
                return MaxContrast;
            }
            if (change < MinContrast)
            {
                return MinContrast;
            }
            return change;
        }

        private void ShowProgress(IProgress<string> progress, int index, int part)
        {
            if (index == part)
            {
                progress.Report("Job is in process...\t 20 %");
            }
            if (index == 2 * part)
            {
                progress.Report("Job is in process...\t 40 %");
            }
            if (index == 3 * part)
            {
                progress.Report("Job is in process...\t 60 %");
            }
            if (index == 4 * part)
            {
                progress.Report("Job is in process...\t 80 %");
            }
        }
    }
}
