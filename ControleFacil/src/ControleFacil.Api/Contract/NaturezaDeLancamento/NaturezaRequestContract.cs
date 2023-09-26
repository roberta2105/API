using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ControleFacil.Api.Contract.NaturezaDeLancamento
{
    public class NaturezaRequestContract
    {
        public string descricao {get; set;} = string.Empty;
        public string observacao {get; set;} = string.Empty;

    }
}