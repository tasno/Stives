using System.ComponentModel;

namespace StIves.Interfaces
{
    public enum PhaseType
    {
        [Description("The game has not started")]
        NotStarted,
        Morning,
        Day,
        Evening,
        Night
    }
}
