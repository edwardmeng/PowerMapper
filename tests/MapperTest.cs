using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Wheatech.EmitMapper.UnitTests.BusinessModel;
using Wheatech.EmitMapper.UnitTests.DataModel;
using Xunit;

namespace Wheatech.EmitMapper.UnitTests
{
    public class MapperTest
    {
        [Fact]
        public void TestMapCollection()
        {
            var roleEntities = new RoleEntity[10];
            for (int i = 0; i < roleEntities.Length; i++)
            {
                roleEntities[i] = new RoleEntity
                {
                    RoleId = Guid.NewGuid(),
                    RoleName = "Manager" + i,
                    LoweredRoleName = "manager" + i
                };
            }
            var roles = new Role[roleEntities.Length];
            for (int i = 0; i < roleEntities.Length; i++)
            {
                roles[i] = new Role
                {
                    RoleId = roleEntities[i].RoleId,
                    RoleName = roleEntities[i].RoleName,
                    Description = roleEntities[i].Description
                };
            }
            // Array
            Assert.Equal(roles, Mapper.Map<RoleEntity, Role>(roleEntities));
            Assert.Equal(roles, Mapper.Map<RoleEntity[], Role[]>(roleEntities));
            // IEnumerable
            AreSequentialEqual(roles, Mapper.Map<RoleEntity, Role>((IEnumerable<RoleEntity>)roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], IEnumerable<Role>>(roleEntities));
            // ICollection
            AreSequentialEqual(roles, Mapper.Map<RoleEntity, Role>((ICollection<RoleEntity>)roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], ICollection<Role>>(roleEntities));
            // IList
            AreSequentialEqual(roles, Mapper.Map<RoleEntity, Role>((IList<RoleEntity>)roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], IList<Role>>(roleEntities));
            // List
            Assert.Equal(roles, Mapper.Map<RoleEntity, Role>(new List<RoleEntity>(roleEntities)));
            Assert.Equal(roles, Mapper.Map<RoleEntity[], List<Role>>(roleEntities));
            // Custom Collection
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], ReadOnlyRoleCollection>(roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], ReadOnlyCollection<Role>>(roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], ReadOnlyRoleCollection1>(roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], ReadOnlyRoleCollection2>(roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], ReadOnlyRoleCollection3>(roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], ReadOnlyRoleCollection4>(roleEntities));
            AreSequentialEqual(roles, Mapper.Map<RoleEntity[], RoleCollection>(roleEntities));
        }

        [Fact]
        public void TestMapClassToClass()
        {
            var entity = new RoleEntity
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                LoweredRoleName = "manager",
                Description = "Department or group manager"
            };
            var role = Mapper.Map<RoleEntity, Role>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);

            role = Mapper.Map<Role>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestMapClassToStruct()
        {
            var entity = new RoleEntity
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                LoweredRoleName = "manager",
                Description = "Department or group manager"
            };
            var role = Mapper.Map<RoleEntity, RoleStruct>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);

            role = Mapper.Map<RoleStruct>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestMapStructToClass()
        {
            var entity = new RoleStructEntity
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                LoweredRoleName = "manager",
                Description = "Department or group manager"
            };
            var role = Mapper.Map<RoleStructEntity, Role>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);

            role = Mapper.Map<Role>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestMapStructToStruct()
        {
            var entity = new RoleStructEntity
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                LoweredRoleName = "manager",
                Description = "Department or group manager"
            };
            var role = Mapper.Map<RoleStructEntity, RoleStruct>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);

            role = Mapper.Map<RoleStruct>(entity);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestCustomMemberMapper()
        {
            var container = Mapper.CreateContainer();
            container.Configure<Role, RoleEntity>()
                .MapMember(target => target.LoweredRoleName, source => source.RoleName.ToLower());
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                Description = "Department or group manager"
            };
            var entity = container.Map<Role, RoleEntity>(role);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.LoweredRoleName, role.RoleName.ToLower());
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestCustomCreator()
        {
            var container = Mapper.CreateContainer();
            container.Configure<Role, RoleEntity>().CreateWith(source => new RoleEntity())
                .MapMember(target => target.LoweredRoleName, source => source.RoleName.ToLower());
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                Description = "Department or group manager"
            };
            var entity = container.Map<Role, RoleEntity>(role);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.LoweredRoleName, role.RoleName.ToLower());
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestCustomMapper()
        {
            var container = Mapper.CreateContainer();
            container.Configure<Role, RoleEntity>().MapWith((source, target) =>
            {
                target.RoleId = source.RoleId;
                target.RoleName = source.RoleName.ToUpper();
                target.LoweredRoleName = source.RoleName.ToLower();
                target.Description = source.Description;
            });
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                Description = "Department or group manager"
            };
            var entity = container.Map<Role, RoleEntity>(role);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName.ToUpper());
            Assert.Equal(entity.LoweredRoleName, role.RoleName.ToLower());
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestCustomConvension()
        {
            var container = Mapper.CreateContainer();
            container.Conventions.Add(
                context =>
                {
                    context.CreateWith(Activator.CreateInstance);
                    foreach (var targetMember in context.TargetMembers.Where(targetMember => targetMember.MemberName.StartsWith("Lowered")))
                    {
                        if (targetMember.MemberType != typeof(string)) continue;
                        Func<string, string> converter = null;
                        string prefix = null;
                        if (targetMember.MemberName.StartsWith("Lowered"))
                        {
                            prefix = "Lowered";
                            converter = source => source?.ToLower();
                        }
                        else if (targetMember.MemberName.StartsWith("Uppered"))
                        {
                            prefix = "Uppered";
                            converter = source => source?.ToUpper();
                        }
                        if (string.IsNullOrEmpty(prefix)) return;
                        var sourceName = targetMember.MemberName.Substring(prefix.Length);
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            var sourceMember = context.SourceMembers[sourceName];
                            if (sourceMember != null && sourceMember.MemberType == typeof(string))
                            {
                                context.Mappings.Set(sourceMember, targetMember).ConvertWith(converter);
                            }
                        }
                    }
                });
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                Description = "Department or group manager"
            };
            var entity = container.Map<Role, RoleEntity>(role);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.LoweredRoleName, role.RoleName.ToLower());
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestShadowMap()
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderCode = "A001",
                Address = "China",
                Items =
                {
                    new OrderItem
                    {
                        OrderItemId = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 2
                    }
                }
            };
            var entity = Mapper.Map<Order, OrderEntity>(order);
            Assert.NotNull(entity);
            Assert.Equal(order.OrderId, entity.OrderId);
            Assert.Equal(order.CustomerId, entity.CustomerId);
            Assert.Equal(order.OrderCode, entity.OrderCode);
            Assert.Equal(order.Address, entity.Address);
            Assert.Null(entity.Items);
        }

        [Fact]
        public void TestHierarchyMap()
        {
            var container = Mapper.CreateContainer();
            container.Configure<Order, OrderEntity>().WithOptions(MemberMapOptions.Hierarchy);
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderCode = "A001",
                Address = "China",
                Items =
                {
                    new OrderItem
                    {
                        OrderItemId = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 2
                    }
                }
            };
            var entity = container.Map<Order, OrderEntity>(order);
            Assert.NotNull(entity);
            Assert.Equal(order.OrderId, entity.OrderId);
            Assert.Equal(order.CustomerId, entity.CustomerId);
            Assert.Equal(order.OrderCode, entity.OrderCode);
            Assert.Equal(order.Address, entity.Address);
            Assert.NotNull(entity.Items);
            Assert.Equal(1, entity.Items.Count);
            var item = entity.Items.First();
            Assert.NotNull(item);
            Assert.Equal(order.Items[0].OrderItemId, item.OrderItemId);
            Assert.Equal(order.Items[0].ProductId, item.ProductId);
            Assert.Equal(order.Items[0].Quantity, item.Quantity);
        }

        [Fact]
        public void TestBeforeMap()
        {
            var container = Mapper.CreateContainer();
            container.Configure<Role, RoleEntity>()
                .BeforeMap((source, target) => target.LoweredRoleName = source.RoleName.ToLower());
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                Description = "Department or group manager"
            };
            var entity = container.Map<Role, RoleEntity>(role);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.LoweredRoleName, role.RoleName.ToLower());
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestAfterMap()
        {
            var container = Mapper.CreateContainer();
            container.Configure<Role, RoleEntity>()
                .AfterMap((source, target) => target.LoweredRoleName = source.RoleName.ToLower());
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "Manager",
                Description = "Department or group manager"
            };
            var entity = container.Map<Role, RoleEntity>(role);
            Assert.Equal(entity.RoleId, role.RoleId);
            Assert.Equal(entity.RoleName, role.RoleName);
            Assert.Equal(entity.LoweredRoleName, role.RoleName.ToLower());
            Assert.Equal(entity.Description, role.Description);
        }

        [Fact]
        public void TestInheritMap()
        {
            var container = Mapper.CreateContainer();
            container.Configure<Order, OrderEntity>().WithOptions(MemberMapOptions.Hierarchy);
            var order = new DerivedOrder
            {
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid().ToString(),
                OrderCode = "A001",
                Address = "China",
                Amount = 32,
                Items =
                {
                    new OrderItem
                    {
                        OrderItemId = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 2
                    }
                }
            };
            ((Order)order).CustomerId = Guid.NewGuid();
            var entity = container.Map<DerivedOrder, DerivedOrderEntity>(order);
            Assert.NotNull(entity);
            Assert.Equal(order.OrderId, entity.OrderId);
            Assert.Equal(order.CustomerId, entity.CustomerId);
            Assert.Equal(((Order)order).CustomerId, ((OrderEntity)entity).CustomerId);
            Assert.Equal(order.OrderCode, entity.OrderCode);
            Assert.Equal(order.Address, entity.Address);
        }

        public static void AreSequentialEqual<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
            {
                Assert.Null(second);
                return;
            }
            if (second == null)
            {
                Assert.Null(first);
                return;
            }
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                Assert.True(secondEnumerator.MoveNext());
                Assert.Equal(firstEnumerator.Current, secondEnumerator.Current);
            }
            Assert.False(secondEnumerator.MoveNext());
        }
    }
}
