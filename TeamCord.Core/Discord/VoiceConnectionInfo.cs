using Discord;
using Discord.WebSocket;

namespace TeamCord.Core
{
    /// <summary>
    /// Connection info of the connected voice channel
    /// </summary>
    public class VoiceConnectionInfo : BaseConnectionInfo
    {
        protected IVoiceChannel VoiceChannel;
        protected int VoiceLatency;

        /// <summary>
        /// Name of the current connected channel
        /// </summary>
        public virtual string ChannelName
        { get { return VoiceChannel.Name; } }

        /// <summary>
        /// Name of the current connected server
        /// </summary>
        public virtual string ServerName
        { get { return VoiceChannel.Guild.Name; } }

        /// <summary>
        /// Audio bitrate of the voice channel
        /// </summary>
        [Unit("bit/s")]
        public virtual int Bitrate
        { get { return VoiceChannel.Bitrate; } }

        /// <summary>
        /// User limit of the voice channel, null if no limit is set
        /// </summary>
        public virtual int? Userlimit
        { get { return VoiceChannel.UserLimit; } }

        /// <summary>
        /// Latency of the discord voice channel
        /// </summary>
        public override int Latency
        { get { return VoiceLatency; } }

        public VoiceConnectionInfo(DiscordSocketClient client) : base(client)
        {
        }

        public VoiceConnectionInfo(DiscordSocketClient client, IVoiceChannel voiceChannel, int voiceLatency) : base(client)
        {
            Client = client;
            VoiceChannel = voiceChannel;
            VoiceLatency = voiceLatency;
        }
    }
}