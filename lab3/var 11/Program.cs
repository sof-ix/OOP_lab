using System;

namespace Lab2Var11
{
    class Program
    {
        public static void Main()
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double a = 0.1;
            double b = 1.0;
            double eps = 0.0001;
            int k = 9;
            int n = 10;

            double h = (b - a) / k;

            for (int i = 0; i<= k; i++)
            {
                double x = a + i * h;
                double first_sg = 1;
                double sn = 0;

                for (int j = 0; j <= n; j++)
                {
                    sn += first_sg;
                    first_sg = first_sg * (2 * j + 3) / (2 * j + 1) * x * x / (j + 1);
                }

                first_sg = 1;
                double se = 0;
                int m = 0;

                while (Math.Abs(first_sg) >= eps)
                {
                    se += first_sg;
                    first_sg = first_sg * (2 * m + 3) / (2 * m + 1) * x * x / (m + 1);
                    m++;
                }

                double y = (1 + 2 * x * x) * Math.Exp(x * x);

                Console.WriteLine($"X={x:F4}\tSN={sn:F6}\tSE={se:F6}\tY={y:F6}");
            }
        }
    }
}