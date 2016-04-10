using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using fepuseAPI.Models;
using fepuseAPI.Infraestructura;

public class FepuseAPI_Context : IdentityDbContext<ApplicationUser>
{
    public FepuseAPI_Context() : base("FepuseAPI_Context", throwIfV1Schema: false)
    {
        this.Configuration.LazyLoadingEnabled = false;
        this.Configuration.ProxyCreationEnabled = false;
    }

    #region Definicion de Tablas DbSet

    public System.Data.Entity.DbSet<fepuseAPI.Models.Liga> Ligas { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Torneo> Torneos { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.PartidoJugador> EstadisticasJugadors { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.EquipoJugadorTorneo> EquiposJugadorTorneos { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Fecha> Fechas { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Partido> Partidos { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Persona> Personas { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.Equipo> Equipoes { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.EquipoTorneo> EquipoTorneos { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Jugador> Jugadors { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Arbitro> Arbitroes { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.ImagenEquipo> ImagenesEquipo { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.ImagenLiga> ImagenesLiga { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.ImagenTorneo> ImagenesTorneo { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.ImagenPersona> ImagenesPersona { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Sede> Sedes { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Profesion> Profesions { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.Categoria> Categorias { get; set; }
    public System.Data.Entity.DbSet<fepuseAPI.Models.PuestoJugador> PuestosJugadores { get; set; }


    #endregion

    public static FepuseAPI_Context Create()
    {
        return new FepuseAPI_Context();
    }

    

}
