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

    public System.Data.Entity.DbSet<fepuseAPI.Models.EstadisticasJugador> EstadisticasJugadors { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Fecha> Fechas { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Partido> Partidoes { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Equipo> Equipoes { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Jugador> Jugadors { get; set; }

    public System.Data.Entity.DbSet<fepuseAPI.Models.Arbitro> Arbitroes { get; set; }

    #endregion

    public static FepuseAPI_Context Create()
    {
        return new FepuseAPI_Context();
    }

    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Partido>()
    //                .HasRequired(m => m.EquipoLocal)
    //                .WithMany(t => t.EquipoLocales)
    //                .HasForeignKey(m => m.EquipoLocalId)
    //                .WillCascadeOnDelete(false);

    //    modelBuilder.Entity<Partido>()
    //                .HasRequired(m => m.EquipoVisitante)
    //                .WithMany(t => t.EquipoVisitantes)
    //                .HasForeignKey(m => m.EquipoVisitanteId)
    //                .WillCascadeOnDelete(false);
    //}

}
