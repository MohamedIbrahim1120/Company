namespace Company.PL.Helpers
{
    public static class DocumentSettings
    {
        // 1. Uplaod
        // ImageName
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Folder Location
            //string folderpath = "E:\\.NET\\Route\\MVC\\Ass3\\Company.PL\\wwwroot\\files\\" + folderName;

            //var folderpath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;

            var folderpath = Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\files" , folderName);

            // 1. Get File Name And Make It Unique
            var FileName = $"{Guid.NewGuid()}{file.FileName}";

            // File Path

            var filepath = Path.Combine(folderpath, FileName);    

           using var filestream = new FileStream(filepath,FileMode.Create);

            file.CopyTo(filestream);

            return FileName;

        }

        // 2. Delete

        public static void DeleteFile(string fileName,string folderName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files" + folderName,fileName);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
