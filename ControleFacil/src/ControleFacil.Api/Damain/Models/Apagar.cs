using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Damain.Models
{
    public class Apagar : Titulo
    {
    public double? valorPago { get; set; }
    public DateTime? dataPagamento { get; set; }
    }
}