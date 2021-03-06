﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;


namespace fepuseAPI.Models
{
    public class Liga
    {
        public int Id { get; set; }
        public String Nombre { get; set; }

        //fpaz: relacion 1 a m con Categoria (muchos)
        public virtual ICollection<Categoria> Categorias { get; set; }

        //fpaz: relacion 1 a m con ImagenLiga (muchos)
        public virtual ICollection<ImagenLiga> ImagenesLiga { get; set; }        

        //fpaz: 1 a m  con arbitros (muchos)
        public virtual ICollection<Arbitro> Arbitros { get; set; }

        //fpaz: 1 a m con noticiasLiga (muchos)
        public virtual ICollection<NoticiaLiga> NoticiasLiga { get; set; }

    }
}