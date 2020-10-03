using System;

namespace TeamCord.Core
{
    public interface INotification
    {
        string Message { get; }
        string Title { get; }
        Action BalloonClick { get; }

        void Notify();
    }
}