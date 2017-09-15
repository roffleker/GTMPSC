using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;


namespace RaceManager
{
    
   
    public class RaceM : Script
    {
        Boolean debug = true;

        struct race_struct
        {
           public List<ColShape> listcs;
           public List<Client> listpl1;
           public List<Client> listpl2;
           public List<Client> listpl3;
           public List<Vector3> listpos;
           public List<String> listtime;
           public Client creator;
           public int zeit;
           public Boolean RaceS;
           public ColShape finalcs;
           public int place;
         
           public string da;
        }
        public MySqlCommand command;

        public MySqlConnection connection;
        List<race_struct> lst_rs = new List<race_struct>();

        string myConnectionString = "SERVER=localhost;" +
                            "DATABASE=racedb;" +
                            "UID=root;" +
                            "PASSWORD=;";

        

        public RaceM()
        {
            API.onClientEventTrigger += OnClientEvent;
            API.onEntityEnterColShape += OnEntityEnterColShapeHandler;
            connection = new MySqlConnection(myConnectionString);
            
        }
        

        public void OnClientEvent(Client player, string eventName, params object[] arguments)
        {
            if (eventName == "btnd") { }
            
        }

        [Command("racecreate", Alias="racec")]
        public void rc(Client player)
        {
            race_struct rs = new race_struct();
            lst_rs.Add(rs);
            rs.listcs = new List<ColShape>();
            rs.listpl1 = new List<Client>();
            rs.listpl2 = new List<Client>();
            rs.listpl3 = new List<Client>();
            rs.listpos = new List<Vector3>();
            rs.listtime = new List<string>();
            rs.place = 1;
            rs.RaceS = false;
            rs.zeit = 0;
            
            StringBuilder date = new StringBuilder();
            date.Append(DateTime.Now.Year);
            date.Append(DateTime.Now.Month);
            date.Append(DateTime.Now.Day);
            date.Append(DateTime.Now.Hour);
            date.Append(DateTime.Now.Second);
            rs.da = date.ToString();
            // command = connection.CreateCommand();
          //  command.CommandText = "CREATE TABLE `racedb`.`race" + da + "_n` ( `name` VARCHAR(255) NOT NULL , PRIMARY KEY (`name`)) ENGINE = InnoDB;";
            // connection.Open();
            //command.ExecuteNonQuery();
            //command.Connection.Close();
            rs.creator = player;
            API.sendChatMessageToPlayer(player, "Es wurde erfolgreich ein Rennen erstellt");
        }

        public void OnEntityEnterColShapeHandler(ColShape shape, NetHandle entity)
        {
            race_struct rs = new race_struct();
                rs.creator = null;
            foreach(race_struct rs2 in lst_rs)
            {
                if (rs2.listcs.Contains(shape)) {
                    rs = rs2;
                }
            }
            if (rs.creator == null) {
                API.sendChatMessageToPlayer(API.getPlayerFromHandle(entity), "Fehler: Colshape not found");
            }
            else {
                int index = rs.listcs.IndexOf(shape) + 1;
                int time = rs.zeit / 100;
                if (rs.listpl2.Contains(API.getPlayerFromHandle(entity)))
                {
                    if (shape == rs.finalcs)
                    {
                        foreach (Client play in rs.listpl2)
                        {
                            API.sendNotificationToPlayer(play, API.getPlayerFromHandle(entity).name + " hat das Rennen als " + rs.place + ". beendet!");
                        }
                        API.sendNotificationToPlayer(API.getPlayerFromHandle(entity), "Du hast das Rennen beedent dein Zeit ist: " + rs.zeit / 100);
                        rs.listtime.Add(API.getPlayerFromHandle(entity).name + ":" + rs.zeit / 100);
                        rs.listpl2.Remove(API.getPlayerFromHandle(entity));
                        rs.listpl3.Add(API.getPlayerFromHandle(entity));
                        rs.place++;
                        int index2 = rs.listcs.Count - 1;
                        while (index2 > -1)
                        {

                            API.triggerClientEvent(API.getPlayerFromHandle(entity), "racerp", index2);
                            index2 -= 1;
                        };

                        if (rs.listpl2.Count == 0)
                        {
                            rs.RaceS = false;

                            rs.listpos.Clear();
                            int index3 = rs.listcs.Count - 1;
                            while (index3 > -1)
                            {
                                API.deleteColShape(rs.listcs.ElementAt(index3));
                                index3 -= 1;
                            };

                            rs.listpl1.Clear();
                            rs.listpl2.Clear();
                            rs.place = 1;

                            foreach (Client player in rs.listpl3) {
                                API.triggerClientEvent(player, "ergebniss", rs.listtime);

                            };
                            rs.listcs.Clear();

                            rs.listtime.Clear();
                        }

                    }
                    else
                    {
                        //API.sendNotificationToPlayer(API.getPlayerFromHandle(entity), "Checkpoint:" + index + ":" + time);
                        //zeit in Datenbank

                       // command = connection.CreateCommand();
                       // command.CommandText = "INSERT INTO `race" + da + "_cp" + index + "` (`name`, `time`) VALUES ('" + API.getPlayerFromHandle(entity).name + "', '" + time + "')";
                       // connection.Open();
                       // command.ExecuteNonQuery();
                       // command.Connection.Close();

                        API.triggerClientEvent(API.getPlayerFromHandle(entity), "racep", index,time);
                    }

                }
            }
        }

        [Command("raceinvite","Usage: /raceinvite [player]", Alias = "racei")]
        public void RaceSync(Client player,string user) {
            race_struct rs = new race_struct();
            rs.creator = null;
            foreach(race_struct rsI in lst_rs)
            {
                if(rsI.creator == player)
                {
                    rs = rsI;
                }
            }
            if (rs.creator == null)
            {
                API.sendChatMessageToPlayer(player, "Du hast kein Rennen erstellt.");
            }
            else
            {
               
                API.sendNotificationToPlayer(player, "Du wurdest zum Rennen eingeladen.");
                rs.listpl1.Add(API.getPlayerFromName(user));

            }
          



        }

        [Command("raceaccept")]
        public void Raceaccept(Client player)
        {
            race_struct rs = new race_struct();
                rs.creator = null;
            foreach (race_struct rsI in lst_rs)
            {
                if (rsI.listpl1.Contains(player))
                {
                    rs = rsI;
                }
            }
            if (rs.creator == null)
            {
                API.sendChatMessageToPlayer(player, "Du wurdest nicht eingeladen.");
            }
            
                foreach (Vector3 point in rs.listpos)
                {
                    API.triggerClientEvent(player, "racesp", point);
                }
                rs.listpl2.Add(player);
                rs.listpl1.Remove(player);

               // command = connection.CreateCommand();
               // command.CommandText = "INSERT INTO `race"+da+"_n` (`name`) VALUES ('"+player.name+"')";
               // connection.Open();
               // command.ExecuteNonQuery();
               // command.Connection.Close();

                API.sendNotificationToPlayer(rs.creator, player.name + " ist dem Rennen beigetreten");


            



        }

        [Command("raceadd", Alias = "racea")]
        public void RacePadd(Client player)
        {

            race_struct rs = new race_struct();
            rs.creator = null;
            foreach (race_struct rsI in lst_rs)
            {
                if (rsI.creator == player)
                {
                    rs = rsI;
                }
            }
            if (rs.creator == null)
            {
                API.sendChatMessageToPlayer(player, "Du hast kein Rennen erstellt.");
            }
            else
            {
                Client pl = player;
                API.triggerClientEvent(pl, "racecp");
                ColShape cs = API.createCylinderColShape(pl.position, 10, 5);
                rs.listcs.Add(cs);
                int i = rs.listcs.IndexOf(cs) + 1;
              //  command = connection.CreateCommand();
               // command.CommandText = "CREATE TABLE `racedb`.`race" + da + "_cp" + i + "` ( `name` VARCHAR(255) NOT NULL , `time` INT NOT NULL , PRIMARY KEY (`name`)) ENGINE = InnoDB;";
                //connection.Open();
                //command.ExecuteNonQuery();
                //command.Connection.Close();

                API.sendNotificationToPlayer(player, "Checkpoint erfolgreich erstellt");


            }


        }

        [Command("raceremove", Alias = "racer")]
        public void RacePrem(Client player)
        {
            race_struct rs = new race_struct();
            rs.creator = null;
            foreach (race_struct rsI in lst_rs)
            {
                if (rsI.creator == player)
                {
                    rs = rsI;
                }
            }
            if (rs.creator == null)
            {
                API.sendChatMessageToPlayer(player, "Du hast kein Rennen erstellt.");
            }
            else
            {
                Client pl = player;
                int index = 0;
                foreach (Vector3 pos in rs.listpos)
                {

                    if (pos.DistanceTo2D(pl.position) < 10)
                    {
                        index = rs.listpos.IndexOf(pos);
                        break;
                    }
                }

                ColShape cs = rs.listcs.ElementAt(index);
                API.triggerClientEvent(pl, "racerp", index);
                API.deleteColShape(cs);
                int i = rs.listcs.IndexOf(cs) + 1;
                //command = connection.CreateCommand();
                //command.CommandText = "DROP TABLE IF EXISTS race" + da + "_cp" + i + ";";
                //connection.Open();
                //command.ExecuteNonQuery();
                //command.Connection.Close();

                API.sendNotificationToPlayer(player, "Checkpoint erfolgreich gelöscht");


            }


        }

        [Command("racestart", Alias = "races")]
        public void Racestart(Client player)
        {
            race_struct rs = new race_struct();
            rs.creator = null;
            foreach (race_struct rsI in lst_rs)
            {
                if (rsI.creator == player)
                {
                    rs = rsI;
                }
            }
            if (rs.creator == null)
            {
                API.sendChatMessageToPlayer(player, "Du hast kein Rennen erstellt.");
            }
            else
            {

                Client pl = player;
                rs.finalcs = rs.listcs.ElementAt(rs.listcs.Count - 1);

               // command = connection.CreateCommand();
               // command.CommandText = "INSERT INTO `races` (`date`, `CP`, `RaceName`) VALUES ('" + da + "', '" + listcs.Count + "', 'race" + da + "');";
               // connection.Open();
               // command.ExecuteNonQuery();
               // command.Connection.Close();

                rs.RaceS = true;
               
                foreach (Client pla in rs.listpl2)
                {
                    API.triggerClientEvent(pla, "racest");
                    pla.freeze(true);
                };
                int i = 10;
                while (i > 0)
                {
                    foreach (Client play in rs.listpl2)
                    {
                        API.sendNotificationToPlayer(play, "Noch " + i + " Sekunden!", true);
                    }

                    i--;
                }
                foreach (Client pla in rs.listpl2)
                {
                    pla.freeze(false);
                };



            }

        }

        [Command("raceclear", Alias = "racec")]
        public void Racestop(Client player)
        {
            race_struct rs = new race_struct();
            rs.creator = null;
            foreach (race_struct rsI in lst_rs)
            {
                if (rsI.creator == player)
                {
                    rs = rsI;
                }
            }
            if (rs.creator == null)
            {
                API.sendChatMessageToPlayer(player, "Du hast kein Rennen erstellt.");
            }
            else
            {

                Client pl = player;
                rs.RaceS = false;
                //stoppen
                //alle punkte löschen
                rs.listpos.Clear();
                int index = rs.listcs.Count - 1;
                while (index < -1)
                {
                    API.deleteColShape(rs.listcs.ElementAt(index));
                    foreach (Client player2 in rs.listpl2)
                    {
                        API.triggerClientEvent(player2, "racerp", index);
                    }
                };

                rs.listpl1.Clear();
                rs.listpl2.Clear();

                
            }

        }

        public void timer()
        {
            race_struct rs;
            while (true)
                {
                

                for (int i = lst_rs.Count;i > -1; i--)
                {
                     rs = lst_rs[i];
                    if (rs.RaceS == true)
                    {
                        rs.zeit += 1;
                    }
                }

                API.sleep(10);
  
                }


        }
    };
    

}

