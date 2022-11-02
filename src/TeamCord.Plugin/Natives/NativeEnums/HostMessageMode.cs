namespace TeamCord.Plugin.Natives
{
    public enum HostMessageMode
    {
        HostMessageMode_NONE = 0,               //dont display anything
        HostMessageMode_LOG,                  //display message inside log
        HostMessageMode_MODAL,                //display message inside a modal dialog
        HostMessageMode_MODALQUIT             //display message inside a modal dialog and quit/close server/connection
    }
}