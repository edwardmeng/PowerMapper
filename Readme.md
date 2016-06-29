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
Wheatech.EmitMapper 2.0 is now blistering fast! Please refer to the benchmark

<table>
    <thead>
        <tr>
            <th>Engine</th>
            <th>Structs</th>
            <th>Simple objects</th>
            <th>Parent-Child</th>
            <th>Parent-Children</th>
            <th>Complex objects</th>
            <th>Advance mapping</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>AutoMapper</td>
            <td>721</td>
            <td>1119</td>
            <td>881</td>
            <td>901</td>
            <td>3943</td>
            <td>4087</td>
        </tr>
        <tr>
            <td>ExpressMapper</td>
            <td>360</td>
            <td>1042</td>
            <td>536</td>
            <td>630</td>
            <td>3120</td>
            <td>2866</td>
        </tr>
        <tr>
            <td>OoMapper</td>
            <td>-</td>
            <td>760</td>
            <td>675</td>
            <td>787</td>
            <td>-</td>
            <td>1904</td>
        </tr>
        <tr>
            <td>ValueInjector</td>
            <td>4444</td>
            <td>10814</td>
            <td>9154</td>
            <td>340</td>
            <td>17681</td>
            <td>18284</td>
        </tr>
        <tr>
            <td>TinyMapper</td>
            <td>-</td>
            <td>573</td>
            <td>-</td>
            <td>-</td>
            <td>-</td>
            <td>-</td>
        </tr>
        <tr>
            <td>Mapster</td>
            <td>338</td>
            <td>628</td>
            <td>1481</td>
            <td>705</td>
            <td>4372</td>
            <td>4479</td>
        </tr>
        <tr>
            <td>Native</td>
            <td>310</td>
            <td>419</td>
            <td>271</td>
            <td>454</td>
            <td>2297</td>
            <td>2096</td>
        </tr>
        <tr>
            <td>EmitMapper</td>
            <td>314</td>
            <td>395</td>
            <td>267</td>
            <td>387</td>
            <td>795</td>
            <td>2761</td>
        </tr>
    </tbody>
</table>

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

#### Converters
The converters will be globally used in the following scenarios:
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