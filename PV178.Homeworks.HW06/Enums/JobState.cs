namespace PV178.Homeworks.HW06.Enums
{
    /// <summary>
    /// Defines job states
    /// </summary>
    public enum JobState
    {
        Created = 0,
        InProgress = 1,
        Finished = 2,   // baseJob has succesfully finished
        Cancelled = 3,
        Faulted = 4     // an exception was encountered while performing the baseJob
    }
}
