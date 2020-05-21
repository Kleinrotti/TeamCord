using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using TeamCord.Core;

namespace TeamCord.Plugin
{
    public class Exports
    {
        private static Thread _settingsThread;

        private static bool Is64Bit()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8;
        }

        #region Required functions

        [DllExport]
        public static String ts3plugin_name()
        {
            return TSPlugin.Instance.PluginName;
        }

        [DllExport]
        public static String ts3plugin_version()
        {
            return TSPlugin.Instance.PluginVersion;
        }

        [DllExport]
        public static int ts3plugin_apiVersion()
        {
            return TSPlugin.Instance.ApiVersion;
        }

        [DllExport]
        public static String ts3plugin_author()
        {
            return TSPlugin.Instance.Author;
        }

        [DllExport]
        public static String ts3plugin_description()
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
            return (int)PluginConfigureOffer.PLUGIN_OFFERS_CONFIGURE_NEW_THREAD;
        }

        [DllExport]
        public static unsafe void ts3plugin_configure(void* handle, void* qParentWidget)
        {
            Action<PluginSettings> act;
            act = GetSettingsCallback;
            //ensure that only one settings window can be opened at the same time
            if (_settingsThread == null || !_settingsThread.IsAlive)
                _settingsThread = Helpers.CreateSTAWindow(act);
        }

        private static void GetSettingsCallback(PluginSettings settings)
        {
            //Exit STA thread of the window when settings are received, if its not exited no more settings window can be opened
            Dispatcher.FromThread(_settingsThread).InvokeShutdown();
            MessageBox.Show("Please restart Teamspeak to apply all changed settings.");
        }

        [DllExport]
        public static void ts3plugin_registerPluginID(String id)
        {
            var functs = (TS3Functions)TSPlugin.Instance.Functions;
            functs.printMessageToCurrentTab(id);
            TSPlugin.Instance.PluginID = id;
        }

        /* Static title shown in the left column in the info frame */

        [DllExport]
        public static unsafe String ts3plugin_infoTitle()
        {
            return "Test plugin info";
        }

        [DllExport]
        public static void ts3plugin_freeMemory(IntPtr data)
        {
            Marshal.FreeHGlobal(data);
        }

        [DllExport]
        public static void ts3plugin_currentServerConnectionChanged(ulong serverConnectionHandlerID)
        {
            var functs = TSPlugin.Instance.Functions;
            functs.printMessageToCurrentTab(serverConnectionHandlerID.ToString());
        }

        [DllExport]
        public static void ts3plugin_onConnectStatusChangeEvent(ulong serverConnectionHandlerID, ConnectStatus newStatus, Ts3ErrorType error)
        {
            //Join/leave a server
            if (newStatus == ConnectStatus.STATUS_DISCONNECTED)
                TSPlugin.Instance.ConnectionHandler.Disconnect();
        }

        [DllExport]
        public unsafe static void ts3plugin_onClientMoveEvent(ulong serverConnectionHandlerID, short clientID, ulong oldChannelID, ulong newChannelID, int visibility, char* moveMessage)
        {
            IntPtr ptr = new IntPtr();
            TSPlugin.Instance.Functions.getChannelVariableAsString(serverConnectionHandlerID, newChannelID, (uint)ChannelProperties.CHANNEL_DESCRIPTION, ref ptr);

            var description = Marshal.PtrToStringAnsi(ptr);
            if (description == null)
                return;
            var id = Helpers.ExtractChannelID(description);

            if (id == 0)
            {
                TSPlugin.Instance.ConnectionHandler.LeaveChannel();
            }
            else
            {
                if (TSPlugin.Instance.Settings.AutomaticJoin)
                {
                    TSPlugin.Instance.ConnectionHandler.JoinChannel(id);
                }
                else
                {
                    if (MessageBox.Show("Would you like to join to discord too?", "TeamCord", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        TSPlugin.Instance.ConnectionHandler.JoinChannel(id);
                    }
                }
            }

            TSPlugin.Instance.Functions.freeMemory(ptr);
        }

        [DllExport]
        public static unsafe void ts3plugin_onEditCapturedVoiceDataEvent(ulong serverConnectionHandlerID, short* samples, int sampleCount, int channels, int* edited)
        {
            TSPlugin.Instance.ConnectionHandler.VoiceData(samples, sampleCount, channels, edited);
        }

        public unsafe static char* my_strcpy(char* destination, int buffer, char* source)
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

        public static unsafe PluginMenuItem* createMenuItem(PluginMenuType type, int id, String text, String icon)
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
        public unsafe static void ts3plugin_initMenus(PluginMenuItem*** menuItems, char** menuIcon)
        {
            int x = 2;
            int sz = x + 1;
            int n = 0;

            *menuItems = (PluginMenuItem**)Marshal.AllocHGlobal(sizeof(PluginMenuItem*) * sz);
            string icon = "2.png";

            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_GLOBAL, 1, "Login", icon);
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_GLOBAL, 2, "Logout", icon);
            (*menuItems)[n++] = createMenuItem(PluginMenuType.PLUGIN_MENU_TYPE_GLOBAL, 3, "Info", icon);

            (*menuItems)[n++] = null;

            *menuIcon = (char*)Marshal.AllocHGlobal(128 * sizeof(char));

            IntPtr ptr = Marshal.StringToHGlobalAnsi("t.png");
            void* strPtr = ptr.ToPointer();
            char* cptr = (char*)strPtr;
            my_strcpy(*menuIcon, 128, cptr);
        }

        [DllExport]
        public static void ts3plugin_onMenuItemEvent(ulong serverConnectionHandlerID, PluginMenuType type, int menuItemID, ulong selectedItemID)
        {
            if (menuItemID == 1)
                TSPlugin.Instance.ConnectionHandler.Connect();
            else if(menuItemID == 2)
                TSPlugin.Instance.ConnectionHandler.Disconnect();
        }
    }
}