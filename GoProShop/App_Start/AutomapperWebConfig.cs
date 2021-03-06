﻿using System;
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
            cfg.CreateMap<ProductSubGroupVM, ProductSubGroupDTO>().ReverseMap();
            cfg.CreateMap<ProductVM, ProductDTO>().ReverseMap();
            cfg.CreateMap<ProductGroupVM, ProductGroupDTO>().ReverseMap();
            cfg.CreateMap<FeedbackVM, FeedbackDTO>().ReverseMap();
            cfg.CreateMap<BaseFeedbackVM, FeedbackDTO>().ReverseMap();
            cfg.CreateMap<ResponseVM, ResponseDTO>().ReverseMap();
            cfg.CreateMap<AdminPageVM, AdminPageDTO>().ReverseMap();
            cfg.CreateMap<StoreVM, StoreDTO>().ReverseMap();
            cfg.CreateMap<OrderedProductVM, OrderedProductDTO>().ReverseMap();
            cfg.CreateMap<CustomerVM, CustomerDTO>().ReverseMap();
            cfg.CreateMap<OrderVM, OrderDTO>().ReverseMap();
            cfg.CreateMap<CartItem, CartItemDTO>().ReverseMap();
            cfg.CreateMap<ChosenItemVm, ChosenItemDto>().ReverseMap();
            cfg.CreateMap(typeof(SearchResultDTO<>), typeof(SearchResultVM<>)).ReverseMap();       
        };
    }
}