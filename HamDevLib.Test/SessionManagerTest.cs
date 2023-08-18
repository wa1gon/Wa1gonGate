//Needs to be moved to loggate test

//namespace HamDotNetToolkit.Test;
//using HamDotNetToolkit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.IO;

//[TestClass]
//public class SettingManagerTests
//{
//    private string _testFileName;

//    [TestInitialize]
//    public void TestInitialize()
//    {
//        _testFileName = "test_settings.json";
//    }

//    [TestCleanup]
//    public void TestCleanup()
//    {
//        if (File.Exists(_testFileName))
//        {
//            File.Delete(_testFileName);
//        }
//    }

//    [TestMethod]
//    public void GetSetting_ExistingKey_ReturnsValue()
//    {
//        // Arrange
//        var settingManager = new SettingManager();
//        settingManager.SetSetting("Username", "john_doe");

//        // Act
//        string result = settingManager.GetSetting("Username");

//        // Assert
//        Assert.AreEqual("john_doe", result);
//    }

//    [TestMethod]
//    public void GetSetting_NonExistingKey_ReturnsNull()
//    {
//        // Arrange
//        var settingManager = new SettingManager(_testFileName);

//        // Act
//        string result = settingManager.GetSetting("NonExistingKey");

//        // Assert
//        Assert.IsNull(result);
//    }

//    [TestMethod]
//    public void SetSetting_ExistingKey_UpdatesValue()
//    {
//        // Arrange
//        var settingManager = new SettingManager(_testFileName);
//        settingManager.SetSetting("Username", "john_doe");

//        // Act
//        settingManager.SetSetting("Username", "new_username");

//        // Assert
//        string result = settingManager.GetSetting("Username");
//        Assert.AreEqual("new_username", result);
//    }

//    [TestMethod]
//    public void SetSetting_NewKey_AddsKeyValue()
//    {
//        // Arrange
//        var settingManager = new SettingManager(_testFileName);

//        // Act
//        settingManager.SetSetting("NewKey", "new_value");

//        // Assert
//        string result = settingManager.GetSetting("NewKey");
//        Assert.AreEqual("new_value", result);
//    }
//}

