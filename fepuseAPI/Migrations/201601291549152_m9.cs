namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "NombreApellido", c => c.String());
            DropColumn("dbo.Personas", "Nombre");
            DropColumn("dbo.Personas", "Apellido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personas", "Apellido", c => c.String());
            AddColumn("dbo.Personas", "Nombre", c => c.String());
            DropColumn("dbo.Personas", "NombreApellido");
        }
    }
}
