namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jugadores : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jugadores", "Federado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jugadores", "Federado", c => c.String());
        }
    }
}
