using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class ApuestasRepository : MySqlRepository
    {
        internal IEnumerable<Apuesta> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from apuesta";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                Apuesta a = null;
                List<Apuesta> apuestas = new List<Apuesta>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetString(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetInt32(4) + " " + res.GetString(5));
                    a = new Apuesta(res.GetInt32(0), res.GetString(1), res.GetDouble(2), res.GetDouble(3), res.GetInt32(4), res.GetString(5));
                    apuestas.Add(a);
                }

                con.Close();
                return apuestas;

            }
            catch (MySqlException a)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return null;
            }
        }
    }
}