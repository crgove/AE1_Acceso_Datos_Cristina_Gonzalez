using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class EventosRepository : MySqlRepository
    {
        internal IEnumerable<Evento> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from evento";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                Evento e = null;
                List<Evento> eventos = new List<Evento>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetString(1) + " " + res.GetString(2) + " " + res.GetDateTime(3) + " " + res.GetDateTime(4));
                    e = new Evento(res.GetInt32(0), res.GetString(1), res.GetString(2), res.GetDateTime(3), res.GetDateTime(4));
                    eventos.Add(e);
                }

                con.Close();
                return eventos;

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return null;
            }
        }


        internal IEnumerable<EventoDTO> RetrieveDTO() 
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from evento";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                List<EventoDTO> eventos = new List<EventoDTO>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetString(1) + " " + res.GetString(2) + " " + res.GetDateTime(3) + " " + res.GetDateTime(4));
                    EventoDTO e = new EventoDTO(res.GetString(1), res.GetString(2));
                    eventos.Add(e);
                }

                con.Close();
                return eventos;

            }
            catch (MySqlException e)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return null;
            }
        }


    }
}