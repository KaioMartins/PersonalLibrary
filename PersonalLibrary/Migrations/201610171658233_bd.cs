namespace PersonalLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autor",
                c => new
                    {
                        AutorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AutorId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Senha = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Emprestimo",
                c => new
                    {
                        EmprestimoId = c.Int(nullable: false, identity: true),
                        Emprestado = c.Boolean(nullable: false),
                        PessoaEmprestimo = c.String(),
                        DataEmprestimo = c.DateTime(nullable: false),
                        DataDevolucao = c.DateTime(nullable: false),
                        LivroId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmprestimoId)
                .ForeignKey("dbo.Livro", t => t.LivroId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.LivroId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Livro",
                c => new
                    {
                        LivroId = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        ISBN = c.String(),
                        DataCompra = c.DateTime(nullable: false),
                        StatusLido = c.Boolean(nullable: false),
                        AutorId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LivroId)
                .ForeignKey("dbo.Autor", t => t.AutorId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.AutorId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emprestimo", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Emprestimo", "LivroId", "dbo.Livro");
            DropForeignKey("dbo.Livro", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Livro", "AutorId", "dbo.Autor");
            DropForeignKey("dbo.Autor", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.Livro", new[] { "UsuarioId" });
            DropIndex("dbo.Livro", new[] { "AutorId" });
            DropIndex("dbo.Emprestimo", new[] { "UsuarioId" });
            DropIndex("dbo.Emprestimo", new[] { "LivroId" });
            DropIndex("dbo.Autor", new[] { "UsuarioId" });
            DropTable("dbo.Livro");
            DropTable("dbo.Emprestimo");
            DropTable("dbo.Usuario");
            DropTable("dbo.Autor");
        }
    }
}
