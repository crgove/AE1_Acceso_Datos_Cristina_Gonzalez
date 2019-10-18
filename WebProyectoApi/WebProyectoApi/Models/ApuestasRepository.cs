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

        internal void Save(Apuesta a) //Función creada para insertar una nueva apuesta
        {
            CultureInfo cullnfo = new System.Globalization.CultureInfo("es-ES");
            cullnfo.NumberFormat.NumberDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cullnfo.NumberFormat.PercentDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = cullnfo;

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            command.CommandText = "insert into apuesta(es_under, cuota, dinero, id_mercado, email_usuario) values (" + a.EsUnder + ",'" + a.Cuota + "','" + a.Dinero + "','" + a.IdMercado + "','" + a.EmailUsuario + "');";
            Debug.WriteLine("comando " +command.CommandText);

            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e) 
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
            }
        }

        internal void ModificarDineroEsUnderMercado(Apuesta apuesta) //Función creada con el objetivo de modificar el dinero(under o over) del mercado una vez se inserta una apuesta 
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            if (apuesta.EsUnder)
            {
                var dineroApostadoUnder = SeleccionarDineroApuestaUnder(apuesta) + apuesta.Dinero;
                //command.CommandText = "update mercado set dinero_apostado_under=" + dineroApostadoUnder + " where id_mercado=" + apuesta.IdMercado + ";";
                command.CommandText = "update mercado set dinero_apostado_under=@dinero_apostado_under where id_mercado=@id_mercado";
                command.Parameters.AddWithValue("@dinero_apostado_under", dineroApostadoUnder);
                command.Parameters.AddWithValue("@id_mercado", apuesta.IdMercado);

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (MySqlException e)
                {
                    Debug.WriteLine("5: No se ha podido realizar la conexión a la BBDD "+e.Message);
                }

            } else
            {
                var dineroApostadoOver = SeleccionarDineroApuestaOver(apuesta) + apuesta.Dinero;
                //command.CommandText = "update mercado set dinero_apostado_over=" + dineroApostadoOver + " where id_mercado=" + apuesta.IdMercado + ";";
                command.CommandText = "update mercado set dinero_apostado_over=@dinero_apostado_over where id_mercado=@id_mercado";
                command.Parameters.AddWithValue("@dinero_apostado_over", dineroApostadoOver);
                command.Parameters.AddWithValue("@id_mercado", apuesta.IdMercado);

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (MySqlException e)
                {
                    Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                }
            }
        }

        internal double SeleccionarDineroApuestaUnder(Apuesta apuesta)
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            //command.CommandText = "select dinero_apostado_under from mercado where id_mercado= " + apuesta.IdMercado + ";";
            command.CommandText = "select dinero_apostado_under from mercado where id_mercado=@id_mercado";
            command.Parameters.AddWithValue("@id_mercado", apuesta.IdMercado);

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                var dineroApostadoUnder = 0.0;

                while (res.Read())
                {
                    dineroApostadoUnder = res.GetDouble(0);

                }

                con.Close();
                return dineroApostadoUnder;

            }
            catch (MySqlException m)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return 0.0;
            }

        }

        internal double SeleccionarDineroApuestaOver(Apuesta apuesta)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            //var idMercado = apuesta.IdMercado;

            //command.CommandText = "select dinero_apostado_over from mercado where id_mercado= " + apuesta.IdMercado + ";";
            command.CommandText = "select dinero_apostado_over from mercado where id_mercado=@id_mercado";
            command.Parameters.AddWithValue("@id_mercado", apuesta.IdMercado);

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                var dineroApostadoOver = 0.0;

                while (res.Read())
                {
                    dineroApostadoOver = res.GetDouble(0);

                }

                con.Close();
                return dineroApostadoOver;

            }
            catch (MySqlException m)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return 0.0;
            }
        }

    }
}