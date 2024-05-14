using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.IO;

namespace PrionaAlik.Extensions
{
    public static class FileExtension
    {

        public static bool IsValidType(this IFormFile file, string type)=> file.ContentType.Contains(type);
        
        public static bool IsValidLength(this IFormFile file, int kb) =>
            file.Length <= kb * 1024;
        public static async Task<string> SaveFileAsync(this IFormFile file,string pat   )
        {
            string ext = Path.GetExtension(file.FileName);
            string newName = Path.GetRandomFileName() + ext;

            await using FileStream fs = new FileStream(newName + ext, FileMode.Create);
            await file.CopyToAsync(fs);
            return newName+ext;
        }

    }
}
