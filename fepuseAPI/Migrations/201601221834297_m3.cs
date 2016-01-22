namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipoes", "LigaId", c => c.Int());
            CreateIndex("dbo.Equipoes", "LigaId");
            AddForeignKey("dbo.Equipoes", "LigaId", "dbo.Ligas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipoes", "LigaId", "dbo.Ligas");
            DropIndex("dbo.Equipoes", new[] { "LigaId" });
            DropColumn("dbo.Equipoes", "LigaId");
        }
    }
}
