namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.EquipoTorneos", "ZonaTorneoId", c => c.Int());
            CreateIndex("dbo.EquipoTorneos", "ZonaTorneoId");
            AddForeignKey("dbo.EquipoTorneos", "ZonaTorneoId", "dbo.ZonaTorneos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZonaTorneos", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.EquipoTorneos", "ZonaTorneoId", "dbo.ZonaTorneos");
            DropIndex("dbo.ZonaTorneos", new[] { "TorneoId" });
            DropIndex("dbo.EquipoTorneos", new[] { "ZonaTorneoId" });
            DropColumn("dbo.EquipoTorneos", "ZonaTorneoId");
            DropTable("dbo.ZonaTorneos");
        }
    }
}
