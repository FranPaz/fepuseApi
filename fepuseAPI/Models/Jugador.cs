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
    public class Jugador : Persona
    {
        public int Matricula { get; set; }
        public string Apodo { get; set; }
        public bool Federado { get; set; }        
        public bool FichaMedica { get; set; }

        //fpaz: 1 a m con EquipoJugadorTorneo (muchos)
        public virtual ICollection<EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; }
        public virtual ICollection<PartidoJugador> PartidosJugados { get; set; }

        //kikexp: 1 a m con Profesion (uno)
        public int? ProfesionId { get; set; }
        public virtual Profesion Profesion { get; set; }

        //fpaz: 1 a m con Puesto (uno)
        public int? PuestoJugadorId { get; set; }
        public PuestoJugador PuestoJugador { get; set; }

        // 1 a m con Categoria (uno)
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        //fpaz; 1 a m con equipo (uno)
        public int? EquipoId { get; set; }
        public virtual Equipo Equipo { get; set; }
    }

    public class EquipoJugadorTorneo 
    {
        //clase que va a tener la info sobre en que equipo jugo un jugador y en que torneo
        // se carga una ves que se arma la lista de buena fe del equipo para un torneo en particular
        public int Id { get; set; }
        //fpaz; 1 a m con jugador (uno)
        public int JugadorId { get; set; }
        public Jugador Jugador { get; set; }

        //fpaz; 1 a m con EquipoTorneo (uno)
        public int EquipoTorneoId { get; set; }
        public EquipoTorneo EquipoTorneo { get; set; }

        //fpaz; 1 a m con torneo (uno)
        //public int? TorneoId { get; set; }
        //public Torneo Torneo { get; set; }

        ////fpaz; 1 a m con equipo (uno)
        //public int EquipoId { get; set; }
        //public virtual Equipo Equipo { get; set; }

        public int NumeroCamiseta { get; set; }

        public bool Habilitado { get; set; } //si esta en false puede ser porque: esta suspendido, o ya no juega en el equipo

    }
}