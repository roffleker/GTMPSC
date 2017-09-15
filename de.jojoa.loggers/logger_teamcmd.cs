using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.loggers
{
    class logger_teamcmd : Script
    {
        static FileStream fs;
        static StreamWriter sw;

        public logger_teamcmd()
        {
            API.onResourceStart += onstart;
            API.onResourceStop += onstop;
        }
        public static void onstart()
        {
             fs = File.Create("C:/Users/Armin/Documents/johannes/gtmp/resources/logs/teamcmd_" + DateTime.Now.ToString() + ".txt");
             sw = new StreamWriter(fs);
        }

        public static void onstop()
        {
            sw.Close();
        }

        public static void log_cmd(Client p, string command, params object[] args)
        {
            string text = "";
            switch(command)
            {
                case "pos":
                    string pos = args[0].ToString();
                    text = DateTime.Now.ToString() + ": /pos by " + p.name + pos;
                    break;
                case "god":

                    text = DateTime.Now.ToString() + "/god by " + p.name + "STATE: " + args[0];
                    break;
            }
            sw.WriteLine(text);
            sw.Flush();
        }
    }
}
