using System;
using System.Threading;
using System.Windows.Threading;
using System.Xml;
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
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(channelDescription);
                var v = doc.GetElementsByTagName("Teamcord").Item(0);

                return Convert.ToUInt64(v.InnerText);
            }
            catch (XmlException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}