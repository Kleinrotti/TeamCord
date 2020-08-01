using System;

namespace TeamCord.Core
{
    public class ConnectionChangedEventArgs : EventArgs
    {
        public virtual ConnectionType ConnectionType { get; protected set; }
        public virtual bool Connected { get; protected set; }

        public ConnectionChangedEventArgs(ConnectionType type, bool connected)
        {
            ConnectionType = type;
            Connected = connected;
        }
    }
}