using Forge.Forms;

namespace TeamCord.Core
{
    public static class TCPrompt
    {
        /// <summary>
        /// Displays a MessageBox prompt with text input
        /// </summary>
        /// <returns></returns>
        public static TCDialogResult<string> Show(string title)
        {
            var result = Forge.Forms.Show.Window().For(new Prompt<string> { Title = title }).Result;
            return new TCDialogResult<string>(result.Model.Value, result.Model.Confirmed);
        }

        /// <summary>
        /// Displays a MessageBox prompt with text input
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TCDialogResult<string> Show(string title, string message)
        {
            var result = Forge.Forms.Show.Window().For(new Prompt<string> { Title = title, Message = message }).Result;
            return new TCDialogResult<string>(result.Model.Value, result.Model.Confirmed);
        }
    }
}