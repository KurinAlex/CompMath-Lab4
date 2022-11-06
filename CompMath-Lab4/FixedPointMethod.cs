namespace CompMath_Lab4
{
    public class FixedPointMethod : IMethod
    {
        private readonly IEnumerable<Function> _functions;
        private readonly IEnumerable<Function> _mappings;
        private readonly Writer _writer;

        public FixedPointMethod(Writer writer, Function[] functions, Function[] mappings)
        {
            _functions = functions;
            _mappings = mappings;
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
