using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    //fpaz: clase que tiene los Torneos definidos para cada categoria de una liga en particular
    public class TorneoCategoria
    {
        public int Id { get; set; }

        //kikexp: uno a muchos con Liga
        public int LigaId { get; set; }
        public virtual Liga Liga { get; set; }

        //fpaz: 1 a M con Categorias (uno)
        public int? CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        //fpaz: 1 a M con Torneo (muchos)
        public virtual ICollection<Torneo> Torneos { get; set; }

    }
}