using System;
using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Infrastructure
{
    public class AutoMapperBLLConfig
    {
        public static readonly Action<IMapperConfigurationExpression> ConfigAction = cfg =>
        {
            cfg.CreateMap<UserDTO, ApplicationUser>().ReverseMap();
        };

        public static void Configure()
        {
            Mapper.Initialize(ConfigAction);
        }
    }
}
