using Discord;
using Discord.Audio;
using Discord.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TeamCord.DiscordLib
{
    internal class AudioService
    {
        public AudioService()
        {
        }

        private AudioOutStream _outStream;
        private IAudioClient _audioClient;
        private IVoiceChannel _voiceChannel;

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel voiceChannel)
        {
            if (voiceChannel != null)
            {
                try
                {
                    await LeaveChannel();
                    _voiceChannel = voiceChannel;
                    _audioClient = await _voiceChannel.ConnectAsync();
                    _outStream = _audioClient.CreatePCMStream(AudioApplication.Voice);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        public async Task LeaveChannel()
        {
            if (_audioClient != null && _audioClient.ConnectionState == ConnectionState.Connected)
                await _audioClient.StopAsync();
        }

        public async void SendVoiceData(MemoryStream stream)
        {
            if (stream != null && _audioClient != null && _outStream != null && _audioClient.ConnectionState == ConnectionState.Connected)
            {
                try
                {
                    stream.Position = 0;
                    await stream.CopyToAsync(_outStream);
                    await _outStream.FlushAsync();
                }
                catch (TaskCanceledException ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
}