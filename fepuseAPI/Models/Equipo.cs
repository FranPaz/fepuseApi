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
        public Equipo()
        {
            this.Torneos = new HashSet<Torneo>();
        }
        public int Id { get; set; }
        public String Nombre { get; set; }
        public Boolean AlDia { get; set; }

        //fpaz: 1 a m con EquipoJugadorTorneo (muchos)
        public virtual ICollection<EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; } // esta es la relacion entre equipos y torneos, y tambien los jugadores de ese equipo en ese torneo               

        //kikxp: muchos a muchos con partido
        public virtual ICollection<Partido> EquipoLocales { get; set; }
        public virtual ICollection<Partido> EquipoVisitantes { get; set; }

        //fpaz: relacion 1 a m con ImagenEquipo (muchos)
        public virtual ICollection<ImagenEquipo> ImagenesEquipo { get; set; }

        //fpaz: m a m con torneos
        public virtual ICollection<Torneo> Torneos { get; set; }

        // 1 a m con liga
        public int? LigaId { get; set; }
        public virtual Liga Liga { get; set; }

    }
}