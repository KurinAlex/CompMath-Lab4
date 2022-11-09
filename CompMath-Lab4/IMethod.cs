namespace CompMath_Lab4
{
    public interface IMethod
    {
        string Name { get; }
        Vector Solve(ICommonTask task, Vector startVector, double error);
    }
}
