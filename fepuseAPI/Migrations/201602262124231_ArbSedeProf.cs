namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArbSedeProf : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Partidoes", new[] { "ArbitroId" });
            CreateTable(
                "dbo.Sedes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profesions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Jugadores", "ProfesionId", c => c.Int(nullable: false));
            AddColumn("dbo.Partidoes", "SedeId", c => c.Int());
            AlterColumn("dbo.Partidoes", "ArbitroId", c => c.Int());
            CreateIndex("dbo.Partidoes", "ArbitroId");
            CreateIndex("dbo.Partidoes", "SedeId");
            CreateIndex("dbo.Jugadores", "ProfesionId");
            AddForeignKey("dbo.Partidoes", "SedeId", "dbo.Sedes", "Id");
            AddForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions", "Id", cascadeDelete: true);
            DropColumn("dbo.Jugadores", "Profesion");
            DropColumn("dbo.Partidoes", "Sede");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Partidoes", "Sede", c => c.String());
            AddColumn("dbo.Jugadores", "Profesion", c => c.String());
            DropForeignKey("dbo.Jugadores", "ProfesionId", "dbo.Profesions");
            DropForeignKey("dbo.Partidoes", "SedeId", "dbo.Sedes");
            DropIndex("dbo.Jugadores", new[] { "ProfesionId" });
            DropIndex("dbo.Partidoes", new[] { "SedeId" });
            DropIndex("dbo.Partidoes", new[] { "ArbitroId" });
            AlterColumn("dbo.Partidoes", "ArbitroId", c => c.Int(nullable: false));
            DropColumn("dbo.Partidoes", "SedeId");
            DropColumn("dbo.Jugadores", "ProfesionId");
            DropTable("dbo.Profesions");
            DropTable("dbo.Sedes");
            CreateIndex("dbo.Partidoes", "ArbitroId");
        }
    }
}
