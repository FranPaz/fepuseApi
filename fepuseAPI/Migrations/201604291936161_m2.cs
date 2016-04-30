namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Noticias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Contenido = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImagenNoticias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoticiaId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        FileSizeInBytes = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.NoticiaId, cascadeDelete: true)
                .Index(t => t.NoticiaId);
            
            CreateTable(
                "dbo.NoticiasCategoria",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.NoticiasLiga",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LigaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.Id)
                .ForeignKey("dbo.Ligas", t => t.LigaId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.LigaId);
            
            CreateTable(
                "dbo.NoticiasTorneo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TorneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Noticias", t => t.Id)
                .ForeignKey("dbo.Torneos", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TorneoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoticiasTorneo", "TorneoId", "dbo.Torneos");
            DropForeignKey("dbo.NoticiasTorneo", "Id", "dbo.Noticias");
            DropForeignKey("dbo.NoticiasLiga", "LigaId", "dbo.Ligas");
            DropForeignKey("dbo.NoticiasLiga", "Id", "dbo.Noticias");
            DropForeignKey("dbo.NoticiasCategoria", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.NoticiasCategoria", "Id", "dbo.Noticias");
            DropForeignKey("dbo.ImagenNoticias", "NoticiaId", "dbo.Noticias");
            DropIndex("dbo.NoticiasTorneo", new[] { "TorneoId" });
            DropIndex("dbo.NoticiasTorneo", new[] { "Id" });
            DropIndex("dbo.NoticiasLiga", new[] { "LigaId" });
            DropIndex("dbo.NoticiasLiga", new[] { "Id" });
            DropIndex("dbo.NoticiasCategoria", new[] { "CategoriaId" });
            DropIndex("dbo.NoticiasCategoria", new[] { "Id" });
            DropIndex("dbo.ImagenNoticias", new[] { "NoticiaId" });
            DropTable("dbo.NoticiasTorneo");
            DropTable("dbo.NoticiasLiga");
            DropTable("dbo.NoticiasCategoria");
            DropTable("dbo.ImagenNoticias");
            DropTable("dbo.Noticias");
        }
    }
}
