using System;
using TeamCord.Core;

namespace TeamCord.Plugin
{
    public static class MenuItems
    {
        /// <summary>
        /// Global
        /// </summary>
        [MenuItemConnection(ConnectionType.Discord)]
        public const int MenuItemConnect = 1;

        /// <summary>
        /// Global
        /// </summary>
        [MenuItemConnection(ConnectionType.Discord)]
        public const int MenuItemDisconnect = 2;

        /// <summary>
        /// Global
        /// </summary>
        public const int MenuItemAbout = 3;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnection(ConnectionType.Voice)]
        public const int MenuItemJoin = 4;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnection(ConnectionType.Voice)]
        public const int MenuItemLeave = 5;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnection(ConnectionType.Voice)]
        public const int MenuItemLink = 6;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnection(ConnectionType.Voice)]
        public const int MenuItemConnectionInfo = 7;
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class MenuItemConnectionAttribute : Attribute
    {
        private ConnectionType _connectionType;

        public MenuItemConnectionAttribute(ConnectionType connectionType)
        {
            _connectionType = connectionType;
        }

        public virtual ConnectionType ConnectionType
        {
            get { return _connectionType; }
        }
    }
}