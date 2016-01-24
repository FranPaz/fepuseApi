namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TorneoArbitroes", "Torneo_Id", "dbo.Torneos");
            DropForeignKey("dbo.TorneoArbitroes", "Arbitro_Id", "dbo.Arbitros");
            DropIndex("dbo.TorneoArbitroes", new[] { "Torneo_Id" });
            DropIndex("dbo.TorneoArbitroes", new[] { "Arbitro_Id" });
            AddColumn("dbo.Arbitros", "LigaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Arbitros", "LigaId");
            AddForeignKey("dbo.Arbitros", "LigaId", "dbo.Ligas", "Id", cascadeDelete: true);
            DropTable("dbo.TorneoArbitroes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TorneoArbitroes",
                c => new
                    {
                        Torneo_Id = c.Int(nullable: false),
                        Arbitro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Torneo_Id, t.Arbitro_Id });
            
            DropForeignKey("dbo.Arbitros", "LigaId", "dbo.Ligas");
            DropIndex("dbo.Arbitros", new[] { "LigaId" });
            DropColumn("dbo.Arbitros", "LigaId");
            CreateIndex("dbo.TorneoArbitroes", "Arbitro_Id");
            CreateIndex("dbo.TorneoArbitroes", "Torneo_Id");
            AddForeignKey("dbo.TorneoArbitroes", "Arbitro_Id", "dbo.Arbitros", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TorneoArbitroes", "Torneo_Id", "dbo.Torneos", "Id", cascadeDelete: true);
        }
    }
}
