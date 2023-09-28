using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Contract.NaturezaDeLancamento;
using ControleFacil.Api.Damain.Models;

namespace ControleFacil.Api.AutoMapper
{
    public class NaturezaProfile : Profile
    {
        public NaturezaProfile()
        {
            CreateMap<NaturezaDeLancamento , NaturezaRequestContract>().ReverseMap();
            CreateMap<NaturezaDeLancamento , NaturezaResponseContract>().ReverseMap();
            
        }
    }
}