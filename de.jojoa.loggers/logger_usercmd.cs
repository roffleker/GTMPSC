using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.loggers
{
    class logger_usercmd : Script
    {
        static FileStream fs;
        static StreamWriter sw;

        public logger_usercmd()
        {
            API.onResourceStart += onstart;
            API.onResourceStop += onstop;
        }
        public static void onstart()
        {
            fs = File.Create("C:/Users/Armin/Documents/johannes/gtmp/resources/logs/usercmd_" + DateTime.Now.ToString() + ".txt");
            sw = new StreamWriter(fs);
        }

        public static void onstop()
        {
            sw.Close();
        }

        public static void log_cmd(Client p, string command, params object[] args)
        {
            string text = "";
            switch (command)
            {
                #region economy
                case "pay":
                    string user = args[0].ToString();
                    string amount = args[1].ToString();
                    string state = args[2].ToString();
                    if (state == "1")
                    {
                        text = DateTime.Now.ToString() + ": /pay by " + p.name + " Empfänger: " + user + " Betrag: " + amount;
                    }
                    else if (state == "2")
                    {
                        text = DateTime.Now.ToString() + ": /pay by " + p.name + " Empfänger: " + user + " Betrag: " + amount + " Error: Zu wenig Geld";
                    }
                    else if (state == "3")
                    {
                        text = DateTime.Now.ToString() + ": /pay by " + p.name + " Empfänger: " + user + " Betrag: " + amount + " Error: Empfänger entfernt";
                    }
                    else if (state == "4")
                    {
                        text = DateTime.Now.ToString() + ": /pay by " + p.name + " Empfänger: " + user + " Betrag: " + amount + " Error: Empfänger nicht online";
                    }
                    break;

                case "bank":
                    string kntNR = args[0].ToString();
                    string state2 = args[1].ToString();
                    if (state2 == "1")
                    {
                        text = DateTime.Now.ToString() + ": /bank by " + p.name + " Konto: " + kntNR;
                    }
                    else if (state2 == "2")
                    {
                        text = DateTime.Now.ToString() + ": /bank by " + p.name + " Konto: " + kntNR + " Error: Konto nicht vorhanden";
                    }
                    else if (state2 == "3")
                    {
                        text = DateTime.Now.ToString() + ": /bank by " + p.name + " Konto: " + kntNR + " Error: Kein Zugriff";
                    }
                    else if (state2 == "4")
                    {
                        text = DateTime.Now.ToString() + ": /bank by " + p.name + " Konto: " + kntNR + " Error: Bei keiner Bank";
                    }
                    break;
                case "getbankaccounts":
                    string state3 = args[0].ToString();
                    if(state3 == "1")
                    {
                        text = DateTime.Now.ToString() + ": /getbankaccounts by " + p.name;
                    } else if(state3 == "2")
                    {
                        text = DateTime.Now.ToString() + ": /getbankaccounts by " + p.name + " Error: Bei keiner Bank";
                    }
                    break;
                case "createbankaccount":
                    string number = args[0].ToString();
                    string state4 = args[1].ToString();
                    if(state4 == "1")
                    {
                        text = DateTime.Now.ToString() + ": /createbankaccount by " + p.name + " Kontonummer: " + number;
                    }
                    else if (state4 == "2")
                    {
                        text = DateTime.Now.ToString() + ": /createbankaccount by " + p.name + " Error: Bei keiner Bank";
                    }
            
                    break;
                #endregion economy

                #region property
                case "savespawn":
                    string id = args[0].ToString();
                    string state5 = args[1].ToString();
                    if(state5 == "1")
                    {
                        text = DateTime.Now.ToString() + ": /savespawn by " + p.name + " PropID: " + id;
                    } else if(state5 == "2")
                    {
                        text = DateTime.Now.ToString() + ": /savespawn by " + p.name + " PropID: " + id + " Error: Immobilie gehört die nicht";
                    }
                    else if (state5 == "3")
                    {
                        text = DateTime.Now.ToString() + ": /savespawn by " + p.name  + " Error: Bei keiner Immobilie";

                    }

                    break;

                case "setPropOwner":
                    string no = args[0].ToString();
                    string ID = args[1].ToString();
                    string state_f = args[2].ToString();
                    string state_s = args[3].ToString();
                    string tx = "";
                    if(state_f == "2")
                    {
                        tx = " IM Mode";
                    } else
                    {
                        tx = " Normal Mode";
                    }
                    if(state_s == "1")
                    {
                        text = DateTime.Now.ToString() + ": /setPropOwner by " + p.name + " Prop ID: " + ID + " New Owner: " + no + tx;
                    } else if (state_s == "2")
                    {
                        text = DateTime.Now.ToString() + ": /setPropOwner by " + p.name + " Prop ID: " + ID + " New Owner: " + no + tx + " Error: Der NewOwner muss in der Nähe sein";
                    } else if (state_s == "3")
                    {
                        text = DateTime.Now.ToString() + ": /setPropOwner by " + p.name + " Prop ID: " + ID + " New Owner: " + no + tx + " Error: Du bist bei keiner Immobilie";
                    } 
                    break;
                    #endregion property
            }
            sw.WriteLine(text);
            sw.Flush();
        }
    }
}
