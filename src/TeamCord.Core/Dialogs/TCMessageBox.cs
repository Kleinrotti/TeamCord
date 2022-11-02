using Forge.Forms;

namespace TeamCord.Core
{
    public static class TCMessageBox
    {
        /// <summary>
        /// Displays a MessageBox
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message)
        {
            _ = Forge.Forms.Show.Window().For(new Alert(message)).Result;
        }

        /// <summary>
        /// Displays a MessageBox
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void Show(string message, string title)
        {
            _ = Forge.Forms.Show.Window().For(new Alert(message, title)).Result;
        }
    }
}