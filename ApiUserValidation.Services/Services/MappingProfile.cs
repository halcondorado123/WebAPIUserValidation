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
            //CreateMap<ClientME, ClientDTO>().ReverseMap();
            CreateMap<PersonME, PersonDTO>().ReverseMap();
            CreateMap<UserME, UserCreateDTO>().ReverseMap();
            CreateMap<GenderME, GenderDTO>().ReverseMap();
            CreateMap<IdentificationME, IdentificationDTO>().ReverseMap();
            CreateMap<RoleME, RoleDTO>().ReverseMap();
            CreateMap<StatusME, StatusDTO>().ReverseMap();

            CreateMap<UserME, UserResponseDTO>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId)) // Heredado de PersonME
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IdentificationNumber, opt => opt.MapFrom(src => src.IdentificationNumber))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientName))
            .ForMember(dest => dest.ClientLastName, opt => opt.MapFrom(src => src.ClientLastName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.CalculateAge()))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RolId))
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId));
            //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            //.ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            //.ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.LastLogin));
        }
    }
}
