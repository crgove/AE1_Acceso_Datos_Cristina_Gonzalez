using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetBoolean(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetInt32(4) + " " + res.GetString(5));
                    a = new Apuesta(res.GetInt32(0), res.GetBoolean(1), res.GetDouble(2), res.GetDouble(3), res.GetInt32(4), res.GetString(5));
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

        internal IEnumerable<ApuestaDTO> RetrieveDTO()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            //command.CommandText = "SELECT ap.*, me.TIPO FROM apuesta AS ap INNER JOIN mercado AS me ON me.ID_MERCADO=ap.ID_APUESTA";
            command.CommandText = "SELECT ap.ES_UNDER, ap.CUOTA, ap.DINERO, ap.EMAIL_USUARIO, me.TIPO FROM apuesta AS ap LEFT JOIN mercado AS me ON ap.ID_MERCADO=me.ID_MERCADO";
  
            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                ApuestaDTO a = null;
                List<ApuestaDTO> apuestas = new List<ApuestaDTO>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetBoolean(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetString(3) + " " + res.GetDouble(4));
                    a = new ApuestaDTO(res.GetBoolean(0), res.GetDouble(1), res.GetDouble(2), res.GetString(3), res.GetDouble(4));
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

        internal void Save(Apuesta a)
        {
            CultureInfo cullnfo = new System.Globalization.CultureInfo("es-ES");
            cullnfo.NumberFormat.NumberDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cullnfo.NumberFormat.PercentDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = cullnfo;

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            command.CommandText = "insert into apuesta(es_under, cuota, dinero, id_mercado, email_usuario) values ('" + a.EsUnder + "','" + a.Cuota + "','" + a.Dinero + "','" + a.IdMercado + "','" + a.EmailUsuario + "');";
            Debug.WriteLine("comando " +command.CommandText);

            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException a)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
            }
        }

        
    }
}