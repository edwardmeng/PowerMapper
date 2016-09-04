namespace PowerMapper
{
    internal sealed class Triplet<TFirst, TSecond, TThird>
    {
        public Triplet(TFirst first, TSecond second, TThird third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public TFirst First { get; }

        public TSecond Second { get; }

        public TThird Third { get; }
    }

    internal static class Triplet
    {
        public static Triplet<TFirst, TSecond, TThird> Create<TFirst, TSecond, TThird>(TFirst first, TSecond second, TThird third)
        {
            return new Triplet<TFirst, TSecond, TThird>(first, second, third);
        }
    }
}
