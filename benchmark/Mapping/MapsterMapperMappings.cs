using System.Collections.Generic;
using Benchmarks.Enums;
using Benchmarks.Models;
using Benchmarks.ViewModels;
using Mapster;

namespace Benchmarks.Mapping
{
    public class MapsterMapperMappings
    {
        public static void Init()
        {
            TypeAdapterConfig<Product, ProductViewModel>
                .NewConfig()
                .Map(dest => dest.DefaultSharedOption, src => src.DefaultOption.Adapt<ProductVariant, ProductVariantViewModel>());

            TypeAdapterConfig<Test, TestViewModel>
                .NewConfig()
                .Map(dest => dest.Age, src => src.Age)
                .Map(dest => dest.Weight, src => src.Weight * 2)
                .Map(dest => dest.Type, src => (Types)src.Type)
                .Map(dest => dest.Name, src => $"{src.Name} - {src.Weight} - {src.Age}")
                .Map(dest => dest.Name, src => $"{src.Name} - {src.Weight} - {src.Age}")
                .Map(dest => dest.SpareTheProduct, src => src.SpareProduct.Adapt<Product, ProductViewModel>())
                .Map(dest => dest.Description, src => $"{src.Name} - {src.Id}");

            TypeAdapterConfig<User, UserViewModel>
                .NewConfig()
                .Map(dest => dest.BelongTo, src => src.Role.Adapt<Role, RoleViewModel>());

            TypeAdapterConfig<Author, AuthorViewModel>
                .NewConfig()
                .Map(dest => dest.OwnedArticles, src => src.Articles.Adapt<List<Article>, List<ArticleViewModel>>());
        }
    }
}
