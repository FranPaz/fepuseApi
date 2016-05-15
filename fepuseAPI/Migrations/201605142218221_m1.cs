namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dni = c.Int(nullable: false),
                        FecNacimiento = c.String(),
                        NombreApellido = c.String(),
                        Direccion = c.String(),
                        Telefono = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImagenPersonas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        FileSizeInBytes = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.PersonaId, cascadeDelete: true)
                .Index(t => t.PersonaId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        LigaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.LigaId);
            
            CreateTable(
                "dbo.Equipoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        AlDia = c.Boolean(nullable: false),
                        CategoriaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Partidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dia = c.String(),
                        Hora = c.String(),
                        GolesLocal = c.Int(nullable: false),
                        GolesVisitante = c.Int(nullable: false),
                        Incidencias = c.String(),
                        Finalizado = c.Boolean(nullable: false),
                        FechaId = c.Int(nullable: false),
                        EquipoLocalId = c.Int(),
                        EquipoVisitanteId = c.Int(),
                        ArbitroId = c.Int(),
                        SedeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Arbitros", t => t.ArbitroId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoLocalId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoVisitanteId)
                .ForeignKey("dbo.Fechas", t => t.FechaId, cascadeDelete: true)
                .ForeignKey("dbo.Sedes", t => t.SedeId)
                .Index(t => t.FechaId)
                .Index(t => t.EquipoLocalId)
                .Index(t => t.EquipoVisitanteId)
                .Index(t => t.ArbitroId)
                .Index(t => t.SedeId);
            
            CreateTable(
                "dbo.Fechas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumFecha = c.Int(nullable: false),
                        torneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.torneoId, cascadeDelete: true)
                .Index(t => t.torneoId);
            
            CreateTable(
                "dbo.Torneos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        FechaInicio = c.String(),
                        FechaFin = c.String(),
                        Finalizado = c.Boolean(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.EquipoJugadorTorneos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JugadorId = c.Int(nullable: false),
                        EquipoTorneoId = c.Int(nullable: false),
                        NumeroCamiseta = c.Int(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        Torneo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipoTorneos", t => t.EquipoTorneoId, cascadeDelete: true)
                .ForeignKey("dbo.Jugadores", t => t.JugadorId)
                .ForeignKey("dbo.Torneos", t => t.Torneo_Id)
                .Index(t => t.JugadorId)
                .Index(t => t.EquipoTorneoId)
                .Index(t => t.Torneo_Id);
            
            CreateTable(
                "dbo.EquipoTorneos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaTorneoId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                        Puntos = c.Double(nullable: false),
                        PartidosJugados = c.Int(nullable: false),
                        PartidosGanados = c.Int(nullable: false),
                        PartidosEmpatados = c.Int(nullable: false),
                        PartidosPerdidos = c.Int(nullable: false),
                        GolesAFavor = c.Int(nullable: false),
                        GolesEnContra = c.Int(nullable: false),
                        DiferenciaGoles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .ForeignKey("dbo.ZonaTorneos", t => t.ZonaTorneoId, cascadeDelete: true)
                .Index(t => t.ZonaTorneoId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.ZonaTorneos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        TorneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.TorneoId);
            
            CreateTable(
                "dbo.ImagenTorneos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TorneoId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        FileSizeInBytes = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.TorneoId);
            
            CreateTable(
                "dbo.Noticias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Contenido = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImagenNoticias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoticiaId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        FileSizeInBytes = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.NoticiaId, cascadeDelete: true)
                .Index(t => t.NoticiaId);
            
            CreateTable(
                "dbo.Ligas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImagenLigas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LigaId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        FileSizeInBytes = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.LigaId);
            
            CreateTable(
                "dbo.PartidoJugadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JugadorId = c.Int(nullable: false),
                        PartidoId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                        Goles = c.Int(nullable: false),
                        TarjetasAmarillas = c.Int(nullable: false),
                        TarjetasRojas = c.Int(nullable: false),
                        InformeArbitro = c.String(),
                        ObservacionesAdiconales = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .ForeignKey("dbo.Jugadores", t => t.JugadorId)
                .ForeignKey("dbo.Partidoes", t => t.PartidoId, cascadeDelete: true)
                .Index(t => t.JugadorId)
                .Index(t => t.PartidoId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.Sedes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImagenEquipoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipoId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        FileSizeInBytes = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.Profesions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PuestoJugadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        LigaId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.LigaId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Arbitros",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LigaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.LigaId);
            
            CreateTable(
                "dbo.Jugadores",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Matricula = c.Int(nullable: false),
                        Apodo = c.String(),
                        Federado = c.Boolean(nullable: false),
                        FichaMedica = c.Boolean(nullable: false),
                        ProfesionId = c.Int(),
                        PuestoJugadorId = c.Int(),
                        CategoriaId = c.Int(nullable: false),
                        EquipoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.Id)
                .ForeignKey("dbo.Profesions", t => t.ProfesionId)
                .ForeignKey("dbo.PuestoJugadors", t => t.PuestoJugadorId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId)
                .Index(t => t.Id)
                .Index(t => t.ProfesionId)
                .Index(t => t.PuestoJugadorId)
                .Index(t => t.CategoriaId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.NoticiasCategoria",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.NoticiasLiga",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LigaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.LigaId);
            
            CreateTable(
                "dbo.NoticiasTorneo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TorneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TorneoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoticiasTorneo", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.NoticiasTorneo", "Id", "dbo.Noticias");
            DropForeignKey("dbo.NoticiasLiga", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.NoticiasLiga", "Id", "dbo.Noticias");
            DropForeignKey("dbo.NoticiasCategoria", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.NoticiasCategoria", "Id", "dbo.Noticias");
            DropForeignKey("dbo.Jugadores", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.Jugadores", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Jugadores", "PuestoJugadorId", "dbo.PuestoJugadors");
            DropForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions");
            DropForeignKey("dbo.Jugadores", "Id", "dbo.Personas");
            DropForeignKey("dbo.Arbitros", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.Arbitros", "Id", "dbo.Personas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ImagenEquipoes", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "SedeId", "dbo.Sedes");
            DropForeignKey("dbo.PartidoJugadors", "PartidoId", "dbo.Partidoes");
            DropForeignKey("dbo.PartidoJugadors", "JugadorId", "dbo.Jugadores");
            DropForeignKey("dbo.PartidoJugadors", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.ImagenLigas", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.Categorias", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.ImagenNoticias", "NoticiaId", "dbo.Noticias");
            DropForeignKey("dbo.ImagenTorneos", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.Fechas", "torneoId", "dbo.Torneos");
            DropForeignKey("dbo.EquipoJugadorTorneos", "Torneo_Id", "dbo.Torneos");
            DropForeignKey("dbo.EquipoJugadorTorneos", "JugadorId", "dbo.Jugadores");
            DropForeignKey("dbo.ZonaTorneos", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.EquipoTorneos", "ZonaTorneoId", "dbo.ZonaTorneos");
            DropForeignKey("dbo.EquipoJugadorTorneos", "EquipoTorneoId", "dbo.EquipoTorneos");
            DropForeignKey("dbo.EquipoTorneos", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.Torneos", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Partidoes", "FechaId", "dbo.Fechas");
            DropForeignKey("dbo.Partidoes", "EquipoVisitanteId", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "EquipoLocalId", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "ArbitroId", "dbo.Arbitros");
            DropForeignKey("dbo.Equipoes", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.ImagenPersonas", "PersonaId", "dbo.Personas");
            DropIndex("dbo.NoticiasTorneo", new[] { "TorneoId" });
            DropIndex("dbo.NoticiasTorneo", new[] { "Id" });
            DropIndex("dbo.NoticiasLiga", new[] { "LigaId" });
            DropIndex("dbo.NoticiasLiga", new[] { "Id" });
            DropIndex("dbo.NoticiasCategoria", new[] { "CategoriaId" });
            DropIndex("dbo.NoticiasCategoria", new[] { "Id" });
            DropIndex("dbo.Jugadores", new[] { "EquipoId" });
            DropIndex("dbo.Jugadores", new[] { "CategoriaId" });
            DropIndex("dbo.Jugadores", new[] { "PuestoJugadorId" });
            DropIndex("dbo.Jugadores", new[] { "ProfesionId" });
            DropIndex("dbo.Jugadores", new[] { "Id" });
            DropIndex("dbo.Arbitros", new[] { "LigaId" });
            DropIndex("dbo.Arbitros", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "LigaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ImagenEquipoes", new[] { "EquipoId" });
            DropIndex("dbo.PartidoJugadors", new[] { "EquipoId" });
            DropIndex("dbo.PartidoJugadors", new[] { "PartidoId" });
            DropIndex("dbo.PartidoJugadors", new[] { "JugadorId" });
            DropIndex("dbo.ImagenLigas", new[] { "LigaId" });
            DropIndex("dbo.ImagenNoticias", new[] { "NoticiaId" });
            DropIndex("dbo.ImagenTorneos", new[] { "TorneoId" });
            DropIndex("dbo.ZonaTorneos", new[] { "TorneoId" });
            DropIndex("dbo.EquipoTorneos", new[] { "EquipoId" });
            DropIndex("dbo.EquipoTorneos", new[] { "ZonaTorneoId" });
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "Torneo_Id" });
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "EquipoTorneoId" });
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "JugadorId" });
            DropIndex("dbo.Torneos", new[] { "CategoriaId" });
            DropIndex("dbo.Fechas", new[] { "torneoId" });
            DropIndex("dbo.Partidoes", new[] { "SedeId" });
            DropIndex("dbo.Partidoes", new[] { "ArbitroId" });
            DropIndex("dbo.Partidoes", new[] { "EquipoVisitanteId" });
            DropIndex("dbo.Partidoes", new[] { "EquipoLocalId" });
            DropIndex("dbo.Partidoes", new[] { "FechaId" });
            DropIndex("dbo.Equipoes", new[] { "CategoriaId" });
            DropIndex("dbo.Categorias", new[] { "LigaId" });
            DropIndex("dbo.ImagenPersonas", new[] { "PersonaId" });
            DropTable("dbo.NoticiasTorneo");
            DropTable("dbo.NoticiasLiga");
            DropTable("dbo.NoticiasCategoria");
            DropTable("dbo.Jugadores");
            DropTable("dbo.Arbitros");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PuestoJugadors");
            DropTable("dbo.Profesions");
            DropTable("dbo.ImagenEquipoes");
            DropTable("dbo.Sedes");
            DropTable("dbo.PartidoJugadors");
            DropTable("dbo.ImagenLigas");
            DropTable("dbo.Ligas");
            DropTable("dbo.ImagenNoticias");
            DropTable("dbo.Noticias");
            DropTable("dbo.ImagenTorneos");
            DropTable("dbo.ZonaTorneos");
            DropTable("dbo.EquipoTorneos");
            DropTable("dbo.EquipoJugadorTorneos");
            DropTable("dbo.Torneos");
            DropTable("dbo.Fechas");
            DropTable("dbo.Partidoes");
            DropTable("dbo.Equipoes");
            DropTable("dbo.Categorias");
            DropTable("dbo.ImagenPersonas");
            DropTable("dbo.Personas");
        }
    }
}
