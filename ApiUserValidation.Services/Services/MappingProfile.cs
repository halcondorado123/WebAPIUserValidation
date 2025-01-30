using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.DTOs.UserAttributesDTO;
using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributesME;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientME, ClientDTO>().ReverseMap();
            CreateMap<PersonME, PersonDTO>().ReverseMap();
            CreateMap<UserInfoME, UserInfoDTO>().ReverseMap();
            CreateMap<GenderME, GenderDTO>().ReverseMap();
            CreateMap<IdentificationME, IdentificationDTO>().ReverseMap();
            CreateMap<RoleME, RoleDTO>().ReverseMap();
            CreateMap<StatusME, StatusDTO>().ReverseMap();
        }
    }
}
