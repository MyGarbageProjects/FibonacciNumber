using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;
using System.IO;

namespace Fibonacci_Numbers
{
    class Program
    {
        private static BigInteger Fibonacci_MatrixExponentiation(int n)
        {
            //-------------------------------
            Stopwatch t = new Stopwatch();
            t.Start();
            //---------------------------------
            if (n < 0)
                return BigInteger.MinusOne;
            else if (n == 1)
                return n;

            matrix2x2 res = new matrix2x2 { a = 1, b = 0, c = 0, d = 1 };
            matrix2x2 fib = new matrix2x2 { a = 1, b = 1, c = 1, d = 0 };

            do {
                if (n % 2 == 1) res *= fib;

                fib *= fib;
            } while ((n /= 2) > 0);
            
            /*do
            { 
                if (n % 2 == 1) Mult2x2Matrix(ref result, F);
                Mult2x2Matrix(ref F, F);

            } while ((n/=2) > 0);*/
            //---------------------------------
            t.Stop();
            TimeSpan ts = t.Elapsed;
            Console.WriteLine("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            //---------------------------------
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\Fibonacci_Matrix_Exponentiation.txt", res.c.ToString(), Encoding.UTF8);
            //File.WriteAllText(Directory.GetCurrentDirectory() + "\\Fibonacci_Matrix_Exponentiation.txt", result[1, 0].ToString(), Encoding.UTF8);
            //return result[1, 0];
            return res.c;
        }
        static void Fibonacci_DynamicProgramming(int n)
        {
            //-------------------------------------------
            Stopwatch t = new Stopwatch();
            t.Start();
            //--------------------------------------------
            BigInteger a = 1, b = 1, c = 1;
            //Console.WriteLine("1] 1");
            //Console.WriteLine("2] 1");
            for (int i = 1; i < n; i++)
            {
                c = a + b;
                b = a;
                a = c;
            }
            //--------------------------------------------
            t.Stop();
            TimeSpan ts = t.Elapsed;
            Console.WriteLine("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\Fibonacci_Dynamic_Programming.txt", a.ToString(), Encoding.UTF8);
            //-----------------------------------------------
            //Console.WriteLine("{0}] {1}", n, a);
            //Console.ReadLine();
        }
        private static void Mult2x2Matrix(ref BigInteger[,] A, BigInteger[,] B)
        {
            BigInteger a = A[0, 0] * B[0, 0] + A[0, 1] * B[1, 0];
            BigInteger b = A[0, 0] * B[0, 1] + A[0, 1] * B[1, 1];
            BigInteger c = A[1, 0] * B[0, 0] + A[1, 1] * B[0, 1];
            BigInteger d = A[1, 0] * B[0, 1] + A[1, 1] * B[1, 1];

            A[0, 0] = a;
            A[0, 1] = b;
            A[1, 0] = c;
            A[1, 1] = d;
        }
        struct matrix2x2
        {
            public BigInteger a, b, c, d;

            public static matrix2x2 operator *(matrix2x2 A, matrix2x2 B)
            {
                return new matrix2x2
                {
                    a = A.a * B.a + A.b * B.c,
                    b = A.a * B.b + A.b * B.d,
                    c = A.c * B.a + A.d * B.c,
                    d = A.c * B.b + A.d * B.d
                };
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("n=1'000'000");
            Console.WriteLine("With matrix exponentiation");
            Fibonacci_MatrixExponentiation(1000000);
            Console.WriteLine();
            Console.WriteLine("With dynamic programming");
            Fibonacci_DynamicProgramming(1000000);
        }
    }
}
