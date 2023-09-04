using AdifLib;
using HamDotNetToolkit;

namespace AdifTest
{
    [TestClass]
    public class AppPatternMatchingTest
    {

        [DataTestMethod]
        [DataRow("app_ABCD_guid", true)]
        [DataRow("App_XYZ_Guid", true)]
        [DataRow("my_app_123_guid", false)]
        [DataRow("app_guid", false)]
        public void TestDetectPattern(string input, bool expectedResult)
        {
            // Act
            bool isMatch = QsoTracking.DetectAppFieldName(input);

            // Assert
            Assert.AreEqual(expectedResult, isMatch);
        }

        [DataTestMethod]
        [DataRow("app_ABCD_guid", "guid", true)]
        [DataRow("App_XYZ_Guid", "Guid",  true)]
        [DataRow("my_app_123_guid", "custom_pattern", false)]
        public void TestStripAllExceptPattern(string input, string pattern, bool expected)
        {
            // Act
            string strippedPattern = QsoTracking.ExtractFieldName(input, pattern);

            // Assert
            if (expected == true)
                Assert.AreEqual(pattern, strippedPattern);
            else
                Assert.AreNotEqual(pattern, string.Empty);
        }
    }
}

