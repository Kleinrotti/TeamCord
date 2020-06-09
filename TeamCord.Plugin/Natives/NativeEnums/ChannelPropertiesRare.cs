namespace TeamCord.Plugin.Natives
{
    public enum ChannelPropertiesRare
    {
        CHANNEL_DUMMY_3 = 18,
        CHANNEL_DUMMY_4,
        CHANNEL_DUMMY_5,
        CHANNEL_DUMMY_6,
        CHANNEL_DUMMY_7,
        CHANNEL_FLAG_MAXCLIENTS_UNLIMITED,      //Available for all channels that are "in view", always up-to-date
        CHANNEL_FLAG_MAXFAMILYCLIENTS_UNLIMITED,//Available for all channels that are "in view", always up-to-date
        CHANNEL_FLAG_MAXFAMILYCLIENTS_INHERITED,//Available for all channels that are "in view", always up-to-date
        CHANNEL_FLAG_ARE_SUBSCRIBED,            //Only available client side, stores whether we are subscribed to this channel
        CHANNEL_FILEPATH,                       //not available client side, the folder used for file-transfers for this channel
        CHANNEL_NEEDED_TALK_POWER,              //Available for all channels that are "in view", always up-to-date
        CHANNEL_FORCED_SILENCE,                 //Available for all channels that are "in view", always up-to-date
        CHANNEL_NAME_PHONETIC,                  //Available for all channels that are "in view", always up-to-date
        CHANNEL_ICON_ID,                        //Available for all channels that are "in view", always up-to-date
        CHANNEL_BANNER_GFX_URL,                 //Available for all channels that are "in view", always up-to-date
        CHANNEL_BANNER_MODE,                    //Available for all channels that are "in view", always up-to-date
        CHANNEL_PERMISSION_HINTS,
        CHANNEL_ENDMARKER_RARE,
        CHANNEL_DELETE_DELAY_DEADLINE = 127     //(for clientlibv2) expected delete time in monotonic clock seconds or 0 if nothing is expected
    }
}