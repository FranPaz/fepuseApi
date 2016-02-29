namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profesion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profesions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Jugadores", "ProfesionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jugadores", "ProfesionId");
            AddForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions", "Id", cascadeDelete: true);
            DropColumn("dbo.Jugadores", "Profesion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jugadores", "Profesion", c => c.String());
            DropForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions");
            DropIndex("dbo.Jugadores", new[] { "ProfesionId" });
            DropColumn("dbo.Jugadores", "ProfesionId");
            DropTable("dbo.Profesions");
        }
    }
}
