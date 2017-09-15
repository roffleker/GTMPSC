using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.mysql
{
    class MySQL_POIData : Script
    {
        #region variables
        public static string conString = "SERVER=localhost;" + "DATABASE=realifegm;" + "UID=root;" + "PASSWORD=;";
        public static MySqlConnection con;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_POIData()
        {
            
        }

        public static void loadPOI()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData";
            con.Open();
            reader = cmd.ExecuteReader();
          
            while (reader.Read())
            {
                Vector3 pos = new Vector3();
                pos.X = reader.GetFloat("X");
                pos.Y = reader.GetFloat("Y");
                pos.Z = reader.GetFloat("Z");
                string type = reader.GetString("Type");
                API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
                API.shared.createTextLabel(type, pos.Add(new Vector3(0, 2, 0)), 5, 10);
            }
        }

        public static List<Vector3> getBanks()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE Type=";
            cmd.Parameters.AddWithValue("@type", "Bank");
            con.Open();
            reader = cmd.ExecuteReader();
            List <Vector3> list = new List<Vector3>();
            while(reader.Read())
            {
                Vector3 pos = new Vector3();
                pos.X = reader.GetFloat("X");
                pos.Y = reader.GetFloat("Y");
                pos.Z = reader.GetFloat("Z");
                list.Add(pos);
            }
            return list;
        }
        
        public static void createTable()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS POIData (Type VARCHAR(100), X double, Y double, Z double, arg1 VARCHAR(100), id int AUTO_INCREMENT)";
            con.Open();
            cmd.ExecuteNonQuery();

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS ShopData (X double, Y double, Z double, SPX double, SPY double, SPZ double, RTX double, RTY double, RTZ double , id int AUTO_INCREMENT)";
            con.Open();
            cmd.ExecuteNonQuery();

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS ShopDataItems (Class VARCHAR(100), name VARCHAR(100), price int, id int AUTO_INCREMENT)";
            con.Open();
            cmd.ExecuteNonQuery();

        }

        public static void addBank(Vector3 pos)
        {
            createTable();
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO POIData (Type,X,Y,Z) VALUES (@type,@x,@y,@z)";
            cmd.Parameters.AddWithValue("@type", "Bank");
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
            API.shared.createTextLabel("Bank", pos.Add(new Vector3(0, 2, 0)), 5, 10);
        }

        public static List<Vector3> getShops()
        {
            createTable();
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE Type=@type";
            cmd.Parameters.AddWithValue("@type", "Shop");
            con.Open();
            reader = cmd.ExecuteReader();
            List<Vector3> list = new List<Vector3>();
            while (reader.Read())
            {
                Vector3 pos = new Vector3();
                pos.X = reader.GetFloat("X");
                pos.Y = reader.GetFloat("Y");
                pos.Z = reader.GetFloat("Z");
                list.Add(pos);
            }
            return list;
        }
        public static List<string> getShopItems(Vector3 pos)
        {
            createTable();
            List<string> ls = new List<string>();
            string clas = "";
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE X=@x";
            cmd.Parameters.AddWithValue("@x", pos.X);
            con.Open();
            reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                clas = reader.GetString("arg1");
            }

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopDataItems WHERE Class=@clas";
            cmd.Parameters.AddWithValue("@clas", clas);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ls.Add(reader.GetString("name") + ":" + reader.GetInt16("price"));
            }
            return ls;
        }

        public static Vector3 getShopSpawn(Vector3 pos)
        {
            createTable();
            Vector3 posSP = new Vector3(0, 0, 0);

           

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE X=@x";
            cmd.Parameters.AddWithValue("@x", pos.X);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                posSP.X = reader.GetFloat("SPX");
                    posSP.Y = reader.GetFloat("SPY");
                    posSP.Z = reader.GetFloat("SPZ");
            }

            return posSP;
        }

        public static Vector3 getShopSpawnRot(Vector3 pos)
        {
            createTable();
            Vector3 posSP = new Vector3(0, 0, 0);

            

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE X=@x";
            cmd.Parameters.AddWithValue("@x", pos.X);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                posSP.X = reader.GetFloat("RTX");
                posSP.Y = reader.GetFloat("RTY");
                posSP.Z = reader.GetFloat("RTZ");
            }

            return posSP;
        }

        public static int addShop(Vector3 pos, string clas)
        {
            createTable();
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO POIData (Type,X,Y,Z,arg1) VALUES (@type,@x,@y,@z,@clas)";
            cmd.Parameters.AddWithValue("@type", "Shop");
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            cmd.Parameters.AddWithValue("@clas", clas);
            con.Open();
            cmd.ExecuteNonQuery();
            API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
            API.shared.createTextLabel("Shop", pos.Add(new Vector3(0, 2, 0)), 5, 10);
            return Convert.ToInt32(cmd.LastInsertedId);
        }

        public static void setSpawn(Vector3 pos, Vector3 rot, int id)
        {
            Vector3 p = null;
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                p = new Vector3(0, 0, 0);
                p.X = reader.GetFloat("X");
                p.Y = reader.GetFloat("Y");
                p.Z = reader.GetFloat("Z");
            }

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO ShopData (X,Y,Z,SPX,SPY,SPZ,RTX,RTY,RTZ) VALUES (@x,@y,@z,@spx,@spy,@spz,@rtx,@rty,@rtz)";
            cmd.Parameters.AddWithValue("@type", "Shop");
            cmd.Parameters.AddWithValue("@x", p.X);
            cmd.Parameters.AddWithValue("@y", p.Y);
            cmd.Parameters.AddWithValue("@z", p.Z);
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            cmd.Parameters.AddWithValue("@x", rot.X);
            cmd.Parameters.AddWithValue("@y", rot.Y);
            cmd.Parameters.AddWithValue("@z", rot.Z);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }


    }
}
