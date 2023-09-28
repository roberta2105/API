using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Contract.NaturezaDeLancamento;
using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;


namespace ControleFacil.Api.Damain.Services.Classes
{
    public class NaturezaService : IServices<NaturezaRequestContract, NaturezaResponseContract, long>
    {
        private readonly INaturezaRepository _naturezaRepository;
        public readonly IMapper _mapper;

        public NaturezaService(INaturezaRepository naturezaRepository, IMapper mapper)
        {
            _naturezaRepository = naturezaRepository;
            _mapper = mapper;
        }

        public async Task<NaturezaResponseContract> Adicionar(NaturezaRequestContract entidade, long idUsuario)
        {
            //inicializa a variável natureza com uma entidade do tipo NaturezaRequestContract para NaturezaDeLancamento
            var natureza = _mapper.Map<NaturezaDeLancamento>(entidade);

            natureza.dataCadastro = DateTime.Now;
            natureza.idUsuario = idUsuario;

            //Adiciona a variável ao banco
            natureza = await _naturezaRepository.Adicionar(natureza);

            //retorna a variavel natureza em formato de NaturezaResponseContract
            return _mapper.Map<NaturezaResponseContract>(natureza);
        }

        public async Task<NaturezaResponseContract> Atualizar(long id, NaturezaRequestContract entidade, long idUsuario)
        {
            var natureza = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            
            natureza.descricao = entidade.descricao;
            natureza.observacao = entidade.observacao;

            natureza = await _naturezaRepository.Atualizar(natureza);

            return _mapper.Map<NaturezaResponseContract>(natureza);
        }

        public async Task Deletar(long id, long idUsuario)
        {
            var natureza = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _naturezaRepository.Deletar(natureza);
        }

        public async Task<IEnumerable<NaturezaResponseContract>> Obter(long idUsuario)
        {
            var natureza = await _naturezaRepository.ObterPeloUsuario(idUsuario);

            return natureza.Select(natureza => _mapper.Map<NaturezaResponseContract>(natureza));
        }

        public async Task<NaturezaResponseContract> Obter(long id, long idUsuario)
        {
            var natureza = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<NaturezaResponseContract>(natureza);
        }

        private async Task<NaturezaDeLancamento> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var NaturezaDeLancamento = await _naturezaRepository.Obter(id);

            if(NaturezaDeLancamento is null || NaturezaDeLancamento.idUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrado nenhuma natureza de lançamento pelo id {id}");
            }
            return NaturezaDeLancamento;
        }

       
    }
}