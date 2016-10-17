using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalLibrary.Models
{
    public class Emprestimo
    {
        public int EmprestimoId { get; set; }
        public Boolean Emprestado { get; set; }
        public string PessoaEmprestimo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public virtual Livro Titulo { get; set; }
        

        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}