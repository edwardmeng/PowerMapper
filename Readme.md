PowerMapper
===================
An extremely fast and friendly convention-based object-object mapper.
It is implemented via the Emit library and uses a fluent configuration API to define an object-object mapping strategy.
PowerMapper is useful for dealing with DTO objects, data access layers an so on.

Get it on NuGet!
---------------
    Install-Package PowerMapper

Supported platform:
------------------
Microsoft .NET Framework 3.5

Microsoft .NET Framework 4.0+

NetStandard 1.5

Comparisons:
-----------
PowerMapper is now blistering fast! Please refer to the benchmark

<table>
    <thead>
        <tr>
            <td rowspan=2>Engine</td>
            <td colspan=2>Structs</td>
            <td colspan=2>Simple objects</td>
            <td colspan=2>Parent-Child</td>
            <td colspan=2>Parent-Children</td>
            <td colspan=2>Complex objects</td>
            <td colspan=2>Advance mapping</td>
        </tr>
        <tr>
            <td>NetFx</td>
            <td>NetCore</td>
            <td>NetFx</td>
            <td>NetCore</td>
            <td>NetFx</td>
            <td>NetCore</td>
            <td>NetFx</td>
            <td>NetCore</td>
            <td>NetFx</td>
            <td>NetCore</td>
            <td>NetFx</td>
            <td>NetCore</td>
        </td>
    </thead>
    <tbody>
        <tr>
            <td>AutoMapper</td>
            <td align=right>426</td>
            <td align=right>225</td>
            <td align=right>802</td>
            <td align=right>519</td>
            <td align=right>617</td>
            <td align=right>360</td>
            <td align=right>679</td>
            <td align=right>569</td>
            <td align=right>2162</td>
            <td align=right>4273</td>
            <td align=right>2270</td>
            <td align=right>2607</td>
        </tr>
        <tr>
            <td>ExpressMapper</td>
            <td align=right>597</td>
            <td align=right>549</td>
            <td align=right>1522</td>
            <td align=right>1112</td>
            <td align=right>917</td>
            <td align=right>298</td>
            <td align=right>960</td>
            <td align=right>542</td>
            <td align=right>2553</td>
            <td align=right>5515</td>
            <td align=right>2407</td>
            <td align=right>3097</td>
        </tr>
        <tr>
            <td>OoMapper</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>884</td>
            <td align=right>-</td>
            <td align=right>783</td>
            <td align=right>-</td>
            <td align=right>922</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>2481</td>
            <td align=right>-</td>
        </tr>
        <tr>
            <td>ValueInjector</td>
            <td align=right>5650</td>
            <td align=right>5611</td>
            <td align=right>13006</td>
            <td align=right>13645</td>
            <td align=right>11167</td>
            <td align=right>11405</td>
            <td align=right>452</td>
            <td align=right>502</td>
            <td align=right>9944</td>
            <td align=right>34226</td>
            <td align=right>9134</td>
            <td align=right>18536</td>
        </tr>
        <tr>
            <td>TinyMapper</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>751</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
            <td align=right>-</td>
        </tr>
        <tr>
            <td>Mapster</td>
            <td align=right>379</td>
            <td align=right>127</td>
            <td align=right>719</td>
            <td align=right>457</td>
            <td align=right>1694</td>
            <td align=right>821</td>
            <td align=right>788</td>
            <td align=right>564</td>
            <td align=right>3527</td>
            <td align=right>4165</td>
            <td align=right>3392</td>
            <td align=right>2781</td>
        </tr>
        <tr>
            <td>Native</td>
            <td align=right>337</td>
            <td align=right>213</td>
            <td align=right>519</td>
            <td align=right>525</td>
            <td align=right>324</td>
            <td align=right>336</td>
            <td align=right>539</td>
            <td align=right>470</td>
            <td align=right>1603</td>
            <td align=right>4111</td>
            <td align=right>1532</td>
            <td align=right>2116</td>
        </tr>
        <tr style="font-weight:bold">
            <td>PowerMapper</td>
            <td align=right>332</td>
            <td align=right>290</td>
            <td align=right>482</td>
            <td align=right>481</td>
            <td align=right>327</td>
            <td align=right>341</td>
            <td align=right>450</td>
            <td align=right>524</td>
            <td align=right>647</td>
            <td align=right>1296</td>
            <td align=right>1418</td>
            <td align=right>3001</td>
        </tr>
    </tbody>
</table>

(NOTE: Benchmark was run against largest set of data,
times are in milliseconds, lower is better. Blank values mean the library did not support the test.)

Features:
--------
#### Mapping
* **Mapping to a new object**

  PowerMapper creates the destination object and maps values to it.

        var targetInstance = Mapper.Map<TSource,TTarget>(sourceInstance);

  or just 

        var targetInstance = Mapper.Map<TTarget>(sourceInstance);

  using extension method

         var targetInstance = sourceInstance.MapTo<TSource,TTarget>();

  or just

         var targetInstance = sourceInstance.MapTo<TTarget>();

* **Mapping to an existing object**

  You make the object, PowerMapper maps to the object.

        TTarget targetInstance = new TTarget();
        Mapper.Map(sourceInstance, targetInstance);

  or using the extension method

        TTarget targetInstance = new TTarget();
        sourceInstance.MapTo(targetInstance);

#### Lists and Arrays
PowerMapper only requires configuration of element types, not of any array or list type that might be used. 
Then all the basic generic collection types are supported:

        var sources = new[]
        {
            new Source { Value = 5 },
            new Source { Value = 6 },
            new Source { Value = 7 }
        };
        IEnumerable<Destination> dest = Mapper.Map<Source, Destination>((IEnumerable<Source>)sources);
        ICollection<Destination> dest = Mapper.Map<Source, Destination>((ICollection<Source>)sources);
        IList<Destination> dest = Mapper.Map<Source, Destination>((IList<Source>)sources);
        List<Destination> dest = Mapper.Map<Source, Destination>(new List<Source>(sources));
        Destination[] dest = Mapper.Map<Source, Destination>(sources);

To be specific, the collection types supported:
* IEnumerable\<T\>
* ICollection\<T\>
* IList\<T\>
* List\<T\>
* Array

The collection type are supported for the properties and fields too.

#### Converters
The converters are global used for value conversions and applied for every type mapping in the following scenarios:
1. Mapping to a new object directly which matches the source type and target type of the registered converter.
2. Mapping to a new object, the mapping source type and target type are implemented from IEnumerable<T>. 
   And the element type of the source and target matches the registered converter.
3. Mapping properties or fields that the source type and target type of the property or field matches the registered converter.
4. Mapping properties or fields that the source type and target type of the property or field are implemented from IEnumerable<T>.
   And the element type of the property or field matches the registered converter.

There have been lots of intrinsic converters for most frequently use as following:
* **Anyting to string**: Call the object.ToString() method.
* **Parse From String**: Detects the Parse method from the source type declaration.
* **Enum Converter**: Convert enumeration value from or to the types: string, int, long, short.
* **Primitive converter**: Convert between the primitive types: byte, sbyte, char, double, float, decimal, int,
  uint, short, ushort, long, ulong, bool, DateTime, TimeSpan, byte[]
* **Convert with operator**: Convert with the explicitly or implicitly declared type convert operators, for example
        
        public static implicit operator string(MySource source)
        {
            return string.Format("The source value is {0}.", source.Value);
        }

* **string <-> Type**
* **DateTime -> DateTimeOffset**
* **Guid <-> byte[]**
* **IPAddress <-> byte[]**
* **string <-> Uri**
* **string <-> TimeZoneInfo**

When the intrinsic converters cannot be performed in your applications, you can register customized converter using the following method:

        Mapper.RegisterConverter((TSource source)=> convert(source));

or register converter on the assembly startup

        public void Configuration(IMappingContainer container)
        {
            container.RegisterConverter((TSource source)=> convert(source));
        }

*NOTE: All the converters will be compiled automatically when the first type mapping performed.
So the codes should be placed in assembly startup or application startup.*

#### Convensions
The convensions are global used for matching type members(properties and fields) and applied for every type mapping.

For example, if the member with name "ID" should be ignored, the code should be like following

       Mapper.Conventions.Add(context => context.Mappings.Ignore("ID"));

For example, if the automatically generated key members of EntityFramework entity should be ignored, the code should be like following

       Mapper.Conventions.Add(context =>
       {
           var keyMembers = (from mapping in context.Mappings
                where Attribute.IsDefined(mapping.TargetMember.ClrMember, typeof(KeyAttribute))
                let attr = mapping.TargetMember.ClrMember.GetCustomAttribute<DatabaseGeneratedAttribute>()
                where attr != null && attr.DatabaseGeneratedOption != DatabaseGeneratedOption.None
                select mapping.TargetMember).ToArray();
           foreach (var keyMember in keyMembers)
           {
               context.Mappings.Ignore(keyMember);
           }
       });

For example, if the target instance should be created by using IoC container, the code should be like

        Mapper.Conventions.Add(context => context.CreateWith(ServiceContainer.GetInstance));

or register convention on the assembly startup

        public void Configuration(IMappingContainer container)
        {
            container.Conventions.Add(context => context.CreateWith(ServiceContainer.GetInstance));
        }

There have been intrinsic convension to match type members by using member name, and its behavior can be controlled by options.

*NOTE: All the custom conventions will be compiled automatically when the first type mapping performed.
So the codes should be placed in assembly startup or application startup.*

#### Options
The mapping options is the convenient way to control the mapping strategy. 

        Mapper.Configure<TSource, TTarget>().WithOptions(MemberMapOptions.IgnoreCase);

or configure on the assembly startup

        public void Configuration(IMappingContainer container)
        {
            container.Configure<TSource, TTarget>().WithOptions(MemberMapOptions.IgnoreCase);
        }

The options for the configuration can be:

1. **MemberMapOptions.IgnoreCase**: The member name will be case insensitively matched. Otherwise, it will be case sensitively.
2. **MemberMapOptions.NonPublic**: The non-public(private, internal, protected) and public members will be included for matching. Otherwise, only the public members will be included.
3. **MemberMapOptions.Hierarchy**: The source type and target type will be mapped hierarchically. Otherwise only the first level will be matched.
4. **MemberMapOptions.Default**: The mapping strategy will be performed as the default behaviors.

#### Configuration
Although the intrinsic strategy covers quite a few destination member mapping scenarios, 
there are the 1 to 5% of destination values that need a little help in resolving.

*NOTE: All the custom configurations will be compiled automatically when the relative type mapping performed.
So the codes should be placed in static constructor or assembly startup.*

**Custom member mapping**

Many times, the custom member mapping logic is domain logic that can go straight on our domain. 
However, if this logic pertains only to the mapping operation, 
it would clutter our source types with unnecessary behavior. 
In these cases, PowerMapper allows for configuring custom member mapping for destination members. 
For example, we might want to have a calculated value just during mapping:

        public class Source
        {
            public int Value1 { get; set; }
            public int Value2 { get; set; }
        }

        public class Destination
        {
            public int Total { get; set; }
        }

For whatever reason, we want Total to be the sum of the source Value properties. 
For some other reason, we can't or shouldn't put this logic on our Source type.
We will supply a custom value resolver to PowerMapper

        Mapper.Configure<Source, Destination>()
            .MapMember(dest => dest.Total, src => src.Value1 + src.Value2);

**Custom constructor**

By default, the mapping engine will use reflection to create an instance throught the parameterless constructor.
If you want to use other approach to create new instance, we can supply a custom constructor method:

        Mapper.Configure<Source, Destination>()
           .CreateWith(src => DestFactory.Create(src));

PowerMapper will execute this callback function instead of using reflection during the mapping operation, 
helpful in scenarios where the target type might have constructor arguments 
or need to be constructed by an IoC container.

**Before and after map actions**

Occasionally, you might need to perform custom logic before or after a map occurs. 
These should be a rarity, as it's more obvious to do this work outside of PowerMapper.

        Mapper.Configure<Source, Destination>()
            .BeforeMap((src, dest) => src.Value = src.Value + 10)
            .AfterMap((src, dest) => dest.Name = "John");

The latter configuration is helpful when you need contextual information fed into before/after map actions.

**Ignore members**

PowerMapper will automatically map properties with the same names. You can ignore members by using the `Ignore` method.
        
        Mapper.Configure<Source, Destination>().Ignore("ID");

or using lambda expression

        Mapper.Configure<Source, Destination>().Ignore(dest => dest.ID);

#### Todo

* Linq Query Support.
* .Net Core Support.
* TransparentProxy Support.

License
------
PowerMapper is Open Source software and it is released under the MIT license.
The licenses allow the use of PowerMapper in free and commercial applications and libraries without restrictions.