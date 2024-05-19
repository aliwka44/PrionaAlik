using System.Text;

namespace PrionaAlik.Extensions
{
    public static class ListExtension
    {
        public static string Bind(this IEnumerable<string>? list, char letter )
        {
            if (list?.Count() == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach ( string item in list ) {
            sb.Append(item);
                sb.Append(letter);
            }
            sb.Remove(sb.Length-1,1);
            return sb.ToString();  
        }
    }
}
