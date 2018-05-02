using System.Collections.Generic;
using Benchmarks.Generators;
using Benchmarks.Mapping;
using Benchmarks.Models;
using Benchmarks.ViewModels;
using Mapster;
using PowerMapper;

namespace Benchmarks.Tests
{
    public class SimpleStructTest : BaseTest<List<Item>, List<ItemViewModel>>
    {
        private IMappingContainer _powerMapper;
        protected override List<Item> GetData()
        {
            return DataGenerator.GetItems(Count);
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

        protected override List<ItemViewModel> AutoMapperMap(List<Item> src)
        {
            return AutoMapper.Mapper.Map<List<Item>, List<ItemViewModel>>(src);
        }
#if !NETCOREAPP
        protected override List<ItemViewModel> ExpressMapperMap(List<Item> src)
        {
            return ExpressMapper.Mapper.Map<List<Item>, List<ItemViewModel>>(src);
        }
#endif
        protected override List<ItemViewModel> ValueInjectorMap(List<Item> src)
        {
            var list = new List<ItemViewModel>();
            foreach (var item in src)
            {
                list.Add(Omu.ValueInjecter.Mapper.Map<Item, ItemViewModel>(item));
            }
            return list;
        }

        protected override List<ItemViewModel> MapsterMap(List<Item> src)
        {
            return TypeAdapter.Adapt<List<Item>, List<ItemViewModel>>(src);
        }

        protected override List<ItemViewModel> TinyMapperMap(List<Item> src)
        {
            var list = new List<ItemViewModel>();
            foreach (var item in src)
            {
                list.Add(Nelibur.ObjectMapper.TinyMapper.Map<Item, ItemViewModel>(item));
            }
            return list;
        }

        protected override List<ItemViewModel> NativeMapperMap(List<Item> src)
        {
            var result = new List<ItemViewModel>();
            foreach (var item in src)
            {
                result.Add(NativeMapping.Map(item));
            }
            return result;
        }

        protected override List<ItemViewModel> PowerMapperMap(List<Item> src)
        {
            return _powerMapper.Map<Item, ItemViewModel>(src);
        }

        protected override string TestName
        {
            get { return "SimpleStructTest"; }
        }

        protected override string Size
        {
            get { return "XS"; }
        }
    }
}
