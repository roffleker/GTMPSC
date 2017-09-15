using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_keys : Script
    {
        public player_keys()
        {
            API.onClientEventTrigger += API_onClientEventTrigger;
        }

        private void API_onClientEventTrigger(Client p, string eventName, params object[] arguments)
        {
            switch (eventName)
            {
                case "KeyL":
                    if (p.isInVehicle)
                    {

                    }
                    else if (methods.getMethods.getNextVehicle(p.position, methods.getMethods.getAccountByClient(p)) != null)
                    {
                        VehicleD vd = methods.getMethods.getNextVehicle(p.position, methods.getMethods.getAccountByClient(p));
                        if (API.getVehicleLocked(vd.v) == true)
                        {
                            API.setVehicleLocked(vd.v, false);
                            API.sendNotificationToPlayer(p, "Vehicle " + vd.v.numberPlate + " unlocked");
                        }
                        else
                        {
                            API.setVehicleLocked(vd.v, true);
                            API.sendNotificationToPlayer(p, "Vehicle " + vd.v.numberPlate + " locked");
                        }
                    }
                    break;
            }
        }

        private void onUpdate()
        { 
            
        }
    }
}
