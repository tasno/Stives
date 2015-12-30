
namespace StIves.Interfaces.Actions
{
    public delegate bool SendRequest(ActionRequest actionRequest);
    public interface IAction
    {
        SendRequest Send { get; }
    }
}
