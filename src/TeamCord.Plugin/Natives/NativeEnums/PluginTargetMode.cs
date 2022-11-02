namespace TeamCord.Plugin.Natives
{
    public enum PluginTargetMode
    {
        PluginCommandTarget_CURRENT_CHANNEL = 0,                  //send plugincmd to all clients in current channel
        PluginCommandTarget_SERVER,                             //send plugincmd to all clients on server
        PluginCommandTarget_CLIENT,                             //send plugincmd to all given client ids
        PluginCommandTarget_CURRENT_CHANNEL_SUBSCRIBED_CLIENTS, //send plugincmd to all subscribed clients in current channel
        PluginCommandTarget_MAX
    }
}