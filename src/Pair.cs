using System.Collections.Generic;

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

        public override int GetHashCode()
        {
            return ReflectionHelper.CombineHashCodes(
                EqualityComparer<TFirst>.Default.GetHashCode(First),
                EqualityComparer<TSecond>.Default.GetHashCode(Second));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Pair<TFirst, TSecond>;
            if (other == null) return false;
            return EqualityComparer<TFirst>.Default.Equals(First, other.First) && EqualityComparer<TSecond>.Default.Equals(Second, other.Second);
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
