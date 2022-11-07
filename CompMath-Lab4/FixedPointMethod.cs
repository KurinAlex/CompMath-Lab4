namespace CompMath_Lab4
{
    public class FixedPointMethod : IMethod
    {
        private readonly Writer _writer;

        public FixedPointMethod(Writer writer)
        {
            _writer = writer;
        }

        public string Name => "Fixed-point";

        public Vector Solve(Vector startVector, double error)
        {
            int variablesCount = startVector.Length;
            if (FunctionData.Functions.Count() != variablesCount)
            {
                throw new ArgumentException("Number of functions and variables are not equal");
            }
            if (FunctionData.Mappings.Count() != variablesCount)
            {
                throw new ArgumentException("Number of mappings and variables are not equal");
            }

            var xOld = startVector;
            var xNew = startVector;
            Vector functionsValues = new(FunctionData.Functions.Select(func => func(startVector)));

            Write(xNew, functionsValues, "Start approximation", 0);

            for (int i = 1; functionsValues.Norm >= error || (xNew - xOld).Norm >= error; i++)
            {
                xOld = xNew;
                xNew = new(FunctionData.Mappings.Select(f => f(xOld)));
                functionsValues = new(FunctionData.Functions.Select(func => func(xNew)));

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
