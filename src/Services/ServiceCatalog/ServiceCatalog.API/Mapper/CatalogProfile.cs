using AutoMapper;
using ServiceCatalog.API.Entities;
using ServiceCatalog.API.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Mapper
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Service, Catalog>().ReverseMap();
        }
    }
}
