using System;
using PV178.Homeworks.HW06.Enums;
using PV178.Homeworks.HW06.Jobs;
using PV178.Homeworks.HW06.Utils.Output;

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
            // TODO...

            throw new NotImplementedException();
        }

        /// <summary>
        /// Schedules multiple jobs extracted from file given via user input 
        /// (in case of incorrect format, error message should be displyed in console)
        /// </summary>
        public static void ProcessBatchScheduleCommand(string lowerCaseInput)
        {
            // TODO...

            throw new NotImplementedException();
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
            // TODO...

            throw new NotImplementedException();
        }
    }
}
