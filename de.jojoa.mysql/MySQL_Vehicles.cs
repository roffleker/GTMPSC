using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
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
    class MySQL_Vehicles : Script
    {
        #region variables
        public static string conString = "SERVER=localhost;" + "DATABASE=realifegm;" + "UID=root;" + "PASSWORD=;";
        public static MySqlConnection con;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables
        public static VehicleD createVehicle(Account owner,string model,Vector3 pos,Vector3 rot)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO VehicleData (Name,model,color1,color2,spawnX,spawnY,spawnZ,spawnRX,spawnRY,spawnRZ) VALUES (@name,@model,@color1,@color2,@spawnX,@spawnY,@spawnZ,@spawnRX,@spawnRY,@spawnRZ)";
                con.Open();
                cmd.Parameters.AddWithValue("@name", owner.name);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@color1", 4);
                cmd.Parameters.AddWithValue("@color2", 12);
                cmd.Parameters.AddWithValue("@spawnX", pos.X);
                cmd.Parameters.AddWithValue("@spawnY", pos.Y);
                cmd.Parameters.AddWithValue("@spawnZ", pos.Z);

                cmd.Parameters.AddWithValue("@spawnRX", rot.X);
                cmd.Parameters.AddWithValue("@spawnRY", rot.Y);
                cmd.Parameters.AddWithValue("@spawnRZ", rot.Z);
                VehicleHash vh = API.shared.vehicleNameToModel(model);

                Vehicle v = API.shared.createVehicle(vh, pos, rot, 4, 12);
                v.numberPlate = "LS" + cmd.LastInsertedId.ToString("D4");
                VehicleD vd = new VehicleD(v, owner, cmd.LastInsertedId.ToString());
               
                cmd.ExecuteNonQuery();
                con.Close();
                return vd;

            }
            return null;
        }

        public static Boolean isTableCreated()
        {
            try
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS VehicleData (Name VARCHAR(100),model VARCHAR(100), color1 int,color2 int, spawnX double , spawnY double , spawnZ double, id int AUTO_INCREMENT)";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void setSpawn(VehicleD vd)
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE VehicleData SET (spawnX, spawnY, spawnZ,spawnRX,spawnRY,spawnRZ) VALUES (@x,@y,@z,@rx,@ry,@rz) WHERE id=@id";
                cmd.Parameters.AddWithValue("@x", vd.v.position.X);
                cmd.Parameters.AddWithValue("@y", vd.v.position.Y);
                cmd.Parameters.AddWithValue("@z", vd.v.position.Z);
                cmd.Parameters.AddWithValue("@rx", vd.v.rotation.X);
                cmd.Parameters.AddWithValue("@ry", vd.v.rotation.Y);
                cmd.Parameters.AddWithValue("@rz", vd.v.rotation.Z);
                cmd.Parameters.AddWithValue("@id", vd.id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void loadVehicles()
        {
            if (isTableCreated())
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM VehicleData";
                
                con.Open();
                reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    int id = reader.GetInt32("id");
                    VehicleHash model = API.shared.vehicleNameToModel(reader.GetString("model"));
                    Vector3 pos = new Vector3(reader.GetDouble("spawnX"), reader.GetDouble("spawnY"), reader.GetDouble("spawnZ"));
                    Vector3 rot = new Vector3(reader.GetDouble("spawnRX"), reader.GetDouble("spawnRY"), reader.GetDouble("spawnRZ"));
                    Account owner = methods.getMethods.getAccountByName(reader.GetString("Name"));
                    int color1 = reader.GetInt32("color1");
                    int color2 = reader.GetInt32("color2");
                    Vehicle v = API.shared.createVehicle(model, pos, rot, color1, color2);
                    v.numberPlate = "LS" + id.ToString("D4");
                    VehicleD vd = new VehicleD(v, owner, id.ToString());
                }
                con.Close();
            }
        }
    }
}
