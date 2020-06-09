namespace TeamCord.Plugin.Natives
{
    public enum ChannelProperties
    {
        CHANNEL_NAME = 0,                       //Available for all channels that are "in view", always up-to-date

        CHANNEL_TOPIC,                          //Available for all channels that are "in view", always up-to-date

        CHANNEL_DESCRIPTION,                    //Must be requested (=> requestChannelDescription)

        CHANNEL_PASSWORD,                       //not available client side

        CHANNEL_CODEC,                          //Available for all channels that are "in view", always up-to-date

        CHANNEL_CODEC_QUALITY,                  //Available for all channels that are "in view", always up-to-date

        CHANNEL_MAXCLIENTS,                     //Available for all channels that are "in view", always up-to-date

        CHANNEL_MAXFAMILYCLIENTS,               //Available for all channels that are "in view", always up-to-date

        CHANNEL_ORDER,                          //Available for all channels that are "in view", always up-to-date

        CHANNEL_FLAG_PERMANENT,                 //Available for all channels that are "in view", always up-to-date

        CHANNEL_FLAG_SEMI_PERMANENT,            //Available for all channels that are "in view", always up-to-date

        CHANNEL_FLAG_DEFAULT,                   //Available for all channels that are "in view", always up-to-date

        CHANNEL_FLAG_PASSWORD,                  //Available for all channels that are "in view", always up-to-date

        CHANNEL_CODEC_LATENCY_FACTOR,           //Available for all channels that are "in view", always up-to-date

        CHANNEL_CODEC_IS_UNENCRYPTED,           //Available for all channels that are "in view", always up-to-date

        CHANNEL_SECURITY_SALT,                  //Not available client side, not used in teamspeak, only SDK. Sets the options+salt for security hash.

        CHANNEL_DELETE_DELAY,                   //How many seconds to wait before deleting this channel

        CHANNEL_UNIQUE_IDENTIFIER,              //Available for all channels that are "in view"

        CHANNEL_ENDMARKER,
    }
}