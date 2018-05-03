using System.Threading;
using AutoMapper;
using Benchmarks.Enums;
using Benchmarks.Models;
using Benchmarks.ViewModels;

namespace Benchmarks.Mapping
{
    public static class AutoMapperMapping
    {
        private static int _initialized;
        public static void Init()
        {
            if (Interlocked.Exchange(ref _initialized, 1) == 0)
            {
                Mapper.Initialize(config =>
                {
                    config.CreateMap<ProductVariant, ProductVariantViewModel>();
                    config.CreateMap<Product, ProductViewModel>()
                        .ForMember(dest => dest.DefaultSharedOption, src => src.MapFrom(m => m.DefaultOption));
                    config.CreateMap<Test, TestViewModel>()
                        .BeforeMap((src, dest) => dest.Age = src.Age)
                        .AfterMap((src, dest) => dest.Weight = src.Weight * 2)
                        .ForMember(dest => dest.Age, src => src.MapFrom(m => m.Age))
                        .ForMember(dest => dest.Type, src => src.MapFrom(m => (Types)m.Type))
                        .ForMember(dest => dest.Name, src => src.MapFrom(m => $"{m.Name} - {m.Weight} - {m.Age}"))
                        .ForMember(dest => dest.SpareTheProduct, src => src.ResolveUsing(m => m.SpareProduct))
                        .ForMember(dest => dest.Description, src => src.MapFrom(m => $"{m.Name} - {m.Id}"))
                        .ConstructUsing((src => new TestViewModel($"{src.Name} - {src.Id}")));
                    config.CreateMap<News, NewsViewModel>();
                    config.CreateMap<Role, RoleViewModel>();
                    config.CreateMap<User, UserViewModel>()
                        .ForMember(dest => dest.BelongTo, src => src.MapFrom(m => m.Role));

                    config.CreateMap<Article, ArticleViewModel>();
                    config.CreateMap<Author, AuthorViewModel>()
                        .ForMember(dest => dest.OwnedArticles, src => src.ResolveUsing(m => m.Articles));

                    config.CreateMap<Item, ItemViewModel>();
                });
            }
        }
    }
}
