﻿#if !NETCOREAPP
using System.Collections.Generic;
using Benchmarks.Enums;
using Benchmarks.Models;
using Benchmarks.ViewModels;
using ExpressMapper;

namespace Benchmarks.Mapping
{
    public static class ExpressMapperMapping
    {
        public static void Init()
        {
            Mapper.Reset();
            Mapper.Register<ProductVariant, ProductVariantViewModel>();
            Mapper.Register<Product, ProductViewModel>()
                .Member(dest => dest.DefaultSharedOption, src => src.DefaultOption);
            Mapper.Register<Test, TestViewModel>()
                .Before((src, dest) => dest.Age = src.Age)
                .After((src, dest) => dest.Weight = src.Weight * 2)
                .Ignore(dest => dest.Age)
                .Member(dest => dest.Type, src => (Types)src.Type)
                .Member(dest => dest.Name, src => $"{src.Name} - {src.Weight} - {src.Age}")
                .Function(dest => dest.SpareTheProduct, src => src.SpareProduct)
                .Instantiate(src => new TestViewModel($"{src.Name} - {src.Id}"));

            Mapper.Register<News, NewsViewModel>();

            Mapper.Register<Role, RoleViewModel>();
            Mapper.Register<User, UserViewModel>()
                .Member(dest => dest.BelongTo, src => src.Role);

            Mapper.Register<Article, ArticleViewModel>();
            Mapper.Register<Author, AuthorViewModel>()
                .Function(dest => dest.OwnedArticles, src => src.Articles);

            Mapper.Register<Item, ItemViewModel>();
            Mapper.Compile();
            // Precompiling direct collection mappings
            Mapper.PrecompileCollection<List<Test>, List<TestViewModel>>();
            Mapper.PrecompileCollection<List<Item>, List<ItemViewModel>>();
            Mapper.PrecompileCollection<List<News>, List<NewsViewModel>>();
            Mapper.PrecompileCollection<List<User>, List<UserViewModel>>();
            Mapper.PrecompileCollection<List<Author>, List<AuthorViewModel>>();
        }

        public static void InitAdvanced()
        {
            Mapper.Reset();
            Mapper.Register<ProductVariant, ProductVariantViewModel>();
            Mapper.Register<Product, ProductViewModel>()
                .Member(dest => dest.DefaultSharedOption, src => src.DefaultOption);
            Mapper.Register<Test, TestViewModel>()
                .Member(dest => dest.Age, src => src.Age)
                .Member(dest => dest.Weight, src => src.Weight * 2)
                .Member(dest => dest.Type, src => (Types)src.Type)
                .Member(dest => dest.Name, src => $"{src.Name} - {src.Weight} - {src.Age}")
                .Member(dest => dest.SpareTheProduct, src => src.SpareProduct)
                .Member(dest => dest.Description, src => $"{src.Name} - {src.Id}");
            Mapper.Compile();
        }
    }
}
#endif