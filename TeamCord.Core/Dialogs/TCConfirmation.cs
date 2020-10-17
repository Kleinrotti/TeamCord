using Forge.Forms;

namespace TeamCord.Core
{
    public static class TCConfirmation
    {
        /// <summary>
        /// Displays a MessageBox confirmation dialog
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TCDialogResult<bool> Show(string message)
        {
            var result = Forge.Forms.Show.Window().For(new Confirmation(message, null, "Yes", "No")).Result;
            return new TCDialogResult<bool>(result.Model.Confirmed);
        }
    }
}