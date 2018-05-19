using System;
using PV178.Homeworks.HW06.Content;
using PV178.Homeworks.HW06.Enums;
using PV178.Homeworks.HW06.Jobs;
using PV178.Homeworks.HW06.Utils.Output;
using PV178.Homeworks.HW06.Utils.Jobs;
using PV178.Homeworks.HW06.Infrastructure;
using System.IO;
using System.Collections.Generic;

namespace PV178.Homeworks.HW06.Utils.Input
{
    public static class CommandProcessor
    {
        /// <summary>
        /// Schedules job with given name. Note that both priority and argument
        /// are optional but must be given in the right order.
        /// 
        /// Usage: "schedule {jobName} {argument(s)} {jobPriority}"
        /// 
        /// Actual examples:
        /// "schedule contrast 10 AboveAverage" -> schedules contrast increase by 10 points with AboveAverage priority
        /// "schedule contrast -10" -> schedules contrast decrease by 10 points with default (Normal) priority
        /// </summary>
        private const string ScheduleJobCommand = "schedule";

        /// <summary>
        /// Schedules all jobs within given file name with given name.
        /// 
        /// Usage: "batch-schedule {filename}"
        /// 
        /// Actual examples:
        /// "batch-schedule jobs1" -> schedules all jobs within file jobs1.txt
        /// </summary>
        private const string BatchScheduleJobCommand = "batch-schedule";

        /// <summary>
        /// Cancels currently running baseJob.
        /// Usage: "cancel"
        /// </summary>
        private const string CancelJobCommand = "cancel";

        /// <summary>
        /// Lists all available jobs.
        /// Usage: "list"
        /// </summary>
        private const string ListAllJobsCommand = "list";

        /// <summary>
        /// Lists all available commands.
        /// Usage: "help"
        /// </summary>
        private const string HelpCommand = "help";

        /// <summary>
        /// Terminates the program.
        /// Usage: "exit"
        /// </summary>
        private const string ExitCommand = "exit";

        /// <summary>
        /// Analyzes user input
        /// </summary>
        /// <param name="input">user input</param>
        public static void AnalyzeInput(string input)
        {
            ConsoleHelper.EraseTypeInText();

            if (string.IsNullOrEmpty(input))
            {
                return;
            }
            var lowerCaseInput = input.ToLower();

            if (lowerCaseInput.Contains(ListAllJobsCommand))
            {
                ProcessListAllJobsCommand();
                ConsoleHelper.PrintTypeInCommand();
                return;
            }
            if (lowerCaseInput.Contains(BatchScheduleJobCommand))
            {
                ProcessBatchScheduleCommand(lowerCaseInput);
                ConsoleHelper.PrintTypeInCommand();
                return;
            }
            if (lowerCaseInput.Contains(ScheduleJobCommand))
            {
                ProcessScheduleCommand(lowerCaseInput);
                ConsoleHelper.PrintTypeInCommand();
                return;
            }

            if (lowerCaseInput.Contains(CancelJobCommand))
            {
                ProcessCancelCommand();
                ConsoleHelper.PrintTypeInCommand();
                return;
            }
            if (lowerCaseInput.Contains(HelpCommand))
            {
                ProcessHelpCommand();
                ConsoleHelper.PrintTypeInCommand();
                return;
            }
            if (lowerCaseInput.Contains(ExitCommand))
            {
                Environment.Exit(0);
            }
            Console.WriteLine($"Command '{input}' was not recognized, type 'help' to see valid commands." + Environment.NewLine);
        }

        /// <summary>
        /// Writes all available jobs
        /// </summary>
        private static void ProcessListAllJobsCommand()
        {
            Console.WriteLine("Available jobs:");
            foreach (var jobName in Enum.GetNames(typeof(JobType)))
            {
                Console.WriteLine(jobName);
            }
            ConsoleHelper.PrintTypeInCommand();
        }

        /// <summary>
        /// Writes all available commands
        /// </summary>
        private static void ProcessHelpCommand()
        {
            Console.WriteLine("Supported commands: (command - description)" + Environment.NewLine);

            Console.WriteLine("'schedule {jobName} {argument} {jobPriority}' - Schedules baseJob, both argument and priority are optional."
                              + Environment.NewLine + "Example: 'schedule contrast 25 AboveAverage'" + Environment.NewLine);
            Console.WriteLine("'batch-schedule {filename}' - Schedules all jobs within given file name with given name."
                              + Environment.NewLine + "Example: 'batch-schedule jobs1'" + Environment.NewLine);
            Console.WriteLine("'list' - Lists all available jobs.");
            Console.WriteLine("'cancel' - Cancels currently running baseJob.");
            Console.WriteLine("'exit' - Terminates the program.");
        }

        /// <summary>
        /// Cancels currently running baseJob
        /// </summary>
        private static void ProcessCancelCommand()
        {
            //Vyvolejte statickou metodu "ImportantPrivateStaticVoidMethod" tridy Customer
            throw new NotImplementedException();
        }

        /// <summary>
        /// Schedules multiple jobs extracted from file given via user input 
        /// (in case of incorrect format, error message should be displyed in console)
        /// </summary>
        public static void ProcessBatchScheduleCommand(string lowerCaseInput)
        {
            string[] commandLine = lowerCaseInput.Split();
            int length = commandLine.Length;

            //try
            //{
                if (CheckInputData(commandLine, length))
                {
                    using (StreamReader file = new StreamReader(Paths.BatchProcessJob(commandLine[1])))
                    {
                        List<BaseJob> jobList = new List<BaseJob>();
                        string line;

                        while ((line = file.ReadLine()) != null)
                        {                   
                            BaseJob job = ProcessScheduleCommand(line, false);

                            // what happens, if one of the items is not correct?
                            if (job != null)
                            {
                                jobList.Add(job);
                            }
                        }
                        JobScheduler.ScheduleJobs(jobList.ToArray());
                    }
                }
            //}
            //catch (Exception)   // better exception handler
            //{
                //Console.WriteLine("File not found.");
            //}
        }

        private static bool CheckInputData(string[] commandLine, int length)
        {
            if (length != 2)
            {
                Console.WriteLine("Not correct command.");
                return false;
            }

            if (!commandLine[0].ToLower().Contains(BatchScheduleJobCommand))
            {
                Console.WriteLine($"Command {BatchScheduleJobCommand} not present.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Schedules baseJob extracted from user input 
        /// (in case of incorrect format, error message should be displyed in console)
        /// </summary>
        /// <param name="input">user input</param>
        /// <param name="scheduleJob">Perform schedule for parsed job</param>
        /// <returns>Parsed job from user input</returns>
        public static BaseJob ProcessScheduleCommand(string input, bool scheduleJob = true)
        {
            BaseJob processedJob = null;
            string[] line = input.Trim().Split();      // command line
            int length = line.Length;

            if (!line[0].ToLower().Contains(ScheduleJobCommand))
            {
                Console.WriteLine($"Command {ScheduleJobCommand} not present.");
                return null;
            }

            JobType jobType;
            if (!Enum.TryParse(line[1], true, out jobType))
            {
                Console.WriteLine("Bad jobtype.");
                return null;
            }
            
            processedJob = InstanceJob(jobType, line, length);
            if (scheduleJob)
            {
                JobScheduler.ScheduleJobs(new BaseJob[] { processedJob });
            }

            return processedJob;
        }

        private static string ConcatArguments(string[] arguments, int length)
        {
            string result = "";

            for (int i = 2; i < length; i++)
            {
                result += " " + arguments[i];
            }

            return result;
        }

        private static BaseJob InstanceJob(JobType jobType, string[] line, int length)
        {
            if (length > 2)
            {
                string last = line[length - 1];
                JobPriority jobPriority = JobPriority.Normal;
                if (!Int32.TryParse(last, out int digit) && Enum.TryParse(last, true, out jobPriority))     // bad behavior, reflection?
                {
                    length -= 1;
                }
                string arguments = ConcatArguments(line, length);
                return JobResolver.Resolve(jobType, jobPriority, arguments);
            }
            return JobResolver.Resolve(jobType);
        }
    }
}
