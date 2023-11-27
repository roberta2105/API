using AutoMapper;
using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;
using ControleFacil.Api.Exceptions;


namespace ControleFacil.Api.Damain.Services.Classes
{
    public class ApagarService : IServices<ApagarRequestContract, ApagarResponseContract, long>
    {
        private readonly IApagarRepository _apagarRepository;
        public readonly IMapper _mapper;

        public ApagarService(IApagarRepository apagarRepository, IMapper mapper)
        {
            _apagarRepository = apagarRepository;
            _mapper = mapper;
        }

        public async Task<ApagarResponseContract> Adicionar(ApagarRequestContract entidade, long idUsuario)
        {
            Validar(entidade);
            //inicializa a variável Apagar com uma entidade do tipo ApagarRequestContract para ApagarDeLancamento
            var apagar = _mapper.Map<Apagar>(entidade);

            apagar.dataCadastro = DateTime.Now;
            apagar.idUsuario = idUsuario;

            //Adiciona a variável ao banco
            apagar = await _apagarRepository.Adicionar(apagar);

            //retorna a variavel apagar em formato de apagarResponseContract
            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        public async Task<ApagarResponseContract> Atualizar(long id, ApagarRequestContract entidade, long idUsuario)
        {
            Validar(entidade);
            var apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            
            apagar.descricao = entidade.descricao;
            apagar.observacao = entidade.observacao;
            apagar.valorOriginal = entidade.valorOriginal;
            apagar.valorPago = entidade.valorPago;
            apagar.dataPagamento = entidade.dataPagamento;
            apagar.dataReferencia = entidade.dataReferencia;
            apagar.dataVencimento = entidade.dataVencimento;
            apagar.idNaturezaDeLancamento = entidade.idNaturezaDeLancamento;

            apagar = await _apagarRepository.Atualizar(apagar);

            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        public async Task Deletar(long id, long idUsuario)
        {
            var apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _apagarRepository.Deletar(apagar);
        }

        public async Task<IEnumerable<ApagarResponseContract>> Obter(long idUsuario)
        {
            var apagar = await _apagarRepository.ObterPeloUsuario(idUsuario);

            return apagar.Select(apagar => _mapper.Map<ApagarResponseContract>(apagar));
        }

        public async Task<ApagarResponseContract> ObterId(long id, long idUsuario)
        {
            var apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        private async Task<Apagar> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var apagar = await _apagarRepository.Obter(id);

            if(apagar is null || apagar.idUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrado nenhuma apagar de lançamento pelo id {id}");
            }
            return apagar;
        }

        private void Validar (ApagarRequestContract entidade)
        {
            if(entidade.valorOriginal < 0 || entidade.valorPago < 0 )
            {
                throw new BadRequestException("Os campos ValorOriginal e ValorPago não podem ser negativos.");
            }
        }

       
    }
}