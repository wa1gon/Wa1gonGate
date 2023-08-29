using HamDotNetToolkit;
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
                        writer.WriteLine($"<id:{qso.Id.Length}>{qso.Id}");
                        writer.WriteLine($"<qso_date:{qso.QsoDate.ToString("yyyyMMdd").Length}>{qso.QsoDate.ToString("yyyyMMdd")}");
                        writer.WriteLine($"<time_on:{qso.QsoDate.ToString("HHmmss").Length}>{qso.QsoDate.ToString("HHmmss")}");
                        writer.WriteLine($"<call:{qso.Call.Length}>{qso.Call}");
                        writer.WriteLine($"<name:{qso.Name.Length}>{qso.Name}");
                        writer.WriteLine($"<mode:{qso.Mode.Length}>{qso.Mode}");

                        WriteOutAdifTagsIfNeeded(writer, qso);

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

            static void WriteOutAdifTagsIfNeeded(StreamWriter writer, Qso qso)
            {
                if (qso.RstRcvd.Length > 0)
                    writer.WriteLine($"<rst_rcvd:{qso.RstRcvd.Length}>{qso.RstRcvd}");

                if (qso.RstSent.Length > 0)
                    writer.WriteLine($"<rst_sent:{qso.RstSent.Length}>{qso.RstSent}");

                if (qso.QSLRcvd.Length > 0)
                    writer.WriteLine($"<qsl_rcvd:{qso.QSLRcvd.Length}>{qso.QSLRcvd}");

                if (qso.QSLSent.Length > 0)
                    writer.WriteLine($"<qsl_sent:{qso.QSLSent.Length}>{qso.QSLSent}");

                if (qso.Freq > 0)
                    writer.WriteLine($"<freq:{qso.Freq.ToString().Length}>{qso.Freq.ToString()}");
                if (qso.FreqRx > 0)
                    writer.WriteLine($"<freq_rx:{qso.FreqRx.ToString().Length}>{qso.FreqRx.ToString()}");

                if (qso.State.Length > 0)
                    writer.WriteLine($"<state:{qso.State.Length}>{qso.State}");
                if (qso.County.Length > 0)
                    writer.WriteLine($"<county:{qso.County.Length}>{qso.County}");
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

                        { "RST_RCVD", qso.RstRcvd },
                        { "RST_SENT", qso.RstSent },
                        { "QSL_SENT", qso.QSLSent },
                        { "QSL_RCVD", qso.QSLRcvd },
                        { "FREQ", qso.Freq.ToString() },
                        { "FREQ_RX", qso.FreqRx.ToString() },
                        { "STATE", qso.State },
                        { "COUNTY", qso.County }
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
                        { "TIME_ON", qso.Name },
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

