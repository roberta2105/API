using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Contract.Usuario
{
    public class UsuarioResponseContract : UsuarioRequestContract
    {
        //Email
        //Senha
        //DataInativacao
        public long id { get; set; } //gerado automaticamente pelo banco
        public DateTime DataCadastro { get; set; } //gerado na hora da criação
    }
}