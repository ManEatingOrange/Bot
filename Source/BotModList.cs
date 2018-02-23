using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Esports_Bot.Source
{
    //Grabs a list of mods in the channel
    class BotModList
    {
       

        public BotModList()
        {
            
        }

        public static string ModList(string channel)
        {
            //Send request for mod list
            string modList = GET("https://tmi.twitch.tv/group/user/" + channel + "/chatters");
            Console.WriteLine(GET("https://tmi.twitch.tv/group/user/" + channel + "/chatters"));
            string splitArr = modList.Split(new[] { '[', ']' }, StringSplitOptions.None)[1];
            return splitArr;
        }

        private static string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
