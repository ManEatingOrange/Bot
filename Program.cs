using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using Esports_Bot.Source;

namespace Esports_Bot
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Channel you wish the bot to join
            string Channel = "";
            bool BotRunning = true;
            string msg;
            //Starte new TcP Stream
            BotTcpStream stream = new BotTcpStream();
            StreamReader recieveServerStream = new StreamReader(stream.GetCurrentStream());
            StreamWriter sendToServer = new StreamWriter(stream.GetCurrentStream());

            //Initialize Bot
            Bot Bot = new Bot(recieveServerStream, sendToServer);
            Bot.Login();
            Bot.Join(Channel);
            BotCommands bCMD = new BotCommands(sendToServer, recieveServerStream);

            //Start bot loop
            while (BotRunning)
            {
                msg = recieveServerStream.ReadLine();
                bCMD.ReadServerMessage(msg);
                bCMD.ReadChatCommand(msg);
            }
        }
    }
}
