using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esports_Bot.Source
{
    class BotStrings
    {
        //Login
        public static string LoginString = "PASS oauth:\r\nNICK \r\n";

        //Join the channel
        public static string JoinString(string channel)
        {
            string joinString = "JOIN " + "#" + channel + "\r\n";
            return joinString;
        }

        //Tells everyone the bot has joined
        public static string AnnounceString(string channel)
        {
            string announceString = ":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :Suh Dude Kappa\r\n";
            return announceString;
        }
    }
}
