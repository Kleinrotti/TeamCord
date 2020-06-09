namespace TeamCord.Plugin.Natives
{
    public enum VirtualServerProperties
    {
        VIRTUALSERVER_UNIQUE_IDENTIFIER = 0,             //available when connected, can be used to identify this particular server installation

        VIRTUALSERVER_NAME,                              //available and always up-to-date when connected

        VIRTUALSERVER_WELCOMEMESSAGE,                    //available when connected,  (=> requestServerVariables)

        VIRTUALSERVER_PLATFORM,                          //available when connected

        VIRTUALSERVER_VERSION,                           //available when connected

        VIRTUALSERVER_MAXCLIENTS,                        //only available on request (=> requestServerVariables), stores the maximum number of clients that may currently join the server

        VIRTUALSERVER_PASSWORD,                          //not available to clients, the server password

        VIRTUALSERVER_CLIENTS_ONLINE,                    //only available on request (=> requestServerVariables),

        VIRTUALSERVER_CHANNELS_ONLINE,                   //only available on request (=> requestServerVariables),

        VIRTUALSERVER_CREATED,                           //available when connected, stores the time when the server was created

        VIRTUALSERVER_UPTIME,                            //only available on request (=> requestServerVariables), the time since the server was started

        VIRTUALSERVER_CODEC_ENCRYPTION_MODE,             //available and always up-to-date when connected

        VIRTUALSERVER_ENCRYPTION_CIPHERS,                //internal use

        VIRTUALSERVER_ENDMARKER,
    }
}