using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalLibrary.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Display(Name = "Usuário")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        public string Senha { get; set; }

        public virtual List<Livro> Livros { get; set; }
        public virtual List<Emprestimo> Emprestimos { get; set; }
        public virtual List<Autor> Autor { get; set; }
    }
}