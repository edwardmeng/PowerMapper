using Benchmarks.Generators;
using Benchmarks.Models;
using Benchmarks.ViewModels;
using PowerMapper;

namespace Benchmarks.Mapping
{
    public static class PowerMapperMapping
    {
        public static IMappingContainer Init()
        {
            var mapper = Mapper.CreateContainer();
            mapper.Configure<Product, ProductViewModel>()
                .WithOptions(MemberMapOptions.Hierarchy)
                .MapMember(dest => dest.DefaultSharedOption, src => src.DefaultOption);
            mapper.Configure<Test, TestViewModel>()
                .WithOptions(MemberMapOptions.Hierarchy)
                .BeforeMap((src, dest) => dest.Age = src.Age)
                .AfterMap((src, dest) => dest.Weight = src.Weight * 2)
                .Ignore(dest => dest.Age)
                .MapMember(dest => dest.Name, src => $"{src.Name} - {src.Weight} - {src.Age}")
                .MapMember(dest => dest.SpareTheProduct, src => src.SpareProduct)
                .CreateWith(src => new TestViewModel($"{src.Name} - {src.Id}"))
                ;

            mapper.Configure<User, UserViewModel>()
                .WithOptions(MemberMapOptions.Hierarchy)
                .MapMember(dest => dest.BelongTo, src => src.Role)
                ;

            mapper.Configure<Author, AuthorViewModel>()
                .WithOptions(MemberMapOptions.Hierarchy)
                .MapMember(dest => dest.OwnedArticles, src => src.Articles)
                ;
            mapper.GetMapper<Author, AuthorViewModel>();
            mapper.GetMapper<User, UserViewModel>();
            mapper.GetMapper<Article, ArticleViewModel>();
            mapper.GetMapper<Test, TestViewModel>();
            mapper.GetMapper<ProductVariant, ProductVariantViewModel>();
            mapper.GetMapper<Item, ItemViewModel>();
            mapper.GetMapper<News, NewsViewModel>();

            mapper.GetMapper<Role, RoleViewModel>();

            // Precompiling direct collection mappings
            mapper.Map<Test, TestViewModel>(DataGenerator.GetTests(1));
            mapper.Map<Item, ItemViewModel>(DataGenerator.GetItems(1));
            mapper.Map<News, NewsViewModel>(DataGenerator.GetNews(1));
            mapper.Map<User, UserViewModel>(DataGenerator.GetUsers(1));
            mapper.Map<Author, AuthorViewModel>(DataGenerator.GetAuthors(1));
            return mapper;
        }
        public static IMappingContainer InitAdvanced()
        {
            var mapper = Mapper.CreateContainer();
            mapper.Configure<Product, ProductViewModel>()
                .WithOptions(MemberMapOptions.Hierarchy)
                .MapMember(dest => dest.DefaultSharedOption, src => src.DefaultOption)
                ;
            mapper.Configure<Test, TestViewModel>()
                .WithOptions(MemberMapOptions.Hierarchy)
                .MapMember(dest => dest.Age, src => src.Age)
                .MapMember(dest => dest.Weight, src => src.Weight * 2)
                .MapMember(dest => dest.Name, src => $"{src.Name} - {src.Weight} - {src.Age}")
                .MapMember(dest => dest.SpareTheProduct, src => src.SpareProduct)
                .MapMember(dest => dest.Description, src => $"{src.Name} - {src.Id}")
                ;
            mapper.GetMapper<ProductVariant, ProductVariantViewModel>();
            mapper.GetMapper<Product, ProductViewModel>();
            mapper.GetMapper<Test, TestViewModel>();
            return mapper;
        }
    }
}
