namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Arbitros", "TorneoId", "dbo.Torneos");
            DropIndex("dbo.Arbitros", new[] { "TorneoId" });
            CreateTable(
                "dbo.TorneoArbitroes",
                c => new
                    {
                        Torneo_Id = c.Int(nullable: false),
                        Arbitro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Torneo_Id, t.Arbitro_Id })
                .ForeignKey("dbo.Torneos", t => t.Torneo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Arbitros", t => t.Arbitro_Id, cascadeDelete: true)
                .Index(t => t.Torneo_Id)
                .Index(t => t.Arbitro_Id);
            
            DropColumn("dbo.Arbitros", "TorneoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arbitros", "TorneoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TorneoArbitroes", "Arbitro_Id", "dbo.Arbitros");
            DropForeignKey("dbo.TorneoArbitroes", "Torneo_Id", "dbo.Torneos");
            DropIndex("dbo.TorneoArbitroes", new[] { "Arbitro_Id" });
            DropIndex("dbo.TorneoArbitroes", new[] { "Torneo_Id" });
            DropTable("dbo.TorneoArbitroes");
            CreateIndex("dbo.Arbitros", "TorneoId");
            AddForeignKey("dbo.Arbitros", "TorneoId", "dbo.Torneos", "Id", cascadeDelete: true);
        }
    }
}
