using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebProyectoApi.Models
{
    public class ApuestasRepository : MySqlRepository
    {

        /// <summary>
        /// Devuelve todas las apuestas de la base de datos
        /// </summary>
        /// <returns>Lista de apuestas</returns>
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

        /// <summary>
        /// Devuelve las apuestas de un mercado en concreto
        /// </summary>
        /// <param name="id">Id de Mercado</param>
        /// <returns>Lista de apuestas de un mercado</returns>
        internal IEnumerable<ApuestaDTO> RetrieveByApuesta(int id)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT ap.ES_UNDER, ap.CUOTA, ap.DINERO, ap.EMAIL_USUARIO, me.TIPO FROM apuesta AS ap INNER JOIN mercado AS me ON ap.id_mercado=me.ID_MERCADO WHERE me.ID_MERCADO=@id_mercado";
            command.Parameters.AddWithValue("@id_mercado", id);

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
            catch (MySqlException m)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return null;
            }

        }

        /// <summary>
        /// Devuelve las apuestas de un usuario en concreto
        /// </summary>
        /// <param name="email">Email de usuario</param>
        /// <returns>Lista de apuestas del usuario</returns>
        internal IEnumerable<ApuestaUsuario> RetrieveByEmail(string email)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT ap.ES_UNDER, ap.CUOTA, ap.DINERO, me.TIPO, ev.ID_EVENTO FROM apuesta AS ap INNER JOIN mercado AS me ON ap.id_mercado=me.ID_MERCADO INNER JOIN evento AS ev ON me.ID_EVENTO=ev.ID_EVENTO WHERE ap.EMAIL_USUARIO=@email_usuario";
            command.Parameters.AddWithValue("@email_usuario", email);

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                ApuestaUsuario a = null;
                List<ApuestaUsuario> apuestas = new List<ApuestaUsuario>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetBoolean(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetInt32(4));
                    a = new ApuestaUsuario(res.GetBoolean(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetInt32(4));
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

        /// <summary>
        /// Devuelve las apuestas con atributos específicos
        /// </summary>
        /// <returns>Lista de apuestas con su tipo de mercado</returns>
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


        /// <summary>
        /// Inserta una apuesta en la tabla apuestas 
        /// </summary>
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
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD "+ e.Message);
            }
        }

        /// <summary>
        /// Modifica el dinero (under o over) del mercado una vez se inserta una apuesta 
        /// </summary>
        internal void ModificarDineroEsUnderMercado(Apuesta apuesta) //Función creada con el objetivo de modificar el dinero(under o over) del mercado una vez se inserta una apuesta 
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            if (apuesta.EsUnder) //Si la apuesta es Under, llamará a la función que selecciona el dinero under y lo suma al dinero under de la apuesta que se acaba de realizar
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
                var dineroApostadoOver = SeleccionarDineroApuestaOver(apuesta) + apuesta.Dinero; //Si no, llamará a la función que recoge el dinero over y lo sumará al dinero de la apuesta a over
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

        /// <summary>
        /// Selecciona el dinero apostado a Under 
        /// </summary>
        /// <returns> El dinero apostado a Under </returns>
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

        /// <summary>
        /// Selecciona el dinero apostado a Over
        /// </summary>
        /// <returns> El dinero apostado a Over </returns>
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