namespace CompMath_Lab4
{
    public delegate double Function(Vector vector);

    public class FunctionData
    {
        private const double Alpha = 0.467;
        private const double Betha = -1.549;
        private const double A = 0.896;
        private const double B = -0.672;
        private const double C = 1.042;
        private const double D = 0.747;

        private static readonly Function f11 = (vector) => Math.Sin(vector[0] + Alpha) + B * vector[1] - C;
        private static readonly Function f12 = (vector) => vector[0] + Math.Cos(vector[1] + Betha) - D;
        private static readonly Function mapping1 = (vector) => D - Math.Cos(vector[1] + Betha);
        private static readonly Function mapping2 = (vector) => (C - Math.Sin(vector[0] + Alpha)) / B;

        private static readonly Function f21 = (vector) => Math.Tan(vector[0] * vector[1] + Alpha) - vector[0] * vector[0];
        private static readonly Function f21DerivativeX = (vector) => vector[1] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2) - 2 * vector[0];
        private static readonly Function f21DerivativeY = (vector) => vector[0] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2);
        private static readonly FunctionData f21Data = new(2, f21, f21DerivativeX, f21DerivativeY);

        private static readonly Function f22 = (vector) => A * vector[0] * vector[0] + B * vector[1] * vector[1] - 1;
        private static readonly Function f22DerivativeX = (vector) => 2 * A * vector[0];
        private static readonly Function f22DerivativeY = (vector) => 2 * B * vector[1];
        private static readonly FunctionData f22Data = new(2, f22, f22DerivativeX, f22DerivativeY);

        public static readonly IEnumerable<Function> Functions = new[] { f11, f12 };
        public static readonly IEnumerable<Function> Mappings = new[] { mapping1, mapping2 };
        public static readonly IEnumerable<FunctionData> FunctionsData = new[] { f21Data, f22Data };

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
