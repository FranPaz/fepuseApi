using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;

namespace fepuseAPI.Models
{
    public class Equipo
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public Boolean AlDia { get; set; }

        //kikxp: uno a muchos con Jugador(muchos)
        public virtual ICollection<Jugador> Jugadores { get; set; }

        public int TorneoId { get; set; }
        public virtual Torneo Torneo { get; set; }

        //kikxp: muchos a muchos con partido

        public virtual ICollection<Partido> EquipoLocales { get; set; }
        public virtual ICollection<Partido> EquipoVisitantes { get; set; }


    }
}