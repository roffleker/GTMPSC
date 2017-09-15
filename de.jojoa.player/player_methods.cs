using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
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
    class player_methods : Script
    {
        public player_methods()
        {
            API.onClientEventTrigger += API_onClientEventTrigger;
        }

        public void API_onClientEventTrigger(Client p, string eventName, params object[] arguments)
        {
            switch(eventName)
            {
                case "setSkin":
                    mysql.MySQL_PlayerData.set(p,"skin",arguments[0].ToString());
                    PedHash pedhash = API.pedNameToModel(arguments[0].ToString());
                    API.setPlayerSkin(p, pedhash);
                    API.setEntityDimension(p, 0);
                    API.setEntityInvincible(p, false);
                    p.freeze(false);
                    API.triggerClientEvent(p, "resetCamera");
                    API.sendChatMessageToPlayer(p, methods.stringMethods.success_skin);
                    API.setEntityData(p, "status", "INGAME");
                    break;
                case "Einzahlen":
                    Bank_Account ba = methods.getMethods.getBankByNumber(Convert.ToInt32(arguments[0].ToString()));
                    string amount = arguments[1].ToString();
                    int am = Convert.ToInt32(amount);
                    Account a = methods.getMethods.getAccountByClient(p);
                    if(a.money >= am)
                    {
                        ba.money += am;
                        a.money -= am;
                    } else
                    {
                        API.sendChatMessageToPlayer(p, "Du hast zu wenig Geld zum Einzahlen");
                    }
                    break;

                case "Auszahlen":
                    Bank_Account ba2 = methods.getMethods.getBankByNumber(Convert.ToInt32(arguments[0].ToString()));
                    string amount2 = arguments[0].ToString();
                    int am2 = Convert.ToInt32(amount2);
                    Account a2 = methods.getMethods.getAccountByClient(p);
                    if (ba2.money >= am2)
                    {
                        ba2.money -= am2;
                        a2.money += am2;
                    }
                    else
                    {
                        API.sendChatMessageToPlayer(p, "Du hast zu wenig Geld zum Auszahlen");
                    }
                    break;
                case "Ueberweisen":
                    Bank_Account ba3 = methods.getMethods.getBankByNumber(Convert.ToInt32(arguments[0].ToString()));
                    Bank_Account empf = methods.getMethods.getBankByNumber(Convert.ToInt32(arguments[2].ToString()));
                    int amoun = Convert.ToInt16(arguments[1].ToString());
                    if(empf != null)
                    {
                        if (ba3.money >= amoun)
                        {
                            ba3.money -= amoun;
                            empf.money += amoun;
                        }
                        else
                        {
                            API.sendChatMessageToPlayer(p, "Dein Bankkonto hat nicht genung Geld");
                        }
                    } else
                    {
                        API.sendChatMessageToPlayer(p, "Dieses Konto gibt es nicht");
                    }
                    break;
                case "VehicleBuy":
                    Account a3 = methods.getMethods.getAccountByClient(p);
                    char te = ':';
                    string car = arguments[0].ToString();
                    string name = car.Split(te).First();
                    int price = Convert.ToInt32(car.Split(te).Last());
                    Vector3 carSP = mysql.MySQL_POIData.getShopSpawn(p.position);
                    Vector3 carRT = mysql.MySQL_POIData.getShopSpawnRot(p.position);
                    if(a3.money >= price)
                    {
                        a3.money -= price;
                        mysql.MySQL_Vehicles.createVehicle(a3, car, carSP, carRT);
                    }
                    break;
                    
            }
        }
    }
}
