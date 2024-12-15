using AutoMapper;
using FactApp.Application.Commands;
using FactApp.Application.DTOs;
using FactApp.Domain.Models;

namespace FactApp.Application.Mappers
{
    public class FactServiceProfile : Profile
    {
        public FactServiceProfile()
        {
            CreateMap<Fact, FactResponse>()
                .ForMember(dest => dest.Fact, opt => opt.MapFrom(src => src.FactContent));

            CreateMap<Fact, NewFactCommand>();

            CreateMap<string, FactResponse>()
                .ForMember(dest => dest.Fact, opt => opt.MapFrom(src => src));

            CreateMap<IList<string>?, FactsResponse>()
                .ForMember(dest => dest.Facts, opt => opt.MapFrom(src => src));
        }
    }
}
