using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;
using ControleFacil.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Damain.Repository.Classes
{
    public class AreceberRepository : IAreceberRepository
    {
        //readonly > apenas leitura
        private readonly ApplicationContext _contexto;

        public AreceberRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<Areceber> Adicionar(Areceber entidade)
        {
            await _contexto.Areceber.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<Areceber> Atualizar(Areceber entidade)
        {

            Areceber entidadeBanco = _contexto.Areceber
            .Where(n => n.id == entidade.id)
            .FirstOrDefault();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);

            _contexto.Update<Areceber>(entidadeBanco);

            await _contexto.SaveChangesAsync();

            return entidadeBanco;

        }

        public async Task Deletar(Areceber areceber)
        {
            //Deleção lógica
            // areceber.dataInativacao = DateTime.Now;
            // await Atualizar(areceber);

            _contexto.Entry(areceber).State = EntityState.Deleted;
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Areceber>> Obter()
        {
            //Retorna uma lista de naturezas ordenados por id
            return await _contexto.Areceber.AsNoTracking()
            .OrderBy(n => n.id)
            .ToListAsync();
        }

        public async Task<Areceber?> Obter(long id)
        {
            return await _contexto.Areceber.AsNoTracking().Where(n => n.id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Areceber>> ObterPeloUsuario(long idUsuario)
        {
            return await _contexto.Areceber.AsNoTracking().Where(n => n.idUsuario == idUsuario)
            .OrderBy(n => n.id)
            .ToListAsync();
        }

        public async Task<IEnumerable<Areceber>> ObterPorNaturezaDeLancamento(long idNaturezaDeLancamento, long idUsuario)
        {
            return await _contexto.Areceber
                .Where(r => r.idNaturezaDeLancamento == idNaturezaDeLancamento && r.idUsuario == idUsuario)
                .ToListAsync();
        }

    }
}