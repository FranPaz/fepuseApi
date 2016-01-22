namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TorneoEquipoes",
                c => new
                    {
                        Torneo_Id = c.Int(nullable: false),
                        Equipo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Torneo_Id, t.Equipo_Id })
                .ForeignKey("dbo.Torneos", t => t.Torneo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Equipoes", t => t.Equipo_Id, cascadeDelete: true)
                .Index(t => t.Torneo_Id)
                .Index(t => t.Equipo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TorneoEquipoes", "Equipo_Id", "dbo.Equipoes");
            DropForeignKey("dbo.TorneoEquipoes", "Torneo_Id", "dbo.Torneos");
            DropIndex("dbo.TorneoEquipoes", new[] { "Equipo_Id" });
            DropIndex("dbo.TorneoEquipoes", new[] { "Torneo_Id" });
            DropTable("dbo.TorneoEquipoes");
        }
    }
}
