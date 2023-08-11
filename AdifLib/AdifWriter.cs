using HamDevLib;
using Newtonsoft.Json;
using System.Text.Json;
//using System.Text.Json;

namespace AdifLib
{
    public static class AdifWriter
    {
        public static void WriteAdifToFile(string filePath, List<Qso> qsoList, Dictionary<string, string>? header = null)
        {
            try
            {
                using (StreamWriter? writer = new StreamWriter(filePath))
                {
                    if (header != null)
                    {
                        foreach (var entry in header)
                        {
                            writer.WriteLine($"<{entry.Key}:{entry.Value.Length}>{entry.Value}");
                        }
                        writer.WriteLine("<EOH>");
                    }

                    foreach (var qso in qsoList)
                    {
                        if (string.IsNullOrEmpty(qso.Call) || string.IsNullOrEmpty(qso.Mode))
                        {
                            continue;
                        }
                        if (qso.Id.IsNullOrEmpty())
                        {
                            qso.Id = Guid.NewGuid().ToString();

                        }
                        writer.WriteLine($"<ID:{qso.Id.Length}>{qso.Id}");
                        writer.WriteLine($"<QSO_DATE:{qso.QsoDate.ToString("yyyyMMdd").Length}>{qso.QsoDate.ToString("yyyyMMdd")}");
                        writer.WriteLine($"<CALL:{qso.Call.Length}>{qso.Call}");
                        writer.WriteLine($"<TIME_ON:{qso.TimeOn.ToString("HHmmss").Length}>{qso.TimeOn.ToString("HHmmss")}");
                        writer.WriteLine($"<MODE:{qso.Mode.Length}>{qso.Mode}");

                        foreach (var field in qso.QsoDetails)
                        {
                            if (!string.IsNullOrEmpty(field.Value))
                            {
                                writer.WriteLine($"<{field.Name}:{field.Value.Length}>{field.Value}");
                            }
                        }

                        writer.WriteLine("<EOR>");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing ADIF file: " + ex.Message);
            }
        }



        public static void WriteAdifToJsonFile(string filePath, List<Qso> qsoList, Dictionary<string, string>? header = null)
        {
            try
            {
                string lastCall = string.Empty;
                List<Dictionary<string, string>> adifRecords = new List<Dictionary<string, string>>();
                int count = 0;
                if (header != null)
                {
                    adifRecords.Add(header);
                }

                foreach (var qso in qsoList)
                {
                    if (qso.Call.IsNullOrEmpty())
                    {
                        continue;
                    }
                    else
                    {
                        lastCall = qso.Call;
                    }
                    count++;
                    Dictionary<string, string> record = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "CALL", qso.Call },
                        { "QSO_DATE", qso.QsoDate.ToString("yyyyMMdd") },
                        { "TIME_ON", qso.TimeOn.ToString("HHmmss") },
                        { "MODE", qso.Mode }
                    };
                    if (qso.Id.IsNullOrEmpty())
                    {
                        qso.Id = Guid.NewGuid().ToString();
                        record["id"] = qso.Id;
                    }
                    foreach (var field in qso.QsoDetails)
                    {
                        if (!string.IsNullOrEmpty(field.Value))
                        {
                            record[field.Name] = field.Value;
                        }
                    }

                    adifRecords.Add(record);
                }

                var serializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    {
                        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                    }
                };

                string jsonData = JsonConvert.SerializeObject(adifRecords, serializerSettings);
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing ADIF to JSON file: " + ex.Message);
            }
        }




        public static string? ConvertAdifToJson(List<Qso> qsoList, Dictionary<string, string>? header = null)
        {
            try
            {
                List<Dictionary<string, string>> adifRecords = new List<Dictionary<string, string>>();

                if (header != null)
                {
                    adifRecords.Add(header);
                }

                foreach (var qso in qsoList)
                {
                    if (string.IsNullOrEmpty(qso.Call) || string.IsNullOrEmpty(qso.Mode))
                    {
                        continue;
                    }

                    Dictionary<string, string> record = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "QSO_DATE", qso.QsoDate.ToString("yyyyMMdd") },
                        { "CALL", qso.Call },
                        { "TIME_ON", qso.TimeOn.ToString("HHmmss") },
                        { "MODE", qso.Mode }
                    };
                    if (qso.Id.IsNullOrEmpty())
                    {
                        qso.Id = Guid.NewGuid().ToString();
                    }
                    foreach (var field in qso.QsoDetails)
                    {
                        if (!string.IsNullOrEmpty(field.Value))
                        {
                            record[field.Name] = field.Value;
                        }
                    }

                    adifRecords.Add(record);
                }

                return System.Text.Json.JsonSerializer.Serialize(adifRecords, new JsonSerializerOptions
                {
                    WriteIndented = true // If you want the JSON to be indented for better readability
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting ADIF to JSON: " + ex.Message);
                return null;
            }
        }

    }
}

