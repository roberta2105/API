using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Damain.Models;

namespace ControleFacil.Api.Damain.Repository.Interfaces
{
    public interface IAreceberRepository : IRepository<Areceber, long>
    {
        Task<IEnumerable<Areceber>> ObterPeloUsuario(long idUsuario);

        Task<IEnumerable<Areceber>> ObterPorNaturezaDeLancamento(long idNaturezaDeLancamento, long idUsuario);
    }


}