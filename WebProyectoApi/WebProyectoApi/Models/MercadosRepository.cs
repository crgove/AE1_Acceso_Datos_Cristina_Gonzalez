using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class MercadosRepository : MySqlRepository
    {

        //EXAMEN PREGUNTA 3
        internal void Save(Mercado m) //Función creada para insertar un nuevo mercado
        {
            CultureInfo cullnfo = new System.Globalization.CultureInfo("es-ES");
            cullnfo.NumberFormat.NumberDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cullnfo.NumberFormat.PercentDecimalSeparator = ".";
            cullnfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = cullnfo;

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            command.CommandText = "insert into mercado(tipo, cuota_over, cuota_under, dinero_apostado_over, dinero_apostado_under) values (" + m.Tipo + ",'" + m.CuotaOver + ",'" + m.CuotaUnder + "');";
            //Debug.WriteLine("comando " + command.CommandText);

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
        internal IEnumerable<Mercado> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from mercado";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                Mercado m = null;
                List<Mercado> mercados = new List<Mercado>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetDouble(4) + " " + res.GetDouble(5) + res.GetInt32(6));
                    m = new Mercado(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetInt32(6));
                    mercados.Add(m);
                }

                con.Close();
                return mercados;

            }
            catch (MySqlException m)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return null;
            }
        }

        //PREGUNTA 3 EXAMEN
        internal IEnumerable<MercadoDTO> RetrieveById(int idEvento) 
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select TIPO, CUOTA_OVER, CUOTA_UNDER from mercado where id_evento=@id_evento";
            command.Parameters.AddWithValue("@id_evento", idEvento);

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                MercadoDTO m = null;
                List<MercadoDTO> mercados = new List<MercadoDTO>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetDouble(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2));
                    m = new MercadoDTO(res.GetDouble(0), res.GetDouble(1), res.GetDouble(2)); 
                    mercados.Add(m);
                }

                con.Close();
                return mercados;

            }
            catch (MySqlException m)
            {
                Debug.WriteLine("2: No se ha podido realizar la conexión a la BBDD " + m.Message);
                return null;
            }

        }


        internal IEnumerable<MercadoDTO> RetrieveDTO()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from mercado";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                MercadoDTO m = null;
                List<MercadoDTO> mercados = new List<MercadoDTO>();

                while (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetDouble(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetDouble(4) + " " + res.GetDouble(5) + res.GetInt32(6));
                    m = new MercadoDTO(res.GetDouble(1), res.GetDouble(2), res.GetDouble(3));
                    mercados.Add(m);
                }

                con.Close();
                return mercados;

            }
            catch (MySqlException m)
            {
                Debug.WriteLine("2: No se ha podido realizar la conexión a la BBDD "+m.Message);
                return null;
            }

        }
        /// <summary>
        /// Actualizamos el mercado 
        /// </summary>
        /// <param name="apuesta">Apuesta de usuario</param>
        public void ActualizarMercado(Apuesta apuesta) //Función creada para actualizar las cuotas del mercado
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            var idMercado = apuesta.IdMercado;

            var dineroOver = RecuperarDineroOver(idMercado); //Llama a la función que recuperará el dinero over de Mercado
            var dineroUnder = RecuperarDineroUnder(idMercado); //llama a la función que recuperará el dinero under de Mercado

            var probabilidadOver = dineroOver/(dineroOver + dineroUnder);
            var probabilidadUnder = dineroUnder/(dineroOver + dineroUnder);

            var cuotaOver = Convert.ToDouble((1 / probabilidadOver) * 0.95);
            var cuotaUnder = Convert.ToDouble((1 / probabilidadUnder) * 0.95);


            //command.CommandText = "update mercado set cuota_over= " + cuotaOver +", cuota_under= " + cuotaUnder + " where id_mercado= " + idMercado + ";";
            command.CommandText = "update mercado set cuota_over=@cuota_over, cuota_under=@cuota_under where id_mercado=@id_mercado";
            command.Parameters.AddWithValue("@cuota_over", cuotaOver); 
            command.Parameters.AddWithValue("@cuota_under", cuotaUnder);
            command.Parameters.AddWithValue("@id_mercado", idMercado);

            //Debug.WriteLine("COMANDO " + command.CommandText+" param1: "+cuotaOver+" param2: "+cuotaUnder+" param3: "+idMercado);

            try
            {
                con.Open();
                command.ExecuteNonQuery();

                con.Close();

            }
            catch (MySqlException m)
            {
                Debug.WriteLine("1: No se ha podido realizar la conexión a la BBDD "+m.Message);
               
            }

        }

        private double RecuperarDineroOver(int idMercado)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            //command.CommandText = "select dinero_apostado_over from mercado where id_mercado= " + idMercado + ";";
            command.CommandText = "select dinero_apostado_over from mercado where id_mercado=@id_mercado";
            command.Parameters.AddWithValue("@id_mercado", idMercado);

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
                Debug.WriteLine("3: No se ha podido realizar la conexión a la BBDD"+m.Message);
                return 0.0;
            }
        }

        private double RecuperarDineroUnder(int idMercado)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            //command.CommandText = "select dinero_apostado_under from mercado where id_mercado= " + idMercado + ";";
            command.CommandText = "select dinero_apostado_under from mercado where id_mercado=@id_mercado";
            command.Parameters.AddWithValue("@id_mercado", idMercado);

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
                Debug.WriteLine("4: No se ha podido realizar la conexión a la BBDD "+m.Message);
                return 0.0;
            }
        }
    }
}