namespace PowerMapper
{
    internal sealed class Pair<TFirst, TSecond>
    {
        public TFirst First { get; }
        public TSecond Second { get; }

        public Pair(TFirst x, TSecond y)
        {
            First = x;
            Second = y;
        }
    }

    internal static class Pair
    {
        public static Pair<TFirst, TSecond> Create<TFirst, TSecond>(TFirst x, TSecond y)
        {
            return new Pair<TFirst, TSecond>(x, y);
        }
    }
}
