namespace PersonalLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoAutor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Autor", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Autor", "Nome", c => c.String());
        }
    }
}
