using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAPI.Services.BaseServices
{
    public class UploadFileHelper
    {
        private const string BASE_PATH = "wwwroot/uploads";

        private const string DEFAULT_UPLOAD_FOLDER = "files";


        public static string UploadFile(IFormFile file, string folderName = "")
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return "";
                }
                if (string.IsNullOrEmpty(folderName)) folderName = DEFAULT_UPLOAD_FOLDER;
                var directoryPath = Path.Combine(BASE_PATH, folderName);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var relativePath = filePath.Replace(BASE_PATH, "").Replace("\\", "/");
                return relativePath;
            }
            catch
            {
                return "";
            }
        }
        public static string GetBase_Path()
        {
            return BASE_PATH;
        }
        public static void RemoveFile(string path)
        {
            string filePath = path.Insert(0, BASE_PATH).Replace("/", "\\");
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        public static string GetFullPath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return string.Empty;
            }

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), BASE_PATH,
                relativePath.TrimStart('/', '\\').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            return string.Empty;
        }
        public static string GetRelativeFilePath(string absolutePath, bool includeUploads = true)
        {
            string rootPath = Directory.GetCurrentDirectory();

            if (includeUploads)
            {
                rootPath = Path.Combine(rootPath, "wwwroot", "uploads");
            }
            else
            {
                rootPath = Path.Combine(rootPath, "wwwroot");
            }

            // Normalize both paths
            rootPath = Path.GetFullPath(rootPath);
            absolutePath = Path.GetFullPath(absolutePath);

            if (!absolutePath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Đường dẫn tuyệt đối không nằm trong thư mục gốc cho phép.");
            }

            string relativePath = absolutePath.Substring(rootPath.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return relativePath.Replace(Path.DirectorySeparatorChar, '/'); // chuẩn hóa dấu gạch chéo
        }
        public static DownloadData GetDownloadData(string path)
        {
            string filePath = path.Insert(0, BASE_PATH).Replace("/", "\\");
            try
            {
                var downloadData = new DownloadData()
                {
                    FileName = Path.GetFileName(filePath),
                    FileBytes = File.ReadAllBytes(filePath)
                };
                return downloadData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    public class DownloadData
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
    }

}
