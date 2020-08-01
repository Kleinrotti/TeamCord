using System;
using TeamCord.Core;

namespace TeamCord.Plugin
{
    public static class MenuItems
    {
        /// <summary>
        /// Global
        /// </summary>
        [MenuItemConnectionAttibute(ConnectionType.Discord)]
        public const int MenuItemLogin = 1;

        /// <summary>
        /// Global
        /// </summary>
        [MenuItemConnectionAttibute(ConnectionType.Discord)]
        public const int MenuItemLogout = 2;

        /// <summary>
        /// Global
        /// </summary>
        public const int MenuItemAbout = 3;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnectionAttibute(ConnectionType.Voice)]
        public const int MenuItemJoin = 4;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnectionAttibute(ConnectionType.Voice)]
        public const int MenuItemLeave = 5;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnectionAttibute(ConnectionType.Voice)]
        public const int MenuItemLink = 6;

        /// <summary>
        /// Channel
        /// </summary>
        [MenuItemConnectionAttibute(ConnectionType.Voice)]
        public const int MenuItemConnectionInfo = 7;
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class MenuItemConnectionAttibute : Attribute
    {
        private ConnectionType _connectionType;

        public MenuItemConnectionAttibute(ConnectionType connectionType)
        {
            _connectionType = connectionType;
        }

        public virtual ConnectionType ConnectionType
        {
            get { return _connectionType; }
        }
    }
}