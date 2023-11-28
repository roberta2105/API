using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;
using ControleFacil.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Damain.Repository.Classes
{
    public class ApagarRepository : IApagarRepository
    {
        //readonly > apenas leitura
        private readonly ApplicationContext _contexto;

        public ApagarRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<Apagar> Adicionar(Apagar apagar)
        {
            //Adicionando o usuario no context
            await _contexto.Apagar.AddAsync(apagar);
            //Para salvar essa mudança no banco
            await _contexto.SaveChangesAsync();

            return apagar;
        }

        public async Task<Apagar> Atualizar(Apagar apagar)
        {
            //declara uma variável "usuarioBanco" do tipo Usuario que é igual a um Usuario no banco onde o Id do
            //Usuario do banco é igual ao Id do usuario que está sendo passado. Ou seja o usuarioBanco aramazena
            //os valores de Usuario existente no banco.
            Apagar apagarBanco = _contexto.Apagar.Where(n => n.id == apagar.id)
            //Retorna o primeiro objeto encontrado ou null.
            .FirstOrDefault();

            //Vai no banco e pega o objeto usuarioBanco, acessa os valores das propriedades dele e seta o valor de "usuario" nele.
            _contexto.Entry(apagarBanco).CurrentValues.SetValues(apagar);
            //Diz ao banco para atualizar os valores de usuarioBanco
            _contexto.Update<Apagar>(apagarBanco);

            //Diz ao banco para salvar essa alteração.
            await _contexto.SaveChangesAsync();

            //Retorna a ApagarBanco atualizada.
            return apagarBanco;

        }

        
        public async Task Deletar(Apagar apagar)
        {
            //Deleção lógica
            // apagar.dataInativacao = DateTime.Now;
            // await Atualizar(apagar);

             _contexto.Entry(apagar).State = EntityState.Deleted;
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Apagar>> Obter()
        {
            //Retorna uma lista de naturezas ordenados por id
            return await _contexto.Apagar.AsNoTracking()
            .OrderBy(n => n.id)
            .ToListAsync();
        }

        public async Task<Apagar?> Obter(long id)
        {
            return await _contexto.Apagar.AsNoTracking().Where(n => n.id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Apagar>> ObterPeloUsuario(long idUsuario)
        {
            return await _contexto.Apagar.AsNoTracking().Where(n => n.idUsuario == idUsuario)
            .OrderBy(n => n.id)
            .ToListAsync();
        }

        public async Task<IEnumerable<Apagar>> ObterPorNaturezaDeLancamento(long idNaturezaDeLancamento, long idUsuario)
        {
                 return await _contexto.Apagar
                .Where(r => r.idNaturezaDeLancamento == idNaturezaDeLancamento && r.idUsuario == idUsuario)
                .ToListAsync();;
        }
    }
}