

namespace HamDotNetToolkit
{
    public interface IQSORepo
    {
        List<Qso>? GetQsoByCall(string callSign);
        List<Qso>? GetAllQsos();
        Qso? GetQsoById(string id);
        bool DeleteCallsByQso(Qso qso, bool defer);
        //bool UpdateByQso(Qso qso);
        bool AddQso(Qso qso, bool defer = true);
        void CreateContext(string connectionString);
        void SaveChanges();
    }
}
