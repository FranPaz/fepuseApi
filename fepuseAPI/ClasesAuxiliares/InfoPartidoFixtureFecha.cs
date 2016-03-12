using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fepuseAPI.Models;

namespace fepuseAPI.ClasesAuxiliares
{
    public class InfoFecha
    {
        public int Id { get; set; }
        public int NumFecha { get; set; }

        //kikexp: uno a muchos con Fixture
        public int torneoId { get; set; }        
        public List<InfoPartidoFixtureFecha> InfoPartidos { get; set; }
    }
    public class InfoPartidoFixtureFecha
    {
        public int Id { get; set; } // es el id del partido       
        public string Dia { get; set; }
        public string Hora { get; set; }
        public string Sede { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public string nombreEquipoLocal { get; set; }
        public string nombreEquipoVisitante { get; set; }
        public string nombreArbitro { get; set; }
    }
}