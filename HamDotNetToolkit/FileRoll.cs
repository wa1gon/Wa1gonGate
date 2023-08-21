namespace HamDotNetToolkit
{
    public class FileToolkit
    {

        public static void RollandCleanupFiles(string baseFileName, int maxRolls)
        {
            if (baseFileName.IsNullOrEmpty())
                return;

            string directoryPath = Path.GetDirectoryName(baseFileName);
            if ( directoryPath.IsNullOrEmpty())
            {
                return;
            }
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);
            string extension = Path.GetExtension(baseFileName);

            var existingFiles = Directory.GetFiles(directoryPath, $"{fileNameWithoutExtension}_*{extension}")
                                         .Select(Path.GetFileName)
                                         .ToList();

            // Determine the next available roll count
            int nextRollCount = 1;
            while (existingFiles.Contains($"{fileNameWithoutExtension}_{nextRollCount}{extension}"))
            {
                nextRollCount++;
            }

            // Roll the file
            string rolledFileName = $"{fileNameWithoutExtension}_{nextRollCount}{extension}";
            string fullRolledFilePath = Path.Combine(directoryPath, rolledFileName);
            File.Move(baseFileName, fullRolledFilePath);

            // Cleanup old rolled files
            var filesToDelete = existingFiles.Take(Math.Max(0, existingFiles.Count - maxRolls));
            foreach (var fileToDelete in filesToDelete)
            {
                if (filesToDelete.IsNullOrEmpty())
                    continue;
                File.Delete(Path.Combine(directoryPath, fileToDelete));
            }
        }
    }
}
