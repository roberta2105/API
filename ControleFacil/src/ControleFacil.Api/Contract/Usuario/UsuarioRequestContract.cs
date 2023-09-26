using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Contract.Usuario
{
    public class UsuarioRequestContract : UsuarioLoginRequestContract
    {
        //Para cadastrar um novo usuário

        //Email 
        //Senha
        public DateTime? DataInativacao { get; set; }
    }
}