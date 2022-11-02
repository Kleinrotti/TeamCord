namespace TeamCord.Plugin.Natives
{
    public enum ClientProperties
    {
        CLIENT_UNIQUE_IDENTIFIER = 0,           //automatically up-to-date for any client "in view", can be used to identify this particular client installation

        CLIENT_NICKNAME,                        //automatically up-to-date for any client "in view"

        CLIENT_VERSION,                         //for other clients than ourself, this needs to be requested (=> requestClientVariables)

        CLIENT_PLATFORM,                        //for other clients than ourself, this needs to be requested (=> requestClientVariables)

        CLIENT_FLAG_TALKING,                    //automatically up-to-date for any client that can be heard (in room / whisper)

        CLIENT_INPUT_MUTED,                     //automatically up-to-date for any client "in view", this clients microphone mute status

        CLIENT_OUTPUT_MUTED,                    //automatically up-to-date for any client "in view", this clients headphones/speakers/mic combined mute status

        CLIENT_OUTPUTONLY_MUTED,                //automatically up-to-date for any client "in view", this clients headphones/speakers only mute status

        CLIENT_INPUT_HARDWARE,                  //automatically up-to-date for any client "in view", this clients microphone hardware status (is the capture device opened?)

        CLIENT_OUTPUT_HARDWARE,                 //automatically up-to-date for any client "in view", this clients headphone/speakers hardware status (is the playback device opened?)

        CLIENT_INPUT_DEACTIVATED,               //only usable for ourself, not propagated to the network

        CLIENT_IDLE_TIME,                       //internal use

        CLIENT_DEFAULT_CHANNEL,                 //only usable for ourself, the default channel we used to connect on our last connection attempt

        CLIENT_DEFAULT_CHANNEL_PASSWORD,        //internal use

        CLIENT_SERVER_PASSWORD,                 //internal use

        CLIENT_META_DATA,                       //automatically up-to-date for any client "in view", not used by TeamSpeak, free storage for sdk users

        CLIENT_IS_MUTED,                        //only make sense on the client side locally, "1" if this client is currently muted by us, "0" if he is not

        CLIENT_IS_RECORDING,                    //automatically up-to-date for any client "in view"

        CLIENT_VOLUME_MODIFICATOR,              //internal use

        CLIENT_VERSION_SIGN,                    //sign

        CLIENT_SECURITY_HASH,                   //SDK use, not used by teamspeak. Hash is provided by an outside source. A channel will use the security salt + other client data to calculate a hash, which must be the same as the one provided here.

        CLIENT_ENCRYPTION_CIPHERS,              //internal use

        CLIENT_DUMMY_4 = 22,
        CLIENT_DUMMY_5,
        CLIENT_DUMMY_6,
        CLIENT_DUMMY_7,
        CLIENT_DUMMY_8,
        CLIENT_DUMMY_9,
        CLIENT_KEY_OFFSET,                      //internal use
        CLIENT_LAST_VAR_REQUEST,                //internal use
        CLIENT_LOGIN_NAME,                      //used for serverquery clients, makes no sense on normal clients currently
        CLIENT_LOGIN_PASSWORD,                  //used for serverquery clients, makes no sense on normal clients currently
        CLIENT_DATABASE_ID,                     //automatically up-to-date for any client "in view", only valid with PERMISSION feature, holds database client id
        CLIENT_CHANNEL_GROUP_ID,                //automatically up-to-date for any client "in view", only valid with PERMISSION feature, holds database client id
        CLIENT_SERVERGROUPS,                    //automatically up-to-date for any client "in view", only valid with PERMISSION feature, holds all servergroups client belongs too
        CLIENT_CREATED,                         //this needs to be requested (=> requestClientVariables), first time this client connected to this server
        CLIENT_LASTCONNECTED,                   //this needs to be requested (=> requestClientVariables), last time this client connected to this server
        CLIENT_TOTALCONNECTIONS,                //this needs to be requested (=> requestClientVariables), how many times this client connected to this server
        CLIENT_AWAY,                            //automatically up-to-date for any client "in view", this clients away status
        CLIENT_AWAY_MESSAGE,                    //automatically up-to-date for any client "in view", this clients away message
        CLIENT_TYPE,                            //automatically up-to-date for any client "in view", determines if this is a real client or a server-query connection
        CLIENT_FLAG_AVATAR,                     //automatically up-to-date for any client "in view", this client got an avatar
        CLIENT_TALK_POWER,                      //automatically up-to-date for any client "in view", only valid with PERMISSION feature, holds database client id
        CLIENT_TALK_REQUEST,                    //automatically up-to-date for any client "in view", only valid with PERMISSION feature, holds timestamp where client requested to talk
        CLIENT_TALK_REQUEST_MSG,                //automatically up-to-date for any client "in view", only valid with PERMISSION feature, holds matter for the request
        CLIENT_DESCRIPTION,                     //automatically up-to-date for any client "in view"
        CLIENT_IS_TALKER,                       //automatically up-to-date for any client "in view"
        CLIENT_MONTH_BYTES_UPLOADED,            //this needs to be requested (=> requestClientVariables)
        CLIENT_MONTH_BYTES_DOWNLOADED,          //this needs to be requested (=> requestClientVariables)
        CLIENT_TOTAL_BYTES_UPLOADED,            //this needs to be requested (=> requestClientVariables)
        CLIENT_TOTAL_BYTES_DOWNLOADED,          //this needs to be requested (=> requestClientVariables)
        CLIENT_IS_PRIORITY_SPEAKER,             //automatically up-to-date for any client "in view"
        CLIENT_UNREAD_MESSAGES,                 //automatically up-to-date for any client "in view"
        CLIENT_NICKNAME_PHONETIC,               //automatically up-to-date for any client "in view"
        CLIENT_NEEDED_SERVERQUERY_VIEW_POWER,   //automatically up-to-date for any client "in view"
        CLIENT_DEFAULT_TOKEN,                   //only usable for ourself, the default token we used to connect on our last connection attempt
        CLIENT_ICON_ID,                         //automatically up-to-date for any client "in view"
        CLIENT_IS_CHANNEL_COMMANDER,            //automatically up-to-date for any client "in view"
        CLIENT_COUNTRY,                         //automatically up-to-date for any client "in view"
        CLIENT_CHANNEL_GROUP_INHERITED_CHANNEL_ID, //automatically up-to-date for any client "in view", only valid with PERMISSION feature, contains channel_id where the channel_group_id is set from
        CLIENT_BADGES,                          //automatically up-to-date for any client "in view", stores icons for partner badges
        CLIENT_MYTEAMSPEAK_ID,                  //automatically up-to-date for any client "in view", stores myteamspeak id
        CLIENT_INTEGRATIONS,                    //automatically up-to-date for any client "in view", stores integrations
        CLIENT_ACTIVE_INTEGRATIONS_INFO,        //stores info from the myts server and contains the subscription info
        CLIENT_MYTS_AVATAR,
        CLIENT_SIGNED_BADGES,
        CLIENT_PERMISSION_HINTS,
        CLIENT_ENDMARKER_RARE,
        CLIENT_HW_ID = 127                      //(for clientlibv2) unique hardware id
    }
}