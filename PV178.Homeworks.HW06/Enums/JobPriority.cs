namespace PV178.Homeworks.HW06.Enums
{
    /// <summary>
    /// Defines baseJob priority (aboveAverage has highest priority, belowAverage lowest)
    /// </summary>
    public enum JobPriority
    {
        Unspecified = 0,    // the order for this value is not defined, therefore jobs can't be scheduled with it
        AboveAverage = 1,
        Normal = 2,    
        BelowAverage = 3
    }
}
