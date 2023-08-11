using HamDevLib;
using System.Reflection;
using static HamDevLib.IRigControl;

[assembly: Parallelize(Workers = 1, Scope = ExecutionScope.MethodLevel)]

namespace HamlibTest
{
    [TestClass]
    public class CatTest
    {
        //[TestMethod]
        //public async void ShouldReadFreqAfterSetting()
        //{
        //    string rc;
        //    HamLibClient sut = new HamLibClient();
        //    sut.Connect("127.0.0.1", 6000);
        //    rc = await sut.SendCommand("F14250000");
        //    rc = await  sut.SendCommand("f");
        //    Assert.AreEqual("14250000",rc);
        //}
        [TestMethod]
        public void ShouldConnect()
        {
            IRigControl? sut1 = new HamLibClient();
            sut1.Connect("127.0.0.1", 6000);
            Assert.AreEqual(true, sut1.IsConnected);
            sut1.Disconnect();
        }

        [TestMethod]
        public void TestSendCommandAsync()
        {
            // Arrange
            using (IRigControl? sut2 = new HamLibClient())
            {
                sut2.Connect("localhost", 4532);
                string? rc = sut2.SendCommand("F 21300000\n");

                // Act
                string? response = sut2.SendCommand("f\n");

                // Assert
                Assert.AreEqual("21300000", response);
                Assert.IsFalse(response?.Contains("ERROR"));
                sut2.Disconnect();
            }
        }

        [TestMethod]
        public void TestSetAndGetFrequency()
        {
            using (IRigControl? sut3 = new HamLibClient())
            {
                // Arrange
                sut3.Connect("localhost", 4532);
                ErrorCode? rc = sut3.SetFrequency(21300000);

                Assert.AreEqual(ErrorCode.Success, rc);

                // Act
                (ErrorCode code, long response) = sut3.GetFrequency();

                // Assert
                Assert.AreEqual(ErrorCode.Success, code);
                Assert.AreEqual(21300000, response);
                sut3.Disconnect();
            }
        }
        [TestMethod]
        public void TestSetAndGetRit()
        {
            using (IRigControl? sut3 = new HamLibClient())
            {
                // Arrange
                sut3.Connect("localhost", 4532);
                ErrorCode? rc = sut3.SetRit(200);

                Assert.AreEqual(ErrorCode.Success, rc);

                // Act
                (ErrorCode code, long response) = sut3.GetRit();

                // Assert
                Assert.AreEqual(ErrorCode.Success, code);
                Assert.AreEqual(200, response);
                sut3.Disconnect();
            }
        }
        [TestMethod]
        public void TestSetAndGetXit()
        {
            using (IRigControl? sut3 = new HamLibClient())
            {
                // Arrange
                sut3.Connect("localhost", 4532);
                ErrorCode? rc = sut3.SetXit(200);

                Assert.AreEqual(ErrorCode.Success, rc);

                // Act
                (ErrorCode code, long response) = sut3.GetXit();

                // Assert
                Assert.AreEqual(ErrorCode.Success, code);
                Assert.AreEqual(200, response);
                sut3.Disconnect();
            }
        }

        [TestMethod]
        public void TestSetAndGetMode()
        {
            // Arrange
            using (IRigControl? sut3 = new HamLibClient())
            {
                sut3.Connect("localhost", 4532);
                ErrorCode? rc = sut3.SetMode("USB", HamLibClient.PassbandDefault);

                Assert.AreEqual(ErrorCode.Success, rc);


                // Act
                (ErrorCode code, string response) = sut3.GetMode();

                // Assert
                Assert.AreEqual("USB", response);
                sut3.Disconnect();
            }
        }

        [TestMethod]
        public void TestSetAndGetVfo()
        {
            // Arrange

            using (IRigControl? sut3 = new HamLibClient())
            {
                sut3.Connect("localhost", 4532);
                ErrorCode? rc = sut3.SetVfo("VFOA");

                Assert.AreEqual(ErrorCode.Success, rc);

                // Act
                (ErrorCode code, string response) = sut3.GetVfo();

                // Assert
                Assert.AreEqual("VFOA", response);
                sut3.Disconnect();
            }
        }

        [TestMethod]
        public void TestValidMode()
        {
            // Arrange
            using (IRigControl? sut4 = new HamLibClient())
            {
                sut4.Connect("localhost", 4532);
                var vm = sut4.GetValidModes();

                Assert.IsTrue(vm.Contains("LSB"));
                sut4.Disconnect();
            }
        }
        [TestMethod]
        public async Task TestGetErrorCode()
        {
            MethodInfo? getErrorCodeMethod = typeof(HamLibClient).GetMethod("GetErrorCode", BindingFlags.NonPublic | BindingFlags.Static);


            Assert.AreEqual(ErrorCode.Success, getErrorCodeMethod?.Invoke(null, new object[] { "RPRT 0" }));
            Assert.AreEqual(ErrorCode.Unsupported, getErrorCodeMethod?.Invoke(null, new object[] { "RPRT 1" }));
            Assert.AreEqual(ErrorCode.InvalidParams, getErrorCodeMethod?.Invoke(null, new object[] { "RPRT 2" }));
            Assert.AreEqual(ErrorCode.NoResponse, getErrorCodeMethod?.Invoke(null, new object[] { "RPRT 3" }));
            Assert.AreEqual(ErrorCode.Timeout, getErrorCodeMethod?.Invoke(null, new object[] { "RPRT 4" }));
            Assert.AreEqual(ErrorCode.Unspecified, getErrorCodeMethod?.Invoke(null, new object[] { "RPRT 5" }));
            Assert.AreEqual(ErrorCode.Unspecified, getErrorCodeMethod?.Invoke(null, new object[] { "RPRT -1" }));
        }
    }
}