using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Contract.Areceber;

namespace ControleFacil.Api.Damain.Services.Interfaces
{
    public interface IReceberService  : IServices<AreceberRequestContract, AreceberResponseContract, long>
    {
        Task<IEnumerable<AreceberResponseContract>> ObterPorNaturezaDeLancamento(long idNaturezaDeLancamento, long idUsuario);
        
    }
}