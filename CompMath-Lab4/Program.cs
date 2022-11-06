using CompMath_Lab4;

namespace Program
{
    public class Program
    {
        const string OutputFileName = "Result.txt";

        const double Error = 1e-5;

        static readonly double[] fixedPointMethodStart = { 0.84, -0.69 };
        static readonly Vector fixedPointMethodStartVector = new(fixedPointMethodStart);

        static readonly double[] newtonMethodStart = { 1.0, 0.4 };
        static readonly Vector newtonMethodStartVector = new(newtonMethodStart);

        static void Main(string[] args)
        {
            using (StreamWriter fileWriter = new(OutputFileName))
            {
                Writer writer = new(fileWriter);
                try
                {
                    var methodsData = new (IMethod, Vector)[]
                    {
                        (new FixedPointMethod(writer), fixedPointMethodStartVector),
                        (new NewtonMethod(writer), newtonMethodStartVector)
                    };

                    foreach (var (method, startVector) in methodsData)
                    {
                        writer.WriteDivider();
                        writer.WriteLine($"{method.Name} method:");
                        writer.WriteDivider();
                        Vector x = method.Solve(startVector, Error);
                        writer.WriteDivider();
                        writer.WriteLine("X:");
                        writer.WriteLine(x);
                        writer.WriteDivider();
                    }
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