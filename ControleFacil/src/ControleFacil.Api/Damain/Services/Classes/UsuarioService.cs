using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;

namespace ControleFacil.Api.Damain.Services.Classes
{
    public class UsuarioService : IUsuarioService
    {

        //Um serviço se comunica com um repositório permitindo a interação com os dados da entidade por meio de uma
        //injeção de dependência.
        private readonly IUsuarioRepository _usuarioRepository;

        //Uma injeção de dependência para mapear objetos entre tipos diferentes.
        public readonly IMapper _mapper;


        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }


        public async Task<UsuarioResponseContract> Adicionar(UsuarioRequestContract entidade, long idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);
        }



        public async Task<UsuarioResponseContract> Atualizar(long id, UsuarioRequestContract entidade, long idUsuario)
        {
            _ = await Obter(id) ?? throw new Exception("Usuário não encontrado para atualização");

            var usuario = _mapper.Map<Usuario>(entidade);
            usuario.Id = id;
            usuario.Senha = GerarHashSenha(entidade.Senha);

            usuario = await _usuarioRepository.Atualizar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest)
        {
            throw new NotImplementedException();
        }

        public async Task Deletar(long id, long idUsuario)
        {
            //?? > Se não
            var usuario = await Obter(id) ?? throw new Exception("Usuário não encontrado para deleção");
            
            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));


        }

        public async Task<IEnumerable<UsuarioResponseContract>> Obter(long idUsuario)
        {
            return await Obter(idUsuario);
        }

        public async Task<UsuarioResponseContract> Obter(long id, long idUsuario)//idUsuario não usado
        {
            var usuario = await _usuarioRepository.Obter(id);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public async Task<UsuarioResponseContract> Obter(string email)
        {
            var usuario = await _usuarioRepository.Obter(email);
            return _mapper.Map<UsuarioResponseContract>(usuario);

        }
        private string GerarHashSenha(string senha)
        {
            string hashSenha;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
                byte[] bytesHashSenha = sha256.ComputeHash(bytesSenha);
                hashSenha = BitConverter.ToString(bytesHashSenha).ToLower();
            }

            return hashSenha;
        }

    }
}