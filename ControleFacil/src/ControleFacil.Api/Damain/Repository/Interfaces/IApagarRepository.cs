using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Damain.Models;

namespace ControleFacil.Api.Damain.Repository.Interfaces
{
    public interface IApagarRepository : IRepository<Apagar, long>
    {
        Task<IEnumerable<Apagar>> ObterPeloUsuario(long idUsuario);

         Task<IEnumerable<Apagar>> ObterPorNaturezaDeLancamento(long idNaturezaDeLancamento, long idUsuario);
    
    }


}