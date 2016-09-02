using System;
using System.Net;
using System.Web.UI.WebControls;
using Xunit;

namespace PowerMapper.UnitTests
{
    public class ConverterTest
    {
        #region Enums

        #region Enum Define

        public enum ByteEnum : byte
        {
            None = 0,
            One = 1,
            Two = 2
        }

        public enum SByteEnum : byte
        {
            None = 0,
            One = 1,
            Two = 2
        }

        public enum Int16Enum : short
        {
            None = 0,
            One = 1,
            Two = 2
        }

        public enum UInt16Enum : ushort
        {
            None = 0,
            One = 1,
            Two = 2
        }

        public enum Int32Enum
        {
            None = 0,
            One = 1,
            Two = 2
        }

        public enum UInt32Enum : uint
        {
            None = 0,
            One = 1,
            Two = 2
        }

        public enum Int64Enum : long
        {
            None = 0,
            One = 1,
            Two = 2
        }

        public enum UInt64Enum : ulong
        {
            None = 0,
            One = 1,
            Two = 2
        }

        #endregion

        #region ByteEnum

        [Fact]
        public void TestFromByteEnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<ByteEnum, sbyte>(ByteEnum.Two));
            Assert.Equal((byte)2, Mapper.Map<ByteEnum, byte>(ByteEnum.Two));
            Assert.Equal((char)2, Mapper.Map<ByteEnum, char>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, int>(ByteEnum.Two));
            Assert.Equal((uint)2, Mapper.Map<ByteEnum, uint>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, long>(ByteEnum.Two));
            Assert.Equal((ulong)2, Mapper.Map<ByteEnum, ulong>(ByteEnum.Two));
            Assert.Equal((short)2, Mapper.Map<ByteEnum, short>(ByteEnum.Two));
            Assert.Equal((ushort)2, Mapper.Map<ByteEnum, ushort>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, float>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, double>(ByteEnum.Two));
            Assert.Equal("Two", Mapper.Map<ByteEnum, string>(ByteEnum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<ByteEnum, sbyte?>(ByteEnum.Two));
            Assert.Equal((byte?)2, Mapper.Map<ByteEnum, byte?>(ByteEnum.Two));
            Assert.Equal((char?)2, Mapper.Map<ByteEnum, char?>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, int?>(ByteEnum.Two));
            Assert.Equal((uint?)2, Mapper.Map<ByteEnum, uint?>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, long?>(ByteEnum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<ByteEnum, ulong?>(ByteEnum.Two));
            Assert.Equal((short?)2, Mapper.Map<ByteEnum, short?>(ByteEnum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<ByteEnum, ushort?>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, float?>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum, double?>(ByteEnum.Two));
        }

        [Fact]
        public void TestFromNullbaleByteEnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<ByteEnum?, sbyte>(ByteEnum.Two));
            Assert.Equal((byte)2, Mapper.Map<ByteEnum?, byte>(ByteEnum.Two));
            Assert.Equal((char)2, Mapper.Map<ByteEnum?, char>(ByteEnum.Two));
            Assert.Equal((uint)2, Mapper.Map<ByteEnum?, uint>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum?, long>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum?, int>(ByteEnum.Two));
            Assert.Equal((ulong)2, Mapper.Map<ByteEnum?, ulong>(ByteEnum.Two));
            Assert.Equal((short)2, Mapper.Map<ByteEnum?, short>(ByteEnum.Two));
            Assert.Equal((ushort)2, Mapper.Map<ByteEnum?, ushort>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum?, float>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum?, double>(ByteEnum.Two));
            Assert.Equal("Two", Mapper.Map<ByteEnum?, string>(ByteEnum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<ByteEnum?, sbyte>((ByteEnum?)null));
            Assert.Equal((byte)0, Mapper.Map<ByteEnum?, byte>((ByteEnum?)null));
            Assert.Equal((char)0, Mapper.Map<ByteEnum?, char>((ByteEnum?)null));
            Assert.Equal((uint)0, Mapper.Map<ByteEnum?, uint>((ByteEnum?)null));
            Assert.Equal(0, Mapper.Map<ByteEnum?, long>((ByteEnum?)null));
            Assert.Equal(0, Mapper.Map<ByteEnum?, int>((ByteEnum?)null));
            Assert.Equal((ulong)0, Mapper.Map<ByteEnum?, ulong>((ByteEnum?)null));
            Assert.Equal((short)0, Mapper.Map<ByteEnum?, short>((ByteEnum?)null));
            Assert.Equal((ushort)0, Mapper.Map<ByteEnum?, ushort>((ByteEnum?)null));
            Assert.Equal(0, Mapper.Map<ByteEnum?, float>((ByteEnum?)null));
            Assert.Equal(0, Mapper.Map<ByteEnum?, double>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, string>((ByteEnum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<ByteEnum?, sbyte?>(ByteEnum.Two));
            Assert.Equal((byte?)2, Mapper.Map<ByteEnum?, byte?>(ByteEnum.Two));
            Assert.Equal((char?)2, Mapper.Map<ByteEnum?, char?>(ByteEnum.Two));
            Assert.Equal((uint?)2, Mapper.Map<ByteEnum?, uint?>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum?, long?>(ByteEnum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<ByteEnum?, ulong?>(ByteEnum.Two));
            Assert.Equal((short?)2, Mapper.Map<ByteEnum?, short?>(ByteEnum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<ByteEnum?, ushort?>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum?, float?>(ByteEnum.Two));
            Assert.Equal(2, Mapper.Map<ByteEnum?, double?>(ByteEnum.Two));

            Assert.Equal(null, Mapper.Map<ByteEnum?, sbyte?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, byte?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, char?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, uint?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, long?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, ulong?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, short?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, ushort?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, float?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, double?>((ByteEnum?)null));
            Assert.Equal(null, Mapper.Map<ByteEnum?, string>((ByteEnum?)null));
        }

        [Fact]
        public void TestToByteEnumConvert()
        {
            Assert.Equal(ByteEnum.Two, Mapper.Map<sbyte, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<byte, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<char, ByteEnum>((char)2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<int, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<uint, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<long, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ulong, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<short, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ushort, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<float, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<double, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<string, ByteEnum>("Two"));

            Assert.Equal(ByteEnum.Two, Mapper.Map<sbyte?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<byte?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<char?, ByteEnum>((char?)2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<int?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<uint?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<long?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ulong?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<short?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ushort?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<float?, ByteEnum>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<double?, ByteEnum>(2));
        }

        [Fact]
        public void TestToNullableByteEnumConvert()
        {
            Assert.Equal(ByteEnum.Two, Mapper.Map<sbyte, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<byte, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<char, ByteEnum?>((char)2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<uint, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<long, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<int, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ulong, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<short, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ushort, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<float, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<double, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<string, ByteEnum?>("Two"));

            Assert.Equal(ByteEnum.Two, Mapper.Map<sbyte?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<byte?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<char?, ByteEnum?>((char?)2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<uint?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<long?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ulong?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<short?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<ushort?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<float?, ByteEnum?>(2));
            Assert.Equal(ByteEnum.Two, Mapper.Map<double?, ByteEnum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, ByteEnum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, ByteEnum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, ByteEnum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, ByteEnum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, ByteEnum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, ByteEnum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, ByteEnum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, ByteEnum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, ByteEnum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, ByteEnum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, ByteEnum?>((string)null));
        }

        #endregion

        #region SByteEnum

        [Fact]
        public void TestFromSByteEnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<SByteEnum, sbyte>(SByteEnum.Two));
            Assert.Equal((byte)2, Mapper.Map<SByteEnum, byte>(SByteEnum.Two));
            Assert.Equal((char)2, Mapper.Map<SByteEnum, char>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, int>(SByteEnum.Two));
            Assert.Equal((uint)2, Mapper.Map<SByteEnum, uint>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, long>(SByteEnum.Two));
            Assert.Equal((ulong)2, Mapper.Map<SByteEnum, ulong>(SByteEnum.Two));
            Assert.Equal((short)2, Mapper.Map<SByteEnum, short>(SByteEnum.Two));
            Assert.Equal((ushort)2, Mapper.Map<SByteEnum, ushort>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, float>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, double>(SByteEnum.Two));
            Assert.Equal("Two", Mapper.Map<SByteEnum, string>(SByteEnum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<SByteEnum, sbyte?>(SByteEnum.Two));
            Assert.Equal((byte?)2, Mapper.Map<SByteEnum, byte?>(SByteEnum.Two));
            Assert.Equal((char?)2, Mapper.Map<SByteEnum, char?>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, int?>(SByteEnum.Two));
            Assert.Equal((uint?)2, Mapper.Map<SByteEnum, uint?>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, long?>(SByteEnum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<SByteEnum, ulong?>(SByteEnum.Two));
            Assert.Equal((short?)2, Mapper.Map<SByteEnum, short?>(SByteEnum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<SByteEnum, ushort?>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, float?>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum, double?>(SByteEnum.Two));
        }

        [Fact]
        public void TestFromNullableSByteEnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<SByteEnum?, sbyte>(SByteEnum.Two));
            Assert.Equal((byte)2, Mapper.Map<SByteEnum?, byte>(SByteEnum.Two));
            Assert.Equal((char)2, Mapper.Map<SByteEnum?, char>(SByteEnum.Two));
            Assert.Equal((uint)2, Mapper.Map<SByteEnum?, uint>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum?, long>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum?, int>(SByteEnum.Two));
            Assert.Equal((ulong)2, Mapper.Map<SByteEnum?, ulong>(SByteEnum.Two));
            Assert.Equal((short)2, Mapper.Map<SByteEnum?, short>(SByteEnum.Two));
            Assert.Equal((ushort)2, Mapper.Map<SByteEnum?, ushort>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum?, float>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum?, double>(SByteEnum.Two));
            Assert.Equal("Two", Mapper.Map<SByteEnum?, string>(SByteEnum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<SByteEnum?, sbyte>((SByteEnum?)null));
            Assert.Equal((byte)0, Mapper.Map<SByteEnum?, byte>((SByteEnum?)null));
            Assert.Equal((char)0, Mapper.Map<SByteEnum?, char>((SByteEnum?)null));
            Assert.Equal((uint)0, Mapper.Map<SByteEnum?, uint>((SByteEnum?)null));
            Assert.Equal(0, Mapper.Map<SByteEnum?, long>((SByteEnum?)null));
            Assert.Equal(0, Mapper.Map<SByteEnum?, int>((SByteEnum?)null));
            Assert.Equal((ulong)0, Mapper.Map<SByteEnum?, ulong>((SByteEnum?)null));
            Assert.Equal((short)0, Mapper.Map<SByteEnum?, short>((SByteEnum?)null));
            Assert.Equal((ushort)0, Mapper.Map<SByteEnum?, ushort>((SByteEnum?)null));
            Assert.Equal(0, Mapper.Map<SByteEnum?, float>((SByteEnum?)null));
            Assert.Equal(0, Mapper.Map<SByteEnum?, double>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, string>((SByteEnum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<SByteEnum?, sbyte?>(SByteEnum.Two));
            Assert.Equal((byte?)2, Mapper.Map<SByteEnum?, byte?>(SByteEnum.Two));
            Assert.Equal((char?)2, Mapper.Map<SByteEnum?, char?>(SByteEnum.Two));
            Assert.Equal((uint?)2, Mapper.Map<SByteEnum?, uint?>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum?, long?>(SByteEnum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<SByteEnum?, ulong?>(SByteEnum.Two));
            Assert.Equal((short?)2, Mapper.Map<SByteEnum?, short?>(SByteEnum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<SByteEnum?, ushort?>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum?, float?>(SByteEnum.Two));
            Assert.Equal(2, Mapper.Map<SByteEnum?, double?>(SByteEnum.Two));

            Assert.Equal(null, Mapper.Map<SByteEnum?, sbyte?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, byte?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, char?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, uint?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, long?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, ulong?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, short?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, ushort?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, float?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, double?>((SByteEnum?)null));
            Assert.Equal(null, Mapper.Map<SByteEnum?, string>((SByteEnum?)null));
        }

        [Fact]
        public void TestToSByteEnumConvert()
        {
            Assert.Equal(SByteEnum.Two, Mapper.Map<sbyte, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<byte, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<char, SByteEnum>((char)2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<int, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<uint, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<long, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ulong, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<short, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ushort, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<float, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<double, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<string, SByteEnum>("Two"));

            Assert.Equal(SByteEnum.Two, Mapper.Map<sbyte?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<byte?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<char?, SByteEnum>((char?)2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<int?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<uint?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<long?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ulong?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<short?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ushort?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<float?, SByteEnum>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<double?, SByteEnum>(2));
        }

        [Fact]
        public void TestToNullableSByteEnumConvert()
        {
            Assert.Equal(SByteEnum.Two, Mapper.Map<sbyte, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<byte, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<char, SByteEnum?>((char)2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<uint, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<long, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<int, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ulong, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<short, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ushort, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<float, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<double, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<string, SByteEnum?>("Two"));

            Assert.Equal(SByteEnum.Two, Mapper.Map<sbyte?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<byte?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<char?, SByteEnum?>((char?)2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<uint?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<long?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ulong?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<short?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<ushort?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<float?, SByteEnum?>(2));
            Assert.Equal(SByteEnum.Two, Mapper.Map<double?, SByteEnum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, SByteEnum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, SByteEnum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, SByteEnum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, SByteEnum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, SByteEnum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, SByteEnum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, SByteEnum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, SByteEnum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, SByteEnum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, SByteEnum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, SByteEnum?>((string)null));
        }

        #endregion

        #region Int16Enum

        [Fact]
        public void TestFromInt16EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<Int16Enum, sbyte>(Int16Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<Int16Enum, byte>(Int16Enum.Two));
            Assert.Equal((char)2, Mapper.Map<Int16Enum, char>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, int>(Int16Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<Int16Enum, uint>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, long>(Int16Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<Int16Enum, ulong>(Int16Enum.Two));
            Assert.Equal((short)2, Mapper.Map<Int16Enum, short>(Int16Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<Int16Enum, ushort>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, float>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, double>(Int16Enum.Two));
            Assert.Equal("Two", Mapper.Map<Int16Enum, string>(Int16Enum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<Int16Enum, sbyte?>(Int16Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<Int16Enum, byte?>(Int16Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<Int16Enum, char?>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, int?>(Int16Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<Int16Enum, uint?>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, long?>(Int16Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<Int16Enum, ulong?>(Int16Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<Int16Enum, short?>(Int16Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<Int16Enum, ushort?>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, float?>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum, double?>(Int16Enum.Two));
        }

        [Fact]
        public void TestFromNullableInt16EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<Int16Enum?, sbyte>(Int16Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<Int16Enum?, byte>(Int16Enum.Two));
            Assert.Equal((char)2, Mapper.Map<Int16Enum?, char>(Int16Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<Int16Enum?, uint>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum?, long>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum?, int>(Int16Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<Int16Enum?, ulong>(Int16Enum.Two));
            Assert.Equal((short)2, Mapper.Map<Int16Enum?, short>(Int16Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<Int16Enum?, ushort>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum?, float>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum?, double>(Int16Enum.Two));
            Assert.Equal("Two", Mapper.Map<Int16Enum?, string>(Int16Enum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<Int16Enum?, sbyte>((Int16Enum?)null));
            Assert.Equal((byte)0, Mapper.Map<Int16Enum?, byte>((Int16Enum?)null));
            Assert.Equal((char)0, Mapper.Map<Int16Enum?, char>((Int16Enum?)null));
            Assert.Equal((uint)0, Mapper.Map<Int16Enum?, uint>((Int16Enum?)null));
            Assert.Equal(0, Mapper.Map<Int16Enum?, long>((Int16Enum?)null));
            Assert.Equal(0, Mapper.Map<Int16Enum?, int>((Int16Enum?)null));
            Assert.Equal((ulong)0, Mapper.Map<Int16Enum?, ulong>((Int16Enum?)null));
            Assert.Equal((short)0, Mapper.Map<Int16Enum?, short>((Int16Enum?)null));
            Assert.Equal((ushort)0, Mapper.Map<Int16Enum?, ushort>((Int16Enum?)null));
            Assert.Equal(0, Mapper.Map<Int16Enum?, float>((Int16Enum?)null));
            Assert.Equal(0, Mapper.Map<Int16Enum?, double>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, string>((Int16Enum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<Int16Enum?, sbyte?>(Int16Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<Int16Enum?, byte?>(Int16Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<Int16Enum?, char?>(Int16Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<Int16Enum?, uint?>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum?, long?>(Int16Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<Int16Enum?, ulong?>(Int16Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<Int16Enum?, short?>(Int16Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<Int16Enum?, ushort?>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum?, float?>(Int16Enum.Two));
            Assert.Equal(2, Mapper.Map<Int16Enum?, double?>(Int16Enum.Two));

            Assert.Equal(null, Mapper.Map<Int16Enum?, sbyte?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, byte?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, char?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, uint?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, long?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, ulong?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, short?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, ushort?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, float?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, double?>((Int16Enum?)null));
            Assert.Equal(null, Mapper.Map<Int16Enum?, string>((Int16Enum?)null));
        }

        [Fact]
        public void TestToInt16EnumConvert()
        {
            Assert.Equal(Int16Enum.Two, Mapper.Map<sbyte, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<byte, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<char, Int16Enum>((char)2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<int, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<uint, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<long, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ulong, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<short, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ushort, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<float, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<double, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<string, Int16Enum>("Two"));

            Assert.Equal(Int16Enum.Two, Mapper.Map<sbyte?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<byte?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<char?, Int16Enum>((char?)2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<int?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<uint?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<long?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ulong?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<short?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ushort?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<float?, Int16Enum>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<double?, Int16Enum>(2));
        }

        [Fact]
        public void TestToNullableInt16EnumConvert()
        {
            Assert.Equal(Int16Enum.Two, Mapper.Map<sbyte, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<byte, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<char, Int16Enum?>((char)2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<uint, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<long, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<int, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ulong, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<short, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ushort, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<float, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<double, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<string, Int16Enum?>("Two"));

            Assert.Equal(Int16Enum.Two, Mapper.Map<sbyte?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<byte?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<char?, Int16Enum?>((char?)2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<uint?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<long?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ulong?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<short?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<ushort?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<float?, Int16Enum?>(2));
            Assert.Equal(Int16Enum.Two, Mapper.Map<double?, Int16Enum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, Int16Enum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, Int16Enum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, Int16Enum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, Int16Enum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, Int16Enum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, Int16Enum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, Int16Enum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, Int16Enum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, Int16Enum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, Int16Enum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, Int16Enum?>((string)null));
        }

        #endregion

        #region UInt16Enum

        [Fact]
        public void TestFromUInt16EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<UInt16Enum, sbyte>(UInt16Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<UInt16Enum, byte>(UInt16Enum.Two));
            Assert.Equal((char)2, Mapper.Map<UInt16Enum, char>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, int>(UInt16Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<UInt16Enum, uint>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, long>(UInt16Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<UInt16Enum, ulong>(UInt16Enum.Two));
            Assert.Equal((short)2, Mapper.Map<UInt16Enum, short>(UInt16Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<UInt16Enum, ushort>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, float>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, double>(UInt16Enum.Two));
            Assert.Equal("Two", Mapper.Map<UInt16Enum, string>(UInt16Enum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<UInt16Enum, sbyte?>(UInt16Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<UInt16Enum, byte?>(UInt16Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<UInt16Enum, char?>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, int?>(UInt16Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<UInt16Enum, uint?>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, long?>(UInt16Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<UInt16Enum, ulong?>(UInt16Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<UInt16Enum, short?>(UInt16Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<UInt16Enum, ushort?>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, float?>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum, double?>(UInt16Enum.Two));
        }

        [Fact]
        public void TestFromNullableUInt16EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<UInt16Enum?, sbyte>(UInt16Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<UInt16Enum?, byte>(UInt16Enum.Two));
            Assert.Equal((char)2, Mapper.Map<UInt16Enum?, char>(UInt16Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<UInt16Enum?, uint>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum?, long>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum?, int>(UInt16Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<UInt16Enum?, ulong>(UInt16Enum.Two));
            Assert.Equal((short)2, Mapper.Map<UInt16Enum?, short>(UInt16Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<UInt16Enum?, ushort>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum?, float>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum?, double>(UInt16Enum.Two));
            Assert.Equal("Two", Mapper.Map<UInt16Enum?, string>(UInt16Enum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<UInt16Enum?, sbyte>((UInt16Enum?)null));
            Assert.Equal((byte)0, Mapper.Map<UInt16Enum?, byte>((UInt16Enum?)null));
            Assert.Equal((char)0, Mapper.Map<UInt16Enum?, char>((UInt16Enum?)null));
            Assert.Equal((uint)0, Mapper.Map<UInt16Enum?, uint>((UInt16Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt16Enum?, long>((UInt16Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt16Enum?, int>((UInt16Enum?)null));
            Assert.Equal((ulong)0, Mapper.Map<UInt16Enum?, ulong>((UInt16Enum?)null));
            Assert.Equal((short)0, Mapper.Map<UInt16Enum?, short>((UInt16Enum?)null));
            Assert.Equal((ushort)0, Mapper.Map<UInt16Enum?, ushort>((UInt16Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt16Enum?, float>((UInt16Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt16Enum?, double>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, string>((UInt16Enum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<UInt16Enum?, sbyte?>(UInt16Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<UInt16Enum?, byte?>(UInt16Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<UInt16Enum?, char?>(UInt16Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<UInt16Enum?, uint?>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum?, long?>(UInt16Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<UInt16Enum?, ulong?>(UInt16Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<UInt16Enum?, short?>(UInt16Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<UInt16Enum?, ushort?>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum?, float?>(UInt16Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt16Enum?, double?>(UInt16Enum.Two));

            Assert.Equal(null, Mapper.Map<UInt16Enum?, sbyte?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, byte?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, char?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, uint?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, long?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, ulong?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, short?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, ushort?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, float?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, double?>((UInt16Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt16Enum?, string>((UInt16Enum?)null));
        }

        [Fact]
        public void TestToUInt16EnumConvert()
        {
            Assert.Equal(UInt16Enum.Two, Mapper.Map<sbyte, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<byte, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<char, UInt16Enum>((char)2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<int, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<uint, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<long, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ulong, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<short, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ushort, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<float, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<double, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<string, UInt16Enum>("Two"));

            Assert.Equal(UInt16Enum.Two, Mapper.Map<sbyte?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<byte?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<char?, UInt16Enum>((char?)2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<int?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<uint?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<long?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ulong?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<short?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ushort?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<float?, UInt16Enum>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<double?, UInt16Enum>(2));
        }

        [Fact]
        public void TestToNullableUInt16EnumConvert()
        {
            Assert.Equal(UInt16Enum.Two, Mapper.Map<sbyte, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<byte, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<char, UInt16Enum?>((char)2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<uint, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<long, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<int, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ulong, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<short, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ushort, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<float, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<double, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<string, UInt16Enum?>("Two"));

            Assert.Equal(UInt16Enum.Two, Mapper.Map<sbyte?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<byte?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<char?, UInt16Enum?>((char?)2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<uint?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<long?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ulong?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<short?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<ushort?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<float?, UInt16Enum?>(2));
            Assert.Equal(UInt16Enum.Two, Mapper.Map<double?, UInt16Enum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, UInt16Enum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, UInt16Enum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, UInt16Enum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, UInt16Enum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, UInt16Enum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, UInt16Enum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, UInt16Enum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, UInt16Enum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, UInt16Enum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, UInt16Enum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, UInt16Enum?>((string)null));
        }

        #endregion

        #region Int32Enum

        [Fact]
        public void TestFromInt32EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<Int32Enum, sbyte>(Int32Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<Int32Enum, byte>(Int32Enum.Two));
            Assert.Equal((char)2, Mapper.Map<Int32Enum, char>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, int>(Int32Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<Int32Enum, uint>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, long>(Int32Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<Int32Enum, ulong>(Int32Enum.Two));
            Assert.Equal((short)2, Mapper.Map<Int32Enum, short>(Int32Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<Int32Enum, ushort>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, float>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, double>(Int32Enum.Two));
            Assert.Equal("Two", Mapper.Map<Int32Enum, string>(Int32Enum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<Int32Enum, sbyte?>(Int32Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<Int32Enum, byte?>(Int32Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<Int32Enum, char?>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, int?>(Int32Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<Int32Enum, uint?>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, long?>(Int32Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<Int32Enum, ulong?>(Int32Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<Int32Enum, short?>(Int32Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<Int32Enum, ushort?>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, float?>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum, double?>(Int32Enum.Two));
        }

        [Fact]
        public void TestFromNullableInt32EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<Int32Enum?, sbyte>(Int32Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<Int32Enum?, byte>(Int32Enum.Two));
            Assert.Equal((char)2, Mapper.Map<Int32Enum?, char>(Int32Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<Int32Enum?, uint>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum?, long>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum?, int>(Int32Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<Int32Enum?, ulong>(Int32Enum.Two));
            Assert.Equal((short)2, Mapper.Map<Int32Enum?, short>(Int32Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<Int32Enum?, ushort>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum?, float>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum?, double>(Int32Enum.Two));
            Assert.Equal("Two", Mapper.Map<Int32Enum?, string>(Int32Enum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<Int32Enum?, sbyte>((Int32Enum?)null));
            Assert.Equal((byte)0, Mapper.Map<Int32Enum?, byte>((Int32Enum?)null));
            Assert.Equal((char)0, Mapper.Map<Int32Enum?, char>((Int32Enum?)null));
            Assert.Equal((uint)0, Mapper.Map<Int32Enum?, uint>((Int32Enum?)null));
            Assert.Equal(0, Mapper.Map<Int32Enum?, long>((Int32Enum?)null));
            Assert.Equal(0, Mapper.Map<Int32Enum?, int>((Int32Enum?)null));
            Assert.Equal((ulong)0, Mapper.Map<Int32Enum?, ulong>((Int32Enum?)null));
            Assert.Equal((short)0, Mapper.Map<Int32Enum?, short>((Int32Enum?)null));
            Assert.Equal((ushort)0, Mapper.Map<Int32Enum?, ushort>((Int32Enum?)null));
            Assert.Equal(0, Mapper.Map<Int32Enum?, float>((Int32Enum?)null));
            Assert.Equal(0, Mapper.Map<Int32Enum?, double>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, string>((Int32Enum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<Int32Enum?, sbyte?>(Int32Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<Int32Enum?, byte?>(Int32Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<Int32Enum?, char?>(Int32Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<Int32Enum?, uint?>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum?, long?>(Int32Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<Int32Enum?, ulong?>(Int32Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<Int32Enum?, short?>(Int32Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<Int32Enum?, ushort?>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum?, float?>(Int32Enum.Two));
            Assert.Equal(2, Mapper.Map<Int32Enum?, double?>(Int32Enum.Two));

            Assert.Equal(null, Mapper.Map<Int32Enum?, sbyte?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, byte?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, char?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, uint?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, long?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, ulong?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, short?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, ushort?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, float?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, double?>((Int32Enum?)null));
            Assert.Equal(null, Mapper.Map<Int32Enum?, string>((Int32Enum?)null));
        }

        [Fact]
        public void TestToInt32EnumConvert()
        {
            Assert.Equal(Int32Enum.Two, Mapper.Map<sbyte, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<byte, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<char, Int32Enum>((char)2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<int, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<uint, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<long, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ulong, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<short, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ushort, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<float, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<double, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<string, Int32Enum>("Two"));

            Assert.Equal(Int32Enum.Two, Mapper.Map<sbyte?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<byte?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<char?, Int32Enum>((char?)2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<int?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<uint?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<long?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ulong?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<short?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ushort?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<float?, Int32Enum>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<double?, Int32Enum>(2));
        }

        [Fact]
        public void TestToNullableInt32EnumConvert()
        {
            Assert.Equal(Int32Enum.Two, Mapper.Map<sbyte, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<byte, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<char, Int32Enum?>((char)2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<uint, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<long, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<int, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ulong, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<short, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ushort, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<float, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<double, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<string, Int32Enum?>("Two"));

            Assert.Equal(Int32Enum.Two, Mapper.Map<sbyte?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<byte?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<char?, Int32Enum?>((char?)2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<uint?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<long?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ulong?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<short?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<ushort?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<float?, Int32Enum?>(2));
            Assert.Equal(Int32Enum.Two, Mapper.Map<double?, Int32Enum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, Int32Enum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, Int32Enum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, Int32Enum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, Int32Enum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, Int32Enum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, Int32Enum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, Int32Enum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, Int32Enum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, Int32Enum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, Int32Enum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, Int32Enum?>((string)null));
        }

        #endregion

        #region UInt32Enum

        [Fact]
        public void TestFromUInt32EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<UInt32Enum, sbyte>(UInt32Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<UInt32Enum, byte>(UInt32Enum.Two));
            Assert.Equal((char)2, Mapper.Map<UInt32Enum, char>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, int>(UInt32Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<UInt32Enum, uint>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, long>(UInt32Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<UInt32Enum, ulong>(UInt32Enum.Two));
            Assert.Equal((short)2, Mapper.Map<UInt32Enum, short>(UInt32Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<UInt32Enum, ushort>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, float>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, double>(UInt32Enum.Two));
            Assert.Equal("Two", Mapper.Map<UInt32Enum, string>(UInt32Enum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<UInt32Enum, sbyte?>(UInt32Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<UInt32Enum, byte?>(UInt32Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<UInt32Enum, char?>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, int?>(UInt32Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<UInt32Enum, uint?>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, long?>(UInt32Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<UInt32Enum, ulong?>(UInt32Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<UInt32Enum, short?>(UInt32Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<UInt32Enum, ushort?>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, float?>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum, double?>(UInt32Enum.Two));
        }

        [Fact]
        public void TestFromNullableUInt32EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<UInt32Enum?, sbyte>(UInt32Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<UInt32Enum?, byte>(UInt32Enum.Two));
            Assert.Equal((char)2, Mapper.Map<UInt32Enum?, char>(UInt32Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<UInt32Enum?, uint>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum?, long>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum?, int>(UInt32Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<UInt32Enum?, ulong>(UInt32Enum.Two));
            Assert.Equal((short)2, Mapper.Map<UInt32Enum?, short>(UInt32Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<UInt32Enum?, ushort>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum?, float>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum?, double>(UInt32Enum.Two));
            Assert.Equal("Two", Mapper.Map<UInt32Enum?, string>(UInt32Enum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<UInt32Enum?, sbyte>((UInt32Enum?)null));
            Assert.Equal((byte)0, Mapper.Map<UInt32Enum?, byte>((UInt32Enum?)null));
            Assert.Equal((char)0, Mapper.Map<UInt32Enum?, char>((UInt32Enum?)null));
            Assert.Equal((uint)0, Mapper.Map<UInt32Enum?, uint>((UInt32Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt32Enum?, long>((UInt32Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt32Enum?, int>((UInt32Enum?)null));
            Assert.Equal((ulong)0, Mapper.Map<UInt32Enum?, ulong>((UInt32Enum?)null));
            Assert.Equal((short)0, Mapper.Map<UInt32Enum?, short>((UInt32Enum?)null));
            Assert.Equal((ushort)0, Mapper.Map<UInt32Enum?, ushort>((UInt32Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt32Enum?, float>((UInt32Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt32Enum?, double>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, string>((UInt32Enum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<UInt32Enum?, sbyte?>(UInt32Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<UInt32Enum?, byte?>(UInt32Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<UInt32Enum?, char?>(UInt32Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<UInt32Enum?, uint?>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum?, long?>(UInt32Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<UInt32Enum?, ulong?>(UInt32Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<UInt32Enum?, short?>(UInt32Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<UInt32Enum?, ushort?>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum?, float?>(UInt32Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt32Enum?, double?>(UInt32Enum.Two));

            Assert.Equal(null, Mapper.Map<UInt32Enum?, sbyte?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, byte?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, char?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, uint?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, long?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, ulong?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, short?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, ushort?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, float?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, double?>((UInt32Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt32Enum?, string>((UInt32Enum?)null));
        }

        [Fact]
        public void TestToUInt32EnumConvert()
        {
            Assert.Equal(UInt32Enum.Two, Mapper.Map<sbyte, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<byte, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<char, UInt32Enum>((char)2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<int, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<uint, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<long, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ulong, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<short, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ushort, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<float, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<double, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<string, UInt32Enum>("Two"));

            Assert.Equal(UInt32Enum.Two, Mapper.Map<sbyte?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<byte?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<char?, UInt32Enum>((char?)2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<int?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<uint?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<long?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ulong?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<short?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ushort?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<float?, UInt32Enum>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<double?, UInt32Enum>(2));
        }

        [Fact]
        public void TestToNullableUInt32EnumConvert()
        {
            Assert.Equal(UInt32Enum.Two, Mapper.Map<sbyte, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<byte, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<char, UInt32Enum?>((char)2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<uint, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<long, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<int, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ulong, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<short, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ushort, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<float, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<double, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<string, UInt32Enum?>("Two"));

            Assert.Equal(UInt32Enum.Two, Mapper.Map<sbyte?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<byte?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<char?, UInt32Enum?>((char?)2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<uint?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<long?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ulong?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<short?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<ushort?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<float?, UInt32Enum?>(2));
            Assert.Equal(UInt32Enum.Two, Mapper.Map<double?, UInt32Enum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, UInt32Enum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, UInt32Enum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, UInt32Enum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, UInt32Enum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, UInt32Enum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, UInt32Enum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, UInt32Enum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, UInt32Enum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, UInt32Enum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, UInt32Enum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, UInt32Enum?>((string)null));
        }

        #endregion

        #region Int64Enum

        [Fact]
        public void TestFromInt64EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<Int64Enum, sbyte>(Int64Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<Int64Enum, byte>(Int64Enum.Two));
            Assert.Equal((char)2, Mapper.Map<Int64Enum, char>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, int>(Int64Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<Int64Enum, uint>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, long>(Int64Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<Int64Enum, ulong>(Int64Enum.Two));
            Assert.Equal((short)2, Mapper.Map<Int64Enum, short>(Int64Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<Int64Enum, ushort>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, float>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, double>(Int64Enum.Two));
            Assert.Equal("Two", Mapper.Map<Int64Enum, string>(Int64Enum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<Int64Enum, sbyte?>(Int64Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<Int64Enum, byte?>(Int64Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<Int64Enum, char?>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, int?>(Int64Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<Int64Enum, uint?>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, long?>(Int64Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<Int64Enum, ulong?>(Int64Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<Int64Enum, short?>(Int64Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<Int64Enum, ushort?>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, float?>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum, double?>(Int64Enum.Two));
        }

        [Fact]
        public void TestFromNullableInt64EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<Int64Enum?, sbyte>(Int64Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<Int64Enum?, byte>(Int64Enum.Two));
            Assert.Equal((char)2, Mapper.Map<Int64Enum?, char>(Int64Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<Int64Enum?, uint>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum?, long>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum?, int>(Int64Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<Int64Enum?, ulong>(Int64Enum.Two));
            Assert.Equal((short)2, Mapper.Map<Int64Enum?, short>(Int64Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<Int64Enum?, ushort>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum?, float>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum?, double>(Int64Enum.Two));
            Assert.Equal("Two", Mapper.Map<Int64Enum?, string>(Int64Enum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<Int64Enum?, sbyte>((Int64Enum?)null));
            Assert.Equal((byte)0, Mapper.Map<Int64Enum?, byte>((Int64Enum?)null));
            Assert.Equal((char)0, Mapper.Map<Int64Enum?, char>((Int64Enum?)null));
            Assert.Equal((uint)0, Mapper.Map<Int64Enum?, uint>((Int64Enum?)null));
            Assert.Equal(0, Mapper.Map<Int64Enum?, long>((Int64Enum?)null));
            Assert.Equal(0, Mapper.Map<Int64Enum?, int>((Int64Enum?)null));
            Assert.Equal((ulong)0, Mapper.Map<Int64Enum?, ulong>((Int64Enum?)null));
            Assert.Equal((short)0, Mapper.Map<Int64Enum?, short>((Int64Enum?)null));
            Assert.Equal((ushort)0, Mapper.Map<Int64Enum?, ushort>((Int64Enum?)null));
            Assert.Equal(0, Mapper.Map<Int64Enum?, float>((Int64Enum?)null));
            Assert.Equal(0, Mapper.Map<Int64Enum?, double>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, string>((Int64Enum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<Int64Enum?, sbyte?>(Int64Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<Int64Enum?, byte?>(Int64Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<Int64Enum?, char?>(Int64Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<Int64Enum?, uint?>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum?, long?>(Int64Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<Int64Enum?, ulong?>(Int64Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<Int64Enum?, short?>(Int64Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<Int64Enum?, ushort?>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum?, float?>(Int64Enum.Two));
            Assert.Equal(2, Mapper.Map<Int64Enum?, double?>(Int64Enum.Two));

            Assert.Equal(null, Mapper.Map<Int64Enum?, sbyte?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, byte?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, char?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, uint?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, long?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, ulong?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, short?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, ushort?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, float?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, double?>((Int64Enum?)null));
            Assert.Equal(null, Mapper.Map<Int64Enum?, string>((Int64Enum?)null));
        }

        [Fact]
        public void TestToInt64EnumConvert()
        {
            Assert.Equal(Int64Enum.Two, Mapper.Map<sbyte, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<byte, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<char, Int64Enum>((char)2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<int, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<uint, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<long, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ulong, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<short, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ushort, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<float, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<double, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<string, Int64Enum>("Two"));

            Assert.Equal(Int64Enum.Two, Mapper.Map<sbyte?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<byte?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<char?, Int64Enum>((char?)2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<int?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<uint?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<long?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ulong?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<short?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ushort?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<float?, Int64Enum>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<double?, Int64Enum>(2));
        }

        [Fact]
        public void TestToNullableInt64EnumConvert()
        {
            Assert.Equal(Int64Enum.Two, Mapper.Map<sbyte, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<byte, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<char, Int64Enum?>((char)2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<uint, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<long, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<int, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ulong, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<short, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ushort, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<float, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<double, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<string, Int64Enum?>("Two"));

            Assert.Equal(Int64Enum.Two, Mapper.Map<sbyte?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<byte?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<char?, Int64Enum?>((char?)2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<uint?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<long?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ulong?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<short?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<ushort?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<float?, Int64Enum?>(2));
            Assert.Equal(Int64Enum.Two, Mapper.Map<double?, Int64Enum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, Int64Enum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, Int64Enum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, Int64Enum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, Int64Enum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, Int64Enum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, Int64Enum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, Int64Enum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, Int64Enum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, Int64Enum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, Int64Enum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, Int64Enum?>((string)null));
        }

        #endregion

        #region UInt64Enum

        [Fact]
        public void TestFromUInt64EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<UInt64Enum, sbyte>(UInt64Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<UInt64Enum, byte>(UInt64Enum.Two));
            Assert.Equal((char)2, Mapper.Map<UInt64Enum, char>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, int>(UInt64Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<UInt64Enum, uint>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, long>(UInt64Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<UInt64Enum, ulong>(UInt64Enum.Two));
            Assert.Equal((short)2, Mapper.Map<UInt64Enum, short>(UInt64Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<UInt64Enum, ushort>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, float>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, double>(UInt64Enum.Two));
            Assert.Equal("Two", Mapper.Map<UInt64Enum, string>(UInt64Enum.Two));

            Assert.Equal((sbyte?)2, Mapper.Map<UInt64Enum, sbyte?>(UInt64Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<UInt64Enum, byte?>(UInt64Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<UInt64Enum, char?>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, int?>(UInt64Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<UInt64Enum, uint?>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, long?>(UInt64Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<UInt64Enum, ulong?>(UInt64Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<UInt64Enum, short?>(UInt64Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<UInt64Enum, ushort?>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, float?>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum, double?>(UInt64Enum.Two));
        }

        [Fact]
        public void TestFromNullableUInt64EnumConvert()
        {
            Assert.Equal((sbyte)2, Mapper.Map<UInt64Enum?, sbyte>(UInt64Enum.Two));
            Assert.Equal((byte)2, Mapper.Map<UInt64Enum?, byte>(UInt64Enum.Two));
            Assert.Equal((char)2, Mapper.Map<UInt64Enum?, char>(UInt64Enum.Two));
            Assert.Equal((uint)2, Mapper.Map<UInt64Enum?, uint>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum?, long>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum?, int>(UInt64Enum.Two));
            Assert.Equal((ulong)2, Mapper.Map<UInt64Enum?, ulong>(UInt64Enum.Two));
            Assert.Equal((short)2, Mapper.Map<UInt64Enum?, short>(UInt64Enum.Two));
            Assert.Equal((ushort)2, Mapper.Map<UInt64Enum?, ushort>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum?, float>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum?, double>(UInt64Enum.Two));
            Assert.Equal("Two", Mapper.Map<UInt64Enum?, string>(UInt64Enum.Two));

            Assert.Equal((sbyte)0, Mapper.Map<UInt64Enum?, sbyte>((UInt64Enum?)null));
            Assert.Equal((byte)0, Mapper.Map<UInt64Enum?, byte>((UInt64Enum?)null));
            Assert.Equal((char)0, Mapper.Map<UInt64Enum?, char>((UInt64Enum?)null));
            Assert.Equal((uint)0, Mapper.Map<UInt64Enum?, uint>((UInt64Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt64Enum?, long>((UInt64Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt64Enum?, int>((UInt64Enum?)null));
            Assert.Equal((ulong)0, Mapper.Map<UInt64Enum?, ulong>((UInt64Enum?)null));
            Assert.Equal((short)0, Mapper.Map<UInt64Enum?, short>((UInt64Enum?)null));
            Assert.Equal((ushort)0, Mapper.Map<UInt64Enum?, ushort>((UInt64Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt64Enum?, float>((UInt64Enum?)null));
            Assert.Equal(0, Mapper.Map<UInt64Enum?, double>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, string>((UInt64Enum?)null));

            Assert.Equal((sbyte?)2, Mapper.Map<UInt64Enum?, sbyte?>(UInt64Enum.Two));
            Assert.Equal((byte?)2, Mapper.Map<UInt64Enum?, byte?>(UInt64Enum.Two));
            Assert.Equal((char?)2, Mapper.Map<UInt64Enum?, char?>(UInt64Enum.Two));
            Assert.Equal((uint?)2, Mapper.Map<UInt64Enum?, uint?>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum?, long?>(UInt64Enum.Two));
            Assert.Equal((ulong?)2, Mapper.Map<UInt64Enum?, ulong?>(UInt64Enum.Two));
            Assert.Equal((short?)2, Mapper.Map<UInt64Enum?, short?>(UInt64Enum.Two));
            Assert.Equal((ushort?)2, Mapper.Map<UInt64Enum?, ushort?>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum?, float?>(UInt64Enum.Two));
            Assert.Equal(2, Mapper.Map<UInt64Enum?, double?>(UInt64Enum.Two));

            Assert.Equal(null, Mapper.Map<UInt64Enum?, sbyte?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, byte?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, char?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, uint?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, long?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, ulong?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, short?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, ushort?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, float?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, double?>((UInt64Enum?)null));
            Assert.Equal(null, Mapper.Map<UInt64Enum?, string>((UInt64Enum?)null));
        }

        [Fact]
        public void TestToUInt64EnumConvert()
        {
            Assert.Equal(UInt64Enum.Two, Mapper.Map<sbyte, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<byte, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<char, UInt64Enum>((char)2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<int, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<uint, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<long, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ulong, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<short, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ushort, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<float, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<double, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<string, UInt64Enum>("Two"));

            Assert.Equal(UInt64Enum.Two, Mapper.Map<sbyte?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<byte?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<char?, UInt64Enum>((char?)2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<int?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<uint?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<long?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ulong?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<short?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ushort?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<float?, UInt64Enum>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<double?, UInt64Enum>(2));
        }

        [Fact]
        public void TestToNullableUInt64EnumConvert()
        {
            Assert.Equal(UInt64Enum.Two, Mapper.Map<sbyte, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<byte, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<char, UInt64Enum?>((char)2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<uint, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<long, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<int, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ulong, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<short, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ushort, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<float, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<double, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<string, UInt64Enum?>("Two"));

            Assert.Equal(UInt64Enum.Two, Mapper.Map<sbyte?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<byte?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<char?, UInt64Enum?>((char?)2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<uint?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<long?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ulong?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<short?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<ushort?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<float?, UInt64Enum?>(2));
            Assert.Equal(UInt64Enum.Two, Mapper.Map<double?, UInt64Enum?>(2));

            Assert.Equal(null, Mapper.Map<sbyte?, UInt64Enum?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<byte?, UInt64Enum?>((byte?)null));
            Assert.Equal(null, Mapper.Map<char?, UInt64Enum?>((char?)null));
            Assert.Equal(null, Mapper.Map<uint?, UInt64Enum?>((uint?)null));
            Assert.Equal(null, Mapper.Map<long?, UInt64Enum?>((long?)null));
            Assert.Equal(null, Mapper.Map<ulong?, UInt64Enum?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<short?, UInt64Enum?>((short?)null));
            Assert.Equal(null, Mapper.Map<ushort?, UInt64Enum?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<float?, UInt64Enum?>((float?)null));
            Assert.Equal(null, Mapper.Map<double?, UInt64Enum?>((double?)null));
            Assert.Equal(null, Mapper.Map<string, UInt64Enum?>((string)null));
        }

        #endregion

        #endregion

        #region Numbers

        #region Byte

        [Fact]
        public void TestByteConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<byte, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<byte, byte>(20));
            Assert.Equal((char)20, Mapper.Map<byte, char>(20));
            Assert.Equal(20, Mapper.Map<byte, int>(20));
            Assert.Equal((uint)20, Mapper.Map<byte, uint>(20));
            Assert.Equal(20, Mapper.Map<byte, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<byte, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<byte, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<byte, ushort>(20));
            Assert.Equal(20, Mapper.Map<byte, decimal>(20));
            Assert.Equal(20, Mapper.Map<byte, float>(20));
            Assert.Equal(20, Mapper.Map<byte, double>(20));
            Assert.Equal(true, Mapper.Map<byte, bool>(20));
            Assert.Equal(false, Mapper.Map<byte, bool>(0));
            Assert.Equal("20", Mapper.Map<byte, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<byte, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<byte, TimeSpan>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<byte, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<byte, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<byte, char?>(20));
            Assert.Equal(20, Mapper.Map<byte, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<byte, uint?>(20));
            Assert.Equal(20, Mapper.Map<byte, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<byte, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<byte, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<byte, ushort?>(20));
            Assert.Equal(20, Mapper.Map<byte, decimal?>(20));
            Assert.Equal(20, Mapper.Map<byte, float?>(20));
            Assert.Equal(20, Mapper.Map<byte, double?>(20));
            Assert.Equal(true, Mapper.Map<byte, bool?>(20));
            Assert.Equal(false, Mapper.Map<byte, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<byte, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<byte, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableByteConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<byte?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<byte?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<byte?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<byte?, uint>(20));
            Assert.Equal(20, Mapper.Map<byte?, long>(20));
            Assert.Equal(20, Mapper.Map<byte?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<byte?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<byte?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<byte?, ushort>(20));
            Assert.Equal(20, Mapper.Map<byte?, decimal>(20));
            Assert.Equal(20, Mapper.Map<byte?, float>(20));
            Assert.Equal(20, Mapper.Map<byte?, double>(20));
            Assert.Equal(true, Mapper.Map<byte?, bool>(20));
            Assert.Equal(false, Mapper.Map<byte?, bool>(0));
            Assert.Equal("20", Mapper.Map<byte?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<byte?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<byte?, TimeSpan>(20));

            Assert.Equal((sbyte)0, Mapper.Map<byte?, sbyte>((byte?)null));
            Assert.Equal((byte)0, Mapper.Map<byte?, byte>((byte?)null));
            Assert.Equal((char)0, Mapper.Map<byte?, char>((byte?)null));
            Assert.Equal((uint)0, Mapper.Map<byte?, uint>((byte?)null));
            Assert.Equal(0, Mapper.Map<byte?, long>((byte?)null));
            Assert.Equal(0, Mapper.Map<byte?, int>((byte?)null));
            Assert.Equal((ulong)0, Mapper.Map<byte?, ulong>((byte?)null));
            Assert.Equal((short)0, Mapper.Map<byte?, short>((byte?)null));
            Assert.Equal((ushort)0, Mapper.Map<byte?, ushort>((byte?)null));
            Assert.Equal(0, Mapper.Map<byte?, decimal>((byte?)null));
            Assert.Equal(0, Mapper.Map<byte?, float>((byte?)null));
            Assert.Equal(0, Mapper.Map<byte?, double>((byte?)null));
            Assert.Equal(false, Mapper.Map<byte?, bool>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, string>((byte?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<byte?, DateTime>((byte?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<byte?, TimeSpan>((byte?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<byte?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<byte?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<byte?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<byte?, uint?>(20));
            Assert.Equal(20, Mapper.Map<byte?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<byte?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<byte?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<byte?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<byte?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<byte?, float?>(20));
            Assert.Equal(20, Mapper.Map<byte?, double?>(20));
            Assert.Equal(true, Mapper.Map<byte?, bool?>(20));
            Assert.Equal(false, Mapper.Map<byte?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<byte?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<byte?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<byte?, sbyte?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, byte?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, char?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, uint?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, long?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, ulong?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, short?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, ushort?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, decimal?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, float?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, double?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, bool?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, DateTime?>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, string>((byte?)null));
            Assert.Equal(null, Mapper.Map<byte?, TimeSpan?>((byte?)null));
        }

        #endregion

        #region SByte

        [Fact]
        public void TestSByteConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<sbyte, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<sbyte, byte>(20));
            Assert.Equal((char)20, Mapper.Map<sbyte, char>(20));
            Assert.Equal(20, Mapper.Map<sbyte, int>(20));
            Assert.Equal((uint)20, Mapper.Map<sbyte, uint>(20));
            Assert.Equal(20, Mapper.Map<sbyte, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<sbyte, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<sbyte, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<sbyte, ushort>(20));
            Assert.Equal(20, Mapper.Map<sbyte, decimal>(20));
            Assert.Equal(20, Mapper.Map<sbyte, float>(20));
            Assert.Equal(20, Mapper.Map<sbyte, double>(20));
            Assert.Equal(true, Mapper.Map<sbyte, bool>(20));
            Assert.Equal(false, Mapper.Map<sbyte, bool>(0));
            Assert.Equal("20", Mapper.Map<sbyte, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<sbyte, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<sbyte, TimeSpan>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<sbyte, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<sbyte, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<sbyte, char?>(20));
            Assert.Equal(20, Mapper.Map<sbyte, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<sbyte, uint?>(20));
            Assert.Equal(20, Mapper.Map<sbyte, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<sbyte, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<sbyte, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<sbyte, ushort?>(20));
            Assert.Equal(20, Mapper.Map<sbyte, decimal?>(20));
            Assert.Equal(20, Mapper.Map<sbyte, float?>(20));
            Assert.Equal(20, Mapper.Map<sbyte, double?>(20));
            Assert.Equal(true, Mapper.Map<sbyte, bool?>(20));
            Assert.Equal(false, Mapper.Map<sbyte, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<sbyte, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<sbyte, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableSByteConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<sbyte?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<sbyte?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<sbyte?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<sbyte?, uint>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, long>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<sbyte?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<sbyte?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<sbyte?, ushort>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, decimal>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, float>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, double>(20));
            Assert.Equal(true, Mapper.Map<sbyte?, bool>(20));
            Assert.Equal(false, Mapper.Map<sbyte?, bool>(0));
            Assert.Equal("20", Mapper.Map<sbyte?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<sbyte?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<sbyte?, TimeSpan>(20));

            Assert.Equal((sbyte)0, Mapper.Map<sbyte?, sbyte>((sbyte?)null));
            Assert.Equal((byte)0, Mapper.Map<sbyte?, byte>((sbyte?)null));
            Assert.Equal((char)0, Mapper.Map<sbyte?, char>((sbyte?)null));
            Assert.Equal((uint)0, Mapper.Map<sbyte?, uint>((sbyte?)null));
            Assert.Equal(0, Mapper.Map<sbyte?, long>((sbyte?)null));
            Assert.Equal(0, Mapper.Map<sbyte?, int>((sbyte?)null));
            Assert.Equal((ulong)0, Mapper.Map<sbyte?, ulong>((sbyte?)null));
            Assert.Equal((short)0, Mapper.Map<sbyte?, short>((sbyte?)null));
            Assert.Equal((ushort)0, Mapper.Map<sbyte?, ushort>((sbyte?)null));
            Assert.Equal(0, Mapper.Map<sbyte?, decimal>((sbyte?)null));
            Assert.Equal(0, Mapper.Map<sbyte?, float>((sbyte?)null));
            Assert.Equal(0, Mapper.Map<sbyte?, double>((sbyte?)null));
            Assert.Equal(false, Mapper.Map<sbyte?, bool>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, string>((sbyte?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<sbyte?, DateTime>((sbyte?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<sbyte?, TimeSpan>((sbyte?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<sbyte?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<sbyte?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<sbyte?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<sbyte?, uint?>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<sbyte?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<sbyte?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<sbyte?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, float?>(20));
            Assert.Equal(20, Mapper.Map<sbyte?, double?>(20));
            Assert.Equal(true, Mapper.Map<sbyte?, bool?>(20));
            Assert.Equal(false, Mapper.Map<sbyte?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<sbyte?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<sbyte?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<sbyte?, sbyte?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, byte?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, char?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, uint?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, long?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, ulong?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, short?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, ushort?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, decimal?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, float?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, double?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, bool?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, DateTime?>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, string>((sbyte?)null));
            Assert.Equal(null, Mapper.Map<sbyte?, TimeSpan?>((sbyte?)null));
        }

        #endregion

        #region Char

        [Fact]
        public void TestCharConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<char, sbyte>((char)20));
            Assert.Equal((byte)20, Mapper.Map<char, byte>((char)20));
            Assert.Equal((char)20, Mapper.Map<char, char>((char)20));
            Assert.Equal(20, Mapper.Map<char, int>((char)20));
            Assert.Equal((uint)20, Mapper.Map<char, uint>((char)20));
            Assert.Equal(20, Mapper.Map<char, long>((char)20));
            Assert.Equal((ulong)20, Mapper.Map<char, ulong>((char)20));
            Assert.Equal((short)20, Mapper.Map<char, short>((char)20));
            Assert.Equal((ushort)20, Mapper.Map<char, ushort>((char)20));
            Assert.Equal(20, Mapper.Map<char, decimal>((char)20));
            Assert.Equal(20, Mapper.Map<char, float>((char)20));
            Assert.Equal(20, Mapper.Map<char, double>((char)20));
            Assert.Equal(true, Mapper.Map<char, bool>((char)20));
            Assert.Equal(false, Mapper.Map<char, bool>((char)0));
            Assert.Equal(((char)20).ToString(), Mapper.Map<char, string>((char)20));
            Assert.Equal(new DateTime(20), Mapper.Map<char, DateTime>((char)20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<char, TimeSpan>((char)20));
            Assert.Equal(BitConverter.GetBytes((char)20), Mapper.Map<char, byte[]>((char)20));

            Assert.Equal((sbyte?)20, Mapper.Map<char, sbyte?>((char)20));
            Assert.Equal((byte?)20, Mapper.Map<char, byte?>((char)20));
            Assert.Equal((char?)20, Mapper.Map<char, char?>((char)20));
            Assert.Equal(20, Mapper.Map<char, int?>((char)20));
            Assert.Equal((uint?)20, Mapper.Map<char, uint?>((char)20));
            Assert.Equal(20, Mapper.Map<char, long?>((char)20));
            Assert.Equal((ulong?)20, Mapper.Map<char, ulong?>((char)20));
            Assert.Equal((short?)20, Mapper.Map<char, short?>((char)20));
            Assert.Equal((ushort?)20, Mapper.Map<char, ushort?>((char)20));
            Assert.Equal(20, Mapper.Map<char, decimal?>((char)20));
            Assert.Equal(20, Mapper.Map<char, float?>((char)20));
            Assert.Equal(20, Mapper.Map<char, double?>((char)20));
            Assert.Equal(true, Mapper.Map<char, bool?>((char)20));
            Assert.Equal(false, Mapper.Map<char, bool?>((char)0));
            Assert.Equal(new DateTime(20), Mapper.Map<char, DateTime?>((char)20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<char, TimeSpan?>((char)20));
        }

        [Fact]
        public void TestNullableCharConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<char?, sbyte>((char)20));
            Assert.Equal((byte)20, Mapper.Map<char?, byte>((char)20));
            Assert.Equal((char)20, Mapper.Map<char?, char>((char)20));
            Assert.Equal((uint)20, Mapper.Map<char?, uint>((char)20));
            Assert.Equal(20, Mapper.Map<char?, long>((char)20));
            Assert.Equal(20, Mapper.Map<char?, int>((char)20));
            Assert.Equal((ulong)20, Mapper.Map<char?, ulong>((char)20));
            Assert.Equal((short)20, Mapper.Map<char?, short>((char)20));
            Assert.Equal((ushort)20, Mapper.Map<char?, ushort>((char)20));
            Assert.Equal(20, Mapper.Map<char?, decimal>((char)20));
            Assert.Equal(20, Mapper.Map<char?, float>((char)20));
            Assert.Equal(20, Mapper.Map<char?, double>((char)20));
            Assert.Equal(true, Mapper.Map<char?, bool>((char)20));
            Assert.Equal(false, Mapper.Map<char?, bool>((char)0));
            Assert.Equal(((char)20).ToString(), Mapper.Map<char?, string>((char)20));
            Assert.Equal(new DateTime(20), Mapper.Map<char?, DateTime>((char)20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<char?, TimeSpan>((char)20));
            Assert.Equal(BitConverter.GetBytes((char)20), Mapper.Map<char?, byte[]>((char)20));

            Assert.Equal((sbyte)0, Mapper.Map<char?, sbyte>((char?)null));
            Assert.Equal((byte)0, Mapper.Map<char?, byte>((char?)null));
            Assert.Equal((char)0, Mapper.Map<char?, char>((char?)null));
            Assert.Equal((uint)0, Mapper.Map<char?, uint>((char?)null));
            Assert.Equal(0, Mapper.Map<char?, long>((char?)null));
            Assert.Equal(0, Mapper.Map<char?, int>((char?)null));
            Assert.Equal((ulong)0, Mapper.Map<char?, ulong>((char?)null));
            Assert.Equal((short)0, Mapper.Map<char?, short>((char?)null));
            Assert.Equal((ushort)0, Mapper.Map<char?, ushort>((char?)null));
            Assert.Equal(0, Mapper.Map<char?, decimal>((char?)null));
            Assert.Equal(0, Mapper.Map<char?, float>((char?)null));
            Assert.Equal(0, Mapper.Map<char?, double>((char?)null));
            Assert.Equal(false, Mapper.Map<char?, bool>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, string>((char?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<char?, DateTime>((char?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<char?, TimeSpan>((char?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<char?, sbyte?>((char?)20));
            Assert.Equal((byte?)20, Mapper.Map<char?, byte?>((char?)20));
            Assert.Equal((char?)20, Mapper.Map<char?, char?>((char?)20));
            Assert.Equal((uint?)20, Mapper.Map<char?, uint?>((char?)20));
            Assert.Equal(20, Mapper.Map<char?, long?>((char?)20));
            Assert.Equal((ulong?)20, Mapper.Map<char?, ulong?>((char?)20));
            Assert.Equal((short?)20, Mapper.Map<char?, short?>((char?)20));
            Assert.Equal((ushort?)20, Mapper.Map<char?, ushort?>((char?)20));
            Assert.Equal(20, Mapper.Map<char?, decimal?>((char?)20));
            Assert.Equal(20, Mapper.Map<char?, float?>((char?)20));
            Assert.Equal(20, Mapper.Map<char?, double?>((char?)20));
            Assert.Equal(true, Mapper.Map<char?, bool?>((char?)20));
            Assert.Equal(false, Mapper.Map<char?, bool?>((char?)0));
            Assert.Equal(new DateTime(20), Mapper.Map<char?, DateTime?>((char?)20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<char?, TimeSpan?>((char?)20));

            Assert.Equal(null, Mapper.Map<char?, sbyte?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, byte?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, char?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, uint?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, long?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, ulong?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, short?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, ushort?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, decimal?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, float?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, double?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, bool?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, DateTime?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, string>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, TimeSpan?>((char?)null));
            Assert.Equal(null, Mapper.Map<char?, byte[]>((char?)null));
        }

        #endregion

        #region Int32

        [Fact]
        public void TestInt32Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<int, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<int, byte>(20));
            Assert.Equal((char)20, Mapper.Map<int, char>(20));
            Assert.Equal(20, Mapper.Map<int, int>(20));
            Assert.Equal((uint)20, Mapper.Map<int, uint>(20));
            Assert.Equal(20, Mapper.Map<int, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<int, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<int, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<int, ushort>(20));
            Assert.Equal(20, Mapper.Map<int, decimal>(20));
            Assert.Equal(20, Mapper.Map<int, float>(20));
            Assert.Equal(20, Mapper.Map<int, double>(20));
            Assert.Equal(true, Mapper.Map<int, bool>(20));
            Assert.Equal(false, Mapper.Map<int, bool>(0));
            Assert.Equal("20", Mapper.Map<int, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<int, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<int, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes(20), Mapper.Map<int, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<int, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<int, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<int, char?>(20));
            Assert.Equal(20, Mapper.Map<int, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<int, uint?>(20));
            Assert.Equal(20, Mapper.Map<int, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<int, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<int, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<int, ushort?>(20));
            Assert.Equal(20, Mapper.Map<int, decimal?>(20));
            Assert.Equal(20, Mapper.Map<int, float?>(20));
            Assert.Equal(20, Mapper.Map<int, double?>(20));
            Assert.Equal(true, Mapper.Map<int, bool?>(20));
            Assert.Equal(false, Mapper.Map<int, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<int, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<int, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableInt32Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<int?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<int?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<int?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<int?, uint>(20));
            Assert.Equal(20, Mapper.Map<int?, long>(20));
            Assert.Equal(20, Mapper.Map<int?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<int?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<int?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<int?, ushort>(20));
            Assert.Equal(20, Mapper.Map<int?, decimal>(20));
            Assert.Equal(20, Mapper.Map<int?, float>(20));
            Assert.Equal(20, Mapper.Map<int?, double>(20));
            Assert.Equal(true, Mapper.Map<int?, bool>(20));
            Assert.Equal(false, Mapper.Map<int?, bool>(0));
            Assert.Equal("20", Mapper.Map<int?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<int?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<int?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes(20), Mapper.Map<int?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<int?, sbyte>((int?)null));
            Assert.Equal((byte)0, Mapper.Map<int?, byte>((int?)null));
            Assert.Equal((char)0, Mapper.Map<int?, char>((int?)null));
            Assert.Equal((uint)0, Mapper.Map<int?, uint>((int?)null));
            Assert.Equal(0, Mapper.Map<int?, long>((int?)null));
            Assert.Equal(0, Mapper.Map<int?, int>((int?)null));
            Assert.Equal((ulong)0, Mapper.Map<int?, ulong>((int?)null));
            Assert.Equal((short)0, Mapper.Map<int?, short>((int?)null));
            Assert.Equal((ushort)0, Mapper.Map<int?, ushort>((int?)null));
            Assert.Equal(0, Mapper.Map<int?, decimal>((int?)null));
            Assert.Equal(0, Mapper.Map<int?, float>((int?)null));
            Assert.Equal(0, Mapper.Map<int?, double>((int?)null));
            Assert.Equal(false, Mapper.Map<int?, bool>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, string>((int?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<int?, DateTime>((int?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<int?, TimeSpan>((int?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<int?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<int?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<int?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<int?, uint?>(20));
            Assert.Equal(20, Mapper.Map<int?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<int?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<int?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<int?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<int?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<int?, float?>(20));
            Assert.Equal(20, Mapper.Map<int?, double?>(20));
            Assert.Equal(true, Mapper.Map<int?, bool?>(20));
            Assert.Equal(false, Mapper.Map<int?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<int?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<int?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<int?, sbyte?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, byte?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, char?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, uint?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, long?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, ulong?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, short?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, ushort?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, decimal?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, float?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, double?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, bool?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, DateTime?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, string>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, TimeSpan?>((int?)null));
            Assert.Equal(null, Mapper.Map<int?, byte[]>((int?)null));
        }

        #endregion

        #region UInt32

        [Fact]
        public void TestUInt32Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<uint, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<uint, byte>(20));
            Assert.Equal((char)20, Mapper.Map<uint, char>(20));
            Assert.Equal(20, Mapper.Map<uint, int>(20));
            Assert.Equal((uint)20, Mapper.Map<uint, uint>(20));
            Assert.Equal(20, Mapper.Map<uint, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<uint, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<uint, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<uint, ushort>(20));
            Assert.Equal(20, Mapper.Map<uint, decimal>(20));
            Assert.Equal(20, Mapper.Map<uint, float>(20));
            Assert.Equal(20, Mapper.Map<uint, double>(20));
            Assert.Equal(true, Mapper.Map<uint, bool>(20));
            Assert.Equal(false, Mapper.Map<uint, bool>(0));
            Assert.Equal("20", Mapper.Map<uint, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<uint, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<uint, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((uint)20), Mapper.Map<uint, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<uint, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<uint, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<uint, char?>(20));
            Assert.Equal(20, Mapper.Map<uint, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<uint, uint?>(20));
            Assert.Equal(20, Mapper.Map<uint, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<uint, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<uint, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<uint, ushort?>(20));
            Assert.Equal(20, Mapper.Map<uint, decimal?>(20));
            Assert.Equal(20, Mapper.Map<uint, float?>(20));
            Assert.Equal(20, Mapper.Map<uint, double?>(20));
            Assert.Equal(true, Mapper.Map<uint, bool?>(20));
            Assert.Equal(false, Mapper.Map<uint, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<uint, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<uint, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableUInt32Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<uint?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<uint?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<uint?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<uint?, uint>(20));
            Assert.Equal(20, Mapper.Map<uint?, long>(20));
            Assert.Equal(20, Mapper.Map<uint?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<uint?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<uint?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<uint?, ushort>(20));
            Assert.Equal(20, Mapper.Map<uint?, decimal>(20));
            Assert.Equal(20, Mapper.Map<uint?, float>(20));
            Assert.Equal(20, Mapper.Map<uint?, double>(20));
            Assert.Equal(true, Mapper.Map<uint?, bool>(20));
            Assert.Equal(false, Mapper.Map<uint?, bool>(0));
            Assert.Equal("20", Mapper.Map<uint?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<uint?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<uint?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((uint)20), Mapper.Map<uint?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<uint?, sbyte>((uint?)null));
            Assert.Equal((byte)0, Mapper.Map<uint?, byte>((uint?)null));
            Assert.Equal((char)0, Mapper.Map<uint?, char>((uint?)null));
            Assert.Equal((uint)0, Mapper.Map<uint?, uint>((uint?)null));
            Assert.Equal(0, Mapper.Map<uint?, long>((uint?)null));
            Assert.Equal(0, Mapper.Map<uint?, int>((uint?)null));
            Assert.Equal((ulong)0, Mapper.Map<uint?, ulong>((uint?)null));
            Assert.Equal((short)0, Mapper.Map<uint?, short>((uint?)null));
            Assert.Equal((ushort)0, Mapper.Map<uint?, ushort>((uint?)null));
            Assert.Equal(0, Mapper.Map<uint?, decimal>((uint?)null));
            Assert.Equal(0, Mapper.Map<uint?, float>((uint?)null));
            Assert.Equal(0, Mapper.Map<uint?, double>((uint?)null));
            Assert.Equal(false, Mapper.Map<uint?, bool>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, string>((uint?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<uint?, DateTime>((uint?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<uint?, TimeSpan>((uint?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<uint?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<uint?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<uint?, char?>(20));
            Assert.Equal(20, Mapper.Map<uint?, int?>(20));
            Assert.Equal(20, Mapper.Map<uint?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<uint?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<uint?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<uint?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<uint?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<uint?, float?>(20));
            Assert.Equal(20, Mapper.Map<uint?, double?>(20));
            Assert.Equal(true, Mapper.Map<uint?, bool?>(20));
            Assert.Equal(false, Mapper.Map<uint?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<uint?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<uint?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<uint?, sbyte?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, byte?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, char?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, int?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, long?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, ulong?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, short?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, ushort?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, decimal?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, float?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, double?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, bool?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, DateTime?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, string>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, TimeSpan?>((uint?)null));
            Assert.Equal(null, Mapper.Map<uint?, byte[]>((uint?)null));
        }

        #endregion

        #region Int16

        [Fact]
        public void TestInt16Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<short, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<short, byte>(20));
            Assert.Equal((char)20, Mapper.Map<short, char>(20));
            Assert.Equal(20, Mapper.Map<short, int>(20));
            Assert.Equal((uint)20, Mapper.Map<short, uint>(20));
            Assert.Equal(20, Mapper.Map<short, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<short, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<short, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<short, ushort>(20));
            Assert.Equal(20, Mapper.Map<short, decimal>(20));
            Assert.Equal(20, Mapper.Map<short, float>(20));
            Assert.Equal(20, Mapper.Map<short, double>(20));
            Assert.Equal(true, Mapper.Map<short, bool>(20));
            Assert.Equal(false, Mapper.Map<short, bool>(0));
            Assert.Equal("20", Mapper.Map<short, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<short, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<short, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((short)20), Mapper.Map<short, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<short, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<short, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<short, char?>(20));
            Assert.Equal(20, Mapper.Map<short, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<short, uint?>(20));
            Assert.Equal(20, Mapper.Map<short, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<short, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<short, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<short, ushort?>(20));
            Assert.Equal(20, Mapper.Map<short, decimal?>(20));
            Assert.Equal(20, Mapper.Map<short, float?>(20));
            Assert.Equal(20, Mapper.Map<short, double?>(20));
            Assert.Equal(true, Mapper.Map<short, bool?>(20));
            Assert.Equal(false, Mapper.Map<short, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<short, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<short, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableInt16Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<short?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<short?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<short?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<short?, uint>(20));
            Assert.Equal(20, Mapper.Map<short?, long>(20));
            Assert.Equal(20, Mapper.Map<short?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<short?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<short?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<short?, ushort>(20));
            Assert.Equal(20, Mapper.Map<short?, decimal>(20));
            Assert.Equal(20, Mapper.Map<short?, float>(20));
            Assert.Equal(20, Mapper.Map<short?, double>(20));
            Assert.Equal(true, Mapper.Map<short?, bool>(20));
            Assert.Equal(false, Mapper.Map<short?, bool>(0));
            Assert.Equal("20", Mapper.Map<short?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<short?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<short?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((short)20), Mapper.Map<short?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<short?, sbyte>((short?)null));
            Assert.Equal((byte)0, Mapper.Map<short?, byte>((short?)null));
            Assert.Equal((char)0, Mapper.Map<short?, char>((short?)null));
            Assert.Equal((uint)0, Mapper.Map<short?, uint>((short?)null));
            Assert.Equal(0, Mapper.Map<short?, long>((short?)null));
            Assert.Equal(0, Mapper.Map<short?, int>((short?)null));
            Assert.Equal((ulong)0, Mapper.Map<short?, ulong>((short?)null));
            Assert.Equal((short)0, Mapper.Map<short?, short>((short?)null));
            Assert.Equal((ushort)0, Mapper.Map<short?, ushort>((short?)null));
            Assert.Equal(0, Mapper.Map<short?, decimal>((short?)null));
            Assert.Equal(0, Mapper.Map<short?, float>((short?)null));
            Assert.Equal(0, Mapper.Map<short?, double>((short?)null));
            Assert.Equal(false, Mapper.Map<short?, bool>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, string>((short?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<short?, DateTime>((short?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<short?, TimeSpan>((short?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<short?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<short?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<short?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<short?, uint?>(20));
            Assert.Equal(20, Mapper.Map<short?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<short?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<short?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<short?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<short?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<short?, float?>(20));
            Assert.Equal(20, Mapper.Map<short?, double?>(20));
            Assert.Equal(true, Mapper.Map<short?, bool?>(20));
            Assert.Equal(false, Mapper.Map<short?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<short?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<short?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<short?, sbyte?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, byte?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, char?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, uint?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, long?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, ulong?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, short?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, ushort?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, decimal?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, float?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, double?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, bool?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, DateTime?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, string>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, TimeSpan?>((short?)null));
            Assert.Equal(null, Mapper.Map<short?, byte[]>((short?)null));
        }

        #endregion

        #region UInt16

        [Fact]
        public void TestUInt16Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<ushort, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<ushort, byte>(20));
            Assert.Equal((char)20, Mapper.Map<ushort, char>(20));
            Assert.Equal(20, Mapper.Map<ushort, int>(20));
            Assert.Equal((uint)20, Mapper.Map<ushort, uint>(20));
            Assert.Equal(20, Mapper.Map<ushort, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<ushort, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<ushort, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<ushort, ushort>(20));
            Assert.Equal(20, Mapper.Map<ushort, decimal>(20));
            Assert.Equal(20, Mapper.Map<ushort, float>(20));
            Assert.Equal(20, Mapper.Map<ushort, double>(20));
            Assert.Equal(true, Mapper.Map<ushort, bool>(20));
            Assert.Equal(false, Mapper.Map<ushort, bool>(0));
            Assert.Equal("20", Mapper.Map<ushort, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<ushort, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ushort, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((ushort)20), Mapper.Map<ushort, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<ushort, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<ushort, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<ushort, char?>(20));
            Assert.Equal(20, Mapper.Map<ushort, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<ushort, uint?>(20));
            Assert.Equal(20, Mapper.Map<ushort, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<ushort, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<ushort, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<ushort, ushort?>(20));
            Assert.Equal(20, Mapper.Map<ushort, decimal?>(20));
            Assert.Equal(20, Mapper.Map<ushort, float?>(20));
            Assert.Equal(20, Mapper.Map<ushort, double?>(20));
            Assert.Equal(true, Mapper.Map<ushort, bool?>(20));
            Assert.Equal(false, Mapper.Map<ushort, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<ushort, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ushort, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableUInt16Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<ushort?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<ushort?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<ushort?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<ushort?, uint>(20));
            Assert.Equal(20, Mapper.Map<ushort?, long>(20));
            Assert.Equal(20, Mapper.Map<ushort?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<ushort?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<ushort?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<ushort?, ushort>(20));
            Assert.Equal(20, Mapper.Map<ushort?, decimal>(20));
            Assert.Equal(20, Mapper.Map<ushort?, float>(20));
            Assert.Equal(20, Mapper.Map<ushort?, double>(20));
            Assert.Equal(true, Mapper.Map<ushort?, bool>(20));
            Assert.Equal(false, Mapper.Map<ushort?, bool>(0));
            Assert.Equal("20", Mapper.Map<ushort?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<ushort?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ushort?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((ushort)20), Mapper.Map<ushort?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<ushort?, sbyte>((ushort?)null));
            Assert.Equal((byte)0, Mapper.Map<ushort?, byte>((ushort?)null));
            Assert.Equal((char)0, Mapper.Map<ushort?, char>((ushort?)null));
            Assert.Equal((uint)0, Mapper.Map<ushort?, uint>((ushort?)null));
            Assert.Equal(0, Mapper.Map<ushort?, long>((ushort?)null));
            Assert.Equal(0, Mapper.Map<ushort?, int>((ushort?)null));
            Assert.Equal((ulong)0, Mapper.Map<ushort?, ulong>((ushort?)null));
            Assert.Equal((short)0, Mapper.Map<ushort?, short>((ushort?)null));
            Assert.Equal((ushort)0, Mapper.Map<ushort?, ushort>((ushort?)null));
            Assert.Equal(0, Mapper.Map<ushort?, decimal>((ushort?)null));
            Assert.Equal(0, Mapper.Map<ushort?, float>((ushort?)null));
            Assert.Equal(0, Mapper.Map<ushort?, double>((ushort?)null));
            Assert.Equal(false, Mapper.Map<ushort?, bool>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, string>((ushort?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<ushort?, DateTime>((ushort?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<ushort?, TimeSpan>((ushort?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<ushort?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<ushort?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<ushort?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<ushort?, uint?>(20));
            Assert.Equal(20, Mapper.Map<ushort?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<ushort?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<ushort?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<ushort?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<ushort?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<ushort?, float?>(20));
            Assert.Equal(20, Mapper.Map<ushort?, double?>(20));
            Assert.Equal(true, Mapper.Map<ushort?, bool?>(20));
            Assert.Equal(false, Mapper.Map<ushort?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<ushort?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ushort?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<ushort?, sbyte?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, byte?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, char?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, uint?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, long?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, ulong?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, short?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, ushort?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, decimal?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, float?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, double?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, bool?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, DateTime?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, string>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, TimeSpan?>((ushort?)null));
            Assert.Equal(null, Mapper.Map<ushort?, byte[]>((ushort?)null));
        }

        #endregion

        #region Int64

        [Fact]
        public void TestInt64Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<long, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<long, byte>(20));
            Assert.Equal((char)20, Mapper.Map<long, char>(20));
            Assert.Equal(20, Mapper.Map<long, int>(20));
            Assert.Equal((uint)20, Mapper.Map<long, uint>(20));
            Assert.Equal(20, Mapper.Map<long, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<long, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<long, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<long, ushort>(20));
            Assert.Equal(20, Mapper.Map<long, decimal>(20));
            Assert.Equal(20, Mapper.Map<long, float>(20));
            Assert.Equal(20, Mapper.Map<long, double>(20));
            Assert.Equal(true, Mapper.Map<long, bool>(20));
            Assert.Equal(false, Mapper.Map<long, bool>(0));
            Assert.Equal("20", Mapper.Map<long, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<long, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<long, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((long)20), Mapper.Map<long, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<long, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<long, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<long, char?>(20));
            Assert.Equal(20, Mapper.Map<long, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<long, uint?>(20));
            Assert.Equal(20, Mapper.Map<long, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<long, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<long, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<long, ushort?>(20));
            Assert.Equal(20, Mapper.Map<long, decimal?>(20));
            Assert.Equal(20, Mapper.Map<long, float?>(20));
            Assert.Equal(20, Mapper.Map<long, double?>(20));
            Assert.Equal(true, Mapper.Map<long, bool?>(20));
            Assert.Equal(false, Mapper.Map<long, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<long, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<long, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableInt64Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<long?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<long?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<long?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<long?, uint>(20));
            Assert.Equal(20, Mapper.Map<long?, long>(20));
            Assert.Equal(20, Mapper.Map<long?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<long?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<long?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<long?, ushort>(20));
            Assert.Equal(20, Mapper.Map<long?, decimal>(20));
            Assert.Equal(20, Mapper.Map<long?, float>(20));
            Assert.Equal(20, Mapper.Map<long?, double>(20));
            Assert.Equal(true, Mapper.Map<long?, bool>(20));
            Assert.Equal(false, Mapper.Map<long?, bool>(0));
            Assert.Equal("20", Mapper.Map<long?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<long?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<long?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((long)20), Mapper.Map<long?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<long?, sbyte>((short?)null));
            Assert.Equal((byte)0, Mapper.Map<long?, byte>((short?)null));
            Assert.Equal((char)0, Mapper.Map<long?, char>((short?)null));
            Assert.Equal((uint)0, Mapper.Map<long?, uint>((short?)null));
            Assert.Equal(0, Mapper.Map<long?, long>((short?)null));
            Assert.Equal(0, Mapper.Map<long?, int>((short?)null));
            Assert.Equal((ulong)0, Mapper.Map<long?, ulong>((short?)null));
            Assert.Equal((short)0, Mapper.Map<long?, short>((short?)null));
            Assert.Equal((ushort)0, Mapper.Map<long?, ushort>((short?)null));
            Assert.Equal(0, Mapper.Map<long?, decimal>((short?)null));
            Assert.Equal(0, Mapper.Map<long?, float>((short?)null));
            Assert.Equal(0, Mapper.Map<long?, double>((short?)null));
            Assert.Equal(false, Mapper.Map<long?, bool>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, string>((short?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<long?, DateTime>((short?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<long?, TimeSpan>((short?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<long?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<long?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<long?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<long?, uint?>(20));
            Assert.Equal(20, Mapper.Map<long?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<long?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<long?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<long?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<long?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<long?, float?>(20));
            Assert.Equal(20, Mapper.Map<long?, double?>(20));
            Assert.Equal(true, Mapper.Map<long?, bool?>(20));
            Assert.Equal(false, Mapper.Map<long?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<long?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<long?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<long?, sbyte?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, byte?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, char?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, uint?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, long?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, ulong?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, short?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, ushort?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, decimal?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, float?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, double?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, bool?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, DateTime?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, string>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, TimeSpan?>((short?)null));
            Assert.Equal(null, Mapper.Map<long?, byte[]>((short?)null));
        }

        #endregion

        #region UInt64

        [Fact]
        public void TestUInt64Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<ulong, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<ulong, byte>(20));
            Assert.Equal((char)20, Mapper.Map<ulong, char>(20));
            Assert.Equal(20, Mapper.Map<ulong, int>(20));
            Assert.Equal((uint)20, Mapper.Map<ulong, uint>(20));
            Assert.Equal(20, Mapper.Map<ulong, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<ulong, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<ulong, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<ulong, ushort>(20));
            Assert.Equal(20, Mapper.Map<ulong, decimal>(20));
            Assert.Equal(20, Mapper.Map<ulong, float>(20));
            Assert.Equal(20, Mapper.Map<ulong, double>(20));
            Assert.Equal(true, Mapper.Map<ulong, bool>(20));
            Assert.Equal(false, Mapper.Map<ulong, bool>(0));
            Assert.Equal("20", Mapper.Map<ulong, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<ulong, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ulong, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((ulong)20), Mapper.Map<ulong, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<ulong, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<ulong, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<ulong, char?>(20));
            Assert.Equal(20, Mapper.Map<ulong, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<ulong, uint?>(20));
            Assert.Equal(20, Mapper.Map<ulong, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<ulong, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<ulong, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<ulong, ushort?>(20));
            Assert.Equal(20, Mapper.Map<ulong, decimal?>(20));
            Assert.Equal(20, Mapper.Map<ulong, float?>(20));
            Assert.Equal(20, Mapper.Map<ulong, double?>(20));
            Assert.Equal(true, Mapper.Map<ulong, bool?>(20));
            Assert.Equal(false, Mapper.Map<ulong, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<ulong, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ulong, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableUInt64Convert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<ulong?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<ulong?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<ulong?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<ulong?, uint>(20));
            Assert.Equal(20, Mapper.Map<ulong?, long>(20));
            Assert.Equal(20, Mapper.Map<ulong?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<ulong?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<ulong?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<ulong?, ushort>(20));
            Assert.Equal(20, Mapper.Map<ulong?, decimal>(20));
            Assert.Equal(20, Mapper.Map<ulong?, float>(20));
            Assert.Equal(20, Mapper.Map<ulong?, double>(20));
            Assert.Equal(true, Mapper.Map<ulong?, bool>(20));
            Assert.Equal(false, Mapper.Map<ulong?, bool>(0));
            Assert.Equal("20", Mapper.Map<ulong?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<ulong?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ulong?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((ulong)20), Mapper.Map<ulong?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<ulong?, sbyte>((ulong?)null));
            Assert.Equal((byte)0, Mapper.Map<ulong?, byte>((ulong?)null));
            Assert.Equal((char)0, Mapper.Map<ulong?, char>((ulong?)null));
            Assert.Equal((uint)0, Mapper.Map<ulong?, uint>((ulong?)null));
            Assert.Equal(0, Mapper.Map<ulong?, long>((ulong?)null));
            Assert.Equal(0, Mapper.Map<ulong?, int>((ulong?)null));
            Assert.Equal((ulong)0, Mapper.Map<ulong?, ulong>((ulong?)null));
            Assert.Equal((short)0, Mapper.Map<ulong?, short>((ulong?)null));
            Assert.Equal((ushort)0, Mapper.Map<ulong?, ushort>((ulong?)null));
            Assert.Equal(0, Mapper.Map<ulong?, decimal>((ulong?)null));
            Assert.Equal(0, Mapper.Map<ulong?, float>((ulong?)null));
            Assert.Equal(0, Mapper.Map<ulong?, double>((ulong?)null));
            Assert.Equal(false, Mapper.Map<ulong?, bool>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, string>((ulong?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<ulong?, DateTime>((ulong?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<ulong?, TimeSpan>((ulong?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<ulong?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<ulong?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<ulong?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<ulong?, uint?>(20));
            Assert.Equal(20, Mapper.Map<ulong?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<ulong?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<ulong?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<ulong?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<ulong?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<ulong?, float?>(20));
            Assert.Equal(20, Mapper.Map<ulong?, double?>(20));
            Assert.Equal(true, Mapper.Map<ulong?, bool?>(20));
            Assert.Equal(false, Mapper.Map<ulong?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<ulong?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<ulong?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<ulong?, sbyte?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, byte?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, char?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, uint?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, long?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, ulong?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, short?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, ushort?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, decimal?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, float?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, double?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, bool?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, DateTime?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, string>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, TimeSpan?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<ulong?, byte[]>((ulong?)null));
        }

        #endregion

        #region Decimal

        [Fact]
        public void TestDecimalConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<decimal, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<decimal, byte>(20));
            Assert.Equal((char)20, Mapper.Map<decimal, char>(20));
            Assert.Equal(20, Mapper.Map<decimal, int>(20));
            Assert.Equal((uint)20, Mapper.Map<decimal, uint>(20));
            Assert.Equal(20, Mapper.Map<decimal, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<decimal, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<decimal, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<decimal, ushort>(20));
            Assert.Equal(20, Mapper.Map<decimal, decimal>(20));
            Assert.Equal(20, Mapper.Map<decimal, float>(20));
            Assert.Equal(20, Mapper.Map<decimal, double>(20));
            Assert.Equal(true, Mapper.Map<decimal, bool>(20));
            Assert.Equal(false, Mapper.Map<decimal, bool>(0));
            Assert.Equal("20", Mapper.Map<decimal, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<decimal, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<decimal, TimeSpan>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<decimal, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<decimal, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<decimal, char?>(20));
            Assert.Equal(20, Mapper.Map<decimal, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<decimal, uint?>(20));
            Assert.Equal(20, Mapper.Map<decimal, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<decimal, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<decimal, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<decimal, ushort?>(20));
            Assert.Equal(20, Mapper.Map<decimal, decimal?>(20));
            Assert.Equal(20, Mapper.Map<decimal, float?>(20));
            Assert.Equal(20, Mapper.Map<decimal, double?>(20));
            Assert.Equal(true, Mapper.Map<decimal, bool?>(20));
            Assert.Equal(false, Mapper.Map<decimal, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<decimal, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<decimal, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableDecimalConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<decimal?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<decimal?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<decimal?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<decimal?, uint>(20));
            Assert.Equal(20, Mapper.Map<decimal?, long>(20));
            Assert.Equal(20, Mapper.Map<decimal?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<decimal?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<decimal?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<decimal?, ushort>(20));
            Assert.Equal(20, Mapper.Map<decimal?, decimal>(20));
            Assert.Equal(20, Mapper.Map<decimal?, float>(20));
            Assert.Equal(20, Mapper.Map<decimal?, double>(20));
            Assert.Equal(true, Mapper.Map<decimal?, bool>(20));
            Assert.Equal(false, Mapper.Map<decimal?, bool>(0));
            Assert.Equal("20", Mapper.Map<decimal?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<decimal?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<decimal?, TimeSpan>(20));

            Assert.Equal((sbyte)0, Mapper.Map<decimal?, sbyte>((decimal?)null));
            Assert.Equal((byte)0, Mapper.Map<decimal?, byte>((decimal?)null));
            Assert.Equal((char)0, Mapper.Map<decimal?, char>((decimal?)null));
            Assert.Equal((uint)0, Mapper.Map<decimal?, uint>((decimal?)null));
            Assert.Equal(0, Mapper.Map<decimal?, long>((decimal?)null));
            Assert.Equal(0, Mapper.Map<decimal?, int>((decimal?)null));
            Assert.Equal((ulong)0, Mapper.Map<decimal?, ulong>((decimal?)null));
            Assert.Equal((short)0, Mapper.Map<decimal?, short>((decimal?)null));
            Assert.Equal((ushort)0, Mapper.Map<decimal?, ushort>((decimal?)null));
            Assert.Equal(0, Mapper.Map<decimal?, decimal>((decimal?)null));
            Assert.Equal(0, Mapper.Map<decimal?, float>((decimal?)null));
            Assert.Equal(0, Mapper.Map<decimal?, double>((decimal?)null));
            Assert.Equal(false, Mapper.Map<decimal?, bool>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, string>((decimal?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<decimal?, DateTime>((decimal?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<decimal?, TimeSpan>((decimal?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<decimal?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<decimal?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<decimal?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<decimal?, uint?>(20));
            Assert.Equal(20, Mapper.Map<decimal?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<decimal?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<decimal?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<decimal?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<decimal?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<decimal?, float?>(20));
            Assert.Equal(20, Mapper.Map<decimal?, double?>(20));
            Assert.Equal(true, Mapper.Map<decimal?, bool?>(20));
            Assert.Equal(false, Mapper.Map<decimal?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<decimal?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<decimal?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<decimal?, sbyte?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, byte?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, char?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, uint?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, long?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, ulong?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, short?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, ushort?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, decimal?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, float?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, double?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, bool?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, DateTime?>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, string>((decimal?)null));
            Assert.Equal(null, Mapper.Map<decimal?, TimeSpan?>((decimal?)null));
        }

        #endregion

        #region Double

        [Fact]
        public void TestDoubleConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<double, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<double, byte>(20));
            Assert.Equal((char)20, Mapper.Map<double, char>(20));
            Assert.Equal(20, Mapper.Map<double, int>(20));
            Assert.Equal((uint)20, Mapper.Map<double, uint>(20));
            Assert.Equal(20, Mapper.Map<double, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<double, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<double, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<double, ushort>(20));
            Assert.Equal(20, Mapper.Map<double, decimal>(20));
            Assert.Equal(20, Mapper.Map<double, float>(20));
            Assert.Equal(20, Mapper.Map<double, double>(20));
            Assert.Equal(true, Mapper.Map<double, bool>(20));
            Assert.Equal(false, Mapper.Map<double, bool>(0));
            Assert.Equal("20", Mapper.Map<double, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<double, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<double, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((double)20), Mapper.Map<double, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<double, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<double, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<double, char?>(20));
            Assert.Equal(20, Mapper.Map<double, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<double, uint?>(20));
            Assert.Equal(20, Mapper.Map<double, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<double, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<double, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<double, ushort?>(20));
            Assert.Equal(20, Mapper.Map<double, decimal?>(20));
            Assert.Equal(20, Mapper.Map<double, float?>(20));
            Assert.Equal(20, Mapper.Map<double, double?>(20));
            Assert.Equal(true, Mapper.Map<double, bool?>(20));
            Assert.Equal(false, Mapper.Map<double, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<double, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<double, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableDoubleConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<double?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<double?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<double?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<double?, uint>(20));
            Assert.Equal(20, Mapper.Map<double?, long>(20));
            Assert.Equal(20, Mapper.Map<double?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<double?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<double?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<double?, ushort>(20));
            Assert.Equal(20, Mapper.Map<double?, decimal>(20));
            Assert.Equal(20, Mapper.Map<double?, float>(20));
            Assert.Equal(20, Mapper.Map<double?, double>(20));
            Assert.Equal(true, Mapper.Map<double?, bool>(20));
            Assert.Equal(false, Mapper.Map<double?, bool>(0));
            Assert.Equal("20", Mapper.Map<double?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<double?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<double?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((double)20), Mapper.Map<double?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<double?, sbyte>((ulong?)null));
            Assert.Equal((byte)0, Mapper.Map<double?, byte>((ulong?)null));
            Assert.Equal((char)0, Mapper.Map<double?, char>((ulong?)null));
            Assert.Equal((uint)0, Mapper.Map<double?, uint>((ulong?)null));
            Assert.Equal(0, Mapper.Map<double?, long>((ulong?)null));
            Assert.Equal(0, Mapper.Map<double?, int>((ulong?)null));
            Assert.Equal((ulong)0, Mapper.Map<double?, ulong>((ulong?)null));
            Assert.Equal((short)0, Mapper.Map<double?, short>((ulong?)null));
            Assert.Equal((ushort)0, Mapper.Map<double?, ushort>((ulong?)null));
            Assert.Equal(0, Mapper.Map<double?, decimal>((ulong?)null));
            Assert.Equal(0, Mapper.Map<double?, float>((ulong?)null));
            Assert.Equal(0, Mapper.Map<double?, double>((ulong?)null));
            Assert.Equal(false, Mapper.Map<double?, bool>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, string>((ulong?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<double?, DateTime>((ulong?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<double?, TimeSpan>((ulong?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<double?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<double?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<double?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<double?, uint?>(20));
            Assert.Equal(20, Mapper.Map<double?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<double?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<double?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<double?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<double?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<double?, float?>(20));
            Assert.Equal(20, Mapper.Map<double?, double?>(20));
            Assert.Equal(true, Mapper.Map<double?, bool?>(20));
            Assert.Equal(false, Mapper.Map<double?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<double?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<double?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<double?, sbyte?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, byte?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, char?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, uint?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, long?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, ulong?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, short?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, ushort?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, decimal?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, float?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, double?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, bool?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, DateTime?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, string>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, TimeSpan?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<double?, byte[]>((ulong?)null));
        }

        #endregion

        #region Single

        [Fact]
        public void TestSingleConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<float, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<float, byte>(20));
            Assert.Equal((char)20, Mapper.Map<float, char>(20));
            Assert.Equal(20, Mapper.Map<float, int>(20));
            Assert.Equal((uint)20, Mapper.Map<float, uint>(20));
            Assert.Equal(20, Mapper.Map<float, long>(20));
            Assert.Equal((ulong)20, Mapper.Map<float, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<float, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<float, ushort>(20));
            Assert.Equal(20, Mapper.Map<float, decimal>(20));
            Assert.Equal(20, Mapper.Map<float, float>(20));
            Assert.Equal(20, Mapper.Map<float, double>(20));
            Assert.Equal(true, Mapper.Map<float, bool>(20));
            Assert.Equal(false, Mapper.Map<float, bool>(0));
            Assert.Equal("20", Mapper.Map<float, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<float, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<float, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((float)20), Mapper.Map<float, byte[]>(20));

            Assert.Equal((sbyte?)20, Mapper.Map<float, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<float, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<float, char?>(20));
            Assert.Equal(20, Mapper.Map<float, int?>(20));
            Assert.Equal((uint?)20, Mapper.Map<float, uint?>(20));
            Assert.Equal(20, Mapper.Map<float, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<float, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<float, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<float, ushort?>(20));
            Assert.Equal(20, Mapper.Map<float, decimal?>(20));
            Assert.Equal(20, Mapper.Map<float, float?>(20));
            Assert.Equal(20, Mapper.Map<float, double?>(20));
            Assert.Equal(true, Mapper.Map<float, bool?>(20));
            Assert.Equal(false, Mapper.Map<float, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<float, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<float, TimeSpan?>(20));
        }

        [Fact]
        public void TestNullableSingleConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<float?, sbyte>(20));
            Assert.Equal((byte)20, Mapper.Map<float?, byte>(20));
            Assert.Equal((char)20, Mapper.Map<float?, char>(20));
            Assert.Equal((uint)20, Mapper.Map<float?, uint>(20));
            Assert.Equal(20, Mapper.Map<float?, long>(20));
            Assert.Equal(20, Mapper.Map<float?, int>(20));
            Assert.Equal((ulong)20, Mapper.Map<float?, ulong>(20));
            Assert.Equal((short)20, Mapper.Map<float?, short>(20));
            Assert.Equal((ushort)20, Mapper.Map<float?, ushort>(20));
            Assert.Equal(20, Mapper.Map<float?, decimal>(20));
            Assert.Equal(20, Mapper.Map<float?, float>(20));
            Assert.Equal(20, Mapper.Map<float?, double>(20));
            Assert.Equal(true, Mapper.Map<float?, bool>(20));
            Assert.Equal(false, Mapper.Map<float?, bool>(0));
            Assert.Equal("20", Mapper.Map<float?, string>(20));
            Assert.Equal(new DateTime(20), Mapper.Map<float?, DateTime>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<float?, TimeSpan>(20));
            Assert.Equal(BitConverter.GetBytes((float)20), Mapper.Map<float?, byte[]>(20));

            Assert.Equal((sbyte)0, Mapper.Map<float?, sbyte>((ulong?)null));
            Assert.Equal((byte)0, Mapper.Map<float?, byte>((ulong?)null));
            Assert.Equal((char)0, Mapper.Map<float?, char>((ulong?)null));
            Assert.Equal((uint)0, Mapper.Map<float?, uint>((ulong?)null));
            Assert.Equal(0, Mapper.Map<float?, long>((ulong?)null));
            Assert.Equal(0, Mapper.Map<float?, int>((ulong?)null));
            Assert.Equal((ulong)0, Mapper.Map<float?, ulong>((ulong?)null));
            Assert.Equal((short)0, Mapper.Map<float?, short>((ulong?)null));
            Assert.Equal((ushort)0, Mapper.Map<float?, ushort>((ulong?)null));
            Assert.Equal(0, Mapper.Map<float?, decimal>((ulong?)null));
            Assert.Equal(0, Mapper.Map<float?, float>((ulong?)null));
            Assert.Equal(0, Mapper.Map<float?, double>((ulong?)null));
            Assert.Equal(false, Mapper.Map<float?, bool>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, string>((ulong?)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<float?, DateTime>((ulong?)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<float?, TimeSpan>((ulong?)null));

            Assert.Equal((sbyte?)20, Mapper.Map<float?, sbyte?>(20));
            Assert.Equal((byte?)20, Mapper.Map<float?, byte?>(20));
            Assert.Equal((char?)20, Mapper.Map<float?, char?>(20));
            Assert.Equal((uint?)20, Mapper.Map<float?, uint?>(20));
            Assert.Equal(20, Mapper.Map<float?, long?>(20));
            Assert.Equal((ulong?)20, Mapper.Map<float?, ulong?>(20));
            Assert.Equal((short?)20, Mapper.Map<float?, short?>(20));
            Assert.Equal((ushort?)20, Mapper.Map<float?, ushort?>(20));
            Assert.Equal(20, Mapper.Map<float?, decimal?>(20));
            Assert.Equal(20, Mapper.Map<float?, float?>(20));
            Assert.Equal(20, Mapper.Map<float?, double?>(20));
            Assert.Equal(true, Mapper.Map<float?, bool?>(20));
            Assert.Equal(false, Mapper.Map<float?, bool?>(0));
            Assert.Equal(new DateTime(20), Mapper.Map<float?, DateTime?>(20));
            Assert.Equal(new TimeSpan(20), Mapper.Map<float?, TimeSpan?>(20));

            Assert.Equal(null, Mapper.Map<float?, sbyte?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, byte?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, char?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, uint?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, long?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, ulong?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, short?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, ushort?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, decimal?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, float?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, double?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, bool?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, DateTime?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, string>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, TimeSpan?>((ulong?)null));
            Assert.Equal(null, Mapper.Map<float?, byte[]>((ulong?)null));
        }

        #endregion

        #endregion

        #region String

        [Fact]
        public void TestFromStringConvert()
        {
            Assert.Equal((sbyte)20, Mapper.Map<string, sbyte>("20"));
            Assert.Equal((byte)20, Mapper.Map<string, byte>("20"));
            Assert.Equal((char)20, Mapper.Map<string, char>(new string((char)20, 1)));
            Assert.Equal((uint)20, Mapper.Map<string, uint>("20"));
            Assert.Equal(20, Mapper.Map<string, long>("20"));
            Assert.Equal((ulong)20, Mapper.Map<string, ulong>("20"));
            Assert.Equal((short)20, Mapper.Map<string, short>("20"));
            Assert.Equal((ushort)20, Mapper.Map<string, ushort>("20"));
            Assert.Equal(20, Mapper.Map<string, decimal>("20"));
            Assert.Equal(20, Mapper.Map<string, float>("20"));
            Assert.Equal(20, Mapper.Map<string, double>("20"));
            Assert.Equal(true, Mapper.Map<string, bool>("true"));
            Assert.Equal(false, Mapper.Map<string, bool>("false"));
            Assert.Equal("20", Mapper.Map<string, string>("20"));
            var now = DateTime.Parse(DateTime.Now.ToString());
            Assert.Equal(now, Mapper.Map<string, DateTime>(now.ToString()));
            var span = new TimeSpan(new Random().Next());
            Assert.Equal(span, Mapper.Map<string, TimeSpan>(span.ToString()));
            var offset = DateTimeOffset.Parse(new DateTimeOffset(DateTime.Now).ToString());
            Assert.Equal(offset, Mapper.Map<string, DateTimeOffset>(offset.ToString()));
            var guid = Guid.NewGuid();
            Assert.Equal(guid, Mapper.Map<string, Guid>(guid.ToString()));
            var unit = Unit.Parse("57px");
            Assert.Equal(unit, Mapper.Map<string, Unit>(unit.ToString()));

            Assert.Equal((sbyte?)20, Mapper.Map<string, sbyte?>("20"));
            Assert.Equal((byte?)20, Mapper.Map<string, byte?>("20"));
            Assert.Equal((char?)20, Mapper.Map<string, char?>(new string((char)20, 1)));
            Assert.Equal(20, Mapper.Map<string, int?>("20"));
            Assert.Equal((uint?)20, Mapper.Map<string, uint?>("20"));
            Assert.Equal(20, Mapper.Map<string, long?>("20"));
            Assert.Equal((ulong?)20, Mapper.Map<string, ulong?>("20"));
            Assert.Equal((short?)20, Mapper.Map<string, short?>("20"));
            Assert.Equal((ushort?)20, Mapper.Map<string, ushort?>("20"));
            Assert.Equal(20, Mapper.Map<string, decimal?>("20"));
            Assert.Equal(20, Mapper.Map<string, float?>("20"));
            Assert.Equal(20, Mapper.Map<string, double?>("20"));
            Assert.Equal(true, Mapper.Map<string, bool?>("true"));
            Assert.Equal(false, Mapper.Map<string, bool?>("false"));
            Assert.Equal(now, Mapper.Map<string, DateTime?>(now.ToString()));
            Assert.Equal(span, Mapper.Map<string, TimeSpan?>(span.ToString()));
            Assert.Equal(offset, Mapper.Map<string, DateTimeOffset?>(offset.ToString()));
            Assert.Equal(guid, Mapper.Map<string, Guid?>(guid.ToString()));
            Assert.Equal(unit, Mapper.Map<string, Unit?>(unit.ToString()));
            Assert.Equal(IPAddress.None, Mapper.Map<string, IPAddress>(IPAddress.None.ToString()));
            var version = typeof(Mapper).Assembly.GetName().Version;
            Assert.Equal(version, Mapper.Map<string, Version>(version.ToString()));
            var uri = new Uri("http://www.wheatsoft.cn");
            Assert.Equal(uri, Mapper.Map<string, Uri>(uri.ToString()));
            var type = typeof(Mapper);
            Assert.Equal(type, Mapper.Map<string, Type>(type.AssemblyQualifiedName));
            var zone = TimeZoneInfo.Local;
            Assert.Equal(zone, Mapper.Map<string, TimeZoneInfo>(zone.ToSerializedString()));
        }

        [Fact]
        public void TestToStringConvert()
        {
            var now = DateTime.Now.ToString();
            Assert.Equal(now, Mapper.Map<DateTime, string>(DateTime.Parse(now)));
            var span = new TimeSpan(new Random().Next());
            Assert.Equal(span.ToString(), Mapper.Map<TimeSpan, string>(span));
            var offset = new DateTimeOffset(DateTime.Now).ToString();
            Assert.Equal(offset, Mapper.Map<DateTimeOffset, string>(DateTimeOffset.Parse(offset)));
            var guid = Guid.NewGuid();
            Assert.Equal(guid.ToString(), Mapper.Map<Guid, string>(guid));
            var unit = Unit.Parse("57px");
            Assert.Equal(unit.ToString(), Mapper.Map<Unit, string>(unit));

            Assert.Equal(IPAddress.None.ToString(), Mapper.Map<IPAddress, string>(IPAddress.None));
            var version = typeof(Mapper).Assembly.GetName().Version;
            Assert.Equal(version.ToString(), Mapper.Map<Version, string>(version));
            var uri = new Uri("http://www.wheatsoft.cn");
            Assert.Equal(uri.ToString(), Mapper.Map<Uri, string>(uri));
            var type = typeof(Mapper);
            Assert.Equal(type.AssemblyQualifiedName, Mapper.Map<Type, string>(type));
            var zone = TimeZoneInfo.Local;
            Assert.Equal(zone.ToSerializedString(), Mapper.Map<TimeZoneInfo, string>(zone));

            Assert.Equal(now, Mapper.Map<DateTime?, string>(DateTime.Parse(now)));
            Assert.Equal(span.ToString(), Mapper.Map<TimeSpan?, string>(span));
            Assert.Equal(offset, Mapper.Map<DateTimeOffset?, string>(DateTimeOffset.Parse(offset)));
            Assert.Equal(guid.ToString(), Mapper.Map<Guid?, string>(guid));
            Assert.Equal(unit.ToString(), Mapper.Map<Unit?, string>(unit));

            Assert.Equal(null, Mapper.Map<DateTime?, string>((DateTime?)null));
            Assert.Equal(null, Mapper.Map<TimeSpan?, string>((TimeSpan?)null));
            Assert.Equal(null, Mapper.Map<DateTimeOffset?, string>((DateTimeOffset?)null));
            Assert.Equal(null, Mapper.Map<Guid?, string>((Guid?)null));
            Assert.Equal(null, Mapper.Map<Unit?, string>((Unit?)null));
            Assert.Equal(null, Mapper.Map<IPAddress, string>((IPAddress)null));
            Assert.Equal(null, Mapper.Map<Version, string>((Version)null));
            Assert.Equal(null, Mapper.Map<Uri, string>((Uri)null));
            Assert.Equal(null, Mapper.Map<Type, string>((Type)null));
            Assert.Equal(null, Mapper.Map<TimeZoneInfo, string>((TimeZoneInfo)null));
        }

        [Fact]
        public void TestFromNullStringConvert()
        {
            Assert.Equal((sbyte)0, Mapper.Map<string, sbyte>((string)null));
            Assert.Equal((byte)0, Mapper.Map<string, byte>((string)null));
            Assert.Equal((char)0, Mapper.Map<string, char>((string)null));
            Assert.Equal(0, Mapper.Map<string, int>((string)null));
            Assert.Equal((uint)0, Mapper.Map<string, uint>((string)null));
            Assert.Equal(0, Mapper.Map<string, long>((string)null));
            Assert.Equal((ulong)0, Mapper.Map<string, ulong>((string)null));
            Assert.Equal((short)0, Mapper.Map<string, short>((string)null));
            Assert.Equal((ushort)0, Mapper.Map<string, ushort>((string)null));
            Assert.Equal(0, Mapper.Map<string, decimal>((string)null));
            Assert.Equal(0, Mapper.Map<string, float>((string)null));
            Assert.Equal(0, Mapper.Map<string, double>((string)null));
            Assert.Equal(false, Mapper.Map<string, bool>((string)null));
            Assert.Equal(DateTime.MinValue, Mapper.Map<string, DateTime>((string)null));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<string, TimeSpan>((string)null));
            Assert.Equal(DateTimeOffset.MinValue, Mapper.Map<string, DateTimeOffset>((string)null));
            Assert.Equal(Guid.Empty, Mapper.Map<string, Guid>((string)null));
            Assert.Equal(Unit.Empty, Mapper.Map<string, Unit>((string)null));

            Assert.Equal(null, Mapper.Map<string, sbyte?>((string)null));
            Assert.Equal(null, Mapper.Map<string, byte?>((string)null));
            Assert.Equal(null, Mapper.Map<string, char?>((string)null));
            Assert.Equal(null, Mapper.Map<string, int?>((string)null));
            Assert.Equal(null, Mapper.Map<string, uint?>((string)null));
            Assert.Equal(null, Mapper.Map<string, long?>((string)null));
            Assert.Equal(null, Mapper.Map<string, ulong?>((string)null));
            Assert.Equal(null, Mapper.Map<string, short?>((string)null));
            Assert.Equal(null, Mapper.Map<string, ushort?>((string)null));
            Assert.Equal(null, Mapper.Map<string, decimal?>((string)null));
            Assert.Equal(null, Mapper.Map<string, float?>((string)null));
            Assert.Equal(null, Mapper.Map<string, double?>((string)null));
            Assert.Equal(null, Mapper.Map<string, bool?>((string)null));
            Assert.Equal(null, Mapper.Map<string, DateTime?>((string)null));
            Assert.Equal(null, Mapper.Map<string, TimeSpan?>((string)null));
            Assert.Equal(null, Mapper.Map<string, DateTimeOffset?>((string)null));
            Assert.Equal(null, Mapper.Map<string, Guid?>((string)null));
            Assert.Equal(null, Mapper.Map<string, Unit?>((string)null));
            Assert.Equal(null, Mapper.Map<string, IPAddress>((string)null));
            Assert.Equal(null, Mapper.Map<string, Version>((string)null));
            Assert.Equal(null, Mapper.Map<string, Uri>((string)null));
            Assert.Equal(null, Mapper.Map<string, Type>((string)null));
            Assert.Equal(null, Mapper.Map<string, TimeZoneInfo>((string)null));
        }

        [Fact]
        public void TestFromEmptyStringConvert()
        {
            Assert.Equal((sbyte)0, Mapper.Map<string, sbyte>(string.Empty));
            Assert.Equal((byte)0, Mapper.Map<string, byte>(string.Empty));
            Assert.Equal((char)0, Mapper.Map<string, char>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, int>(string.Empty));
            Assert.Equal((uint)0, Mapper.Map<string, uint>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, long>(string.Empty));
            Assert.Equal((ulong)0, Mapper.Map<string, ulong>(string.Empty));
            Assert.Equal((short)0, Mapper.Map<string, short>(string.Empty));
            Assert.Equal((ushort)0, Mapper.Map<string, ushort>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, decimal>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, float>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, double>(string.Empty));
            Assert.Equal(false, Mapper.Map<string, bool>(string.Empty));
            Assert.Equal(DateTime.MinValue, Mapper.Map<string, DateTime>(string.Empty));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<string, TimeSpan>(string.Empty));
            Assert.Equal(DateTimeOffset.MinValue, Mapper.Map<string, DateTimeOffset>(string.Empty));
            Assert.Equal(Guid.Empty, Mapper.Map<string, Guid>(string.Empty));
            Assert.Equal(Unit.Empty, Mapper.Map<string, Unit>(string.Empty));

            Assert.Equal((sbyte?)0, Mapper.Map<string, sbyte?>(string.Empty));
            Assert.Equal((byte?)0, Mapper.Map<string, byte?>(string.Empty));
            Assert.Equal((char?)0, Mapper.Map<string, char?>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, int?>(string.Empty));
            Assert.Equal((uint?)0, Mapper.Map<string, uint?>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, long?>(string.Empty));
            Assert.Equal((ulong?)0, Mapper.Map<string, ulong?>(string.Empty));
            Assert.Equal((short?)0, Mapper.Map<string, short?>(string.Empty));
            Assert.Equal((ushort?)0, Mapper.Map<string, ushort?>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, decimal?>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, float?>(string.Empty));
            Assert.Equal(0, Mapper.Map<string, double?>(string.Empty));
            Assert.Equal(false, Mapper.Map<string, bool?>(string.Empty));
            Assert.Equal(DateTime.MinValue, Mapper.Map<string, DateTime?>(string.Empty));
            Assert.Equal(null, Mapper.Map<string, IPAddress>(string.Empty));
            Assert.Equal(null, Mapper.Map<string, Version>(string.Empty));
            Assert.Equal(null, Mapper.Map<string, Uri>(string.Empty));
            Assert.Equal(null, Mapper.Map<string, Type>(string.Empty));
            Assert.Equal(null, Mapper.Map<string, TimeZoneInfo>(string.Empty));
        }

        [Fact]
        public void TestFromWhiteSpaceConvert()
        {
            Assert.Equal((sbyte)0, Mapper.Map<string, sbyte>(" "));
            Assert.Equal((byte)0, Mapper.Map<string, byte>(" "));
            Assert.Equal((char)0, Mapper.Map<string, char>(" "));
            Assert.Equal(0, Mapper.Map<string, int>(" "));
            Assert.Equal((uint)0, Mapper.Map<string, uint>(" "));
            Assert.Equal(0, Mapper.Map<string, long>(" "));
            Assert.Equal((ulong)0, Mapper.Map<string, ulong>(" "));
            Assert.Equal((short)0, Mapper.Map<string, short>(" "));
            Assert.Equal((ushort)0, Mapper.Map<string, ushort>(" "));
            Assert.Equal(0, Mapper.Map<string, decimal>(" "));
            Assert.Equal(0, Mapper.Map<string, float>(" "));
            Assert.Equal(0, Mapper.Map<string, double>(" "));
            Assert.Equal(false, Mapper.Map<string, bool>(" "));
            Assert.Equal(DateTime.MinValue, Mapper.Map<string, DateTime>(" "));
            Assert.Equal(TimeSpan.Zero, Mapper.Map<string, TimeSpan>(" "));
            Assert.Equal(DateTimeOffset.MinValue, Mapper.Map<string, DateTimeOffset>(" "));
            Assert.Equal(Guid.Empty, Mapper.Map<string, Guid>(" "));
            Assert.Equal(Unit.Empty, Mapper.Map<string, Unit>(" "));

            Assert.Equal((sbyte?)0, Mapper.Map<string, sbyte?>(" "));
            Assert.Equal((byte?)0, Mapper.Map<string, byte?>(" "));
            Assert.Equal((char?)0, Mapper.Map<string, char?>(" "));
            Assert.Equal(0, Mapper.Map<string, int?>(" "));
            Assert.Equal((uint?)0, Mapper.Map<string, uint?>(" "));
            Assert.Equal(0, Mapper.Map<string, long?>(" "));
            Assert.Equal((ulong?)0, Mapper.Map<string, ulong?>(" "));
            Assert.Equal((short?)0, Mapper.Map<string, short?>(" "));
            Assert.Equal((ushort?)0, Mapper.Map<string, ushort?>(" "));
            Assert.Equal(0, Mapper.Map<string, decimal?>(" "));
            Assert.Equal(0, Mapper.Map<string, float?>(" "));
            Assert.Equal(0, Mapper.Map<string, double?>(" "));
            Assert.Equal(false, Mapper.Map<string, bool?>(" "));
            Assert.Equal(DateTime.MinValue, Mapper.Map<string, DateTime?>(" "));
            Assert.Equal(null, Mapper.Map<string, IPAddress>(" "));
            Assert.Equal(null, Mapper.Map<string, Version>(" "));
            Assert.Equal(null, Mapper.Map<string, Uri>(" "));
            Assert.Equal(null, Mapper.Map<string, Type>(" "));
            Assert.Equal(null, Mapper.Map<string, TimeZoneInfo>(" "));
        }

        #endregion

        #region Misc

        [Fact]
        public void TestNullableToNonNullable()
        {
            Assert.Equal(DateTime.MinValue, Mapper.Map<DateTime?, DateTime>((DateTime?)null));
            var now = DateTime.Now;
            Assert.Equal(now, Mapper.Map<DateTime?, DateTime>(now));

            Assert.Equal(TimeSpan.Zero, Mapper.Map<TimeSpan?, TimeSpan>((TimeSpan?)null));
            var span = new TimeSpan(new Random().Next());
            Assert.Equal(span, Mapper.Map<TimeSpan?, TimeSpan>(span));

            Assert.Equal(DateTimeOffset.MinValue, Mapper.Map<DateTimeOffset?, DateTimeOffset>((DateTimeOffset?)null));
            var offset = new DateTimeOffset(DateTime.Now);
            Assert.Equal(offset, Mapper.Map<DateTimeOffset?, DateTimeOffset>(offset));

            Assert.Equal(Guid.Empty, Mapper.Map<Guid?, Guid>((Guid?)null));
            var guid = Guid.NewGuid();
            Assert.Equal(guid, Mapper.Map<Guid?, Guid>(guid));

            Assert.Equal(Unit.Empty, Mapper.Map<Unit?, Unit>((Unit?)null));
            var unit = Unit.Parse("57px");
            Assert.Equal(unit, Mapper.Map<Unit?, Unit>(unit));
        }

        [Fact]
        public void TestFromByteArray()
        {
            Assert.Equal(20, Mapper.Map<byte[], int>(BitConverter.GetBytes(20)));
            Assert.Equal((uint)20, Mapper.Map<byte[], uint>(BitConverter.GetBytes((uint)20)));
            Assert.Equal((short)20, Mapper.Map<byte[], short>(BitConverter.GetBytes((short)20)));
            Assert.Equal((ushort)20, Mapper.Map<byte[], ushort>(BitConverter.GetBytes((ushort)20)));
            Assert.Equal(20, Mapper.Map<byte[], long>(BitConverter.GetBytes((long)20)));
            Assert.Equal((ulong)20, Mapper.Map<byte[], ulong>(BitConverter.GetBytes((ulong)20)));
            Assert.Equal(true, Mapper.Map<byte[], bool>(BitConverter.GetBytes(true)));
            Assert.Equal(false, Mapper.Map<byte[], bool>(BitConverter.GetBytes(false)));
            Assert.Equal((char)20, Mapper.Map<byte[], char>(BitConverter.GetBytes((char)20)));
            Assert.Equal(20, Mapper.Map<byte[], double>(BitConverter.GetBytes((double)20)));
            Assert.Equal(20, Mapper.Map<byte[], float>(BitConverter.GetBytes((float)20)));

            var guid = Guid.NewGuid();
            Assert.Equal(guid, Mapper.Map<byte[], Guid>(guid.ToByteArray()));
            Assert.Equal(guid.ToByteArray(), Mapper.Map<Guid, byte[]>(guid));

            Assert.Equal(IPAddress.None.GetAddressBytes(), Mapper.Map<IPAddress, byte[]>(IPAddress.None));
            Assert.Equal(IPAddress.None, Mapper.Map<byte[], IPAddress>(IPAddress.None.GetAddressBytes()));
        }

        [Fact]
        public void TestDateTimeToDateTimeOffset()
        {
            var now = DateTime.Now;
            Assert.Equal(new DateTimeOffset(now), Mapper.Map<DateTime?, DateTimeOffset>(now));
            Assert.Equal(DateTimeOffset.MinValue, Mapper.Map<DateTime?, DateTimeOffset>((DateTime?)null));
            Assert.Equal(new DateTimeOffset(now), Mapper.Map<DateTime, DateTimeOffset>(now));
        }

        #endregion
    }
}
