namespace PowerMapper.UnitTests
{
    public static class Assert
    {
        public static void Equal(object expected, object actual)
        {
#if NetCore
            Xunit.Assert.Equal(expected, actual);
#else
            NUnit.Framework.Assert.AreEqual(expected, actual);
#endif
        }

        public static void NotNull(object value)
        {
#if NetCore
            Xunit.Assert.NotNull(value);
#else
            NUnit.Framework.Assert.NotNull(value);
#endif
        }

        public static void Null(object value)
        {
#if NetCore
            Xunit.Assert.Null(value);
#else
            NUnit.Framework.Assert.Null(value);
#endif
        }

        public static void True(bool condition)
        {
#if NetCore
            Xunit.Assert.True(condition);
#else
            NUnit.Framework.Assert.True(condition);
#endif
        }

        public static void False(bool condition)
        {
#if NetCore
            Xunit.Assert.False(condition);
#else
            NUnit.Framework.Assert.False(condition);
#endif
        }
    }
}
