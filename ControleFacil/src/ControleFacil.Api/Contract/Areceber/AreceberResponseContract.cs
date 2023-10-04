using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Contract.Areceber
{
    public class AreceberResponseContract : AreceberRequestContract
    {
      
       public long id {get; set;}
       public long idUsuario {get; set;}
       public DateTime dataCadastro {get; set;}
       public DateTime? dataInativacao {get; set;}



    }
}