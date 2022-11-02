using Discord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamCord.Core
{
    public class TCChannel : IChannel
    {
        public string Name { get; }

        public DateTimeOffset CreatedAt => throw new NotImplementedException();

        public ulong Id { get; }

        public Task<IUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IReadOnlyCollection<IUser>> GetUsersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public TCChannel(ulong id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}