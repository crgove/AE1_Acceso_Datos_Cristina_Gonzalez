using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class MySqlRepository
    {
        internal MySqlConnection Connect()
        {
            string conexionString = @"Server=127.0.0.1;Port=3306;Database=place_my_bet;Uid=root;password=;SslMode=none";

            MySqlConnection con = new MySqlConnection(conexionString);
            return con;


        }
    }
}