using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class ApuestasExamenRepository: MySqlRepository
    {
        //PREGUNTA 1
        internal IEnumerable<ApuestaExamen> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            //command.CommandText = "select * from apuesta";
            command.CommandText = "select usuario.NOMBRE, apuesta.DINERO, apuesta.CUOTA, mercado.ID_MERCADO FROM apuesta INNER JOIN mercado ON mercado.ID_MERCADO = apuesta.ID_MERCADO INNER JOIN usuario ON apuesta.EMAIL_USUARIO = usuario.EMAIL_USUARIO;";

            try
            {
                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                ApuestaExamen a = null;
                List<ApuestaExamen> apuestasExamen = new List<ApuestaExamen>();

                while (res.Read())
                {
                    //Debug.WriteLine("Recuperado: " + res.GetInt32(0) + " " + res.GetBoolean(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetInt32(4) + " " + res.GetString(5));
                    a = new ApuestaExamen(res.GetString(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3));
                    apuestasExamen.Add(a);
                }

                con.Close();
                return apuestasExamen;

            }
            catch (MySqlException a)
            {
                Debug.WriteLine("No se ha podido realizar la conexión a la BBDD");
                return null;
            }
        }

    }
}