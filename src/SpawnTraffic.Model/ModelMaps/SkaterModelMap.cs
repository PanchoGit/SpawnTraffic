using System;
using AutoMapper;

namespace SpawnTraffic.Model.ModelMaps
{
    public class SkaterModelMap : Profile
    {
        public SkaterModelMap()
        {
            CreateMap<AddSkaterModel, SkaterModel>()
                .ForMember(s => s.Id, x => x.MapFrom(_ => Guid.NewGuid()))
                .ForMember(s => s.Created, x => x.MapFrom(_ => DateTimeOffset.UtcNow));
        }
    }
}
