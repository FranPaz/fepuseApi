using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fepuseAPI.Models;

namespace fepuseAPI.ClasesAuxiliares
{
    #region objetos de resultados estadisticos
    public abstract class Estadisticas
    {
        public int JugadorId { get; set; } // es el id del jugador
        public string NombreJugador { get; set; }
        public int PartidosJugados { get; set; }
        public string NombreEquipo { get; set; }
    }

    public class Goleadores : Estadisticas
    {
        public int Goles { get; set; }
    }

    public class Amonestados : Estadisticas
    {
        public int CantTarjAmarillas { get; set; }
    }

    public class Expulsados : Estadisticas
    {
        public int CantTarjRojas { get; set; }
    }
    #endregion

    public class EstaditicasTorneo
    {
        public Torneo Torneo { get; set; }
        public ICollection<Goleadores> Goleadores { get; set; }
        public ICollection<Amonestados> Amonestados { get; set; }
        public ICollection<Expulsados> Expulsados { get; set; }

    }
}