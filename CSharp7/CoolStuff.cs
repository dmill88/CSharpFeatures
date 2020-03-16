using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp7
{
    public class CoolStuff
    {
        public string ParseStringAsInteger(string input)
        {
            string numberAsString;
            if (int.TryParse(input, out int result))
                numberAsString = result.ToString();
            else
                numberAsString = string.Empty;

            return numberAsString;
        }

        public static double StandardDeviationWithTuple(IEnumerable<double> sequence)
        {
            var computation = ComputeSumAndSumOfSquares(sequence);

            var variance = computation.SumOfSquares - computation.Sum * computation.Sum / computation.Count;
            return Math.Sqrt(variance / computation.Count);
        }

        private static (int Count, double Sum, double SumOfSquares) ComputeSumAndSumOfSquares(IEnumerable<double> sequence)
        {
            double sum = 0;
            double sumOfSquares = 0;
            int count = 0;

            foreach (var item in sequence)
            {
                count++;
                sum += item;
                sumOfSquares += item * item;
            }

            return (count, sum, sumOfSquares);
        }

        public static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
        {
            int population1 = 0, population2 = 0;
            double area = 0;

            if (name == "New York City")
            {
                area = 468.48;
                if (year1 == 1960)
                {
                    population1 = 7781984;
                }
                if (year2 == 2010)
                {
                    population2 = 8175133;
                }
                return (name, area, year1, population1, year2, population2);
            }

            return ("", 0, 0, 0, 0, 0);
        }

        public static int SumPositiveNumbers(IEnumerable<object> sequence)
        {
            int sum = 0;
            foreach (var i in sequence)
            {
                switch (i)
                {
                    case int n when n < 1:
                        break;
                    case IEnumerable<int> childSequence:
                        {
                            foreach (var item in childSequence)
                                sum += (item > 0) ? item : 0;
                            break;
                        }
                    case int n when n > 0:
                        sum += n;
                        break;
                    case null:
                        throw new NullReferenceException("Null found in sequence");
                    default:
                        throw new InvalidOperationException("Unrecognized type");
                }
            }
            return sum;
        }

        public static ref int FindIntInMatrix(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];
            throw new InvalidOperationException("Not found");
        }

        public static IEnumerable<char> AlphabetSubset(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            return alphabetSubsetImplementation();

            IEnumerable<char> alphabetSubsetImplementation()
            {
                for (var c = start; c < end; c++)
                    yield return c;
            }
        }

        public static int LocalFunctionFactorial(int n)
        {
            return nthFactorial(n);

            int nthFactorial(int number) => (number < 2) ?
                1 : number * nthFactorial(number - 1);
        }

        public static int LambdaFactorial(int n)
        {
            Func<int, int> nthFactorial = default;

            nthFactorial = (number) => (number < 2) ?
                1 : number * nthFactorial(number - 1);

            return nthFactorial(n);
        }

        public async ValueTask<int> GetMyLuckyNumber()
        {
            await Task.Delay(100);
            return 88;
        }

        public long AddSomeMorePlease(long some)
        {
            long total = some + 100_000_000_000;
            return total; 
        }

    }
}
