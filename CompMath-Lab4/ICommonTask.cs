namespace CompMath_Lab4
{
    public interface ICommonTask
    {
        IEnumerable<Function> Functions { get; }
        IEnumerable<Function> Mappings { get; }
        IEnumerable<FunctionData> FunctionsData { get; }
    }
}
