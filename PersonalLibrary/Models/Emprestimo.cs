using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalLibrary.Models
{
    public class Emprestimo
    {
        public int EmprestimoId { get; set; }
        [Display(Name = "Emprestado")]
        public Boolean Emprestado { get; set; }

        [Display(Name = "Emprestado para: ")]
        public string PessoaEmprestimo { get; set; }

        [Display(Name = "Data do Empréstimo")]
        public DateTime DataEmprestimo { get; set; }

        [Display(Name = "Data da Devolução")]
        public DateTime DataDevolucao { get; set; }


        [Display(Name = "Livro")]
        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public virtual Livro Livro { get; set; }


        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}