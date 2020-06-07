using Discord;
using Discord.Audio;
using Discord.Audio.Streams;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCord.DiscordLib
{
    internal class AudioService
    {
        public AudioService()
        {
        }

        private AudioStream _outStream;
        private IAudioClient _audioClient;
        private IVoiceChannel _voiceChannel;
        private Action<byte[], int> _voiceCallback;

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel voiceChannel, Action<byte[], int> voiceCallback)
        {
            if (voiceChannel != null)
            {
                try
                {
                    //await LeaveChannel();
                    _voiceChannel = voiceChannel;
                    _audioClient = await _voiceChannel.ConnectAsync();
                    _voiceCallback = voiceCallback;
                    _audioClient.Connected += _audioClient_Connected;
                    _audioClient.StreamCreated += _audioClient_StreamCreated;
                    _audioClient.StreamDestroyed += _audioClient_StreamDestroyed;
                    _outStream = _audioClient.CreatePCMStream(AudioApplication.Mixed, null, 100, 5);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private async Task _audioClient_Connected()
        {
            var users = await _voiceChannel.GetUsersAsync().ToListAsync();
            foreach (var v in users[0])
            {
                if (!v.IsBot)
                {
                    await ListenUserAsync(v);
                }
            }
        }

        private Task _audioClient_StreamDestroyed(ulong arg)
        {
            Console.WriteLine("Stream destroyed");
            return Task.CompletedTask;
        }

        private async Task _audioClient_StreamCreated(ulong arg1, AudioInStream arg2)
        {
            //Triggers when user joined to the channel
            await ListenUserAsync(arg2);
            return;
        }

        [Command("leave", RunMode = RunMode.Async)]
        public async Task LeaveChannel()
        {
            if (_audioClient != null && _audioClient.ConnectionState == ConnectionState.Connected)
            {
                await _audioClient.StopAsync();
                await _voiceChannel.DisconnectAsync();
            }
        }

        public async void SendVoiceData(byte[] buffer)
        {
            if (_audioClient != null && _outStream != null && _audioClient.ConnectionState == ConnectionState.Connected)
            {
                try
                {
                    await _outStream.WriteAsync(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task ListenUserAsync(IGuildUser user)
        {
            var socketUser = user as SocketGuildUser;
            var userAduioStream = (InputStream)socketUser.AudioStream;

            try
            {
                var buffer = new byte[3840];
                while (await userAduioStream.ReadAsync(buffer, 0, buffer.Length) > 0)
                {
                    _voiceCallback(buffer, 3840);
                }
            }
            catch (Exception ex) { }
            finally
            {
            }
        }

        private async Task ListenUserAsync(AudioInStream stream)
        {
            try
            {
                var buffer = new byte[3840];
                while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0)
                {
                    _voiceCallback(buffer, 3840);
                }
            }
            catch (Exception ex) { }
            finally
            {
            }
        }
    }
}