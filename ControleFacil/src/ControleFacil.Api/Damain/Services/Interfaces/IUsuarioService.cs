using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Contract.Usuario;

namespace ControleFacil.Api.Damain.Services
{
    public interface IUsuarioService : IServices<UsuarioRequestContract, UsuarioResponseContract, long>
    {
        Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest);

        Task<UsuarioResponseContract> Obter(string email);
    
    
    
    }
}