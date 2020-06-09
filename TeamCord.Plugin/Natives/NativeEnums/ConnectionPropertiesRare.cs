namespace TeamCord.Plugin.Natives
{
    public enum ConnectionPropertiesRare
    {
        CONNECTION_DUMMY_0 = 52,
        CONNECTION_DUMMY_1,
        CONNECTION_DUMMY_2,
        CONNECTION_DUMMY_3,
        CONNECTION_DUMMY_4,
        CONNECTION_DUMMY_5,
        CONNECTION_DUMMY_6,
        CONNECTION_DUMMY_7,
        CONNECTION_DUMMY_8,
        CONNECTION_DUMMY_9,
        CONNECTION_FILETRANSFER_BANDWIDTH_SENT,                     //how many bytes per second are currently being sent by file transfers
        CONNECTION_FILETRANSFER_BANDWIDTH_RECEIVED,                 //how many bytes per second are currently being received by file transfers
        CONNECTION_FILETRANSFER_BYTES_RECEIVED_TOTAL,               //how many bytes we received in total through file transfers
        CONNECTION_FILETRANSFER_BYTES_SENT_TOTAL,                   //how many bytes we sent in total through file transfers
        CONNECTION_ENDMARKER_RARE,
    }
}