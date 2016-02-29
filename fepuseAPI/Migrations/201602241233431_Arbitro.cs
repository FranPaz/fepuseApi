namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Arbitro : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Partidoes", new[] { "ArbitroId" });
            AlterColumn("dbo.Partidoes", "ArbitroId", c => c.Int());
            CreateIndex("dbo.Partidoes", "ArbitroId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Partidoes", new[] { "ArbitroId" });
            AlterColumn("dbo.Partidoes", "ArbitroId", c => c.Int(nullable: false));
            CreateIndex("dbo.Partidoes", "ArbitroId");
        }
    }
}
