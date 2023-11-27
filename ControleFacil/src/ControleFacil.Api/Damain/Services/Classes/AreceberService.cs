using AutoMapper;
using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;
using ControleFacil.Api.Damain.Services.Interfaces;
using ControleFacil.Api.Exceptions;


namespace ControleFacil.Api.Damain.Services.Classes
{
    public class AreceberService : IReceberService
    {
        private readonly IAreceberRepository _areceberRepository;
        public readonly IMapper _mapper;

        public AreceberService(IAreceberRepository areceberRepository, IMapper mapper)
        {
            _areceberRepository = areceberRepository;
            _mapper = mapper;
        }

        public async Task<AreceberResponseContract> Adicionar(AreceberRequestContract entidade, long idUsuario)
        {
            Validar(entidade);
            //inicializa a variável Areceber com uma entidade do tipo AreceberRequestContract para AreceberDeLancamento
            var areceber = _mapper.Map<Areceber>(entidade);

            areceber.dataCadastro = DateTime.Now;
            areceber.idUsuario = idUsuario;

            //Adiciona a variável ao banco
            areceber = await _areceberRepository.Adicionar(areceber);

            //retorna a variavel Areceber em formato de AreceberResponseContract
            return _mapper.Map<AreceberResponseContract>(areceber);
        }

        public async Task<AreceberResponseContract> Atualizar(long id, AreceberRequestContract entidade, long idUsuario)
        {
            Validar(entidade);
            var Areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            Areceber.descricao = entidade.descricao;
            Areceber.observacao = entidade.observacao;
            Areceber.valorOriginal = entidade.valorOriginal;
            Areceber.valorRecebido = entidade.valorRecebido;
            Areceber.dataRecebimento = entidade.dataRecebimento;
            Areceber.dataReferencia = entidade.dataReferencia;
            Areceber.dataVencimento = entidade.dataVencimento;
            Areceber.idNaturezaDeLancamento = entidade.idNaturezaDeLancamento;

            Areceber = await _areceberRepository.Atualizar(Areceber);

            return _mapper.Map<AreceberResponseContract>(Areceber);
        }

        public async Task Deletar(long id, long idUsuario)
        {
            var areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario) ?? throw new NotFoundException("Recebimento não encontrado para deleção");

            await _areceberRepository.Deletar(_mapper.Map<Areceber>(areceber));
        }

        public async Task<IEnumerable<AreceberResponseContract>> Obter(long idUsuario)
        {
            var areceber = await _areceberRepository.ObterPeloUsuario(idUsuario);

            return areceber.Select(areceber => _mapper.Map<AreceberResponseContract>(areceber));
        }

        public async Task<AreceberResponseContract> ObterId(long id, long idUsuario)
        {
            var areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<AreceberResponseContract>(areceber);
        }


        private async Task<Areceber> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var areceber = await _areceberRepository.Obter(id);

            if (areceber is null || areceber.idUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrado nenhum recebimento pelo id {id}");
            }
            return areceber;
        }

        public async Task<IEnumerable<AreceberResponseContract>> ObterPorNaturezaDeLancamento(long idNaturezaDeLancamento, long idUsuario)
        {
            var recebimentos = await _areceberRepository.ObterPorNaturezaDeLancamento(idNaturezaDeLancamento, idUsuario);

            return recebimentos.Select(recebimento => _mapper.Map<AreceberResponseContract>(recebimento));
        }


        private void Validar(AreceberRequestContract entidade)
        {
            if (entidade.valorOriginal < 0 || entidade.valorRecebido < 0)
            {
                throw new BadRequestException("Os campos ValorOriginal e ValorRecebido não podem ser negativos.");
            }
        }

     
    }
}