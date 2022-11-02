namespace TeamCord.Plugin.Natives
{
    public enum ConnectionProperties
    {
        CONNECTION_PING = 0,                                        //average latency for a round trip through and back this connection
        CONNECTION_PING_DEVIATION,                                  //standard deviation of the above average latency
        CONNECTION_CONNECTED_TIME,                                  //how long the connection exists already
        CONNECTION_IDLE_TIME,                                       //how long since the last action of this client
        CONNECTION_CLIENT_IP,                                       //IP of this client (as seen from the server side)
        CONNECTION_CLIENT_PORT,                                     //Port of this client (as seen from the server side)
        CONNECTION_SERVER_IP,                                       //IP of the server (seen from the client side) - only available on yourself, not for remote clients, not available server side
        CONNECTION_SERVER_PORT,                                     //Port of the server (seen from the client side) - only available on yourself, not for remote clients, not available server side
        CONNECTION_PACKETS_SENT_SPEECH,                             //how many Speech packets were sent through this connection
        CONNECTION_PACKETS_SENT_KEEPALIVE,
        CONNECTION_PACKETS_SENT_CONTROL,
        CONNECTION_PACKETS_SENT_TOTAL,                              //how many packets were sent totally (this is PACKETS_SENT_SPEECH + PACKETS_SENT_KEEPALIVE + PACKETS_SENT_CONTROL)
        CONNECTION_BYTES_SENT_SPEECH,
        CONNECTION_BYTES_SENT_KEEPALIVE,
        CONNECTION_BYTES_SENT_CONTROL,
        CONNECTION_BYTES_SENT_TOTAL,
        CONNECTION_PACKETS_RECEIVED_SPEECH,
        CONNECTION_PACKETS_RECEIVED_KEEPALIVE,
        CONNECTION_PACKETS_RECEIVED_CONTROL,
        CONNECTION_PACKETS_RECEIVED_TOTAL,
        CONNECTION_BYTES_RECEIVED_SPEECH,
        CONNECTION_BYTES_RECEIVED_KEEPALIVE,
        CONNECTION_BYTES_RECEIVED_CONTROL,
        CONNECTION_BYTES_RECEIVED_TOTAL,
        CONNECTION_PACKETLOSS_SPEECH,
        CONNECTION_PACKETLOSS_KEEPALIVE,
        CONNECTION_PACKETLOSS_CONTROL,
        CONNECTION_PACKETLOSS_TOTAL,                                //the probability with which a packet round trip failed because a packet was lost
        CONNECTION_SERVER2CLIENT_PACKETLOSS_SPEECH,                 //the probability with which a speech packet failed from the server to the client
        CONNECTION_SERVER2CLIENT_PACKETLOSS_KEEPALIVE,
        CONNECTION_SERVER2CLIENT_PACKETLOSS_CONTROL,
        CONNECTION_SERVER2CLIENT_PACKETLOSS_TOTAL,
        CONNECTION_CLIENT2SERVER_PACKETLOSS_SPEECH,
        CONNECTION_CLIENT2SERVER_PACKETLOSS_KEEPALIVE,
        CONNECTION_CLIENT2SERVER_PACKETLOSS_CONTROL,
        CONNECTION_CLIENT2SERVER_PACKETLOSS_TOTAL,
        CONNECTION_BANDWIDTH_SENT_LAST_SECOND_SPEECH,               //howmany bytes of speech packets we sent during the last second
        CONNECTION_BANDWIDTH_SENT_LAST_SECOND_KEEPALIVE,
        CONNECTION_BANDWIDTH_SENT_LAST_SECOND_CONTROL,
        CONNECTION_BANDWIDTH_SENT_LAST_SECOND_TOTAL,
        CONNECTION_BANDWIDTH_SENT_LAST_MINUTE_SPEECH,               //howmany bytes/s of speech packets we sent in average during the last minute
        CONNECTION_BANDWIDTH_SENT_LAST_MINUTE_KEEPALIVE,
        CONNECTION_BANDWIDTH_SENT_LAST_MINUTE_CONTROL,
        CONNECTION_BANDWIDTH_SENT_LAST_MINUTE_TOTAL,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_SECOND_SPEECH,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_SECOND_KEEPALIVE,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_SECOND_CONTROL,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_SECOND_TOTAL,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_MINUTE_SPEECH,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_MINUTE_KEEPALIVE,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_MINUTE_CONTROL,
        CONNECTION_BANDWIDTH_RECEIVED_LAST_MINUTE_TOTAL,
        CONNECTION_ENDMARKER,
    }
}