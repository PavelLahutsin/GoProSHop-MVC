using System;
using AutoMapper;
using GoProShop.ViewModels;
using GoProShop.BLL.DTO;
using System.Collections.Generic;

namespace GoProShop.App_Start
{
    public static class AutoMapperWebConfig
    {
        public static readonly Action<IMapperConfigurationExpression> ConfigAction = cfg =>
        {
            cfg.CreateMap<UserRegisterVM, UserDTO>().ReverseMap();
            cfg.CreateMap<UserLoginVM, UserLoginDTO>().ReverseMap();
            cfg.CreateMap<UserEditVM, UserDTO>().ReverseMap();
            cfg.CreateMap<ProductSubGroupVM, ProductSubGroupDTO>().ReverseMap();
            cfg.CreateMap<ProductVM, ProductDTO>().ReverseMap();
            cfg.CreateMap<ProductGroupVM, ProductGroupDTO>().ReverseMap();
            cfg.CreateMap<FeedbackVM, FeedbackDTO>().ReverseMap();
            cfg.CreateMap<ResponseVM, ResponseDTO>().ReverseMap();
        };
    }
}