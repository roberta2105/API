using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Damain.Models
{
    public class Usuario
    {
        //Chave primária da tabela
        [Key]
        public long id { get; set;}

        //Campo obrigatório. Passa um erro caso o campo esteja vazio
        [Required(ErrorMessage = "O campo de email é obrigatório")]
        public string Email { get; set; } = string.Empty; //inicializa a string como vazia. Para ela não ser nula

        [Required(ErrorMessage = "O campo de senha é obrigatório")]
        public string Senha { get; set;} = string.Empty;

        //Campo obrigatório, mas não retorna erro.
        [Required]
        public DateTime DataCadastro { get; set; }

        public DateTime? DataInativacao { get; set; }


    }
}