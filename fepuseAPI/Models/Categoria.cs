using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // fpaz: 1 a m con liga (uno)
        public int LigaId { get; set; }
        public virtual Liga Liga { get; set; }

        //fpaz: 1 a M con Torneo (muchos)
        public virtual ICollection<Torneo> Torneos { get; set; }

        //fpaz: 1 a M con Equipos (muchos)
        public virtual ICollection<Equipo> Equipos { get; set; }

        //fpaz: 1 a m con noticiasCategoria (muchos)
        public virtual ICollection<NoticiaCategoria> NoticiasCategoria { get; set; }
        
    }
}