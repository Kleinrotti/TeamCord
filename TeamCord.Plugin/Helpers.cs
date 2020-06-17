using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;
using TeamCord.GUI;

namespace TeamCord.Plugin
{
    public static class Helpers
    {
        public static Thread CreateSTAWindow<T>(Action<T> callback)
        {
            ThreadStart threadStart = new ThreadStart(CreateWindow);
            // create a thread
            Thread newWindowThread = new Thread(threadStart);

            void CreateWindow()
            {
                SettingsWindow window = new SettingsWindow(TSPlugin.Instance.Settings);
                window.Callback = ConvertAct(callback);
                window.Show();

                // start the Dispatcher processing
                Dispatcher.Run();
            }

            // set the apartment state
            newWindowThread.SetApartmentState(ApartmentState.STA);

            // make the thread a background thread
            newWindowThread.IsBackground = true;
            newWindowThread.Name = "SettingsWindow";

            // start the thread
            newWindowThread.Start();
            return newWindowThread;
        }

        private static Action<object> ConvertAct<T>(Action<T> myActionT)
        {
            if (myActionT == null) return null;
            else return new Action<object>(o => myActionT((T)o));
        }

        public static ulong ExtractChannelID(string channelDescription)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<TS3ChannelJson>(channelDescription);

                return Convert.ToUInt64(obj.Teamcord.ChannelID);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string UserListToTs3String(IList<string> userList)
        {
            string data = "[b][color=red]--- Userlist ---[/color][/b]\n";
            foreach (var v in userList)
            {
                data += "- " + v + "\n";
            }
            return data;
        }
    }
}