using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Server.Elements;
using VehicleInfoLoader;
using VehicleInfoLoader.Data;

namespace RealifeGM.de.jojoa.data
{
    class VehicleD
    {
        public Vehicle v;
        public Account owner;
        public VehicleManifest vm;
        public int id;

        public VehicleD(Vehicle v, Account owner,string i)
        {
            this.v = v;
            vm = VehicleInfo.Get(v);
            this.owner = owner;
            methods.getMethods.lvd.Add(this);
            id = Convert.ToInt32(i);
            
        }
    }
}
