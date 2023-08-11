
using HamDevLib;
using SqlServerRepo;

namespace DatabaseRepoTest;


[TestClass]
public class SqlRepoTest
{

    private const string TestFilePath = @"C:\github\wa1gonsuite\Wa1gonSuite\AdifTest\data\FullAClogADIF.adi";
    private const string connStr = "Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadioTest";

    [TestCleanup]
    public void TestCleanup()
    {
        // Teardown code here (runs after each test method)
    }
    [TestMethod]
    public void ShouldGetAllQsos()
    {
        IQSORepo sut = new SqlRepo();
        sut.CreateContext(connStr);

        var foo = sut.GetAllQsos();
        //Assert.IsNotNull(foo);
        //Assert.AreEqual(54121, foo.Count);
    }
    [TestMethod]
    public void ShouldGetAllk5erQsos()
    {
        IQSORepo sut = new SqlRepo();
        sut.CreateContext(connStr);

        var foo = sut.GetQsoByCall("K5er");
        Assert.IsNotNull(foo);
        Assert.AreEqual(24, foo.Count);
    }
    [TestMethod]
    public void ShouldRemoveQsos()
    {
        IQSORepo sut = new SqlRepo();
        sut.CreateContext(connStr);

        var foo = sut.GetAllQsos().Skip(100).Take(20).ToList();
        var qso = foo[5];
        sut.DeleteCallsByQso(qso, false);
        var deleteItem = sut.GetQsoById(qso.Id);
        Assert.IsNull(deleteItem);
    }
    [TestMethod]
    public void ShouldUpdateQsos()
    {
        IQSORepo sut = new SqlRepo();
        sut.CreateContext(connStr);

        var foo = sut.GetAllQsos().Skip(100).Take(20).ToList();
        var qso = foo[5];

        var now = DateTime.Now;
        qso.QsoDate = now;
        sut.SaveChanges();

        var updatedItem = sut.GetQsoById(qso.Id);
        Assert.AreEqual(updatedItem.QsoDate, now);
    }

}