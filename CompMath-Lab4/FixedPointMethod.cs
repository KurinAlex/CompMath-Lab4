namespace CompMath_Lab4
{
    public static class FixedPointMethod
    {
        public static Vector Solve(Writer writer, Vector start, double error, Function[] functions, Function[] mappings)
        {
            int variablesCount = start.Length;
            if (functions.Length != variablesCount)
            {
                throw new ArgumentException("Number of functions and variables are not equal");
            }
            if (mappings.Length != variablesCount)
            {
                throw new ArgumentException("Number of mappings and variables are not equal");
            }

            var xOld = start;
            var xNew = start;
            Vector functionsValues = new(functions.Select(func => func(start)));

            void Write(string message, int it)
            {
                writer.WriteDivider();
                writer.WriteLine($"{message}:");
                writer.WriteDivider();
                writer.WriteLine($"X_{it}:");
                writer.WriteLine(xNew);
                writer.WriteDivider();
                writer.WriteLine($"F(X_{it}):");
                writer.WriteLine(functionsValues);
                writer.WriteDivider();
                writer.WriteLine($"||F(X_{it})||:");
                writer.WriteLine(functionsValues.Norm);
                writer.WriteDivider();
            }

            Write("Start approximation", 0);

            for (int i = 1; functionsValues.Norm >= error || (xNew - xOld).Norm >= error; i++)
            {
                xOld = xNew;
                xNew = new(mappings.Select(f => f(xOld)));
                functionsValues = new(functions.Select(func => func(xNew)));

                Write($"{i} iteration", i);
            }
            return xNew;
        }
    }
}
