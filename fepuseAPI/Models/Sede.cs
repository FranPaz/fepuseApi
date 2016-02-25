﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public class Sede
    {
        public int Id { get; set; }
        public String Nombre { get; set; }

        //kikexp: uno a muchos con partido
        public virtual ICollection<Partido> Partidos { get; set; }
    }
}