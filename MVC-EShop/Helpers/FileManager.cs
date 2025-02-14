namespace MVC_EShop.Helpers;

public static class FileManager
{
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };

    public static string UploadFile(IFormFile file)
    {
        try
        {
            // Check file extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Only .jpg, .jpeg, and .png files are allowed.");
            }

            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var path = Path.Combine("wwwroot", "images", fileName);
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        catch (Exception)
        {
            throw;
        }
    }

    
    public static void DeleteFile(string fileName)
    {

        var path = Path.Combine("wwwroot", "images", fileName);

        if (File.Exists(path))
        {
            File.Delete(path); // Deletes the file from the filesystem
        }

    }
}
