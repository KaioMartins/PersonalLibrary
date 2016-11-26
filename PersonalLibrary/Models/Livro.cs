using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalLibrary.Models
{
    public class Livro
    {
        
        public int LivroId { get; set; }

        [Required(ErrorMessage = "Digite o nome do Livro")]
        [MaxLength(60)]
        [Display(Name = "Livro")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Digite o ISBN")]
        [MaxLength(60)]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Digite a Data da Compra do Livro")]
        [Display(Name = "Data da Compra")]
        public DateTime DataCompra { get; set; }

        [Display(Name = "Leitura Concluída")]
        public Boolean StatusLido { get; set; }

        [Required(ErrorMessage = "Cadastre um Autor antes de Cadastrar um Livro")]
        [Display(Name = "Autor")]
        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public virtual Autor Autor { get; set; }
        
        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

    }
}