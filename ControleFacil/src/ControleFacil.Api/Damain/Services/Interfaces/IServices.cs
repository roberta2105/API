using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Damain.Services
{
    /// <summary>
    /// Interface generica para criação de serviços do tipo CRUD
    /// </summary>
    /// <typeparam name="RQ">Contrato de Request</typeparam>
    /// <typeparam name="RS">Contrato de Response</typeparam>
    /// <typeparam name="I">Tipo do Id</typeparam> <summary>

    public interface IServices<RQ, RS, I> where RQ : class
    {
        //Obtem todas as entidades de acordo com o usuario específico.
        Task<IEnumerable<RS>> Obter(I idUsuario);
        
        //Obtem a entidade por id de acordo com o usuario específico
        Task<RS> ObterId(I id, I idUsuario);

        //Adiciona a entidade de acordo com o id do usuario.
        Task<RS> Adicionar(RQ entidade, I idUsuario);

        //Atualiza uma entidade específica de um usuário específico.
        Task<RS> Atualizar(I id, RQ entidade, I idUsuario);

        //Deleta uma entidade específica de um usuário específico.
        Task Deletar(I id, I idUsuario);
    }
}