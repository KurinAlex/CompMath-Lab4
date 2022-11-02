using CompMath_Lab4;

namespace Program
{
    public class Program
    {
        const string OutputFileName = "Result.txt";

        const double Alpha = 0.467;
        const double Betha = -1.549;
        const double A = 0.896;
        const double B = -0.672;
        const double C = 1.042;
        const double D = 0.747;

        const double Error = 1e-5;

        static readonly double[] fixedPointMethodStart = { 0.8, -0.1 };
        static readonly Vector fixedPointMethodStartVector = new(fixedPointMethodStart);

        static readonly Function f11 = (vector) => Math.Sin(vector[0] + Alpha) + B * vector[1] - C;
        static readonly Function f12 = (vector) => vector[0] + Math.Cos(vector[1] + Betha) - D;
        static readonly Function mapping1 = (vector) => D - Math.Cos(vector[1] + Betha);
        static readonly Function mapping2 = (vector) => (C - Math.Sin(vector[0] + Alpha)) / B;

        static readonly double[] newtonMethodStart = { 1, 0 };
        static readonly Vector newtonMethodStartVector = new(newtonMethodStart);

        static readonly Function f21 = (vector) => Math.Tan(vector[0] * vector[1] + Alpha) - vector[0] * vector[0];
        static readonly Function f21DerivativeX = (vector) => vector[1] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2) - 2 * vector[0];
        static readonly Function f21DerivativeY = (vector) => vector[0] * Math.Pow(Math.Cos(vector[0] * vector[1] + Alpha), -2);
        static readonly FunctionData f21Data = new(2, f21, f21DerivativeX, f21DerivativeY);

        static readonly Function f22 = (vector) => A * vector[0] * vector[0] + B * vector[1] * vector[1] - 1;
        static readonly Function f22DerivativeX = (vector) => 2 * A * vector[0];
        static readonly Function f22DerivativeY = (vector) => 2 * B * vector[1];
        static readonly FunctionData f22Data = new(2, f22, f22DerivativeX, f22DerivativeY);

        static void Main(string[] args)
        {
            using (StreamWriter fileWriter = new(OutputFileName))
            {
                Writer writer = new(fileWriter);

                try
                {
                    Vector x;

                    writer.WriteLine("Fixed-point method:");
                    writer.WriteDivider();
                    x = FixedPointMethod.Solve(writer, fixedPointMethodStartVector, Error, new[] { f11, f12 }, new[] { mapping1, mapping2 });
                    writer.WriteLine("X:");
                    writer.WriteLine(x);
                    writer.WriteDivider();

                    writer.WriteLine("Newton method:");
                    writer.WriteDivider(); 
                    x = NewtonMethod.Solve(writer, newtonMethodStartVector, Error, f21Data, f22Data);
                    writer.WriteLine("X:");
                    writer.WriteLine(x);
                    writer.WriteDivider();
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
            Console.ReadKey();
        }
    }
}