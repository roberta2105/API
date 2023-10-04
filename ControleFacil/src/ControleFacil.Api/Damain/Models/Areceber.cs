using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Damain.Models
{
    public class Areceber : Titulo
    {
    [Required(ErrorMessage ="Campo obrigatório")]
    public double? valorRecebido { get; set; }
    public DateTime? dataRecebimento { get; set; }
    }
}