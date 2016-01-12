using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public class Torneo
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public int AñoInicio { get; set; }
        public int AñoFin { get; set; }

        //kikexp: uno a muchos con Liga
        public int LigaId { get; set; }
        public virtual Liga Liga { get; set; }

        //public virtual ICollection<Equipo> Equipos { get; set; }

        //kikexp: uno a muchos con Fecha(muchos)
        public virtual ICollection<Fecha> Fechas { get; set; }
    }
}