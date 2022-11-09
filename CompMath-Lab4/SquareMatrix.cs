namespace CompMath_Lab4
{
    public struct SquareMatrix
    {
        private readonly IEnumerable<IEnumerable<double>> _matrix;

        public SquareMatrix(IEnumerable<IEnumerable<double>> matrix)
        {
            int n = matrix.Count();
            if (matrix.Any(row => row.Count() != n))
            {
                throw new ArgumentException("Rows of matrix have different sizes");
            }

            _matrix = matrix;
        }

        public SquareMatrix GetInverse()
        {
            int n = _matrix.Count();
            int m = n * 2;
            double[][] matrix = _matrix
                .Select((row, i) => row
                    .Concat(Enumerable.Repeat(0.0, i))
                    .Append(1.0)
                    .Concat(Enumerable.Repeat(0.0, n - i - 1))
                    .ToArray())
                .ToArray();

            for (int k = 0; k < n; k++)
            {
                double f = matrix[k][k];
                for (int j = k; j < m; j++)
                {
                    matrix[k][j] /= f;
                }

                for (int i = 0; i < n; i++)
                {
                    if (i == k)
                    {
                        continue;
                    }

                    f = matrix[i][k] / matrix[k][k];
                    for (int j = k; j < m; j++)
                    {
                        matrix[i][j] -= matrix[k][j] * f;
                    }
                }
            }
            return new(matrix.Select(row => row.Skip(n)));
        }

        public static Vector operator *(SquareMatrix matrix, Vector vector)
        {
            if (matrix._matrix.Count() != vector.Length)
            {
                throw new ArgumentException("Sizes of matrix and vector are not equal");
            }

            return new(matrix._matrix
                .Select(d => d
                    .Select((n, i) => (n, i))
                    .Sum(t => t.n * vector[t.i])));
        }
    }
}
