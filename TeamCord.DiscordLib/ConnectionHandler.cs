using Discord;
using Discord.WebSocket;
using SharpDX.Text;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TeamCord.DiscordLib
{
    public class ConnectionHandler : IDisposable
    {
        private DiscordSocketClient client;

        private static short[] span = new short[960];
        private byte[] mem = new byte[span.Length * sizeof(short)];

        private AudioService audioService;

        private MemoryStream memStream;
        private byte[] _token;

        public ConnectionHandler(byte[] token)
        {
            _token = token;
            client = new DiscordSocketClient();
            audioService = new AudioService();
            client.Disconnected += Client_Disconnected;
            client.LoggedOut += Client_LoggedOut;
            client.LoggedIn += Client_LoggedIn;
            client.Connected += Client_Connected;
            client.Log += Client_Log;
        }

        #region Events

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }

        private Task Client_Connected()
        {
            Console.WriteLine("Client connected");
            return Task.CompletedTask;
        }

        private Task Client_LoggedIn()
        {
            Console.WriteLine("Client logged in");
            return Task.CompletedTask;
        }

        private Task Client_LoggedOut()
        {
            Console.WriteLine("Client logged out");
            return Task.CompletedTask;
        }

        private Task Client_Disconnected(Exception arg)
        {
            Console.WriteLine("Client disconnected");
            return Task.CompletedTask;
        }

        #endregion Events

        public async void Connect()
        {
            if (client.ConnectionState != ConnectionState.Connected || client.ConnectionState == ConnectionState.Connecting)
            {
                await client.LoginAsync(0, Encoding.Default.GetString(_token));
                await client.StartAsync();
            }
        }

        public async void JoinChannel(ulong channelID)
        {
            if (client.ConnectionState != ConnectionState.Connecting || client.ConnectionState != ConnectionState.Connected)
                Connect();
            var channel = client.GetChannel(channelID) as SocketVoiceChannel;
            await audioService.JoinChannel(channel);
            memStream = new MemoryStream(mem);
        }

        public async void LeaveChannel()
        {
            await audioService.LeaveChannel();
        }

        public async void Disconnect()
        {
            if (audioService != null)
                await audioService.LeaveChannel();
            await client.StopAsync();
            await client.LogoutAsync();
        }

        public unsafe void VoiceData(short* samplesPtr, int sampleCount, int channels, int* edited)
        {
            //if (_audioClient == null)
            //    return;
            //var v = sizeof(short) * sampleCount * channels; //960
            //Console.WriteLine("Samples :" + *samplesPtr + " Count: " + sampleCount + " Edited: " + *edited);

            for (int ctr = 0; ctr < span.Length; ctr++)
            {
                span[ctr] = ((*(ctr + samplesPtr)));
            }

            Buffer.BlockCopy(span, 0, mem, 0, span.Length);

            Task.Run(() =>
            {
                audioService.SendVoiceData(memStream);
            });
        }

        public void Dispose()
        {
            audioService.LeaveChannel().Wait();
            client.StopAsync().Wait();
            client.LogoutAsync().Wait();
            client.Dispose();
        }
    }
}