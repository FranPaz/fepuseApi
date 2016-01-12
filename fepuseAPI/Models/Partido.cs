﻿using System;
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
        public int Hora { get; set; }
        public String Sede { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public String Incidencias { get; set; }
                
        //kikexp: uno a muchos con Fecha
        public int FechaId { get; set; }
        public virtual Fecha Fecha { get; set; }


        public int? EquipoLocalId { get; set; }

        [InverseProperty("EquipoLocales")]
        public virtual Equipo EquipoLocal { get; set; }


        public int? EquipoVisitanteId { get; set; }

        [InverseProperty("EquipoVisitantes")]
        public virtual Equipo EquipoVisitante { get; set; }


        //public virtual ICollection<EstadisticasJugador> EstadisticasJugadores { get; set; }
    }
}