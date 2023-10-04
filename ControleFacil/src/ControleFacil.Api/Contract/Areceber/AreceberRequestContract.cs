using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ControleFacil.Api.Contract.Areceber
{
    public class AreceberRequestContract
    {
        public long idNaturezaDeLancamento { get; set; }
        public string descricao { get; set; } = string.Empty;
        public string? observacao { get; set; } = string.Empty;
        public double valorOriginal { get; set; }
        public double valorRecebido { get; set; }
        public DateTime? dataReferencia { get; set; }
        public DateTime? dataVencimento { get; set; }
        public DateTime? dataRecebimento { get; set; }





    }
}