using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{

    public class PartidoJugador
    {
        public int Id { get; set; }
        //fpaz: 1 a m con jugador (uno)
         public int JugadorId { get; set; }
        public virtual Jugador Jugador { get; set; }

        //fpaz: 1 a m con partido (uno)
        public int PartidoId { get; set; }
        public virtual Partido Partido { get; set; }

        //fpaz: 1 a m con equipo (uno)
        public int EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }

        public int Goles { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }

        public string InformeArbitro { get; set; }
        public string ObservacionesAdiconales { get; set; }

    }
}