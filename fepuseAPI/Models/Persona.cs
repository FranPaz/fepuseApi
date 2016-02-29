using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fepuseAPI.Models
{
    public abstract class Persona //fpaz: clase base para jugadores y arbitros
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public string FecNacimiento { get; set; }
        public String NombreApellido { get; set; }        
        public String Direccion { get; set; }
        public int Telefono { get; set; }
        public String Email { get; set; }

        //fpaz: relacion 1 a m con ImagenPersona (muchos)
        public virtual ICollection<ImagenPersona> ImagenesPersona { get; set; }


    }
}