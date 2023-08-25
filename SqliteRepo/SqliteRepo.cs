using HamDotNetToolkit;
using SqliteRepo;

namespace SqlServerRepo
{
    public class SqliteRepo : IQSORepo
    {
        protected QsoSqliteContext? context = null;
        public void CreateContext(string connectionString)
        {
            if (context is not null)
                return;
            context = new QsoSqliteContext(connectionString);

        }
        public void CreateContext()
        {
            if (context is not null)
                return;
            context = new QsoSqliteContext("Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadio");
        }
        public bool AddQso(Qso qso, bool defer = true)
        {
            try
            {
                if (qso is null || qso.Call.IsNullOrEmpty())
                    throw new NullReferenceException("You can't create a QSO without a call or the QSO is null");

                context?.Add(qso);
                if (defer == false)
                    context?.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Add error {e.Message}");
                return false;
            }

        }
        public void SaveChanges()
        {
            context?.SaveChanges();
        }


        public bool DeleteCallsByQso(Qso qso, bool defer = true)
        {
            try
            {
                context?.Remove(qso);
                if (defer == false)
                    context?.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Delete error {e.Message}");
                return false;
            }
        }

        public List<Qso>? GetAllQsos()
        {
            try
            {
                var qslList = context?.Qsos.ToList();
                return qslList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"All Query {e.Message}");
                return null;
            }
        }

        public List<Qso>? GetQsoByCall(string callSign)
        {
            try
            {
                var qslList = context?.Qsos.Where(q => q.Call == callSign).ToList();
                return qslList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Call sign Query {e.Message}");
                return null;
            }
        }

        public Qso? GetQsoById(string id)
        {
            try
            {
                var qso = context?.Qsos.Where(q => q.Id == id).FirstOrDefault();
                return qso;
            }
            catch (Exception e)
            {
                Console.WriteLine($"By Id Query {e.Message}");
                return null;
            }
        }
    }
}
