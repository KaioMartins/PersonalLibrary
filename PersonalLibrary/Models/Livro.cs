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
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime DataCompra { get; set; }
        public Boolean StatusLido { get; set; }

        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public virtual Autor Autor { get; set; }
        

        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

    }
}