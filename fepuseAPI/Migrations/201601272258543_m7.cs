namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TorneoEquipoes", "Torneo_Id", "dbo.Torneos");
            DropForeignKey("dbo.TorneoEquipoes", "Equipo_Id", "dbo.Equipoes");
            DropIndex("dbo.TorneoEquipoes", new[] { "Torneo_Id" });
            DropIndex("dbo.TorneoEquipoes", new[] { "Equipo_Id" });
            CreateTable(
                "dbo.EquipoTorneos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TorneoId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.TorneoId)
                .Index(t => t.EquipoId);
            
            DropTable("dbo.TorneoEquipoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TorneoEquipoes",
                c => new
                    {
                        Torneo_Id = c.Int(nullable: false),
                        Equipo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Torneo_Id, t.Equipo_Id });
            
            DropForeignKey("dbo.EquipoTorneos", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.EquipoTorneos", "EquipoId", "dbo.Equipoes");
            DropIndex("dbo.EquipoTorneos", new[] { "EquipoId" });
            DropIndex("dbo.EquipoTorneos", new[] { "TorneoId" });
            DropTable("dbo.EquipoTorneos");
            CreateIndex("dbo.TorneoEquipoes", "Equipo_Id");
            CreateIndex("dbo.TorneoEquipoes", "Torneo_Id");
            AddForeignKey("dbo.TorneoEquipoes", "Equipo_Id", "dbo.Equipoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TorneoEquipoes", "Torneo_Id", "dbo.Torneos", "Id", cascadeDelete: true);
        }
    }
}
