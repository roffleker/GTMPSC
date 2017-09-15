using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_chat : Script
    {
        public player_chat()
        {
            API.onChatMessage += onPlayerChat;
        }
        #region events
        public void onPlayerChat(Client p,string message,CancelEventArgs e)
        {
            if(API.getEntityData(p,"status")!="INGAME")
            {
                e.Cancel = true;
            }
        }
        #endregion events
    }

}
