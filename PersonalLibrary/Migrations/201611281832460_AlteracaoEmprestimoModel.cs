namespace PersonalLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoEmprestimoModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emprestimo", "PessoaEmprestimo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emprestimo", "PessoaEmprestimo", c => c.String());
        }
    }
}
