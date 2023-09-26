using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;
using ControleFacil.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Damain.Repository.Classes
{
    public class NaturezaRepository : INaturezaRepository
    {
        //readonly > apenas leitura
        private readonly ApplicationContext _contexto;

        public NaturezaRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<NaturezaDeLancamento> Adicionar(NaturezaDeLancamento natureza)
        {
            //Adicionando o usuario no context
            await _contexto.NaturezaDeLancamento.AddAsync(natureza);
            //Para salvar essa mudança no banco
            await _contexto.SaveChangesAsync();

            return natureza;
        }

        public async Task<NaturezaDeLancamento> Atualizar(NaturezaDeLancamento natureza)
        {
            //declara uma variável "usuarioBanco" do tipo Usuario que é igual a um Usuario no banco onde o Id do
            //Usuario do banco é igual ao Id do usuario que está sendo passado. Ou seja o usuarioBanco aramazena
            //os valores de Usuario existente no banco.
            NaturezaDeLancamento naturezaBanco = _contexto.NaturezaDeLancamento.Where(n => n.id == natureza.id)
            //Retorna o primeiro objeto encontrado ou null.
            .FirstOrDefault();

            //Vai no banco e pega o objeto usuarioBanco, acessa os valores das propriedades dele e seta o valor de "usuario" nele.
            _contexto.Entry(naturezaBanco).CurrentValues.SetValues(natureza);
            //Diz ao banco para atualizar os valores de usuarioBanco
            _contexto.Update<NaturezaDeLancamento>(naturezaBanco);

            //Diz ao banco para salvar essa alteração.
            await _contexto.SaveChangesAsync();

            //Retorna a naturezaBanco atualizada.
            return naturezaBanco;

        }

        //Deleção lógica
        public async Task Deletar(NaturezaDeLancamento natureza)
        {
            natureza.dataInativacao = DateTime.Now;
            await Atualizar(natureza);
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> Obter()
        {
            //Retorna uma lista de naturezas ordenados por id
            return await _contexto.NaturezaDeLancamento.AsNoTracking()
            .OrderBy(n => n.id)
            .ToListAsync();
        }

        public async Task<NaturezaDeLancamento> Obter(long id)
        {
            return await _contexto.NaturezaDeLancamento.AsNoTracking().Where(n => n.id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> ObterPeloUsuario(long idUsuario)
        {
            return await _contexto.NaturezaDeLancamento.AsNoTracking().Where(n => n.idUsuario == idUsuario)
            .OrderBy(n => n.id)
            .ToListAsync();
        }

    }
}