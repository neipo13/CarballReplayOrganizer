using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarballReplayOrganizer.FileOperations
{
    public static class FileUtility
    {
        public static bool MoveReplayFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                var destDir = Path.GetDirectoryName(destinationFilePath);
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }
                File.Move(sourceFilePath, destinationFilePath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file: {ex.Message}");
                return false;
            }
        }

        public static void CreateDestinationIfNotExists(string destinationFilePath)
        {
        }
    }
}
