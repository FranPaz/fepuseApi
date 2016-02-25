using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public class Profesion
    {
        public int Id { get; set; }
        public String Nombre { get; set; }

        //kikexp: uno a muchos con Jugadores
        public virtual ICollection<Jugador> Jugadores { get; set; }
    }
}