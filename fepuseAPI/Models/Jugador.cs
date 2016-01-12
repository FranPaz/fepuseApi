using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;


namespace fepuseAPI.Models
{
    public class Jugador
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public int Matricula { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Apodo { get; set; }
        public String Federado { get; set; }
        public String Profesion { get; set; }
        public String Direccion { get; set; }
        public int Telefono { get; set; }
        public String Email { get; set; }
        public Boolean FichaMedica { get; set; }

        //kikexp: uno a muchos con equipo
        public int EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }

        public virtual ICollection<EstadisticasJugador> EstadisticasJugadores { get; set; }
    }
}