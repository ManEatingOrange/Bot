using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Esports_Bot.Source
{
    class BotTcpStream
    {
        //Connect to twitch irc-like server
        private static Int32 port = 6667;
        private TcpClient client = new TcpClient("irc.twitch.tv", port);
        private NetworkStream stream;
        public BotTcpStream()
        {
            stream = client.GetStream();
        }

        public NetworkStream GetCurrentStream()
        {
            return stream;
        }
    }
}
