namespace HamDevLib.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidationsTests
    {
        [DataTestMethod]
        [DataRow("W1A", true)]     // 1x1 call sign
        [DataRow("N4AB", true)]    // 2x1 call sign
        [DataRow("KD5XYZ", true)]  // 2x3 call sign
        [DataRow("W100", false)]    // Special event call sign
        [DataRow("K2B", true)]     // 1x2 call sign
        [DataRow("WABC", false)]   // Too short
        [DataRow("AB1", false)]    // Too short
        [DataRow("W12345", false)] // Too long
        [DataRow("W123", false)]   // 2x3 format but too short
        [DataRow("W1AB", true)]   // 2x1 format but too short
        [DataRow("KAB1", false)]   // Invalid format
        //[DataRow("W12C", false)]   // Invalid format
        [DataRow("WABC1", false)]  // Invalid format
        //[DataRow("W123AB", false)] // Invalid format
        public void TestCallSignValidation(string callSign, bool expectedResult)
        {
            bool actualResult = Validations.ValidateCallSign(callSign);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }

}
