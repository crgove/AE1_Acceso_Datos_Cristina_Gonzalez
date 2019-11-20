using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class EventosRepository : MySqlRepository
    {

        //ACTIVIDAD 3 EXAMEN
        internal void Save(EventoExamen e) //Función creada para insertar una nueva apuesta
        {
            CultureInfo cullnfo = new System.Globalization.CultureInfo("es-ES");
            cullnfo.NumberFormat.NumberDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cullnfo.NumberFormat.PercentDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = cullnfo;

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            command.CommandText = "insert into evento(local, visitante, dinero) values (" + e.EquipoLocal + ",'" + e.EquipoVisitante + "','" + e.Dinero + "');";
            Debug.WriteLine("comando " + command.CommandText);

            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD " + e.Message);
            }
        }

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