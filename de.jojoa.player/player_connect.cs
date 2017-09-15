using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_connect : Script
    {
        public player_connect()
        {
            
            API.onPlayerFinishedDownload += onPlayerFinishedDownload;
        }

        #region events
        public void onPlayerFinishedDownload(Client p)
        {
            API.triggerClientEvent(p, "interpolateCamera",methods.locationMethods.loc_vinewood, methods.locationMethods.loc_unknown,1000000);
            API.setEntityData(p, "status", "CONNECTING");
            if (de.jojoa.mysql.MySQL_PlayerData.playerExists(p))
            {
                for (int i = 0; i < 50; i++)
                {
                    API.sendChatMessageToPlayer(p, "  ");
                }

                foreach (String msg in de.jojoa.methods.stringMethods.login_msg)
                {
                    API.sendChatMessageToPlayer(p, msg.Replace("%name%", p.name));
                }
                API.triggerClientEvent(p, "cefconnect", 0);
            }
            else
            {
                for (int i = 0; i < 50; i++)
                {
                    API.sendChatMessageToPlayer(p, "  ");
                }

                foreach (String msg in de.jojoa.methods.stringMethods.register_msg)
                {
                    API.sendChatMessageToPlayer(p, msg.Replace("%name%", p.name));
                }
                API.triggerClientEvent(p, "cefconnect", 1);
            }
        }

        #endregion events
    }
}
