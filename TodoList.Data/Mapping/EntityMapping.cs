using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Data.Entities;
using TodoList.Models.Enums;
using TodoList.Models.Models;

namespace TodoList.Data.Mapping
{
    public static class EntityMapping
    {
        public static MapperConfiguration GetMapperConfig(bool includeCollections = true) => new MapperConfiguration(cfg =>
        {
            if (includeCollections)
            {
                cfg.CreateMap<ObjectiveDB, ObjectiveDTO>()
                    .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));
            }
            else
            {
                cfg.CreateMap<ObjectiveDB, ObjectiveDTO>()
                    .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey))
                    .ForMember(dto => dto.Tasks, opt => opt.MapFrom(dbo => (IEnumerable<TaskDTO>)null));
            }

            cfg.CreateMap<TaskDB, TaskDTO>()
                .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));

            cfg.CreateMap<ObjectiveHistoryDB, ObjectiveHistoryDTO>()
                .ForMember(dto => dto.PreviousStatusType, opt => opt.MapFrom(dbo => (StatusTypes?)dbo.PreviousStatusTypeKey))
                .ForMember(dto => dto.CurrentStatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.CurrentStatusTypeKey));

            cfg.CreateMap<ObjectiveDTO, ObjectiveDB>()
                .ForMember(dbo => dbo.StatusTypeKey, opt => opt.MapFrom(dbo => (int)dbo.StatusType));

            cfg.CreateMap<TaskDTO, TaskDB>()
                .ForMember(dbo => dbo.StatusTypeKey, opt => opt.MapFrom(dbo => (int)dbo.StatusType));

            cfg.CreateMap<ObjectiveHistoryDTO, ObjectiveHistoryDB>()
                .ForMember(dto => dto.PreviousStatusTypeKey, opt => opt.MapFrom(dbo => (int?)dbo.PreviousStatusType))
                .ForMember(dto => dto.CurrentStatusTypeKey, opt => opt.MapFrom(dbo => (int)dbo.CurrentStatusType));
        });

        public static IMapper GetMapper(bool includeCollections = true) => new Mapper(GetMapperConfig(includeCollections));
    }
}
