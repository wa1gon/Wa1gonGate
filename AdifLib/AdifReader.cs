using HamDotNetToolkit;
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

                        if (call.IsNullOrEmpty())
                        {
                            continue;
                        }
                        qso.Call = call;
                    }

                    if (record.TryGetValue("QSO_DATE", out string? qsoDateValue) &&
                        DateTime.TryParseExact(qsoDateValue, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime qsoDate))
                    {
                        qso.QsoDate = qsoDate;
                    }

                    if (record.TryGetValue("NAME", out string? name))
                        qso.Name = name;

                    if (record.TryGetValue("MODE", out string? mode))
                        qso.Mode = mode;

                    if (record.TryGetValue("ID", out string? id))
                        qso.Id = id;

                    if (record.TryGetValue("RST_SENT", out string? rstSent))
                        qso.RstSent = rstSent;

                    if (record.TryGetValue("RST_RCVD", out string? rstRcvd))
                        qso.QSLRcvd = rstRcvd;

                    if (record.TryGetValue("FREQ", out string? freq))
                        qso.Freq = Decimal.Parse(freq);

                    if (record.TryGetValue("FREQ_RX", out string? freqrx))
                        qso.FreqRx = Decimal.Parse(freqrx);

                    if (record.TryGetValue("STATE", out string? state))
                        qso.State = state;

                    if (record.TryGetValue("COUNTY", out string? county))
                        qso.County = county;

                    foreach (var field in record)
                    {
                        // Skip the known properties we have already processed (QSO_DATE, CALL, TIME_ON, and MODE)
                        if (string.Equals(field.Key, "QSO_DATE", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "CALL", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "NAME", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "ID", StringComparison.OrdinalIgnoreCase) ||

                            string.Equals(field.Key, "rst_sent", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "rst_rcvd", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "QSL_sent", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "rst_rcvd", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "freq", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "freq_rx", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "State", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Key, "county", StringComparison.OrdinalIgnoreCase) ||


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
            string? timeOn = string.Empty;
            string? dateOn = string.Empty;
            try
            {
                using (StreamReader? reader = new StreamReader(filePath))
                {
                    string? line;
                    bool isHeader = true;
                    Qso currentQso = new();


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
                                {
                                    qsoList.Add(currentQso);
                                    currentQso = new Qso();
                                }
                            }
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
                                            #region
                                            if (currentQso is null) continue;

                                            if (tag.Equals("QSO_DATE", StringComparison.OrdinalIgnoreCase))
                                                dateOn = value;
                                            //currentQso.QsoDate = DateTime.ParseExact(value, "yyyyMMdd", null);
                                            else if (tag.Equals("time_on", StringComparison.OrdinalIgnoreCase))
                                                timeOn = value;

                                            else if (tag.Equals("CALL", StringComparison.OrdinalIgnoreCase))
                                                currentQso.Call = value;

                                            else if (tag.Equals("Name", StringComparison.OrdinalIgnoreCase))
                                                currentQso.Name = value;

                                            else if (tag.Equals("MODE", StringComparison.OrdinalIgnoreCase))
                                                currentQso.Mode = value;

                                            else if (tag.Equals("Rst_Sent", StringComparison.OrdinalIgnoreCase))
                                                currentQso.RstSent = value;

                                            else if (tag.Equals("Rst_Rcvd", StringComparison.OrdinalIgnoreCase))

                                                currentQso.RstRcvd = value;

                                            else if (tag.Equals("QSL_Sent", StringComparison.OrdinalIgnoreCase))

                                                currentQso.QSLSent = value;

                                            else if (tag.Equals("QSL_Rcvd", StringComparison.OrdinalIgnoreCase))

                                                currentQso.QSLRcvd = value;

                                            else if (tag.Equals("Freq", StringComparison.OrdinalIgnoreCase))

                                                currentQso.Freq = decimal.Parse(value);

                                            else if (tag.Equals("Freq_Rx", StringComparison.OrdinalIgnoreCase))

                                                currentQso.FreqRx = decimal.Parse(value);


                                            else if (tag.Equals("State", StringComparison.OrdinalIgnoreCase))

                                                currentQso.State = value;

                                            else if (tag.Equals("County", StringComparison.OrdinalIgnoreCase))
                                            {
                                                if (currentQso == null)
                                                {
                                                    currentQso = new Qso();
                                                }
                                                currentQso.County = value;
                                            }
                                            else
                                            {
                                                //currentQso.QsoDetails[tag] = value;
                                                currentQso.QsoDetails.Add(new QsoDetail() { Name = tag, Value = value });
                                            }
                                            #endregion
                                            currentQso.QsoDate = CombineDateTime(dateOn, timeOn);
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



        public static DateTime CombineDateTime(string datePart, string timePart)
        {


            //if (datePart.Length != 8 || (timePart.Length != 4 && timePart.Length != 6))
            if (datePart.Length != 8 )
            {
                return DateTime.MinValue; 
            }


            int hour = 0;
            int minute = 0;
            int second = 0;

            int year = int.Parse(datePart.Substring(0, 4));
            int month = int.Parse(datePart.Substring(4, 2));
            int day = int.Parse(datePart.Substring(6, 2));

            if (timePart.IsNullOrEmpty() == false && (timePart.Length != 6 || timePart.Length != 4))
            {
                hour = int.Parse(timePart.Substring(0, 2));
                minute = int.Parse(timePart.Substring(2, 2));
            }

            if (timePart.IsNullOrEmpty() == false && timePart.Length == 6)
            {
                second = int.Parse(timePart.Substring(4, 2));
            }

            DateTime combinedDateTime = new DateTime(
                year,
                month,
                day,
                hour,
                minute,
                second,
                DateTimeKind.Utc
            );

            return combinedDateTime;
        }
    }
}





//////////////////////////






