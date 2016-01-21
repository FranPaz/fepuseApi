using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;

namespace fepuseAPI.Models
{
    [Table("Arbitros")]
    public class Arbitro:Persona
    {
        //kikexp: uno a muchos con Torneo
        public int TorneoId { get; set; }
        public virtual Torneo Torneo { get; set; }

        //fpaz: 1 a m con partidos
        public virtual ICollection<Partido> Partidos { get; set; }

    }
}