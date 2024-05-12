using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace PrionaAlik.Extensions
{
    public static class FileExtension
    {

        public static bool IsValidType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool IsValidLength(this IFormFile file, int kb) { 
        file.Length<= kb*1024
        }
    }
}
