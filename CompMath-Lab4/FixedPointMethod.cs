namespace CompMath_Lab4
{
    public class FixedPointMethod : IMethod
    {
        private readonly Writer _writer;

        private const double Alpha = 0.467;
        private const double Betha = -1.549;
        private const double B = -0.672;
        private const double C = 1.042;
        private const double D = 0.747;

        private static readonly Function f11 = (vector) => Math.Sin(vector[0] + Alpha) + B * vector[1] - C;
        private static readonly Function f12 = (vector) => vector[0] + Math.Cos(vector[1] + Betha) - D;
        private static readonly Function mapping1 = (vector) => D - Math.Cos(vector[1] + Betha);
        private static readonly Function mapping2 = (vector) => (C - Math.Sin(vector[0] + Alpha)) / B;

        private static readonly IEnumerable<Function> _functions = new[] { f11, f12 };
        private static readonly IEnumerable<Function> _mappings = new[] { mapping1, mapping2 };

        public FixedPointMethod(Writer writer)
        {
            _writer = writer;
        }

        public string Name => "Fixed-point";

        public Vector Solve(Vector startVector, double error)
        {
            int variablesCount = startVector.Length;
            if (_functions.Count() != variablesCount)
            {
                throw new ArgumentException("Number of functions and variables are not equal");
            }
            if (_mappings.Count() != variablesCount)
            {
                throw new ArgumentException("Number of mappings and variables are not equal");
            }

            var xOld = startVector;
            var xNew = startVector;
            Vector functionsValues = new(_functions.Select(func => func(startVector)));

            Write(xNew, functionsValues, "Start approximation", 0);

            for (int i = 1; functionsValues.Norm >= error || (xNew - xOld).Norm >= error; i++)
            {
                xOld = xNew;
                xNew = new(_mappings.Select(f => f(xOld)));
                functionsValues = new(_functions.Select(func => func(xNew)));

                Write(xNew, functionsValues, $"{i} iteration", i);
            }
            return xNew;
        }

        private void Write(Vector x, Vector functionsValues, string message, int it)
        {
            _writer.WriteDivider();
            _writer.WriteLine($"{message}:");
            _writer.WriteDivider();
            _writer.WriteLine($"X_{it}:");
            _writer.WriteLine(x);
            _writer.WriteDivider();
            _writer.WriteLine($"F(X_{it}):");
            _writer.WriteLine(functionsValues);
            _writer.WriteDivider();
            _writer.WriteLine($"||F(X_{it})||:");
            _writer.WriteLine(functionsValues.Norm);
            _writer.WriteDivider();
        }
    }
}
