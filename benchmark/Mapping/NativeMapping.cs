using System.Linq;
using Benchmarks.Enums;
using Benchmarks.Models;
using Benchmarks.ViewModels;

namespace Benchmarks.Mapping
{
    public static class NativeMapping
    {
        public static ItemViewModel Map(Item src)
        {
            return new ItemViewModel()
            {
                Id = src.Id,
                Name = src.Name,
                Height = src.Height,
                Length = src.Length,
                Weight = src.Weight,
                Width = src.Width
            };
        }

        public static TestViewModel Map(Test src)
        {
            if (src == null)
            {
                return default(TestViewModel);
            }
            var dst = new TestViewModel($"{src.Name} - {src.Id}")
            {
                Id = src.Id,
                Created = src.Created,
                Age = src.Age,
                Name = $"{src.Name} - {src.Weight} - {src.Age}",
                Type = (Types) src.Type,
                Weight = src.Weight,
                Product = Map(src.Product),
                SpareTheProduct = Map(src.SpareProduct),
                Products = src.Products.Select(Map).ToList()
            };

            return dst;
        }

        public static ProductViewModel Map(Product src)
        {
            if (src == null)
            {
                return default(ProductViewModel);
            }
            var dst = new ProductViewModel
            {
                Id = src.Id,
                Description = src.Description,
                ProductName = src.ProductName,
                Weight = src.Weight,
                DefaultSharedOption = Map(src.DefaultOption),
                Options = src.Options.Select(Map).ToList()
            };
            return dst;
        }

        public static NewsViewModel Map(News src)
        {
            return new NewsViewModel()
            {
                Id = src.Id,
                IsXml = src.IsXml,
                Provider = src.Provider,
                StartDate = src.StartDate,
                Url = src.Url
            };
        }

        public static ProductVariantViewModel Map(ProductVariant src)
        {
            if (src == null)
            {
                return default(ProductVariantViewModel);
            }
            var dst = new ProductVariantViewModel
            {
                Id = src.Id,
                Color = src.Color,
                Size = src.Size
            };
            return dst;
        }

        public static UserViewModel Map(User src)
        {
            if (src == null)
            {
                return default(UserViewModel);
            }
            var dst = new UserViewModel
            {
                Id = src.Id,
                Active = src.Active,
                CreatedOn = src.CreatedOn,
                Deleted = src.Deleted,
                UserName = src.UserName,
                Email = src.Email,
                Address = src.Address,
                Age = src.Age,
                BelongTo = Map(src.Role)
            };

            return dst;
        }

        public static RoleViewModel Map(Role src)
        {
            if (src == null)
            {
                return default(RoleViewModel);
            }
            var dst = new RoleViewModel
            {
                Id = src.Id,
                Active = src.Active,
                CreatedOn = src.CreatedOn,
                Deleted = src.Deleted,
                Name = src.Name
            };
            return dst;
        }

        public static ArticleViewModel Map(Article src)
        {
            if (src == null)
            {
                return default(ArticleViewModel);
            }
            var dst = new ArticleViewModel
            {
                Id = src.Id,
                CreatedOn = src.CreatedOn,
                Text = src.Text,
                Title = src.Title
            };

            return dst;
        }

        public static AuthorViewModel Map(Author src)
        {
            if (src == null)
            {
                return default(AuthorViewModel);
            }
            var dst = new AuthorViewModel
            {
                Id = src.Id,
                Age = src.Age,
                FirstName = src.FirstName,
                LastName = src.LastName,
                OwnedArticles = src.Articles.Select(Map).ToList()
            };

            return dst;
        }
    }
}
