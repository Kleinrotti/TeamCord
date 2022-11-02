namespace TeamCord.Plugin.Natives
{
    public enum MytsDataUnsetFlags
    {
        MytsDataUnsetFlag_None = 0,
        MytsDataUnsetFlag_Badges = 1,
        MytsDataUnsetFlag_Avatar = 1 << 1,

        MytsDataUnsetFlag_All = MytsDataUnsetFlag_Badges | MytsDataUnsetFlag_Avatar // make sure "all" really contains all flags
    }
}