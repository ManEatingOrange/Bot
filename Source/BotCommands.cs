using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Addons
using Esports_Bot.Addons;

namespace Esports_Bot.Source
{
    //Holds all the bot commands
    class BotCommands
    {
        //Member variables
        private StreamWriter botOutput;
        private StreamReader serverInput;
        private string channel;
        private Series series;
        private string[] teams;
        private int[] scores;
        private bool seriesCreated = false;
        private string response;

        //Constructor
        public BotCommands(StreamWriter output, StreamReader input)
        {
            //grabs IRC client input output streams
            botOutput = output;
            
            serverInput = input;
        }


        //Mainly makes sure the bot sends PONG when pinged 
        public void ReadServerMessage(string msg)
        {
            switch (msg)
            {
                case "PING :tmi.twitch.tv":
                    Console.WriteLine("twitch.tv: PING");
                    botOutput.WriteLine("PONG :tmi.twitch.tv");
                    botOutput.Flush();
                    Console.WriteLine("Esports_Bot: PONG");
                    break;
                default:
                    Console.WriteLine(msg);
                    break;
            }
        }

        //Holds all bot commands they are seperated into standard user commands and mod commands
        public void ReadChatCommand(string command)
        {
            if (command.Contains("PRIVMSG"))
            {
                string[] cmd = command.Split(new[] { '!', ' ', ':' }, StringSplitOptions.None);
                if (cmd.Length >= 8)
                {
                    switch (cmd[7])
                    {
                        //User Commands
                        case "Series":
                            if (seriesCreated)
                            {
                                scores = series.GetScores();
                                Console.WriteLine("User Requested Series Update");
                                botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + teams[0] + ": " + scores[0] + " vs " + teams[1] + ": " + scores[1] + "\r\n");
                                botOutput.Flush();
                            }
                            else
                            {
                                Console.WriteLine("Series requested but none exists.");
                                botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :Sorry but there is no series in progress!\r\n");
                                botOutput.Flush();
                            }
                            break;
                        case "test":
                            Console.WriteLine("Command !test Requested");
                            botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :This is a test command!\r\n");
                            botOutput.Flush();
                            break;

                        //Mod Only commands
                        //These Commands influence the ongoing series--------------------------------------------------------------------------------------------------------------------------------
                        case "NewSeries":
                            Console.WriteLine("Command !NewSeries Requested");

                            response = BotModList.ModList(channel);
                            if (response.Contains(cmd[1]))
                            {
                                series = new Series(cmd[8], channel);
                                seriesCreated = true;
                                teams = series.GetTeams();
                                scores = series.GetScores();
                                Console.WriteLine("User is mod executing command.");
                                Console.WriteLine("User " + cmd[1] + "created new series");
                                botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :New series vs " + cmd[8] + ": " + teams[0] + ": " + scores[0] + " vs " + teams[1] + ": " + scores[1] + "\r\n");
                                botOutput.Flush();
                            }
                            break;
                        case "Win":
                            Console.WriteLine("Command !Win Requested");

                            response = BotModList.ModList(channel);
                            if (response.Contains(cmd[1]))
                            {
                                if (seriesCreated)
                                {
                                    series.win();
                                    scores = series.GetScores();
                                    Console.WriteLine("Series Has Been Updated");
                                    botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :Series has been updated. Congrats on the Win!\r\n");
                                    botOutput.Flush();
                                    botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + teams[0] + ": " + scores[0] + " vs " + teams[1] + ": " + scores[1] + "\r\n");
                                    botOutput.Flush();
                                }
                                else
                                {
                                    Console.WriteLine("Series Update Failed");
                                    botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :Sorry but there is no series to update!\r\n");
                                    botOutput.Flush();
                                }
                            }
                            break;
                        case "Loss":
                            Console.WriteLine("Command !Loss Requested");

                            response = BotModList.ModList(channel);
                            if (response.Contains(cmd[1]))
                            {
                                if (seriesCreated)
                                {
                                    series.loss();
                                    scores = series.GetScores();
                                    Console.WriteLine("Series Has Been Updated");
                                    botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :Series has been updated. LUL\r\n");
                                    botOutput.Flush();
                                    botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + teams[0] + ": " + scores[0] + " vs " + teams[1] + ": " + scores[1] + "\r\n");
                                    botOutput.Flush();
                                }
                                else
                                {
                                    Console.WriteLine("Series Update Failed");
                                    botOutput.WriteLine(":" + channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG #" + channel + " :Sorry but there is no series to update!\r\n");
                                    botOutput.Flush();
                                }
                            }
                            break;
                            //End Series commands---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        default:
                            break;
                    }
                }
            }
        }
    }
}
