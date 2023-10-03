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
    public class UsuarioRepository : IUsuarioRepository
    {
        //readonly > apenas leitura
        private readonly ApplicationContext _contexto;

        public UsuarioRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            //Adicionando o usuario no context
            await _contexto.Usuario.AddAsync(usuario);
            //Para salvar essa mudança no banco
            await _contexto.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> Atualizar(Usuario usuario)
        {
            //declara uma variável "usuarioBanco" do tipo Usuario que é igual a um Usuario no banco onde o Id do
            //Usuario do banco é igual ao Id do usuario que está sendo passado. Ou seja o usuarioBanco aramazena
            //os valores de Usuario existente no banco.
            Usuario usuarioBanco = _contexto.Usuario.Where(u => u.id == usuario.id)
            //Retorna o primeiro objeto encontrado ou null.
            .FirstOrDefault();

            //Vai no banco e pega o objeto usuarioBanco, acessa os valores das propriedades dele e seta o valor de "usuario" nele.
            _contexto.Entry(usuarioBanco).CurrentValues.SetValues(usuario);
            //Diz ao banco para atualizar os valores de usuarioBanco
            _contexto.Update<Usuario>(usuarioBanco);

            //Diz ao banco para salvar essa alteração.
            await _contexto.SaveChangesAsync();

            //Retorna o usuarioBanco atualizado.
            return usuarioBanco;

        }

        public async Task Deletar(Usuario usuario)
        {
            //Acha a entidade e muda o status para deletado
            _contexto.Entry(usuario).State = EntityState.Deleted;
            await _contexto.SaveChangesAsync();
        }

        public async Task<Usuario?> Obter(string email)
        {
            //Retorna o email apenas para leitura e exibição
            return await _contexto.Usuario.AsNoTracking().Where(u => u.Email == email)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Usuario>> Obter()
        {
            //Retorna uma lista de usuários ordenados por id
            return await _contexto.Usuario.AsNoTracking()
                                        .OrderBy(u => u.id)
                                        .ToListAsync();
        }

        public async Task<Usuario> Obter(long id)
        {
            return await _contexto.Usuario.AsNoTracking().Where(u => u.id == id)
            .FirstOrDefaultAsync();
        }
    }
}