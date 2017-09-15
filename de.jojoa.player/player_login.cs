using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_login : Script
    {
        public player_login()
        {
            
            API.onClientEventTrigger += onClientTriggered;
            
        }
        #region events
       
        public void onClientTriggered(Client p,string eventName, params object[] arguments)
        {
            switch (eventName)
            {
                case "checkLogin":
                    for (int i = 0; i < 50; i++)
                    {
                        API.sendChatMessageToPlayer(p, "  ");
                    }
                    API.sendChatMessageToPlayer(p, methods.stringMethods.server_success_getted_data);

                    if(mysql.MySQL_PlayerData.playerExists(p))
                    {
                        string hashedpw_stored = mysql.MySQL_PlayerData.getString(p, "Password");

                        MD5 md5 = new MD5CryptoServiceProvider();
                        byte[] textToHash = Encoding.Default.GetBytes(arguments[0].ToString());
                        byte[] result = md5.ComputeHash(textToHash);


                        string hashedpw_input = System.BitConverter.ToString(result);
                        if (hashedpw_input == hashedpw_stored)
                        {
                            for (int i = 0; i < 50; i++)
                            {
                                API.sendChatMessageToPlayer(p, "  ");
                            }
                            API.sendChatMessageToPlayer(p, methods.stringMethods.success_login);
                            API.consoleOutput(methods.stringMethods.console_login.Replace("%name%", p.name));
                            

                            mysql.MySQL_PlayerData.updateDatas(p);

                            if(mysql.MySQL_PlayerData.getString(p,"skin") == "null")
                            {
                                PedHash pedhash = API.pedNameToModel("Abigail");
                                API.setPlayerSkin(p, pedhash);


                                API.triggerClientEvent(p, "setCamera", API.getEntityPosition(p).Subtract(new Vector3(0, 3, 0)));
                                p.freeze(true);
                                API.setEntityInvincible(p, true);
                                API.triggerClientEvent(p, "skinMenu");
                                API.setEntityDimension(p, GetRandomNumber(50, 50000));
                                foreach (string msg in methods.stringMethods.choose_skin)
                                {
                                    API.sendChatMessageToPlayer(p, msg);
                                }

                            } else
                            {
                                PedHash pedhash = API.pedNameToModel(mysql.MySQL_PlayerData.getString(p, "skin"));
                                API.setPlayerSkin(p, pedhash);

                                Account a = methods.getMethods.getAccountByName(p.name);
                                a.setClient(p);
                                API.triggerClientEvent(p, "resetCamera");
                                API.setEntityData(p, "status", "INGAME");
                                string spawn = a.spawn;
                                Vector3 spawn_pos;
                                
                                spawn_pos = new Vector3(0, 0, 0);
                                if (spawn != "newbie")
                                {
                                    spawn_pos = methods.getMethods.getPropertyByID(spawn).pos;
                                    p.position = spawn_pos;
                                }
                                


                            }


                        }
                        else
                        {
                            API.triggerClientEvent(p, "wrong", 0);
                        }
                    }
                    else
                    {
                        API.triggerClientEvent(p, "wrong", 0);
                    }
                    break;
            }

        }



        #endregion events

        #region constructors
        public int GetRandomNumber(int min,int max)
        {
            Random random = new Random();
                return random.Next(min, max);
        }

        #endregion constructors

    }

}
