using HamDotNetToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamDotNetToolkit.Test;

[TestClass]
public class NumericExtensionsTests
{
    [TestMethod]
    public void TestParseOrDefault_Int_Success()
    {
        string input = "123";
        int result = input.ParseOrDefault<int>();
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void TestParseOrDefault_Decimal_Success()
    {
        string input = "123.45";
        decimal result = input.ParseOrDefault<decimal>();
        Assert.AreEqual(123.45m, result);
    }

    [TestMethod]
    public void TestParseOrDefault_Float_Success()
    {
        string input = "123.45";
        float result = input.ParseOrDefault<float>();
        Assert.AreEqual(123.45f, result);
    }

    [TestMethod]
    public void TestParseOrDefault_InvalidInput_DefaultValue()
    {
        string input = "abc"; // Invalid input

        int intValue = input.ParseOrDefault<int>(); // Default: 0
        decimal decimalValue = input.ParseOrDefault<decimal>(); // Default: 0.0
        float floatValue = input.ParseOrDefault<float>(); // Default: 0.0

        Assert.AreEqual(0, intValue);
        Assert.AreEqual(0.0m, decimalValue);
        Assert.AreEqual(0.0f, floatValue);
    }
}
