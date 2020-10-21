using Discord;
using System.Collections.Generic;

namespace TeamCord.Core
{
    public class TCServer : IEntity<ulong>
    {
        public IList<TCChannel> VoiceChannels { get; }

        public ulong Id { get; }

        public string Name { get; }

        public TCServer(ulong id, string name)
        {
            Id = id;
            Name = name;
            VoiceChannels = new List<TCChannel>();
        }
    }
}