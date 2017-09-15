using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_register : Script
    {
        public player_register()
        {
            API.onClientEventTrigger += onClientTriggerd;
        }
        #region events
        public void onClientTriggerd(Client p, string eventName, params object[] arguments)
        {
            switch(eventName) {
                case "checkRegister":
                    for (int i = 0; i < 50; i++)
                    {
                        API.sendChatMessageToPlayer(p, "  ");
                    }
                    API.sendChatMessageToPlayer(p, methods.stringMethods.server_success_getted_data);

                    if (mysql.MySQL_PlayerData.playerExists(p))
                    {
                        API.triggerClientEvent(p, "wrong", 1);
                    }
                    else
                    {
                        if(arguments[0] == arguments[1])
                        {
                            string password = arguments[0].ToString();
                            MD5 md5 = new MD5CryptoServiceProvider();
                            byte[] textToHash = Encoding.Default.GetBytes(password);
                            byte[] result = md5.ComputeHash(textToHash);

                            
                            string hashedpw = System.BitConverter.ToString(result);
                            mysql.MySQL_PlayerData.registerPlayer(p, hashedpw);
                            Account a = new Account(p.name);
                            mysql.MySQL_Ranks.registerPlayer(a);

                            API.sendChatMessageToPlayer(p, methods.stringMethods.success_register);
                            API.consoleOutput(methods.stringMethods.console_register.Replace("%name%", p.name));
                          
                            a.setClient(p);



                            API.triggerClientEvent(p, "cefconnect",0);
                        }
                        else
                        {
                            API.triggerClientEvent(p, "wrong", 1);

                        }
                    }

                    break;
            }
        }
        #endregion events
    }
}
