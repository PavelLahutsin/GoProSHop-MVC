﻿using System;
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
            cfg.CreateMap<ProductSubGroup, ProductSubGroupDTO>().ReverseMap();
            cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            cfg.CreateMap<ProductGroup, ProductGroupDTO>().ReverseMap();
            cfg.CreateMap<Feedback, FeedbackDTO>().ReverseMap();
            cfg.CreateMap<Order, OrderDTO>().ReverseMap();
            cfg.CreateMap<Customer, CustomerDTO>().ReverseMap();
            cfg.CreateMap<OrderedProduct, OrderedProductDTO>().ReverseMap();
            cfg.CreateMap<Store, StoreDTO>().ReverseMap();
        };
    }
}
