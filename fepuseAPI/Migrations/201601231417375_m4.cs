namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipoJugadorTorneos", "TorneoId", "dbo.Torneos");
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "TorneoId" });
            AlterColumn("dbo.EquipoJugadorTorneos", "TorneoId", c => c.Int());
            CreateIndex("dbo.EquipoJugadorTorneos", "TorneoId");
            AddForeignKey("dbo.EquipoJugadorTorneos", "TorneoId", "dbo.Torneos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipoJugadorTorneos", "TorneoId", "dbo.Torneos");
            DropIndex("dbo.EquipoJugadorTorneos", new[] { "TorneoId" });
            AlterColumn("dbo.EquipoJugadorTorneos", "TorneoId", c => c.Int(nullable: false));
            CreateIndex("dbo.EquipoJugadorTorneos", "TorneoId");
            AddForeignKey("dbo.EquipoJugadorTorneos", "TorneoId", "dbo.Torneos", "Id", cascadeDelete: true);
        }
    }
}
