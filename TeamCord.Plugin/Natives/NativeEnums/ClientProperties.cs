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

        CLIENT_ENDMARKER,
    }
}