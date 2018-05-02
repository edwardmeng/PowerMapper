﻿using System.Collections.Generic;
using Benchmarks.Generators;
using Benchmarks.Mapping;
using Benchmarks.Models;
using Benchmarks.ViewModels;
using Mapster;
using PowerMapper;

namespace Benchmarks.Tests
{
    public class SimpleTest : BaseTest<List<News>, List<NewsViewModel>>
    {
        private IMappingContainer _powerMapper;
        protected override List<News> GetData()
        {
            return DataGenerator.GetNews(Count);
        }

        protected override void InitAutoMapper()
        {
            AutoMapperMapping.Init();
        }

#if !NETCOREAPP
        protected override void InitExpressMapper()
        {
            ExpressMapperMapping.Init();
        }
#endif
        protected override void InitValueInjectorMapper()
        {
            ValueInjectorMappings.Init();
        }

        protected override void InitMapsterMapper()
        {
            MapsterMapperMappings.Init();
        }

        protected override void InitTinyMapper()
        {
            TinyMapperMappings.Init();
        }

        protected override void InitNativeMapper()
        {
        }

        protected override void InitPowerMapper()
        {
            _powerMapper = PowerMapperMapping.Init();
        }

        protected override List<NewsViewModel> AutoMapperMap(List<News> src)
        {
            return AutoMapper.Mapper.Map<List<News>, List<NewsViewModel>>(src);
        }

#if !NETCOREAPP
        protected override List<NewsViewModel> ExpressMapperMap(List<News> src)
        {
            return ExpressMapper.Mapper.Map<List<News>, List<NewsViewModel>>(src);
        }
#endif
        protected override List<NewsViewModel> ValueInjectorMap(List<News> src)
        {
            var list = new List<NewsViewModel>();
            foreach (var item in src)
            {
                list.Add(Omu.ValueInjecter.Mapper.Map<News, NewsViewModel>(item));
            }
            return list;
        }

        protected override List<NewsViewModel> MapsterMap(List<News> src)
        {
            return TypeAdapter.Adapt<List<News>, List<NewsViewModel>>(src);
        }

        protected override List<NewsViewModel> TinyMapperMap(List<News> src)
        {
            var list = new List<NewsViewModel>();
            foreach (var item in src)
            {
                list.Add(Nelibur.ObjectMapper.TinyMapper.Map<News, NewsViewModel>(item));
            }
            return list;
        }

        protected override List<NewsViewModel> NativeMapperMap(List<News> src)
        {
            var result = new List<NewsViewModel>();
            foreach (var newse in src)
            {
                result.Add(NativeMapping.Map(newse));
            }
            return result;
        }

        protected override List<NewsViewModel> PowerMapperMap(List<News> src)
        {
            return _powerMapper.Map<News, NewsViewModel>(src);
        }

        protected override string TestName
        {
            get { return "SimpleTest"; }
        }

        protected override string Size
        {
            get { return "S"; }
        }
    }
}
