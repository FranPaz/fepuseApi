using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fepuseAPI.Models;

namespace fepuseAPI.ClasesAuxiliares
{
    public class EquipoCategoria //fpaz: Clase para mostrar todos los equipos de la Liga agrupados por Categoria
    {
        public Categoria Categoria { get; set; }
        public ICollection<Equipo> Equipos { get; set; }
    }
}