using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Shared.Math;

namespace RealifeGM.de.jojoa.data
{
    class Account
    {
        public Dictionary<string, Boolean> rank = new Dictionary<string, Boolean>();
        public string spawn;
        public int id;
        public Client p;
        public int money;
        public string name;
        string skin;
       public List<Property> props = new List<Property>();
        List<Vehicle> cars = new List<Vehicle>();
       
        public List<int> jobids;
        public Account(string nameI)
        {
            name = nameI;
            id = mysql.MySQL_PlayerData.getIntByName(name, "id");
            rank = mysql.MySQL_Ranks.getRank(name);
            money = mysql.MySQL_PlayerData.getIntByName(name, "money");
            skin = mysql.MySQL_PlayerData.getStringByName(name, "skin");
            spawn = mysql.MySQL_PlayerData.getStringByName(name, "spawnID");
            de.jojoa.methods.getMethods.lacc.Add(this);
        }
        public Account(string nameI,int state)
        {
            name = nameI;
        }
        public double multiplyer = 1;

        #region property
        public void setClient(Client player)
        {
            p = player;
        }
        public void addProp(Property prop)
        {
            props.Add(prop);
            MPcalc();
        }
        public void remProp(Property prop)
        {
            props.Remove(prop);
            MPcalc();
        }
        #endregion property

        
        public void MPcalc()
        {
            int type = 0;
            foreach (Property prop in props)
            {
                if (type < prop.type)
                {
                    type = prop.type;
                }
            }
            switch (type)
            {
                case 1:
                    multiplyer = 0.8;
                    break;
                case 2:
                    multiplyer = 1.0;
                    break;
                case 3:
                    multiplyer = 1.3;
                    break;
                case 4:
                    multiplyer = 1.5;
                    break;
                case 5:
                    multiplyer = 1.7;
                    break;
                case 6:
                    multiplyer = 2.0;
                    break;
            }
        }
        #region Vehicle
        public void addVeh(Vehicle car)
        {
            cars.Add(car);
        }
        public void remVeh(Vehicle car)
        {
            cars.Remove(car);
        }
        #endregion Vehicle
        
    }
}
