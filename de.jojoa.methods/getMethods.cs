using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.methods
{
    class getMethods
    {
        public static List<Account> lacc = new List<Account>();
        public static List<Bank_Account> lba = new List<Bank_Account>();
        public static List<VehicleD> lvd = new List<VehicleD>();
        public getMethods()
        {

        }
        public static Account getAccountByClient(Client p)
        {
            foreach (Account acc in lacc)
            {
                if (acc.p == p)
                {
                    return acc;
                }
            }
            return null;
        }
        public static Account getAccountByName(string user)
        {
            foreach (Account acc in lacc)
            {
                if (acc.name == user)
                {
                    return acc;
                }
            }
            return null;
        }

        public static Property getPropertyByPos(Vector3 pos)
        {
            foreach (Property prop in mysql.MySQL_PropertyData.getAll())
            {
                if (prop.pos.DistanceTo(pos) < 2)
                {
                    return prop;

                }

            }
            return null;
        }
        public static Property getPropertyByID(string id)
        {
            foreach (Property prop in mysql.MySQL_PropertyData.getAll())
            {
                if (prop.ID == id)
                {
                    return prop;

                }

            }
            return null;
        }

        public static Bank_Account getBankByNumber(int NR)
        {
            
            foreach(Bank_Account b in lba)
            {
                if(b.number == NR)
                {
                    return b;
                }
            }
           
            return null;
        }
        public static List<Bank_Account> getBanksByUser(Account user)
        {
            List <Bank_Account> lb = new List<Bank_Account>();
            foreach (Bank_Account b in lba)
            {
                if (b.owner == user)
                {
                    lb.Add(b);
                }
            }

            return lb;
        }

        public static VehicleD getVehiclebyVehicle(Vehicle v)
        {
            foreach (VehicleD vd in lvd)
            {
                if (vd.v == v)
                {
                    return vd;
                }
            }

            return null;
        }
        public static VehicleD getNextVehicle(Vector3 pos,Account owne)
        {
            VehicleD vdd = null;
            foreach(VehicleD vd in lvd)
            {
                if(vd.v.position.DistanceTo(pos) < 10 && vd.owner == owne)
                {
                    vdd = vd;
                }
                
            }
            return vdd;
        }
    }
}
