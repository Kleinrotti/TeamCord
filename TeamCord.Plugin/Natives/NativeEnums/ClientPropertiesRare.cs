namespace TeamCord.Plugin.Natives
{
    public enum ClientPropertiesRare
    {
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