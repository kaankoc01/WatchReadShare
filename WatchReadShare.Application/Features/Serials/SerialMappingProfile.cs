using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WatchReadShare.Application.Features.Serials.Create;
using WatchReadShare.Application.Features.Serials.Dto;
using WatchReadShare.Application.Features.Serials.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Serials
{
    public class SerialMappingProfile : Profile
    {
        public SerialMappingProfile()
        {

            CreateMap<Serial, SerialDto>().ReverseMap();
            CreateMap<CreateSerialRequest, Serial>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            CreateMap<UpdateSerialRequest, Serial>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        }
    }
}
