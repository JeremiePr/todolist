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
        public static MapperConfiguration GetMapperConfig() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ObjectiveDB, ObjectiveDTO>()
                    .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));

            cfg.CreateMap<TaskDB, TaskDTO>()
                .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));

            cfg.CreateMap<ObjectiveHistoryDB, ObjectiveHistoryDTO>()
                .ForMember(dto => dto.PreviousStatusType, opt => opt.MapFrom(dbo => (StatusTypes?)dbo.PreviousStatusTypeKey))
                .ForMember(dto => dto.CurrentStatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.CurrentStatusTypeKey));

            // DTO to DB

            cfg.CreateMap<ObjectiveDTO, ObjectiveDB>()
                .ForMember(dbo => dbo.StatusTypeKey, opt => opt.MapFrom(dto => (int)dto.StatusType))
                .ForMember(dbo => dbo.StatusType, opt => opt.Ignore());

            cfg.CreateMap<TaskDTO, TaskDB>()
                .ForMember(dbo => dbo.StatusTypeKey, opt => opt.MapFrom(dto => (int)dto.StatusType))
                .ForMember(dbo => dbo.StatusType, opt => opt.Ignore())
                .ForMember(dbo => dbo.Objective, opt => opt.Ignore());

            cfg.CreateMap<ObjectiveHistoryDTO, ObjectiveHistoryDB>()
                .ForMember(dbo => dbo.PreviousStatusTypeKey, opt => opt.MapFrom(dto => (int?)dto.PreviousStatusType))
                .ForMember(dbo => dbo.CurrentStatusTypeKey, opt => opt.MapFrom(dto => (int)dto.CurrentStatusType))
                .ForMember(dbo => dbo.PreviousStatusType, opt => opt.Ignore())
                .ForMember(dbo => dbo.CurrentStatusType, opt => opt.Ignore())
                .ForMember(dbo => dbo.Objective, opt => opt.Ignore());
        });

        public static IMapper GetMapper(MapperConfiguration config) => new Mapper(config);
    }
}
