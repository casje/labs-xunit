using AutoMapper;
using Labs.Feedback.API.Extensions;

namespace Labs.Feedback.API;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Dto => Model
        CreateMap<Dto.MensagemDto, Model.Mensagem>()
           .ForMember(d => d.Categoria, op => op.MapFrom(o => o.Categoria.ToCategoria()));

        // Model => Dto
        CreateMap<Model.Mensagem, Dto.MensagemDto>()
            .ForMember(d => d.Categoria, op => op.MapFrom(o => o.Categoria.ToString()));
    }
}