using AutoMapper;
using StudyNetCore.WebApp.Dto;
using StudyNetCore.WebApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore.WebApp.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<TProduct, ProductDto>().ReverseMap();
        }
    }
}
