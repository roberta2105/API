using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Damain.Repository.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Damain.Services.Classes
{
    public class UsuarioService : IUsuarioService
    {

        //Um serviço se comunica com um repositório permitindo a interação com os dados da entidade por meio de uma
        //injeção de dependência.
        private readonly IUsuarioRepository _usuarioRepository;

        //Uma injeção de dependência para mapear objetos entre
        public readonly IMapper _mapper;

        private readonly TokenService _tokenService;


        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            TokenService tokenService)
        {
            //Retorna os métodos de IUsuarioRepository e por injeção de dependência retorna as classes herdadas por essa classe
            _usuarioRepository = usuarioRepository;
            //Retorna uma entidade apartir de um RS e um RQ
            _mapper = mapper;

            _tokenService = tokenService;
        }

        public async Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest)
        {
            UsuarioResponseContract usuario = await Obter(usuarioLoginRequest.Email);

            var hashSenha = GerarHashSenha(usuarioLoginRequest.Senha);

            if(usuario is null  || usuario.Senha != hashSenha)
            {
                throw new AuthenticationException("Usuario ou senha inválida.");
            }

            return new UsuarioLoginResponseContract{
                id = usuario.id,
                Email = usuario.Email,
                Token = _tokenService.GerarToken(_mapper.Map<Usuario>(usuario))
            };
        }

        public async Task<UsuarioResponseContract> Adicionar(UsuarioRequestContract entidade, long idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);
        }


        public async Task<UsuarioResponseContract> Atualizar(long id, UsuarioRequestContract entidade, long idUsuario)
        {
            _ = await Obter(id) ?? throw new NotFoundException("Usuário não encontrado para atualização");

            var usuario = _mapper.Map<Usuario>(entidade);
            usuario.id = id;
            usuario.Senha = GerarHashSenha(entidade.Senha);

            usuario = await _usuarioRepository.Atualizar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);
        }


        public async Task Deletar(long id, long idUsuario)
        {
            //?? > Se não
            var usuario = await _usuarioRepository.Obter(id) ?? throw new NotFoundException("Usuário não encontrado para deleção");
            
            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));


        }

        public async Task<IEnumerable<UsuarioResponseContract>> Obter(long idUsuario)
        {
            var usuarios = await _usuarioRepository.Obter();

            return usuarios.Select(usuario => _mapper.Map<UsuarioResponseContract>(usuario));
        }

        public async Task<UsuarioResponseContract> ObterId(long id, long idUsuario)//idUsuario não usado
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
                hashSenha = BitConverter.ToString(bytesHashSenha).Replace("-","").ToLower();
            }

            return hashSenha;
        }

    }
}