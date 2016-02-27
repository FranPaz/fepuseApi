using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace fepuseAPI.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public DateTime Dia { get; set; }
        public String Hora { get; set; }
        //public String Sede { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public String Incidencias { get; set; }
        public bool Finalizado { get; set; }
                
        //kikexp: uno a muchos con Fecha
        public int FechaId { get; set; }
        public virtual Fecha Fecha { get; set; }


        public int? EquipoLocalId { get; set; }

        [InverseProperty("EquipoLocales")]
        public virtual Equipo EquipoLocal { get; set; }


        public int? EquipoVisitanteId { get; set; }

        [InverseProperty("EquipoVisitantes")]
        public virtual Equipo EquipoVisitante { get; set; }

        //fpaz: 1 a m con PartidoJugador (muchos)
        public virtual ICollection<PartidoJugador> JugadoresDelPartido { get; set; } //relacion de partido con el jugador

        //fpaz: 1 a m con arbritos (uno)
        public int? ArbitroId { get; set; }
        public virtual Arbitro Arbitro { get; set; }

        //kikexp: 1 a m con sede
        public int? SedeId { get; set; }
        public virtual Sede Sede { get; set; }

    }
}