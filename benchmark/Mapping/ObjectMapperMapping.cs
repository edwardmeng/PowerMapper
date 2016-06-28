using Benchmarks.Enums;
using Benchmarks.Models;
using Benchmarks.ViewModels;
using Wheatech.ObjectMapper;

namespace Benchmarks.Mapping
{
    public static class ObjectMapperMapping
    {
        public static ObjectMapper Init()
        {
            var mapper = new ObjectMapper();
            mapper.Configure<Product, ProductViewModel>().MapMember(dest => dest.DefaultSharedOption, src => src.DefaultOption);
            mapper.Configure<Test, TestViewModel>()
                .BeforeMap((src, dest) => dest.Age = src.Age)
                .AfterMap((src, dest) => dest.Weight = src.Weight * 2)
                .Ignore(dest => dest.Age)
                .MapMember(dest => dest.Type, src => (Types)src.Type)
                .MapMember(dest => dest.Name, src => string.Format("{0} - {1} - {2}", src.Name, src.Weight, src.Age))
                .MapMember(dest => dest.SpareTheProduct, src => src.SpareProduct)
                .CreateWith(src => new TestViewModel(string.Format("{0} - {1}", src.Name, src.Id)));

            mapper.Configure<User, UserViewModel>()
                .MapMember(dest => dest.BelongTo, src => src.Role);

            mapper.Configure<Author, AuthorViewModel>()
                .MapMember(dest => dest.OwnedArticles, src => src.Articles);
            mapper.GetMapper<Author, AuthorViewModel>();
            mapper.GetMapper<User, UserViewModel>();
            mapper.GetMapper<Article, ArticleViewModel>();
            mapper.GetMapper<Test, TestViewModel>();
            mapper.GetMapper<ProductVariant, ProductVariantViewModel>();
            mapper.GetMapper<Item, ItemViewModel>();
            mapper.GetMapper<News, NewsViewModel>();

            mapper.GetMapper<Role, RoleViewModel>();
            // Precompiling direct collection mappings
            //Mapper.PrecompileCollection<List<Test>, List<TestViewModel>>();
            //Mapper.PrecompileCollection<List<Item>, List<ItemViewModel>>();
            //Mapper.PrecompileCollection<List<News>, List<NewsViewModel>>();
            //Mapper.PrecompileCollection<List<User>, List<UserViewModel>>();
            //Mapper.PrecompileCollection<List<Author>, List<AuthorViewModel>>();
            return mapper;
        }
        public static ObjectMapper InitAdvanced()
        {
            var mapper = new ObjectMapper();
            mapper.Configure<Product, ProductViewModel>()
                .MapMember(dest => dest.DefaultSharedOption, src => src.DefaultOption);
            mapper.Configure<Test, TestViewModel>()
                .MapMember(dest => dest.Age, src => src.Age)
                .MapMember(dest => dest.Weight, src => src.Weight * 2)
                .MapMember(dest => dest.Type, src => (Types)src.Type)
                .MapMember(dest => dest.Name, src => string.Format("{0} - {1} - {2}", src.Name, src.Weight, src.Age))
                .MapMember(dest => dest.SpareTheProduct, src => src.SpareProduct)
                .MapMember(dest => dest.Description, src => string.Format("{0} - {1}", src.Name, src.Id));
            mapper.GetMapper<ProductVariant, ProductVariantViewModel>();
            mapper.GetMapper<Product, ProductViewModel>();
            mapper.GetMapper<Test, TestViewModel>();
            return mapper;
        }
    }
}
