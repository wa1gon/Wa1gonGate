using AdifLib;
using HamDotNetToolkit;

namespace AdifTest
{
    [TestClass]
    public class AdifTest
    {
        //private const string TestFilePath = @"../../../data/AClogADIF.adi";
        private const string TestFilePath = @"../../../data/FullACLogADIF.adi";
        private const string TestOutFilePath = @"../../../data/adifout.adi";
        private const string TestJsonFilePath = @"../../../data/adifJsonOut.json";
        [TestMethod]
        public void ShouldReadAdifFile()
        {

            // Arrange
            //List<Dictionary<string, string>> expectedAdifRecords = new List<Dictionary<string, string>>();


            // Act
            (Dictionary<string, string> header, List<Qso> actualAdifRecords) = AdifReader.ReadAdifFromFile(TestFilePath);

            // Assert
            Assert.AreEqual(54121, actualAdifRecords.Count);



            //for (int i = 0; i < expectedAdifRecords.Count; i++)
            //{
            //    CollectionAssert.AreEqual(expectedAdifRecords[i], actualAdifRecords[i]);
            //}
        }

        [TestMethod]
        public void ShouldWriteAdifFile()
        {

            // Arrange
            List<Dictionary<string, string>> expectedAdifRecords = new List<Dictionary<string, string>>();


            // Act
            (Dictionary<string, string> header, List<Qso> actualAdifRecords) = AdifReader.ReadAdifFromFile(TestFilePath);

            // Assert
            Assert.AreEqual(54121, actualAdifRecords.Count);

            AdifWriter.WriteAdifToFile(TestOutFilePath, actualAdifRecords);

            (Dictionary<string, string> header2, List<Qso> actualAdifRecords2) = AdifReader.ReadAdifFromFile(TestFilePath);
            Assert.AreEqual(actualAdifRecords2.Count, actualAdifRecords2.Count);
            //AdifWriter.WriteAdifToJsonFile(TestJsonFilePath,)
        }


        [TestMethod]
        public void ShouldWriteAndReadJsonFile()
        {

            // Arrange
            List<Dictionary<string, string>> expectedAdifRecords = new List<Dictionary<string, string>>();


            // Act
            (Dictionary<string, string> header, List<Qso> orignalAdifRecords) =
                AdifReader.ReadAdifFromFile(TestFilePath);

            // Assert

            foreach (var qso in orignalAdifRecords)
            {
                if (qso.Call.IsNullOrEmpty())
                {
                    Assert.IsNull(qso.Call);
                }
            }

            AdifWriter.WriteAdifToJsonFile(TestJsonFilePath, orignalAdifRecords, header);
            var actualAdifRecordsJ = AdifReader.ReadAdifFromJsonFile(TestJsonFilePath);
            Assert.IsNotNull(actualAdifRecordsJ);
            // compare if something goes wrong.
            //AdifComparer.CompareQSOLists(orignalAdifRecords, actualAdifRecordsJ);
            int counter = 0;
            foreach (var qso in actualAdifRecordsJ)
            {

                if (qso.Call.IsNullOrEmpty() == true)
                {
                    Console.WriteLine($"Null call found at location {counter}");
                }
                counter++;
            }

            Assert.AreEqual(orignalAdifRecords.Count, actualAdifRecordsJ.Count);
        }
        [DataTestMethod]
        [DataRow("20230828", "103000", "2023-08-28 10:30:00")]
        [DataRow("20211231", "2359", "2021-12-31 23:59:00")]
        public void ShouldCombineDateAndTime(string datePart, string timePart, string expectDateTimeString)
        {
            var newDtg = AdifReader.CombineDateTime(datePart, timePart);
            Assert.AreEqual(expectDateTimeString, newDtg.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}

