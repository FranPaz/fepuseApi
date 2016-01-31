namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartidoJugadors", "EquipoId", c => c.Int(nullable: false));
            CreateIndex("dbo.PartidoJugadors", "EquipoId");
            AddForeignKey("dbo.PartidoJugadors", "EquipoId", "dbo.Equipoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartidoJugadors", "EquipoId", "dbo.Equipoes");
            DropIndex("dbo.PartidoJugadors", new[] { "EquipoId" });
            DropColumn("dbo.PartidoJugadors", "EquipoId");
        }
    }
}
