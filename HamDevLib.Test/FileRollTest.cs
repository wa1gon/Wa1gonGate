using HamDotNetToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamDevLib.Test;
[TestClass]
public class FileRollTest
{
    private const string TestDirectory = @"C:\Temp\TestRolling";
    private const string BaseFileName = "testfile.txt";

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        Directory.CreateDirectory(TestDirectory);
        File.Create(Path.Combine(TestDirectory, BaseFileName)).Close();
    }
    [TestMethod]
    public void TestRollingAndCleanup()
    {
        int maxRolls = 2; // Maximum number of rolled files to keep
        FileToolkit.RollandCleanupFiles(Path.Combine(TestDirectory, BaseFileName), maxRolls);

        // Check if the rolled file was created
        Assert.IsTrue(File.Exists(Path.Combine(TestDirectory, "testfile_1.txt")));

        // Check if the old file was deleted
        Assert.IsFalse(File.Exists(Path.Combine(TestDirectory, "testfile.txt")));

        // Check if the cleanup was performed as expected
        Assert.IsFalse(File.Exists(Path.Combine(TestDirectory, "testfile_0.txt")));
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Directory.Delete(TestDirectory, true);
    }
}
