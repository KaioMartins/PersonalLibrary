using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PersonalLibrary.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("StrConn")
        {
            this.Configuration.LazyLoadingEnabled = false;
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Livro> Livro { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Autor> Autor { get; set; }
    }
}