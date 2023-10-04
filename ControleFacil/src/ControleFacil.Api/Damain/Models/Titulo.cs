using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Damain.Models
{
    public abstract class Titulo
    {
        
        [Key]
        public long id { get; set; }

        [Required]
        public long idUsuario { get; set; } 

        public Usuario Usuario { get; set; } 
    
        [Required]
        public long idNaturezaDeLancamento { get; set; } 

        public NaturezaDeLancamento NaturezaDeLancamento { get; set; } 

        [Required(ErrorMessage ="Campo obrigatório")]
        public string descricao { get; set; } = string.Empty;

        public string observacao { get; set; } = string.Empty;

        [Required(ErrorMessage ="Campo obrigatório")]
        public double valorOriginal { get; set; }

        [Required]
        public DateTime dataCadastro { get; set; }

        public DateTime? dataReferencia { get; set; }

        [Required]
        public DateTime? dataVencimento { get; set; }



        public DateTime? dataInativacao { get; set; }


    }
}