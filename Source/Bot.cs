using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Esports_Bot.Source
{
    class Bot
    {
        private StreamReader serverResponse;
        private StreamWriter botOutput;
        public BotCommands botCMD;
        private List<string> channels;
        public Bot(StreamReader streamReader, StreamWriter streamWriter)
        {
            serverResponse = streamReader;
            botOutput = streamWriter;    
        }

        public void Login()
        {
            //Login to IRC server
            botOutput.WriteLine(BotStrings.LoginString);
            Console.WriteLine("Sent login request to server.");
            Console.WriteLine(BotStrings.LoginString);
            botOutput.Flush();
            Console.WriteLine("Recieved Response From Server: \r\n" + serverResponse.ReadLine());
            //Ask for additional commands
            botOutput.WriteLine("CAP REQ :twitch.tv/commands");
            botOutput.Flush();
            Console.WriteLine(serverResponse.ReadLine());
        }

        public void Join(string channel)
        {
            //Tell Bot to join the channel
            botOutput.WriteLine(BotStrings.JoinString(channel));
            Console.WriteLine("Bot is attempting to join channel: " + channel + "\r\n");
            Console.WriteLine(BotStrings.JoinString(channel));
            botOutput.Flush();

            //Tell Bot to join the channel
            botOutput.WriteLine(BotStrings.JoinString(channel));
            Console.WriteLine("Bot is attempting to join channel: " + channel + "\r\n");
            Console.WriteLine(BotStrings.JoinString(channel));
            botOutput.Flush();

            //Tell Everyone That the bot has joined 
            botOutput.WriteLine(BotStrings.AnnounceString(channel));
            Console.WriteLine("Bot Has joined channel:" + channel + "\r\n");
            Console.WriteLine("\r\nPogChamp");
            botOutput.Flush();
        }
    }
}
