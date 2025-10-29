/*
 * Suite de pruebas completa - demostrando testeabilidad
 * Usar xUnit para ejecutar: dotnet test
 */

using Xunit;
using MaintainabilityTestability.Good;

namespace GoodCalculator.Tests
{
    /// <summary>
    /// Probar operaciones matemáticas puras
    /// </summary>
    public class OperationsTests
    {
        [Fact]
        public void Add_ShouldReturnCorrectSum()
        {
            Assert.Equal(8, Operations.Add(5, 3));
            Assert.Equal(-2, Operations.Add(-5, 3));
            Assert.Equal(0, Operations.Add(0, 0));
        }

        [Fact]
        public void Subtract_ShouldReturnCorrectDifference()
        {
            Assert.Equal(6, Operations.Subtract(10, 4));
            Assert.Equal(-6, Operations.Subtract(4, 10));
        }

        [Fact]
        public void Multiply_ShouldReturnCorrectProduct()
        {
            Assert.Equal(42, Operations.Multiply(6, 7));
            Assert.Equal(0, Operations.Multiply(5, 0));
        }

        [Fact]
        public void Divide_ShouldReturnCorrectQuotient()
        {
            Assert.Equal(4, Operations.Divide(20, 5));
            Assert.Equal(3.333, Operations.Divide(10, 3), 2);
        }

        [Fact]
        public void Divide_ByZero_ShouldThrowException()
        {
            Assert.Throws<DivideByZeroException>(() => Operations.Divide(10, 0));
        }

        [Fact]
        public void GetOperation_WithValidOperator_ShouldReturnFunction()
        {
            var addFunc = Operations.GetOperation('+');
            Assert.Equal(8, addFunc(5, 3));
        }

        [Fact]
        public void GetOperation_WithInvalidOperator_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => Operations.GetOperation('%'));
        }
    }

    /// <summary>
    /// Probar parseo de expresiones
    /// </summary>
    public class OperationParserTests
    {
        [Fact]
        public void Parse_Addition_ShouldReturnCorrectComponents()
        {
            var (a, op, b) = OperationParser.Parse("5 + 3");
            Assert.Equal(5, a);
            Assert.Equal('+', op);
            Assert.Equal(3, b);
        }

        [Fact]
        public void Parse_Subtraction_ShouldReturnCorrectComponents()
        {
            var (a, op, b) = OperationParser.Parse("10 - 4");
            Assert.Equal(10, a);
            Assert.Equal('-', op);
            Assert.Equal(4, b);
        }

        [Fact]
        public void Parse_WithWhitespace_ShouldHandleCorrectly()
        {
            var (a, op, b) = OperationParser.Parse("  15  *  3  ");
            Assert.Equal(15, a);
            Assert.Equal('*', op);
            Assert.Equal(3, b);
        }

        [Fact]
        public void Parse_InvalidExpression_ShouldThrowException()
        {
            // Demasiados operadores
            Assert.Throws<ArgumentException>(() => OperationParser.Parse("5 + 3 + 2"));
        }

        [Fact]
        public void Parse_InvalidNumbers_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => OperationParser.Parse("abc + def"));
        }

        [Fact]
        public void Parse_NoOperator_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => OperationParser.Parse("123"));
        }
    }

    /// <summary>
    /// Probar integración de calculadora
    /// </summary>
    public class CalculatorTests
    {
        private readonly Calculator _calc;

        public CalculatorTests()
        {
            _calc = new Calculator();
        }

        [Fact]
        public void Calculate_Addition_ShouldReturnCorrectResult()
        {
            var result = _calc.Calculate("5 + 3");
            Assert.Equal(8, result);
        }

        [Fact]
        public void Calculate_Subtraction_ShouldReturnCorrectResult()
        {
            var result = _calc.Calculate("10 - 4");
            Assert.Equal(6, result);
        }

        [Fact]
        public void Calculate_Multiplication_ShouldReturnCorrectResult()
        {
            var result = _calc.Calculate("6 * 7");
            Assert.Equal(42, result);
        }

        [Fact]
        public void Calculate_Division_ShouldReturnCorrectResult()
        {
            var result = _calc.Calculate("20 / 5");
            Assert.Equal(4, result);
        }

        [Theory]
        [InlineData("5 + 3", 8)]
        [InlineData("10 - 4", 6)]
        [InlineData("6 * 7", 42)]
        [InlineData("20 / 5", 4)]
        public void Calculate_VariousExpressions_ShouldReturnCorrectResults(string expr, double expected)
        {
            var result = _calc.Calculate(expr);
            Assert.Equal(expected, result);
        }
    }

    /// <summary>
    /// Probar calculadora con historial
    /// </summary>
    public class CalculatorWithHistoryTests
    {
        [Fact]
        public void Calculate_ShouldTrackHistory()
        {
            var calc = new CalculatorWithHistory();
            
            calc.Calculate("5 + 3");
            calc.Calculate("10 - 4");

            var history = calc.GetHistory();
            Assert.Equal(2, history.Count);
            Assert.Equal(8, history[0].Result);
            Assert.Equal(6, history[1].Result);
        }

        [Fact]
        public void GetHistory_ShouldReturnCopy()
        {
            var calc = new CalculatorWithHistory();
            calc.Calculate("5 + 3");
            
            var history1 = calc.GetHistory();
            var history2 = calc.GetHistory();

            // Deben ser iguales pero no el mismo objeto
            Assert.Equal(history1.Count, history2.Count);
            Assert.NotSame(history1, history2);
        }

        [Fact]
        public void Calculate_ShouldIncludeTimestamp()
        {
            var calc = new CalculatorWithHistory();
            var before = DateTime.Now;
            
            var result = calc.Calculate("5 + 3");
            
            var after = DateTime.Now;
            Assert.InRange(result.Timestamp, before, after);
        }

        [Fact]
        public void Calculate_ShouldStoreExpression()
        {
            var calc = new CalculatorWithHistory();
            var result = calc.Calculate("5 + 3");
            
            Assert.Equal("5 + 3", result.Expression);
            Assert.Equal(8, result.Result);
        }
    }
}
