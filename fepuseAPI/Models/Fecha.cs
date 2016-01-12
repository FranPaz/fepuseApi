using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;

namespace fepuseAPI.Models
{
    public class Fecha
    {
        public int Id { get; set; }
        public DateTime Dia { get; set; }

        //kikexp: uno a muchos con Fixture
        public int torneoId { get; set; }
        public virtual Torneo Torneo { get; set; }

        //kikexp: uno a muchos con Partido(muchos)
        public virtual ICollection<Partido> Partidos { get; set; }
    }
}