using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Exceptions
{
    //Informação inválida
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message){}

    }
}