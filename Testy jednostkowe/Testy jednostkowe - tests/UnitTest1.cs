using System;
using Xunit;
using QuadraticEquationSolver;

namespace Testy_jednostkowe___tests
{
    public class QuadraticEquationSolverTests
    {
        /// <summary>
        /// Testy sprawdzaj�ce przypadek, gdy wyr�nik (b^2 - 4ac) jest ujemny
        /// i nie ma pierwiastk�w rzeczywistych.
        /// </summary>
        /// <param name="a">Wsp�czynnik a</param>
        /// <param name="b">Wsp�czynnik b</param>
        /// <param name="c">Wsp�czynnik c</param>
        [Theory]
        [InlineData(1, 0, 1)]    // b^2 - 4ac = 0 - 4 = -4  -> brak pierwiastk�w
        [InlineData(2, 2, 2)]    // 4 - 16 = -12            -> brak pierwiastk�w
        [InlineData(1, 1, 2)]    // 1 - 8 = -7              -> brak pierwiastk�w
        public void CalculateQuadraticRoots_NoRealSolutions_ReturnsEmpty(double a, double b, double c)
        {
            // Act
            var roots = Program.CalculateQuadraticRoots(a, b, c);

            // Assert
            Assert.NotNull(roots);
            Assert.Empty(roots);
        }

        /// <summary>
        /// Testy sprawdzaj�ce przypadek, gdy wyr�nik (b^2 - 4ac) wynosi 0,
        /// co oznacza dok�adnie jeden pierwiastek rzeczywisty.
        /// </summary>
        /// <param name="a">Wsp�czynnik a</param>
        /// <param name="b">Wsp�czynnik b</param>
        /// <param name="c">Wsp�czynnik c</param>
        /// <param name="expectedRoot">Oczekiwany pierwiastek</param>
        [Theory]
        [InlineData(1, 2, 1, -1)]     // delta = 4 - 4 = 0 -> x = -b / 2a = -1
        [InlineData(4, 4, 1, -0.5)]   // delta = 16 - 16 = 0 -> x = -4 / 8 = -0.5
        public void CalculateQuadraticRoots_OneRealSolution_ReturnsSingleRoot(
            double a, double b, double c, double expectedRoot)
        {
            // Act
            var roots = Program.CalculateQuadraticRoots(a, b, c);

            // Assert
            Assert.Single(roots);
            Assert.Equal(expectedRoot, roots[0], precision: 5);
            // precision: 5 -> dopuszczalna r�nica rz�du 10^-5
        }

        /// <summary>
        /// Testy sprawdzaj�ce przypadek, gdy wyr�nik (b^2 - 4ac) jest dodatni
        /// i istniej� dwa r�ne pierwiastki rzeczywiste.
        /// </summary>
        /// <param name="a">Wsp�czynnik a</param>
        /// <param name="b">Wsp�czynnik b</param>
        /// <param name="c">Wsp�czynnik c</param>
        /// <param name="expectedRoot1">Oczekiwany pierwszy pierwiastek</param>
        /// <param name="expectedRoot2">Oczekiwany drugi pierwiastek</param>
        [Theory]
        [InlineData(1, -3, 2, 2, 1)]    // delta = 9 - 8 = 1 -> x1=2, x2=1
        [InlineData(1, 5, 6, -2, -3)]   // delta = 25 - 24 = 1 -> x1=-2, x2=-3
        public void CalculateQuadraticRoots_TwoRealSolutions_ReturnsTwoDistinctRoots(
            double a, double b, double c,
            double expectedRoot1, double expectedRoot2)
        {
            // Act
            var roots = Program.CalculateQuadraticRoots(a, b, c);

            // Assert
            Assert.NotNull(roots);
            Assert.Equal(2, roots.Length);

            // Je�eli kolejno�� pierwiastk�w jest istotna, sprawdzamy wprost:
            Assert.Equal(expectedRoot1, roots[0], precision: 5);
            Assert.Equal(expectedRoot2, roots[1], precision: 5);

            // Alternatywnie mo�na sprawdza�, czy oba spodziewane pierwiastki wyst�puj�
            // bez za�o�enia o kolejno�ci (np. metodami Assert.Contains z warunkami).
        }

        /// <summary>
        /// (Dodatkowy test) Sprawdza, czy metoda rzuca wyj�tek ArgumentException,
        /// gdy a = 0 (w�wczas to nie jest r�wnanie kwadratowe).
        /// </summary>
        /// <param name="a">Wsp�czynnik a</param>
        /// <param name="b">Wsp�czynnik b</param>
        /// <param name="c">Wsp�czynnik c</param>
        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(0, 5, -1)]
        public void CalculateQuadraticRoots_AIsZero_ThrowsArgumentException(double a, double b, double c)
        {
            // Assert
            Assert.Throws<ArgumentException>(() => Program.CalculateQuadraticRoots(a, b, c));
        }
    }
}
