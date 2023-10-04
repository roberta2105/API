using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Exceptions
{
    //Informação não encontrada
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message){}

    }
}