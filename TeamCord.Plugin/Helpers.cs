using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TeamCord.Core;

namespace TeamCord.Plugin
{
    public static class Helpers
    {
        private static string _pattern = @"\{""Teamcord(.|\s$)*}}";

        public static ulong ExtractChannelID(string channelDescription)
        {
            try
            {
                //search for a json match in the channel description
                var match = Regex.Match(channelDescription, _pattern);
                if (!match.Success)
                {
                    Logging.Log("No regex match found in channelDescription", LogLevel.LogLevel_DEBUG);
                    return 0;
                }
                channelDescription = match.Value;
                Console.WriteLine(channelDescription);
                var obj = JsonConvert.DeserializeObject<TS3Json<TS3ChannelJson>>(channelDescription);

                return obj.Teamcord.ChannelID;
            }
            catch (NullReferenceException ex)
            {
                Logging.Log(ex.Message, LogLevel.LogLevel_DEBUG);
                return 0;
            }
            catch (Exception ex)
            {
                Logging.Log(ex.Message, LogLevel.LogLevel_WARNING);
                return 0;
            }
        }

        /// <summary>
        /// Removes all channelID entries of a string
        /// </summary>
        /// <param name="channelDescription"></param>
        /// <returns></returns>
        public static string RemoveChannelID(string channelDescription)
        {
            var matches = Regex.Matches(channelDescription, _pattern);
            if (matches.Count < 1)
            {
                Logging.Log("No channelID was removed due to no regex matches", LogLevel.LogLevel_DEBUG);
                return channelDescription;
            }
            foreach (Match m in matches)
            {
                channelDescription = channelDescription.Replace(m.Value, "");
            }
            return channelDescription;
        }

        public static ulong ExtractClientID(string clientDescription)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<TS3Json<TS3ClientJson>>(clientDescription);

                return obj.Teamcord.ClientID;
            }
            catch (NullReferenceException ex)
            {
                Logging.Log(ex.Message, LogLevel.LogLevel_DEBUG);
                return 0;
            }
            catch (Exception ex)
            {
                Logging.Log(ex.Message, LogLevel.LogLevel_WARNING);
                return 0;
            }
        }

        public static string ChannelIDToJsonString(ulong channelID)
        {
            var val = new TS3Json<TS3ChannelJson>(new TS3ChannelJson(channelID));
            var json = JsonConvert.SerializeObject(val);
            return json;
        }

        public static string DiscordIDToJsonString(ulong discordID)
        {
            var val = new TS3Json<TS3ClientJson>(new TS3ClientJson(discordID));
            var json = JsonConvert.SerializeObject(val);
            return json;
        }

        public static string UserListToTs3String(IList<string> userList)
        {
            string data = "[b][color=red]--- Userlist ---[/color][/b]\n";
            foreach (var v in userList)
            {
                data += "- " + v + "\n";
            }
            return data;
        }
    }
}