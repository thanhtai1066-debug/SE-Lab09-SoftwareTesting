using BasicMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BasicMathTests;
[TestClass]
public class BasicMathsTests
{
    private readonly BasicMaths math = new();

    [DataTestMethod]
    [DataRow(1d, 1d, 2d)]
    [DataRow(-1d, -1d, -2d)]
    [DataRow(0d, 0d, 0d)]
    [DataRow(2147483647d, 1d, 2147483648d)]
    [DataRow(-2147483648d, -1d, -2147483649d)]
    public void Add_EP_BVA(double a, double b, double expected)
        => Assert.AreEqual(expected, math.Add(a,b), 0.000001);

    [DataTestMethod]
    [DataRow(5d, 3d, 2d)]
    [DataRow(-5d, -3d, -2d)]
    [DataRow(0d, 0d, 0d)]
    [DataRow(-2147483648d, 1d, -2147483649d)]
    [DataRow(2147483647d, -1d, 2147483648d)]
    public void Subtract_EP_BVA(double a, double b, double expected)
        => Assert.AreEqual(expected, math.Subtract(a,b), 0.000001);

    [DataTestMethod]
    [DataRow(2d, 3d, 6d)]
    [DataRow(-2d, -3d, 6d)]
    [DataRow(0d, 5d, 0d)]
    [DataRow(2147483647d, 1d, 2147483647d)]
    public void Multiply_EP_BVA(double a, double b, double expected)
        => Assert.AreEqual(expected, math.Multiply(a,b), 0.000001);

    [DataTestMethod]
    [DataRow(6d, 3d, 2d)]
    [DataRow(-6d, -3d, 2d)]
    [DataRow(0d, 1d, 0d)]
    [DataRow(2147483647d, 1d, 2147483647d)]
    public void Divide_EP_BVA(double a, double b, double expected)
        => Assert.AreEqual(expected, math.Divide(a,b), 0.000001);

    [TestMethod]
    public void Divide_ByZero_Throws()
        => Assert.ThrowsException<DivideByZeroException>(() => math.Divide(10,0));
}
