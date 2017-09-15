using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_vehicle : Script
    {
        public player_vehicle()
        {
            API.onPlayerEnterVehicle += API_onPlayerEnterVehicle;
        }

        private void API_onPlayerEnterVehicle(Client player, NetHandle vehicle)
        {
            Vehicle v = API.getEntityFromHandle<Vehicle>(vehicle);
            VehicleD vd = methods.getMethods.getVehiclebyVehicle(v);
            
        }
    }
}
