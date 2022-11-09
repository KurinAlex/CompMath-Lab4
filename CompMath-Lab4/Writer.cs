namespace CompMath_Lab4
{
    public class Writer
    {
        private const int DividerLength = 54;
        private static readonly string s_divider = new('-', DividerLength);

        private readonly StreamWriter _fileWriter;

        public Writer(StreamWriter fileWriter) => _fileWriter = fileWriter;

        public void WriteLine(object obj)
        {
            string? line = obj.ToString();
            Console.WriteLine(line);
            _fileWriter.WriteLine(line);
        }

        public void WriteDivider()
        {
            WriteLine(s_divider);
        }
    }
}
