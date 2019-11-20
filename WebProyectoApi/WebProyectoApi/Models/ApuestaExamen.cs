using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class ApuestaExamen
    {
        //PREGUNTA 1 EXAMEN
        public ApuestaExamen(string nombreUsuario, double cantidad, double mercado, double cuota_apuesta)
        {
            Nombre_usuario = nombreUsuario;
            Cantidad = cantidad;
            Mercado = mercado;
            Cuota_apuesta = cuota_apuesta;

        }

        public string Nombre_usuario { get; set; }
        public double Cantidad { get; set; }

        public double Mercado { get; set; }

        public double Cuota_apuesta { get; set; }
    }
}