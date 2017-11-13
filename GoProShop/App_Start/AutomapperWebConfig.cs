using System;
using AutoMapper;
using GoProShop.ViewModels;
using GoProShop.BLL.DTO;

namespace GoProShop.App_Start
{
    public static class AutoMapperWebConfig
    {
        public static readonly Action<IMapperConfigurationExpression> ConfigAction = cfg =>
        {
            cfg.CreateMap<UserRegisterVM, UserDTO>().ReverseMap();
            cfg.CreateMap<UserLoginVM, UserLoginDTO>().ReverseMap();
            cfg.CreateMap<UserEditVM, UserDTO>().ReverseMap();
        };

        public static void Configure()
        {
            Mapper.Initialize(ConfigAction);
        }
    }
}