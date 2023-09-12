using System.Text;
using System.Text.RegularExpressions;

namespace Utility {
    public class CsvUtility {
        
        public static string EscapeStringCsv(string source, char sep = ';', string sourceEncoding = "utf-8", string encoding = "windows-1251//TRANSLIT") {
            string str = (sourceEncoding != encoding)
                ? Encoding.Convert(Encoding.GetEncoding(sourceEncoding),
                    Encoding.GetEncoding(encoding),
                    Encoding.GetEncoding(sourceEncoding).GetBytes(source)).ToString()
                : source;

            if (Regex.IsMatch(str, $"[\r\n\"{Regex.Escape(sep.ToString())}]")) {
                return $"\"{str.Replace("\"", "\"\"")}\"";
            }
            else
            {
                return str;
            }
        }
    }
}