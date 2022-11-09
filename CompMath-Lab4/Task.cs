namespace CompMath_Lab4
{
    public class Task : ICommonTask
    {
        private const double Alpha = 0.467;
        private const double Betha = -1.549;
        private const double A = 0.896;
        private const double B = -0.672;
        private const double C = 1.042;
        private const double D = 0.747;

        private readonly Function f11 = (vector) => Math.Sin(vector[0] + Alpha) + B * vector[1] - C;
        private readonly Function f12 = (vector) => vector[0] + Math.Cos(vector[1] + Betha) - D;
        private readonly Function mapping1 = (vector) => D - Math.Cos(vector[1] + Betha);
        private readonly Function mapping2 = (vector) => (C - Math.Sin(vector[0] + Alpha)) / B;

        private readonly Function f21 = (vector) => Math.Tan(vector[0] * vector[1] + Alpha) - vector[0] * vector[0];
        private readonly Function f21DerivativeX = (vector) => vector[1] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2) - 2 * vector[0];
        private readonly Function f21DerivativeY = (vector) => vector[0] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2);

        private readonly Function f22 = (vector) => A * vector[0] * vector[0] + B * vector[1] * vector[1] - 1;
        private readonly Function f22DerivativeX = (vector) => 2 * A * vector[0];
        private readonly Function f22DerivativeY = (vector) => 2 * B * vector[1];

        public IEnumerable<Function> Functions => new[] { f11, f12 };

        public IEnumerable<Function> Mappings => new[] { mapping1, mapping2 };

        public IEnumerable<FunctionData> FunctionsData => new FunctionData[] {
            new(2, f21, f21DerivativeX, f21DerivativeY),
            new(2, f22, f22DerivativeX, f22DerivativeY) };
    }
}
