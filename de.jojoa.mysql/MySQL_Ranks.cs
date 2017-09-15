using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using MySql.Data.MySqlClient;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.mysql
{
    class MySQL_Ranks : Script
    {
        #region variables
        public static string conString = "SERVER=localhost;" + "DATABASE=realifegm;" + "UID=root;" + "PASSWORD=;";
        public static MySqlConnection con;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables
        #region registerPlayer
        public static void registerPlayer(Account name)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO RankData (Name, createprop, removeprop, god, fly , kick, pos,id) VALUES (@name,@cp,@rp,@god,@fly,@kick,@pos,@id)";
                con.Open();
                cmd.Parameters.AddWithValue("@name", name.name);
                cmd.Parameters.AddWithValue("@cp", 0);
                cmd.Parameters.AddWithValue("@rp", 0);
                cmd.Parameters.AddWithValue("@god", 0);
                cmd.Parameters.AddWithValue("@fly", 0);
                cmd.Parameters.AddWithValue("@kick", 0);
                cmd.Parameters.AddWithValue("@pos", 0);
                cmd.Parameters.AddWithValue("@id", name.id);
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }
        #endregion registerPlayer

      

        #region get
        public static String getString(Client p, string get)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
                cmd.Parameters.AddWithValue("@name", p.name);
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

        public static String getStringByName(string name, string get)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
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
                cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
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
                cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
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
        public static void set(Client p, string set, string whattoset)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE RankData SET " + set + "=@whattoset WHERE Name=@name";
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
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS RankData(Name VARCHAR(100),id int,createprop tinyint,removeprop tinyint,fly tinyint,god tinyint,kick tinyint,pos tinyint)";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        

        public static Dictionary<string,Boolean> getRank(string user)
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
            cmd.Parameters.AddWithValue("@name", user);

            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Dictionary<string, Boolean> dic = new Dictionary<string, bool>();
                dic.Add("createprop", reader.GetBoolean("createprop"));
                dic.Add("removeprop", reader.GetBoolean("removeprop"));
                dic.Add("fly", reader.GetBoolean("fly"));
                dic.Add("god", reader.GetBoolean("god"));
                dic.Add("kick", reader.GetBoolean("kick"));
                dic.Add("pos", reader.GetBoolean("pos"));
                reader.Close();
                con.Close();
                return dic;

            }
            
            reader.Close();
            con.Close();
            return null;
        }
        #endregion othermethods

    }
}