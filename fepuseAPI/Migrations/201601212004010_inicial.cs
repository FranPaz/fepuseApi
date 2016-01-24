namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dni = c.Int(nullable: false),
                        Nombre = c.String(),
                        Apellido = c.String(),
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
                "dbo.EquipoJugadorTorneos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JugadorId = c.Int(nullable: false),
                        TorneoId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .ForeignKey("dbo.Jugadores", t => t.JugadorId)
                .Index(t => t.JugadorId)
                .Index(t => t.TorneoId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.Equipoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        AlDia = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Partidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiaYHora = c.DateTime(nullable: false),
                        Sede = c.String(),
                        GolesLocal = c.Int(nullable: false),
                        GolesVisitante = c.Int(nullable: false),
                        Incidencias = c.String(),
                        FechaId = c.Int(nullable: false),
                        EquipoLocalId = c.Int(),
                        EquipoVisitanteId = c.Int(),
                        ArbitroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Arbitros", t => t.ArbitroId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoLocalId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoVisitanteId)
                .ForeignKey("dbo.Fechas", t => t.FechaId, cascadeDelete: true)
                .Index(t => t.FechaId)
                .Index(t => t.EquipoLocalId)
                .Index(t => t.EquipoVisitanteId)
                .Index(t => t.ArbitroId);
            
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
                        AñoInicio = c.Int(nullable: false),
                        AñoFin = c.Int(nullable: false),
                        LigaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.LigaId);
            
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
                        Goles = c.Int(nullable: false),
                        TarjetasAmarillas = c.Int(nullable: false),
                        TarjetasRojas = c.Int(nullable: false),
                        InformeArbitro = c.String(),
                        ObservacionesAdiconales = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jugadores", t => t.JugadorId)
                .ForeignKey("dbo.Partidoes", t => t.PartidoId, cascadeDelete: true)
                .Index(t => t.JugadorId)
                .Index(t => t.PartidoId);
            
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
                        TorneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TorneoId);
            
            CreateTable(
                "dbo.Jugadores",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Matricula = c.Int(nullable: false),
                        Apodo = c.String(),
                        Federado = c.String(),
                        Profesion = c.String(),
                        FichaMedica = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jugadores", "Id", "dbo.Personas");
            DropForeignKey("dbo.Arbitros", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.Arbitros", "Id", "dbo.Personas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EquipoJugadorTorneos", "JugadorId", "dbo.Jugadores");
            DropForeignKey("dbo.ImagenEquipoes", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.EquipoJugadorTorneos", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.PartidoJugadors", "PartidoId", "dbo.Partidoes");
            DropForeignKey("dbo.PartidoJugadors", "JugadorId", "dbo.Jugadores");
            DropForeignKey("dbo.Torneos", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.ImagenLigas", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.ImagenTorneos", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.Fechas", "torneoId", "dbo.Torneos");
            DropForeignKey("dbo.EquipoJugadorTorneos", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.Partidoes", "FechaId", "dbo.Fechas");
            DropForeignKey("dbo.Partidoes", "EquipoVisitanteId", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "EquipoLocalId", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "ArbitroId", "dbo.Arbitros");
            DropForeignKey("dbo.ImagenPersonas", "PersonaId", "dbo.Personas");
            DropIndex("dbo.Jugadores", new[] { "Id" });
            DropIndex("dbo.Arbitros", new[] { "TorneoId" });
            DropIndex("dbo.Arbitros", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "LigaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ImagenEquipoes", new[] { "EquipoId" });
            DropIndex("dbo.PartidoJugadors", new[] { "PartidoId" });
            DropIndex("dbo.PartidoJugadors", new[] { "JugadorId" });
            DropIndex("dbo.ImagenLigas", new[] { "LigaId" });
            DropIndex("dbo.ImagenTorneos", new[] { "TorneoId" });
            DropIndex("dbo.Torneos", new[] { "LigaId" });
            DropIndex("dbo.Fechas", new[] { "torneoId" });
            DropIndex("dbo.Partidoes", new[] { "ArbitroId" });
            DropIndex("dbo.Partidoes", new[] { "EquipoVisitanteId" });
            DropIndex("dbo.Partidoes", new[] { "EquipoLocalId" });
            DropIndex("dbo.Partidoes", new[] { "FechaId" });
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "EquipoId" });
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "TorneoId" });
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "JugadorId" });
            DropIndex("dbo.ImagenPersonas", new[] { "PersonaId" });
            DropTable("dbo.Jugadores");
            DropTable("dbo.Arbitros");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ImagenEquipoes");
            DropTable("dbo.PartidoJugadors");
            DropTable("dbo.ImagenLigas");
            DropTable("dbo.Ligas");
            DropTable("dbo.ImagenTorneos");
            DropTable("dbo.Torneos");
            DropTable("dbo.Fechas");
            DropTable("dbo.Partidoes");
            DropTable("dbo.Equipoes");
            DropTable("dbo.EquipoJugadorTorneos");
            DropTable("dbo.ImagenPersonas");
            DropTable("dbo.Personas");
        }
    }
}