using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using MySql.Data.MySqlClient;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.mysql
{
    class MySQL_PlayerData : Script
    {
        #region variables
        public static string conString = "SERVER=localhost;" + "DATABASE=realifegm;" + "UID=root;" + "PASSWORD=;";
        public static MySqlConnection con;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        #region registerPlayer
        public static void registerPlayer(Client p , string hash)
        {
            if(isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO PlayerData (Name, Password, lastIP, level, money, skin, tutorial,spawnID) VALUES (@name, @hash, @ip, @level, @money, @skin, @tutorial,@spawn)";
                con.Open();
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@ip", p.address);
                cmd.Parameters.AddWithValue("@level",1);
                cmd.Parameters.AddWithValue("@money", 5000);
                cmd.Parameters.AddWithValue("@skin", "null");
                cmd.Parameters.AddWithValue("@tutorial", false);
                cmd.Parameters.AddWithValue("@spawn", "newbie");
                cmd.ExecuteNonQuery();
                con.Close();

            }
        
        }
        #endregion registerPlayer

        #region playerExists
        public static Boolean playerExists(Client p)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM PlayerData";
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader.GetString("Name");
                    if (name == p.name)
                    {
                        con.Close();
                        reader.Close();
                        return true;
                    }
                }
                con.Close();
                reader.Close();
            }    
            
            return false;

        }
        #endregion playerExists

        #region get
        public static String getString(Client p, string get)
        {
            if(isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM PlayerData WHERE Name=@name";
                cmd.Parameters.AddWithValue("@name", p.name);
                con.Open();
                reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    string getted = reader.GetString(get);
                    return getted;
                }
                reader.Close();
                con.Close();
            }
            return null;
        }

        public static String getStringByName(string name, string get)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM PlayerData WHERE Name=@name";
                cmd.Parameters.AddWithValue("@name", name);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string getted = reader.GetString(get);
                    return getted;
                }
                reader.Close();
                con.Close();
            }
            return null;
        }

        public static int getInt(Client p, string get)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM PlayerData WHERE Name=@name";
                cmd.Parameters.AddWithValue("@name", p.name);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int getted = reader.GetInt16(get);
                    return getted;
                }
                reader.Close();
                con.Close();
            }
            return 0;
        }

        public static int getIntByName(string name, string get)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM PlayerData WHERE Name=@name";
                cmd.Parameters.AddWithValue("@name", name);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int getted = reader.GetInt16(get);
                    return getted;
                }
                reader.Close();
                con.Close();
            }
            return 0;
        }
        #endregion get

        #region set
        public static void set(Client p, string set,string whattoset)
        {
            if(isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE PlayerData SET " + set + "=@whattoset WHERE Name=@name";
                cmd.Parameters.AddWithValue("@whattoset", whattoset);
                cmd.Parameters.AddWithValue("@name", p.name);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        #endregion set

        #region othermethods
        public static Boolean isTableCreated()
        {
            try
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS PlayerData(Name VARCHAR(100), Password VARCHAR(100), lastIP VARCHAR(100), level int, money int, skin VARCHAR(100),tutorial VARCHAR(10),spawnID int,id int AUTO_INCREMENT)";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        public static void updateDatas(Client p)
        {
            set(p, "lastIP", p.address);

        }

        public static void loadAccounts()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM PlayerData";
           
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string name = reader.GetString("Name");
                Account a = new Account(name);
               
                
            }
            reader.Close();
            con.Close();
        }
        #endregion othermethods

    }
}
