namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jugador : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions");
            DropIndex("dbo.Jugadores", new[] { "ProfesionId" });
            AlterColumn("dbo.Jugadores", "ProfesionId", c => c.Int());
            CreateIndex("dbo.Jugadores", "ProfesionId");
            AddForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions");
            DropIndex("dbo.Jugadores", new[] { "ProfesionId" });
            AlterColumn("dbo.Jugadores", "ProfesionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jugadores", "ProfesionId");
            AddForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions", "Id", cascadeDelete: true);
        }
    }
}
