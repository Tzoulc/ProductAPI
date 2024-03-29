﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductAPI.DbContexts;
using ProductAPI.Entities;
using ProductAPI.Models;
using ProductAPI.Responses;

namespace ProductAPI.Profiles
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductCreationDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
        }
    }
}
