using BasicMath;
using CsvHelper;
using System.Globalization;
using Xunit;
namespace BasicMathCsvTests;
public class CalculatorCsvTests
{
    public class Row
    {
        public double A {get;set;}
        public double B {get;set;}
        public double Expected {get;set;}
    }
    private static IEnumerable<object[]> Read(string file)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Data", file);
        using var stream = new StreamReader(path);
        using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);
        foreach (var row in csv.GetRecords<Row>())
            yield return new object[] { row.A, row.B, row.Expected };
    }
    public static IEnumerable<object[]> AddData() => Read("add_testdata.csv");
    public static IEnumerable<object[]> SubtractData() => Read("subtract_testdata.csv");
    public static IEnumerable<object[]> MultiplyData() => Read("multiply_testdata.csv");
    public static IEnumerable<object[]> DivideData() => Read("divide_testdata.csv");
    [Theory, MemberData(nameof(AddData))]
    public void Add_FromCsv(double a,double b,double expected)
        => Assert.Equal(expected, new BasicMaths().Add(a,b), 7);
    [Theory, MemberData(nameof(SubtractData))]
    public void Subtract_FromCsv(double a,double b,double expected)
        => Assert.Equal(expected, new BasicMaths().Subtract(a,b), 7);
    [Theory, MemberData(nameof(MultiplyData))]
    public void Multiply_FromCsv(double a,double b,double expected)
        => Assert.Equal(expected, new BasicMaths().Multiply(a,b), 7);
    [Theory, MemberData(nameof(DivideData))]
    public void Divide_FromCsv(double a,double b,double expected)
        => Assert.Equal(expected, new BasicMaths().Divide(a,b), 7);
}
