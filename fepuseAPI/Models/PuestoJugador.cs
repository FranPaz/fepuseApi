using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public class PuestoJugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //fpaz: 1 a m con jugador (m)
        public virtual ICollection<Jugador> Jugadores { get; set; }
    }
}