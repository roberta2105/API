using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Damain.Models
{
    public class NaturezaDeLancamento
    {
        [Key]
        public long id {get; set;}

        [Required]
        public long idUsuario {get; set;}

        public Usuario Usuario {get; set;}

        [Required (ErrorMessage = "A descrição é obrigatória")]
        public string descricao {get; set;} = string.Empty;

        public string observacao {get; set;} = string.Empty;

        [Required]
        public DateTime dataCadastro {get; set;}
        
        public DateTime? dataInativacao {get; set;}





    }
}