using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;

namespace fepuseAPI.Models
{
    public class Equipo
    {       
        public int Id { get; set; }
        public String Nombre { get; set; }
        public Boolean AlDia { get; set; }

        ////fpaz: 1 a m con EquipoJugadorTorneo (muchos)
        //public virtual ICollection<EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; } // esta es la relacion entre equipos y torneos, y tambien los jugadores de ese equipo en ese torneo               

        //kikxp: muchos a muchos con partido
        public virtual ICollection<Partido> EquipoLocales { get; set; }
        public virtual ICollection<Partido> EquipoVisitantes { get; set; }

        //fpaz: relacion 1 a m con ImagenEquipo (muchos)
        public virtual ICollection<ImagenEquipo> ImagenesEquipo { get; set; }
       
        // 1 a m con Categoria (uno)
        public int? CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        //fpaz: 1 a M con EquipoTorneo (muchos)
        public virtual ICollection<EquipoTorneo> EquipoTorneos { get; set; }

        //fpaz: 1 a m con PartidoJugador
        public virtual ICollection<PartidoJugador> PartidosJugadores { get; set; }

        //fpaz: 1 a m con jugadores
        public virtual ICollection<Jugador> PlantillaActual { get; set; }

    }

    public class EquipoTorneo
    {
        public int Id { get; set; }

        //fpaz: 1 a M con torneo (uno)
        public int ZonaTorneoId { get; set; }
        public virtual ZonaTorneo ZonaTorneo { get; set; }
        
        ////fpaz: 1 a M con torneo (uno)
        //public int TorneoId { get; set; }
        //public virtual Torneo Torneo { get; set; }

        //fpaz: 1 a M con Equipo (uno)
        public int EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }

        public double Puntos { get; set; }
        public int PartidosJugados { get; set; }
        public int PartidosGanados { get; set; }
        public int PartidosEmpatados { get; set; }
        public int PartidosPerdidos { get; set; }
        public int GolesAFavor { get; set; }
        public int GolesEnContra { get; set; }
        public int DiferenciaGoles { get; set; }

        //fpaz: 1 a m con EquipoJugadorTorneo (muchos)
        public virtual ICollection<EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; } // esta es la relacion entre equipos y torneos, y tambien los jugadores de ese equipo en ese torneo               

        
    }
}