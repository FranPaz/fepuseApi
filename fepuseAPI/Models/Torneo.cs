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
        public String FechaInicio { get; set; }
        public String FechaFin { get; set; }
        public bool Finalizado { get; set; }
        
        //kikexp: uno a muchos con Fecha(muchos)
        public virtual ICollection<Fecha> Fechas { get; set; }

        //fpaz: relacion 1 a m con ImagenLiga (muchos)
        public virtual ICollection<ImagenTorneo> ImagenesTorneo { get; set; }

        //fpaz: 1 a m con EquipoJugadorTorneo (muchos)
        public virtual ICollection<EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; }

        //fpaz: 1 a M con EquipoTorneo (muchos)
        //public virtual ICollection<EquipoTorneo> EquipoTorneos { get; set; }

        //fpaz: 1 a M con Categorias (uno)
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<ZonaTorneo> ZonasTorneo { get; set; }


    }
    public class ZonaTorneo 
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //fpaz: 1 a M con torneo (uno)
        public int TorneoId { get; set; }
        public virtual Torneo Torneo { get; set; }

        public virtual ICollection<EquipoTorneo> EquiposTorneo { get; set; }
    }
}