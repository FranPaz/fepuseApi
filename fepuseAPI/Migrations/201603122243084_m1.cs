namespace fepuseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Torneos", "FechaInicio", c => c.String());
            AddColumn("dbo.Torneos", "FechaFin", c => c.String());
            AddColumn("dbo.Torneos", "Finalizado", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Partidoes", "Dia", c => c.String());
            DropColumn("dbo.Torneos", "AñoInicio");
            DropColumn("dbo.Torneos", "AñoFin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Torneos", "AñoFin", c => c.String());
            AddColumn("dbo.Torneos", "AñoInicio", c => c.String());
            AlterColumn("dbo.Partidoes", "Dia", c => c.DateTime(nullable: false));
            DropColumn("dbo.Torneos", "Finalizado");
            DropColumn("dbo.Torneos", "FechaFin");
            DropColumn("dbo.Torneos", "FechaInicio");
        }
    }
}
