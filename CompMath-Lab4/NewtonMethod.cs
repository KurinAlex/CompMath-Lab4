namespace CompMath_Lab4
{
    public class NewtonMethod : IMethod
    {
        private readonly Writer _writer;

        public NewtonMethod(Writer writer)
        {
            _writer = writer;
        }

        public string Name => "Newton";

        public Vector Solve(Vector startVector, double error)
        {
            int variablesCount = startVector.Length;
            if (FunctionData.FunctionsData.Count() != variablesCount)
            {
                throw new ArgumentException("Number of functions and variables are not equal");
            }
            if (FunctionData.FunctionsData.Any(d => d.VariablesCount != variablesCount))
            {
                throw new ArgumentException("Numbers of variables in functions are not equal");
            }

            var x = startVector;
            var functions = FunctionData.FunctionsData.Select(fd => fd.Function);
            var derivatives = FunctionData.FunctionsData.Select(fd => fd.Derivatives);

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
