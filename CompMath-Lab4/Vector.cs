using System.Globalization;

namespace CompMath_Lab4
{
    public struct Vector
    {
        private readonly double[] _vector;

        public Vector(IEnumerable<double> vector)
        {
            _vector = vector.ToArray();
        }

        public int Length => _vector.Length;
        public double this[int i] => _vector[i];
        public double Norm => Math.Sqrt(_vector.Sum(n => n * n));

        public override string ToString() =>
            $"({string.Join(", ", _vector.Select(n => n.ToString(CultureInfo.InvariantCulture)))})";

        public static Vector operator-(Vector left, Vector right)
        {
            if(left.Length != right.Length)
            {
                throw new ArgumentException("Vectors sizes are not equal");
            }

            return new(left._vector
                .Zip(right._vector)
                .Select(p => p.First - p.Second));
        }
    }
}
