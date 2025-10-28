namespace AirportProject4.Shared.attachmentService
{
    public interface IattachmentService
    {
        string? UploadImage(IFormFile file,string folderName);

        bool DeleteImage(string imagePath);
    }
}
