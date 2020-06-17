using Discord;
using Discord.Audio;
using Discord.Audio.Streams;
using Discord.Commands;
using Discord.WebSocket;
using NAudio.Wave;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCord.DiscordLib
{
    internal class AudioService
    {
        private AudioStream _outStream;
        private IAudioClient _audioClient;
        private IVoiceChannel _voiceChannel;
        private BufferedWaveProvider _waveProvider;
        private WaveOut _waveOut;

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel voiceChannel)
        {
            if (voiceChannel != null)
            {
                try
                {
                    //await LeaveChannel();
                    _voiceChannel = voiceChannel;
                    _audioClient = await _voiceChannel.ConnectAsync();
                    _audioClient.Disconnected += _audioClient_Disconnected;
                    _audioClient.StreamCreated += _audioClient_StreamCreated;
                    _audioClient.StreamDestroyed += _audioClient_StreamDestroyed;
                    _outStream = _audioClient.CreatePCMStream(AudioApplication.Mixed, null, 100, 5);
                    InitSpeakers();
                    await ListenToUsersAsync();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private Task _audioClient_Disconnected(Exception arg)
        {
            _waveOut.Stop();
            return Task.CompletedTask;
        }

        private async Task ListenToUsersAsync()
        {
            var users = (await _voiceChannel.GetUsersAsync().ToListAsync()).FirstOrDefault();
            foreach (var v in users)
            {
                //only play users audio data
                if (!v.IsBot)
                {
                    var socketUser = v as SocketGuildUser;
                    var userAduioStream = socketUser.AudioStream;
                    await ListenUserAsync(userAduioStream);
                }
            }
        }

        private void InitSpeakers()
        {
            _waveProvider = new BufferedWaveProvider(new WaveFormat(48000, 2));
            _waveOut = new WaveOut();
            _waveOut.Init(_waveProvider);
            _waveOut.Play();
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

        private async Task ListenUserAsync(AudioInStream stream)
        {
            try
            {
                var buffer = new byte[3840];
                while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0)
                {
                    _waveProvider.AddSamples(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex) { }
        }
    }
}