//using Microsoft.Data.Sqlite;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace DatabaseRepo
//{
//    public class SqliteRepo
//    {
//        public static void SaveRecordsToDatabase(string databasePath, List<Dictionary<string, string>> records)
//        {
//            try
//            {
//                // Create a new SQLite database connection
//                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + databasePath))
//                {
//                    connection.Open();

//                    // Assuming the table "adif_records" already exists

//                    // Insert the records into the database
//                    using (SqliteCommand insertCommand = new SQLiteCommand(
//                        "INSERT INTO adif_records (Call, QSO_Date, Time_On, Time_Off, Band, Comment, N3FJP_COMPUTERNAME, Cont, Country, DXCC, CQz, " +
//                        "Freq, Gridsquare, ITUz, Mode, Submode, N3FJP_ModeContest, Name, Pfx, QSL_Sent, QSL_Rcvd, RST_Sent, RST_Rcvd, N3FJP_SPCNum, " +
//                        "N3FJP_StationID, LOTW_QSL_SENT) VALUES (@Call, @QSO_Date, @Time_On, @Time_Off, @Band, @Comment, @N3FJP_COMPUTERNAME, @Cont, " +
//                        "@Country, @DXCC, @CQz, @Freq, @Gridsquare, @ITUz, @Mode, @Submode, @N3FJP_ModeContest, @Name, @Pfx, @QSL_Sent, @QSL_Rcvd, " +
//                        "@RST_Sent, @RST_Rcvd, @N3FJP_SPCNum, @N3FJP_StationID, @LOTW_QSL_SENT)", connection))
//                    {
//                        // Bind the parameters to the insert command for each record
//                        foreach (var record in records)
//                        {
//                            insertCommand.Parameters.Clear();
//                            foreach (var field in record)
//                            {
//                                insertCommand.Parameters.AddWithValue("@" + field.Key, field.Value);
//                            }

//                            // Execute the insert command for each record
//                            insertCommand.ExecuteNonQuery();
//                        }
//                    }

//                    Console.WriteLine("Records saved to the database.");
//                }
//            }
//            catch (Exception ex)
//            {
//                TestContext.WriteLine("Error saving records to the database: " + ex.Message);
//            }
//        }
//    }
//}