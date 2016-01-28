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

        //kikexp: uno a muchos con Fecha(muchos)
        public virtual ICollection<Fecha> Fechas { get; set; }

        //fpaz: relacion 1 a m con ImagenLiga (muchos)
        public virtual ICollection<ImagenTorneo> ImagenesTorneo { get; set; }

        //fpaz: 1 a m con EquipoJugadorTorneo (muchos)
        public virtual ICollection<EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; }

        //fpaz: 1 a M con EquipoTorneo (muchos)
        public virtual ICollection<EquipoTorneo> EquipoTorneos { get; set; }

    }
}