namespace StIves.Interfaces.Actions
{
    public interface IEvent
    {
        string Name { get; }
        string Prompt { get; }
        ActionRequest GetActionRequest(IPlayer player);
    }
}