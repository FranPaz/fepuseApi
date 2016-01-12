using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{

    public class EstadisticasJugador
    {
        public int Id { get; set; }

        //public int? PartidoId { get; set; }
        //[InverseProperty("EstadisticasJugadores")]
        //public virtual Partido Partido { get; set; }

        public int? JugadorId { get; set; }
        public virtual Jugador Jugador { get; set; }

        public int Goles { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }

    }
}