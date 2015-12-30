using System.ComponentModel;

namespace StIves.Interfaces.Actions
{
    public enum ActionRequestType
    {
        [Description("Which lady will you add to your fiancee list?")]
        Fiancee,
        [Description("Where shall {0} work today?")]
        WorkAssignment,
        Billet,
        [Description("Where is the cart going?")]
        Location,
        ShowInnPrices,
        DrawEvent,
    }
}