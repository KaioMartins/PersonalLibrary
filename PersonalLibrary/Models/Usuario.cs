using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalLibrary.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual List<Livro> Livros { get; set; }
        public virtual List<Emprestimo> Emprestimos { get; set; }
        public virtual List<Autor> Autor { get; set; }
    }
}