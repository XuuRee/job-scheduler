namespace PV178.Homeworks.HW06.Utils.Jobs
{
    /// <summary>
    /// Simple class for job ID assigning
    /// </summary>
    public static class JobIdAssigner
    {
        private static long idCounter;

        /// <summary>
        /// Assigns unique identifier to job
        /// </summary>
        /// <returns></returns>
        public static long AssignId()
        {
            idCounter++;
            return idCounter;
        }

        /// <summary>
        /// Assigns unique identifier to job
        /// </summary>
        /// <returns></returns>
        public static void Reset()
        {
            idCounter = 0;
        }
    }
}
