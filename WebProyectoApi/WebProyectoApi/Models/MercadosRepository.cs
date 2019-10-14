using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class MercadosRepository : MySqlRepository
    {

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
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return null;
            }

        }
    }
}