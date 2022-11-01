using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TeamCord.Plugin.Natives;

namespace TeamCord.Plugin
{
    internal sealed class Exports
    {
        private static bool Is64Bit()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8;
        }

        #region Required functions

        [DllExport]
        public static string ts3plugin_name()
        {
            return TSPlugin.Instance.PluginName;
        }

        [DllExport]
        public static string ts3plugin_version()
        {
            return TSPlugin.Instance.PluginVersion;
        }

        [DllExport]
        public static int ts3plugin_apiVersion()
        {
            return TSPlugin.Instance.ApiVersion;
        }

        [DllExport]
        public static string ts3plugin_author()
        {
            return TSPlugin.Instance.Author;
        }

        [DllExport]
        public static string ts3plugin_description()
        {
            return TSPlugin.Instance.Description;
        }

        [DllExport]
        public static void ts3plugin_setFunctionPointers(TS3Functions funcs)
        {
            TSPlugin.Instance.Functions = funcs;
        }

        [DllExport]
        public static int ts3plugin_init()
        {
            return TSPlugin.Instance.Init();
        }

        [DllExport]
        public static void ts3plugin_shutdown()
        {
            TSPlugin.Instance.Shutdown();
        }

        #endregion Required functions

        [DllExport]
        public static int ts3plugin_offersConfigure()
        {
            return (int)PluginConfigureOffer.PLUGIN_OFFERS_CONFIGURE_QT_THREAD;
        }

        [DllExport]
        public static int ts3plugin_requestAutoload()
        {
            return 1;
        }

        [DllExport]
        public static unsafe void ts3plugin_configure(void* handle, void* qParentWidget)
        {
            TSPlugin.Instance.OpenSettings();
        }

        [DllExport]
        public static void ts3plugin_registerPluginID(string id)
        {
            TSPlugin.Instance.PluginID = id;
        }

        [DllExport]
        public static void ts3plugin_freeMemory(IntPtr data)
        {
            Marshal.FreeHGlobal(data);
        }

        [DllExport]
        public static void ts3plugin_onConnectStatusChangeEvent(ulong serverConnectionHandlerID, ConnectStatus newStatus, Ts3ErrorType error)
        {
            //Join/leave a server
            if (newStatus == ConnectStatus.STATUS_DISCONNECTED)
                TSPlugin.Instance.ConnectionHandler?.Disconnect();
            else if (newStatus == ConnectStatus.STATUS_CONNECTION_ESTABLISHED)
            {
                TSPlugin.Instance.Ts3ServerChanged(serverConnectionHandlerID);
                if (TSPlugin.Instance.Settings.DiscordAutoLogin)
                    TSPlugin.Instance.ConnectionHandler?.Connect();
            }
        }

        [DllExport]
        public static void ts3plugin_onClientMoveEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID,
            int visibility, [MarshalAs(UnmanagedType.LPStr)] string moveMessage)
        {
            TSPlugin.Instance.Ts3ChannelChanged(serverConnectionHandlerID, clientID, newChannelID);
        }

        [DllExport]
        public static unsafe void ts3plugin_onEditCapturedVoiceDataEvent(ulong serverConnectionHandlerID,
            short* samples, int sampleCount, int channels, int* edited)
        {
            if (TSPlugin.Instance.Settings.UseTeamspeakVoiceActivation)
            {
                //only process voice data if teamspeak would send it
                if (*edited == 2)
                {
                    TSPlugin.Instance.ConnectionHandler?.ProcessVoiceData(samples, sampleCount, channels);
                }
            }
            else
            {
                TSPlugin.Instance.ConnectionHandler?.ProcessVoiceData(samples, sampleCount, channels);
            }
        }

        [DllExport]
        public static void ts3plugin_onClientSelfVariableUpdateEvent(ulong serverConnectionHandlerID, int flag, [MarshalAs(UnmanagedType.LPStr)] string oldValue,
            [MarshalAs(UnmanagedType.LPStr)] string newValue)
        {
            if (flag == (int)ClientProperties.CLIENT_OUTPUT_MUTED)
            {
                var val = Convert.ToInt32(newValue);
                TSPlugin.Instance.Deaf(val != 0);
            }
            else if (flag == (int)ClientProperties.CLIENT_INPUT_MUTED)
            {
                var val = Convert.ToInt32(newValue);
                TSPlugin.Instance.Mute(val != 0);
            }
        }

        private static unsafe char* my_strcpy(char* destination, int buffer, char* source)
        {
            char* p = destination;
            int x = 0;
            while (*source != '\0' && x < buffer)
            {
                *p++ = *source++;
                x++;
            }
            *p = '\0';
            return destination;
        }

        private static byte[] convertLPSTR(string _string)
        {
            List<byte> lpstr = new List<byte>();
            foreach (char c in _string.ToCharArray())
            {
                lpstr.Add(Convert.ToByte(c));
            }
            lpstr.Add(Convert.ToByte('\0'));

            return lpstr.ToArray();
        }

        public static unsafe PluginMenuItem* createMenuItem(PluginMenuType type, int id, string text, string icon)
        {
            PluginMenuItem* menuItem = (PluginMenuItem*)Marshal.AllocHGlobal(sizeof(PluginMenuItem));
            menuItem->type = type;
            menuItem->id = id;

            IntPtr i_ptr = Marshal.StringToHGlobalAnsi(icon);
            void* i_strPtr = i_ptr.ToPointer();
            char* i_cptr = (char*)i_strPtr;
            *menuItem->icon = *my_strcpy(menuItem->icon, 128, i_cptr);

            IntPtr t_ptr = Marshal.StringToHGlobalAnsi(text);
            void* t_strPtr = t_ptr.ToPointer();
            char* t_cptr = (char*)t_strPtr;
            my_strcpy(menuItem->text, 128, t_cptr);

            return menuItem;
        }

        [DllExport]
        public static unsafe void ts3plugin_initMenus(PluginMenuItem*** menuItems, char** menuIcon)
        {
            int menuItemCount = 8;
            int n = 0;

            *menuItems = (PluginMenuItem**)Marshal.AllocHGlobal(sizeof(PluginMenuItem*) * menuItemCount);

            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_GLOBAL, MenuItems.MenuItemAbout, "About", "");
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_CHANNEL, MenuItems.MenuItemJoin, "Join", "");
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_CHANNEL, MenuItems.MenuItemLeave, "Leave", "");
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_CHANNEL, MenuItems.MenuItemLink, "Link to channel", "");
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_GLOBAL, MenuItems.MenuItemConnect, "Connect", "");
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_GLOBAL, MenuItems.MenuItemDisconnect, "Disconnect", "");
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_CHANNEL, MenuItems.MenuItemConnectionInfo, "Connection info", "");

            (*menuItems)[n++] = null;

            *menuIcon = (char*)Marshal.AllocHGlobal(256 * sizeof(char));

            IntPtr ptr = Marshal.StringToHGlobalAnsi("logo.png");
            void* strPtr = ptr.ToPointer();
            char* cptr = (char*)strPtr;
            my_strcpy(*menuIcon, 256, cptr);
        }

        [DllExport]
        public static void ts3plugin_onMenuItemEvent(ulong serverConnectionHandlerID, PluginMenuType type, int menuItemID, ulong selectedItemID)
        {
            switch (menuItemID)
            {
                case MenuItems.MenuItemJoin:
                    {
                        string description;
                        TSPlugin.Instance.Functions.getChannelVariableAsString(serverConnectionHandlerID, selectedItemID, ChannelProperties.CHANNEL_DESCRIPTION, out description);
                        var channelid = Helpers.ExtractChannelID(description);
                        if (channelid != 0)
                            TSPlugin.Instance.ConnectionHandler.JoinChannel(channelid);
                        break;
                    }

                case MenuItems.MenuItemLeave:
                    TSPlugin.Instance.ConnectionHandler.LeaveChannel();
                    break;

                case MenuItems.MenuItemLink:
                    TSPlugin.Instance.LinkDiscordChannel(serverConnectionHandlerID, selectedItemID);
                    break;

                case MenuItems.MenuItemConnect:
                    TSPlugin.Instance.ConnectionHandler.Connect();
                    break;

                case MenuItems.MenuItemDisconnect:
                    TSPlugin.Instance.ConnectionHandler.Disconnect();
                    break;

                case MenuItems.MenuItemConnectionInfo:
                    TSPlugin.Instance.ShowConnectionInfo();
                    break;

                case MenuItems.MenuItemAbout:
                    TSPlugin.Instance.ShowAboutWindow();
                    break;
            }
        }

        [DllExport]
        public static IntPtr ts3plugin_infoTitle()
        {
            return Marshal.StringToHGlobalAnsi("TeamCord");
        }

        [DllExport]
        public static void ts3plugin_infoData(ulong serverConnectionHandlerID, ulong id, PluginItemType type, [MarshalAs(UnmanagedType.LPStr)] ref string data)
        {
            switch (type)
            {
                case PluginItemType.PLUGIN_CHANNEL:
                    if (TSPlugin.Instance.ConnectionHandler == null)
                        return;
                    data = TSPlugin.Instance.GetDiscordUserlistAsTs3String(serverConnectionHandlerID, id);
                    break;

                case PluginItemType.PLUGIN_CLIENT:
                    break;

                case PluginItemType.PLUGIN_SERVER:
                    break;

                default:
                    Console.WriteLine("Invalid item type: %d\n", type);
                    data = null;  /* Ignore */
                    return;
            }
        }

        [DllExport]
        public static void ts3plugin_onServerUpdatedEvent(ulong serverConnectionHandlerID)
        {
            //used as work around to update channel info of plugin because of memory violation when called outside of an export function
            TSPlugin.Instance.Functions.requestInfoUpdate(serverConnectionHandlerID, PluginItemType.PLUGIN_CHANNEL, TSPlugin.Instance.CurrentChannel);
        }
    }
}