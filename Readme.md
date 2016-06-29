Wheatech.EmitMapper
===================
A extremely fast and friendly convention-based object-object mapper.
It is implemented via the Emit library and uses a fluent configuration API to define an object-object mapping strategy.
EmitMapper is useful for dealing with DTO objects, data access layers an so on.

Get it on NuGet!
---------------
    Install-Package Wheatech.EmitMapper

Supported platform:
------------------
Microsoft .NET Framework 4.6.1

Comparisons:
-----------
Wheatech.EmitMapper is now blistering fast! Please refer to the benchmark

| Engine            | Structs | Simple objects | Parent-Child | Parent-Children | Complex objects | Advance mapping |
|-------------------|--------:|---------------:|-------------:|----------------:|----------------:|----------------:|
| AutoMapper        |     721 |           1119 |          881 |             901 |            3943 |            4087 |
| ExpressMapper     |     360 |           1042 |          536 |             630 |            3120 |            2866 |
| OoMapper          |       - |            760 |          675 |             787 |               - |            1904 |
| ValueInjector     |    4444 |          10814 |         9154 |             340 |           17681 |           18284 |
| TinyMapper        |       - |            573 |            - |               - |               - |               - |
| Mapster           |     338 |            628 |         1481 |             705 |            4372 |            4479 |
| Native            |     310 |            419 |          271 |             454 |            2297 |            2096 |
| **EmitMapper**   |**314** |        **395** |     **267** |         **387** |         **795** |       **2761** |

(NOTE: Benchmark was run against largest set of data,
times are in milliseconds, lower is better. Blank values mean the library did not support the test.)

Features:
--------
#### Mapping
* **Mapping to a new object**

  EmitMapper creates the destination object and maps values to it.

        var targetInstance = Mapper.Map<TSource,TTarget>(sourceInstance);

  or just 

        var targetInstance = Mapper.Map<TTarget>(sourceInstance);

  using extension method

         var targetInstance = sourceInstance.MapTo<TSource,TTarget>();

  or just

         var targetInstance = sourceInstance.MapTo<TTarget>();

* **Mapping to an existing object**

  You make the object, EmitMapper maps to the object.

        TTarget targetInstance = new TTarget();
        Mapper.Map(sourceInstance, targetInstance);

  or using the extension method

        TTarget targetInstance = new TTarget();
        sourceInstance.MapTo(targetInstance);

#### Lists and Arrays
EmitMapper only requires configuration of element types, not of any array or list type that might be used. 
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

There have been intrinsic convension to match type members by using member name, and its behavior can be controlled by options.

#### Options
The mapping options is the convenient way to control the mapping strategy. 

        Mapper.Configure<TSource, TTarget>().WithOptions(MemberMapOptions.IgnoreCase);

The options for the configuration can be:
1. **MemberMapOptions.IgnoreCase**: The member name will be case insensitively matched. Otherwise, it will be case sensitively.
2. **MemberMapOptions.NonPublic**: The non-public(private, internal, protected) and public members will be included for matching. Otherwise, only the public members will be included.
3. **MemberMapOptions.Hierarchy**: The source type and target type will be mapped hierarchically. Otherwise only the first level will be matched.
4. **MemberMapOptions.Default**: The mapping strategy will be performed as the default behaviors.

#### Configuration
