using System;
using System.IO;
using System.Text;
using Mhf.Server.Common.Crc;

namespace Mhf.Server.Common
{
    /// <summary>
    /// Generates MHFUP.DAT file of all files from specified root directory.
    /// The file has entry ended by a LineFeed (LF / 0x0A)
    /// </summary>
    public class MhfUpDat
    {
        private readonly DirectoryInfo _rootDirectoryInfo;

        public MhfUpDat(string rootDirectoryPath)
        {
            _rootDirectoryInfo = new DirectoryInfo(rootDirectoryPath);
            if (!_rootDirectoryInfo.Exists)
            {
                throw new FileNotFoundException("Folder does not exist", rootDirectoryPath);
            }
        }

        public string GetUpdateEntry(string filePath)
        {
            return GetUpdateEntry(new FileInfo(filePath));
        }

        public string GetUpdateEntry(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                return null;
            }

            fileInfo.Refresh();
            byte[] file = Util.ReadFile(fileInfo.FullName);
            uint crc32 = Crc32.Compute(file);
            DateTime lastWriteTime = fileInfo.LastWriteTime;
            long lastWriteFileTime = lastWriteTime.ToFileTime();
            string lastWriteFileTimeHex = $"{lastWriteFileTime:X16}";
            string fileTimeHex1 = lastWriteFileTimeHex.Substring(8);
            string fileTimeHex2 = lastWriteFileTimeHex.Substring(0, 8);
            return $"{crc32:X8},{fileTimeHex1},{fileTimeHex2},{GetFileNameEntry(fileInfo)},{file.Length},0";
        }

        public string CreateMhfUpDat()
        {
            StringBuilder sb = new StringBuilder();
            foreach (FileInfo fileInfo in _rootDirectoryInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                sb.Append($"{GetUpdateEntry(fileInfo)}/n");
            }

            return sb.ToString();
        }

        public void SaveMhfUpDat(string destinationFilePath)
        {
            string mhfUpDat = CreateMhfUpDat();
            Util.WriteFile(Encoding.UTF8.GetBytes(mhfUpDat), destinationFilePath);
        }

        private string GetFileNameEntry(FileInfo fileInfo)
        {
            string relativeDirectory = Util.RelativeDirectory(_rootDirectoryInfo.FullName, fileInfo.DirectoryName);
            if (string.IsNullOrEmpty(relativeDirectory))
            {
                return $"exe\\{fileInfo.Name}";
            }

            string path = Path.Combine(relativeDirectory, fileInfo.Name);
            return path;
        }
    }
}
