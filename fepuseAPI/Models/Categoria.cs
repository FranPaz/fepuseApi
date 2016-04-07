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

        //fpaz: uno a muchos con TorneosCategoria(muchos)
        public virtual ICollection<TorneoCategoria> TorneosCategoria { get; set; } // fpaz: tiene el array de torneos segun la categoria
    }
}