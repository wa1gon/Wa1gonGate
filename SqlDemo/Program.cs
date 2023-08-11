// See https://aka.ms/new-console-template for more information
using AdifLib;
using DatabaseRepo.SqlServer;
using HamDevLib;

string databaseName = "AmateurRadioTest";
using (QsoContext context = new QsoContext($"Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = {databaseName}"))
//using (QsoContext context = new QsoContext("Data Source = Spock\\sqlexpress\\B; Initial Catalog = AmateurRadio"))
{
    context.Database.EnsureCreated();
}
//GetQsos();
AddQso();
//GetQsos();

void GetQsos()
{
    using var context = new QsoContext($"Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = {databaseName}");
    //using var context = new QsoContext("Data Source = Spock\\sqlexpress\\B; Initial Catalog = AmateurRadio");
    var qsos = context.Qsos.ToList();
    foreach (var qso in qsos)
    {
        Console.WriteLine(qso.Call);
    }
}
void AddQso()
{
    Console.WriteLine($"Start time: {DateTime.Now}");
    var rc = AdifReader.ReadAdifFromFile("C:/tmp/adifdata/FullAClogAdif.adi");
    using (QsoContext context = new QsoContext($"Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = {databaseName}"))
    //using (QsoContext context = new QsoContext("Data Source = Spock\\sqlexpress\\B; Initial Catalog = AmateurRadio"))
    {
        context.Database.EnsureCreated();

        foreach (var qso in rc.qsoList)
        {
            if (qso.Id.IsNullOrEmpty())
                qso.Id = Guid.NewGuid().ToString();
            context.Add(qso);
        }
        context.SaveChanges();
    }
    Console.WriteLine($"End time: {DateTime.Now}");

    //var qso = new Qso { Call = "KB1ETC", Mode = "USB", TimeOn = DateTime.Now, QsoDate = DateTime.Now };
    //qso.QsoDetails.Add(new QsoDetail() { Name = "name", Value = "Susan Wagoner" });
    //qso.QsoDetails.Add(new QsoDetail() { Name = "freq", Value = "14.150" });
    //var id = qso.GetSHA256Hash();
    //qso.Id = id;
    //using var context = new QsoContext();
    //context.Qsos.Add(qso);
    //context.SaveChanges();

}
