
namespace AirportProject4.Shared.attachmentService
{
    public class attachmentService : IattachmentService
    {
        List<string> allowedExtensions = new List<string> { ".jpg", ".png", ".jpeg" };
         const int maxsize= 2097152; // 2MB in bytes
        public string? UploadImage(IFormFile file, string folderName)
        {
            //1
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension)) return null;
            //2
            if (file.Length > maxsize||file.Length==0) return null;
            //3
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files",folderName);
            //4
            var FileName = $"{Guid.NewGuid()}_{file.FileName}";
            //5
            var filePath = Path.Combine(FolderPath, FileName);
            //6
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("Files", folderName, FileName).Replace("\\","/");

        }

        public bool DeleteImage(string imagePath)
        {
            if (!File.Exists(imagePath)) return false;
            else
            {
                File.Delete(imagePath);
                return true;
            }

        }

       
    }
}
