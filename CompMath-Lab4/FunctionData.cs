namespace CompMath_Lab4
{
    public delegate double Function(Vector vector);

    public class FunctionData
    {
        public FunctionData(int variablesCount, Function function, params Function[] derivatives)
        {
            if (derivatives.Length != variablesCount)
            {
                throw new ArgumentException("Number of derivatives and variables are not equal");
            }

            VariablesCount = variablesCount;
            Function = function;
            Derivatives = derivatives;
        }

        public int VariablesCount { get; init; }
        public Function Function { get; init; }
        public IEnumerable<Function> Derivatives { get; init; }
    }
}
