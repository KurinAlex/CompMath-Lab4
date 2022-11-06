namespace CompMath_Lab4
{
    public class NewtonMethod : IMethod
    {
        private readonly Writer _writer;

        private const double Alpha = 0.467;
        private const double A = 0.896;
        private const double B = -0.672;

        private static readonly Function f21 = (vector) => Math.Tan(vector[0] * vector[1] + Alpha) - vector[0] * vector[0];
        private static readonly Function f21DerivativeX = (vector) => vector[1] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2) - 2 * vector[0];
        private static readonly Function f21DerivativeY = (vector) => vector[0] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2);
        private static readonly FunctionData f21Data = new(2, f21, f21DerivativeX, f21DerivativeY);

        private static readonly Function f22 = (vector) => A * vector[0] * vector[0] + B * vector[1] * vector[1] - 1;
        private static readonly Function f22DerivativeX = (vector) => 2 * A * vector[0];
        private static readonly Function f22DerivativeY = (vector) => 2 * B * vector[1];
        private static readonly FunctionData f22Data = new(2, f22, f22DerivativeX, f22DerivativeY);

        private static readonly IEnumerable<FunctionData> _functionsData = new[] { f21Data, f22Data };

        public NewtonMethod(Writer writer)
        {
            _writer = writer;
        }

        public string Name => "Newton";

        public Vector Solve(Vector startVector, double error)
        {
            int variablesCount = startVector.Length;
            if (_functionsData.Count() != variablesCount)
            {
                throw new ArgumentException("Number of functions and variables are not equal");
            }
            if (_functionsData.Any(d => d.VariablesCount != variablesCount))
            {
                throw new ArgumentException("Numbers of variables in functions are not equal");
            }

            var x = startVector;
            var functions = _functionsData.Select(fd => fd.Function);
            var derivatives = _functionsData.Select(fd => fd.Derivatives);

            Vector functionsValues = new(functions.Select(func => func(startVector)));
            SquareMatrix derivativesValues = new(derivatives.Select(ders => ders.Select(d => d(startVector))));
            var diff = derivativesValues.GetInverse() * functionsValues;

            Write(x, functionsValues, "Start approximation", 0);

            for (int i = 1; functionsValues.Norm >= error || diff.Norm >= error; i++)
            {
                x -= diff;

                functionsValues = new(functions.Select(func => func(x)));
                derivativesValues = new(derivatives.Select(ders => ders.Select(d => d(x))));
                diff = derivativesValues.GetInverse() * functionsValues;

                Write(x, functionsValues, $"{i} iteration", i);
            }
            return x;
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
