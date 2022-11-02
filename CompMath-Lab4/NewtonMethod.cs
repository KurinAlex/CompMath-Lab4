namespace CompMath_Lab4
{
    public static class NewtonMethod
    {
        public static Vector Solve(Writer writer, Vector start, double error, params FunctionData[] functionsData)
        {
            int variablesCount = start.Length;
            if (functionsData.Length != variablesCount)
            {
                throw new ArgumentException("Number of functions and variables are not equal");
            }
            if (functionsData.Any(d => d.VariablesCount != variablesCount))
            {
                throw new ArgumentException("Numbers of variables in functions are not equal");
            }

            var x = start;
            var functions = functionsData.Select(fd => fd.Function);
            var derivatives = functionsData.Select(fd => fd.Derivatives);

            Vector functionsValues = new(functions.Select(func => func(start)));
            SquareMatrix derivativesValues = new(derivatives.Select(ders => ders.Select(d => d(start))));
            var diff = derivativesValues.GetInverse() * functionsValues;

            void Write(string message, int it)
            {
                writer.WriteDivider();
                writer.WriteLine($"{message}:");
                writer.WriteDivider();
                writer.WriteLine($"X_{it}:");
                writer.WriteLine(x);
                writer.WriteDivider();
                writer.WriteLine($"F(X_{it}):");
                writer.WriteLine(functionsValues);
                writer.WriteDivider();
                writer.WriteLine($"||F(X_{it})||:");
                writer.WriteLine(functionsValues.Norm);
                writer.WriteDivider();
            }

            Write("Start approximation", 0);

            for (int i = 1; functionsValues.Norm >= error || diff.Norm >= error; i++)
            {
                x -= diff;

                functionsValues = new(functions.Select(func => func(x)));
                derivativesValues = new(derivatives.Select(ders => ders.Select(d => d(x))));
                diff = derivativesValues.GetInverse() * functionsValues;

                Write($"{i} iteration", i);
            }
            return x;
        }
    }
}
