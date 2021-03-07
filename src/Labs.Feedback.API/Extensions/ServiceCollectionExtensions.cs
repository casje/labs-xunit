using System;
using AutoMapper;
using Labs.Feedback.API;
using Labs.Feedback.API.Extensions;
using Labs.Feedback.API.Model;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<String, Categoria>().ConvertUsing(x => x.ToCategoria());
                mc.CreateMap<String, Guid>().ConvertUsing(x => x.ToGuid());
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}