using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Contract.Apagar;

namespace ControleFacil.Api.Damain.Services.Interfaces
{
    public interface IApagarService  : IServices<ApagarRequestContract, ApagarResponseContract, long>
    {
        Task<IEnumerable<ApagarResponseContract>> ObterPorNaturezaDeLancamento(long idNaturezaDeLancamento, long idUsuario);
        
    }
}