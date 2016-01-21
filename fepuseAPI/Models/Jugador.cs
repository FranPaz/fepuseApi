using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;


namespace fepuseAPI.Models
{
    [Table("Jugadores")]
    public class Jugador:Persona
    {        
        public int Matricula { get; set; }     
        public string Apodo { get; set; }
        public string Federado { get; set; }
        public string Profesion { get; set; }        
        public bool FichaMedica { get; set; }

        //fpaz: 1 a m con EquipoJugadorTorneo (muchos)
        public virtual ICollection<EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; }
        public virtual ICollection<PartidoJugador> PartidosJugados { get; set; }
    }

    public class EquipoJugadorTorneo  //clase que va a tener la info sobre en que equipo jugo un jugador y en que torneo
    {
        public int Id { get; set; }
        //fpaz; 1 a m con jugador (uno)
        public int JugadorId { get; set; }
        public Jugador Jugador { get; set; }        

        //fpaz; 1 a m con torneo (uno)
        public int TorneoId { get; set; }
        public Torneo Torneo { get; set; }

        //fpaz; 1 a m con equipo (uno)
        public int EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }
    }
}