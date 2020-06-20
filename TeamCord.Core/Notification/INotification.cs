namespace TeamCord.Core
{
    public interface INotification
    {
        string Message { get; }
        string Title { get; }

        void Notify();
    }
}