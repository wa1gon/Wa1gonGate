using HamDevLib;
using Newtonsoft.Json;

namespace AdifLib
{

    public static class AdifReader
    {


        public static List<Qso>? ReadAdifFromJsonFile(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                List<Dictionary<string, string>>? adifRecords = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                if (adifRecords is null)
                    return null;

                List<Qso> qsoList = new List<Qso>();
                int count = 0;
                foreach (var record in adifRecords)
                {
                    Qso qso = new();

                    if (record.TryGetValue("CALL", out string? call))
                    {
                        qso.Call = call;
                        if (call.IsNullOrEmpty())
                        {
                            continue;
                        }
                    }

                    if (record.TryGetValue("QSO_DATE", out string? qsoDateValue) &&
                        DateTime.TryParseExact(qsoDateValue, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime qsoDate))
                    {
                        qso.QsoDate = qsoDate;
                    }



                    if (record.TryGetValue("TIME_ON", out string? timeOnValue) && DateTime.TryParseExact(timeOnValue, "HHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime timeOn))
                    {
                        qso.TimeOn = timeOn;
                    }

                    if (record.TryGetValue("MODE", out string? mode))
                    {
                        qso.Mode = mode;
                    }

                    if (record.TryGetValue("Id", out string? id))
                    {
                        qso.Id = id;
                    }


                    foreach (var field in record)
                    {
                        // Skip the known properties we have already processed (QSO_DATE, CALL, TIME_ON, and MODE)
                        if (string.Equals(field.Key, "QSO_DATE", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "CALL", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "TIME_ON", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "Id", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "MODE", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        qso.QsoDetails.Add(new QsoDetail() { Name = field.Key, Value = field.Value });
                    }

                    if (qso.Call.IsNullOrEmpty() == false)
                        qsoList.Add(qso);
                    count++;
                }

                return qsoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading ADIF JSON file: " + ex.Message);
                return null;
            }
        }

        public static (Dictionary<string, string> header, List<Qso> qsoList) ReadAdifFromFile(string filePath)
        {
            Dictionary<string, string> header = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            List<Qso> qsoList = new List<Qso>();

            try
            {
                using (StreamReader? reader = new StreamReader(filePath))
                {
                    string? line;
                    bool isHeader = true;
                    Qso? currentQso = null;


                    while ((line = reader?.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Equals("<EOH>", StringComparison.OrdinalIgnoreCase))
                        {
                            isHeader = false;
                            continue;
                        }

                        if (line.Equals("<EOR>", StringComparison.OrdinalIgnoreCase))
                        {
                            if (currentQso != null)
                            {
                                if (currentQso.Call.IsNullOrEmpty() == false)
                                    qsoList.Add(currentQso);
                                currentQso = null;
                            }
                            currentQso = null;
                            continue;
                        }

                        if (line.StartsWith("<") && line.EndsWith(">"))
                        {
                            // Skip processing of tags like <TAG:4> as they have already been handled
                            continue;
                        }

                        while (line.Length > 0)
                        {
                            int colonIndex = line.IndexOf(':');
                            if (colonIndex > 0)
                            {
                                string tag = line.Substring(0, colonIndex).TrimStart('<'); // Remove leading '<'
                                int lengthStartIndex = colonIndex + 1;
                                int lengthEndIndex = line.IndexOf('>', lengthStartIndex);
                                if (lengthEndIndex > lengthStartIndex)
                                {
                                    int length = int.Parse(line.Substring(lengthStartIndex, lengthEndIndex - lengthStartIndex));
                                    int valueStartIndex = lengthEndIndex + 1;
                                    if (line.Length >= valueStartIndex + length)
                                    {
                                        string value = line.Substring(valueStartIndex, length);
                                        if (isHeader)
                                        {
                                            header[tag] = value;
                                        }
                                        else
                                        {
                                            if (tag.Equals("QSO_DATE", StringComparison.OrdinalIgnoreCase))
                                            {
                                                if (currentQso == null)
                                                {
                                                    currentQso = new Qso();
                                                }
                                                currentQso.QsoDate = DateTime.ParseExact(value, "yyyyMMdd", null);
                                            }
                                            else if (tag.Equals("CALL", StringComparison.OrdinalIgnoreCase))
                                            {
                                                if (currentQso == null)
                                                {
                                                    currentQso = new Qso();
                                                }
                                                currentQso.Call = value;
                                            }
                                            else if (tag.Equals("TIME_ON", StringComparison.OrdinalIgnoreCase))
                                            {
                                                if (currentQso == null)
                                                {
                                                    currentQso = new Qso();
                                                }
                                                currentQso.TimeOn = DateTime.ParseExact(value, "HHmmss", null);
                                            }
                                            else if (tag.Equals("MODE", StringComparison.OrdinalIgnoreCase))
                                            {
                                                if (currentQso == null)
                                                {
                                                    currentQso = new Qso();
                                                }
                                                currentQso.Mode = value;
                                            }
                                            else
                                            {
                                                if (currentQso == null)
                                                {
                                                    // Ignore fields outside QSO records
                                                }
                                                else
                                                {

                                                    //currentQso.QsoDetails[tag] = value;
                                                    currentQso.QsoDetails.Add(new QsoDetail() { Name = tag, Value = value });
                                                }
                                            }
                                        }

                                        // Move to the next tag-value pair in the line
                                        line = line.Substring(valueStartIndex + length).Trim();
                                    }
                                    else
                                    {
                                        // Incomplete line, break the loop and continue to the next line
                                        break;
                                    }
                                }
                                else
                                {
                                    // Invalid line, break the loop and continue to the next line
                                    break;
                                }
                            }
                            else
                            {
                                // Invalid line, break the loop and continue to the next line
                                break;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading ADIF file: " + ex.Message);
            }

            return (header, qsoList);
        }
    }
}





//////////////////////////






