namespace MVCCore11IdentityUygulama.Utilities
{
    public class FileOperations
    {
        public static string UploadImage(IFormFile imageName, string folderPath = "wwwroot/images/")
        {
            string guid = Guid.NewGuid().ToString();
            string fileName = guid + "_" + imageName.FileName;
            string filePath = folderPath + fileName;
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            imageName.CopyTo(fileStream);
            fileStream.Close();
            return fileName;

        }

        public static void DeleteImage(string fileName, string folderPath = "wwwroot/images/")
        {
            string filePath = folderPath + fileName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
