namespace StIves.Interfaces.Actions
{
    public class Option
    {
        public Option(string prompt, PerformAction action, object target, bool isDefault = false)
        {
            Prompt = prompt;
            Action = action;
            IsDefault = isDefault;
            Target = target;
        }
        public readonly string Prompt;
        public readonly bool IsDefault;
        public readonly PerformAction Action;
        public readonly object Target;
    }

    public delegate void PerformAction(ActionRequest response, object target);
}
