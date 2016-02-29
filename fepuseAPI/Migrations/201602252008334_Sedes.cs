namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sedes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sedes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Partidoes", "SedeId", c => c.Int());
            CreateIndex("dbo.Partidoes", "SedeId");
            AddForeignKey("dbo.Partidoes", "SedeId", "dbo.Sedes", "Id");
            DropColumn("dbo.Partidoes", "Sede");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partidoes", "Sede", c => c.String());
            DropForeignKey("dbo.Partidoes", "SedeId", "dbo.Sedes");
            DropIndex("dbo.Partidoes", new[] { "SedeId" });
            DropColumn("dbo.Partidoes", "SedeId");
            DropTable("dbo.Sedes");
        }
    }
}
