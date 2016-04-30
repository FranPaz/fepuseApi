using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public abstract class Noticia
    {
        public int Id { get; set; }
        public String Titulo { get; set; }

        public String Contenido { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public virtual ICollection<ImagenNoticia> ImagenesNoticia { get; set; }
        
    }

     [Table("NoticiasLiga")]
    public class NoticiaLiga : Noticia
    { 
        //fpaz: relacion 1 a m con Liga (uno)
        public int LigaId { get; set; }
        public virtual Liga Liga { get; set; }
    }

    [Table("NoticiasCategoria")]
    public class NoticiaCategoria : Noticia
    {
        //fpaz: relacion 1 a m con Categoria (uno)
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }

    [Table("NoticiasTorneo")]
    public class NoticiaTorneo : Noticia
    {
        //fpaz: relacion 1 a m con Torneo (uno)
        public int TorneoId { get; set; }
        public virtual Torneo Torneo { get; set; }
    }
}