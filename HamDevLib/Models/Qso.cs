using System.Security.Cryptography;
using System.Text;

namespace HamDevLib;

public class Qso
{
    public string Id { get; set; } = string.Empty;
    public DateTime QsoDate { get; set; }
    public string Call { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string RstSent { get; set; } = string.Empty;
    public string RstRcvd { get; set; } = string.Empty;
    public string QSLSent { get; set; } = string.Empty;
    public string QSLRcvd { get; set; } = string.Empty;
    public decimal Freq { get; set; } = decimal.Zero;
    public decimal FreqRx { get; set; } = decimal.Zero;
    public string State { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;


    public virtual ICollection<QsoDetail> QsoDetails { get; set; } = new List<QsoDetail>();
    public override string ToString()
    {
        return $"{Call}: {QsoDate} - {Name}  {Mode}";
    }
    public string GetSHA256Hash()
    {
        // Concatenate the properties into a single string representation
        string dataToHash = $"{QsoDate}:{Call}:{Name}:{Mode}";

        //Append the dictionary data in a consistent order
        foreach (var entry in QsoDetails.OrderBy(entry => entry.Name))
        {
            dataToHash += $":{entry.Name}={entry.Value}";
        }

        // Compute the SHA-256 hash
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

