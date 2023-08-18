﻿namespace LogGate
{
    public class MauiHelper
    {
        public static string GetSettingsFilePath(string fileName)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //string appDataPath = FileSystem.Current.AppDataDirectory;
            appDataPath = Path.Combine(appDataPath, "LogGate");

            var dirInfo = Directory.CreateDirectory(appDataPath);
            appDataPath = Path.Combine(appDataPath, fileName);
            return appDataPath;

        }
    }
}
