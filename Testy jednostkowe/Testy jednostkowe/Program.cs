using System;

namespace QuadraticEquationSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Podaj współczynnik a: ");
                double a = double.Parse(Console.ReadLine());

                Console.Write("Podaj współczynnik b: ");
                double b = double.Parse(Console.ReadLine());

                Console.Write("Podaj współczynnik c: ");
                double c = double.Parse(Console.ReadLine());

                double[] roots = CalculateQuadraticRoots(a, b, c);

                switch (roots.Length)
                {
                    case 0:
                        Console.WriteLine("Brak pierwiastków rzeczywistych.");
                        break;
                    case 1:
                        Console.WriteLine($"Jeden pierwiastek rzeczywisty: x = {roots[0]}");
                        break;
                    case 2:
                        Console.WriteLine($"Dwa pierwiastki rzeczywiste: x1 = {roots[0]}, x2 = {roots[1]}");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Błąd formatu danych wejściowych. Upewnij się, że wprowadzasz liczby.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił nieoczekiwany błąd: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
                Console.ReadKey();
            }
        }


        public static double[] CalculateQuadraticRoots(double a, double b, double c)
        {
            if (Math.Abs(a) < double.Epsilon)
            {
                throw new ArgumentException("Współczynnik 'a' nie może być zerem, ponieważ nie jest to równanie kwadratowe.");
            }

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                // Brak pierwiastków rzeczywistych
                return Array.Empty<double>();
            }
            else if (Math.Abs(discriminant) < double.Epsilon)
            {
                // Jeden pierwiastek (delta = 0)
                double x = -b / (2 * a);
                return new double[] { x };
            }
            else
            {
                // Dwa pierwiastki
                double sqrtDiscriminant = Math.Sqrt(discriminant);
                double x1 = (-b + sqrtDiscriminant) / (2 * a);
                double x2 = (-b - sqrtDiscriminant) / (2 * a);
                return new double[] { x1, x2 };
            }
        }
    }
}
