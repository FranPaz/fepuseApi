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
        //fpaz: 1 a m con partidos
        public virtual ICollection<Partido> Partidos { get; set; }

        //fpaz: 1 a m con liga (uno)
        public int LigaId { get; set; }
        public virtual Liga Liga { get; set; }

    }
}