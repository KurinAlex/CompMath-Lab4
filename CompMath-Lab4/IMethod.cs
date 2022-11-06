namespace CompMath_Lab4
{
    public interface IMethod
    {
        string Name { get; }
        Vector Solve(Vector startVector, double error);
    }
}
