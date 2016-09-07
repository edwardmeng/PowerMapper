using System.Collections.Generic;

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

        public override int GetHashCode()
        {
            return ReflectionHelper.CombineHashCodes(
                EqualityComparer<TFirst>.Default.GetHashCode(First), 
                EqualityComparer<TSecond>.Default.GetHashCode(Second),
                EqualityComparer<TThird>.Default.GetHashCode(Third));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Triplet<TFirst, TSecond, TThird>;
            if (other == null) return false;
            return EqualityComparer<TFirst>.Default.Equals(First, other.First) &&
                   EqualityComparer<TSecond>.Default.Equals(Second, other.Second) &&
                   EqualityComparer<TThird>.Default.Equals(Third, other.Third);
        }
    }

    internal static class Triplet
    {
        public static Triplet<TFirst, TSecond, TThird> Create<TFirst, TSecond, TThird>(TFirst first, TSecond second, TThird third)
        {
            return new Triplet<TFirst, TSecond, TThird>(first, second, third);
        }
    }
}
