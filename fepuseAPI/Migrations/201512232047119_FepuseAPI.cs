namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FepuseAPI : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arbitroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dni = c.Int(nullable: false),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Telefono = c.Int(nullable: false),
                        LigaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.LigaId);
            
            CreateTable(
                "dbo.Ligas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Fechas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dia = c.DateTime(nullable: false),
                        torneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.torneoId, cascadeDelete: true)
                .Index(t => t.torneoId);
            
            CreateTable(
                "dbo.Partidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hora = c.Int(nullable: false),
                        Sede = c.String(),
                        GolesLocal = c.Int(nullable: false),
                        GolesVisitante = c.Int(nullable: false),
                        Incidencias = c.String(),
                        FechaId = c.Int(nullable: false),
                        EquipoLocalId = c.Int(),
                        EquipoVisitanteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipoes", t => t.EquipoLocalId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoVisitanteId)
                .ForeignKey("dbo.Fechas", t => t.FechaId, cascadeDelete: true)
                .Index(t => t.FechaId)
                .Index(t => t.EquipoLocalId)
                .Index(t => t.EquipoVisitanteId);
            
            CreateTable(
                "dbo.Equipoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        AlDia = c.Boolean(nullable: false),
                        TorneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.TorneoId);
            
            CreateTable(
                "dbo.Jugadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dni = c.Int(nullable: false),
                        Matricula = c.Int(nullable: false),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Apodo = c.String(),
                        Federado = c.String(),
                        Profesion = c.String(),
                        Direccion = c.String(),
                        Telefono = c.Int(nullable: false),
                        Email = c.String(),
                        FichaMedica = c.Boolean(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.EstadisticasJugadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JugadorId = c.Int(),
                        Goles = c.Int(nullable: false),
                        TarjetasAmarillas = c.Int(nullable: false),
                        TarjetasRojas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jugadors", t => t.JugadorId)
                .Index(t => t.JugadorId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Torneos", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.Fechas", "torneoId", "dbo.Torneos");
            DropForeignKey("dbo.Partidoes", "FechaId", "dbo.Fechas");
            DropForeignKey("dbo.Partidoes", "EquipoVisitanteId", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "EquipoLocalId", "dbo.Equipoes");
            DropForeignKey("dbo.Equipoes", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.EstadisticasJugadors", "JugadorId", "dbo.Jugadors");
            DropForeignKey("dbo.Jugadors", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.Arbitroes", "LigaId", "dbo.Ligas");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "LigaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EstadisticasJugadors", new[] { "JugadorId" });
            DropIndex("dbo.Jugadors", new[] { "EquipoId" });
            DropIndex("dbo.Equipoes", new[] { "TorneoId" });
            DropIndex("dbo.Partidoes", new[] { "EquipoVisitanteId" });
            DropIndex("dbo.Partidoes", new[] { "EquipoLocalId" });
            DropIndex("dbo.Partidoes", new[] { "FechaId" });
            DropIndex("dbo.Fechas", new[] { "torneoId" });
            DropIndex("dbo.Torneos", new[] { "LigaId" });
            DropIndex("dbo.Arbitroes", new[] { "LigaId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EstadisticasJugadors");
            DropTable("dbo.Jugadors");
            DropTable("dbo.Equipoes");
            DropTable("dbo.Partidoes");
            DropTable("dbo.Fechas");
            DropTable("dbo.Torneos");
            DropTable("dbo.Ligas");
            DropTable("dbo.Arbitroes");
        }
    }
}
