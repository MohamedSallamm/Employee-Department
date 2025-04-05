using static NuGet.Packaging.PackagingConstants;
using System.Xml.Linq;

namespace PresentationLayer.Helper
{
    public static class DocumentSettings
    {
        //Upload function
        public static string UploadFile(IFormFile file, string FolderName)
        {
            //1. Get Located Folder Path    
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName); //The Path of folder will saving folder uploaded (wwwroot\Files\Images\)

            //2. Get File Name and Make it Unique 
            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get File Path[Folder Path + FileName]
            string FilePath = Path.Combine(FolderPath, FileName);

            //4. Save File As Streams
            using var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);

            //5. Return File Name
            return FileName;


            //Delete function
        }
    }
}