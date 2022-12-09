using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace FarmFresh.Application.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex UnderscoredSplitterRegex = new Regex("([a-z\\d])([A-Z]+)", RegexOptions.Compiled);
        private static readonly Regex UnderscoredRegex = new Regex("[-\\s]+", RegexOptions.Compiled);
        private static readonly Regex Titleizer = new Regex("(?:^|\\s|-)\\S", RegexOptions.Compiled);

        public static string Titleize(this string sentence)
        {
            return Titleizer.Replace(sentence.ToLower(), match => match.Value.ToUpper());
        }

        public static string Underscored(this string sentence)
        {
            return UnderscoredRegex.Replace(UnderscoredSplitterRegex.Replace(sentence.Trim(), "$1_$2"), "_").ToLower();
        }

        public static string Humanize(this string sentence)
        {
            return sentence.Underscored().Replace('_', ' ').Capitalize();
        }

        public static string Camelize(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            return char.ToLower(word[0]) + word.Substring(1);
        }

        public static string Capitalize(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            return char.ToUpper(word[0]) + word.Substring(1);
        }

        public static string ThrowIfNullOrEmpty(this string obj, string name, string message = "")
        {
            if (string.IsNullOrEmpty(obj))
                throw new NullReferenceException($"{name} is null or empty. {message}".Trim());

            return obj;
        }

        public static T DeserializeJson<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString.Trim());
        }

        public static string ToBase64(this string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        public static string FromBase64(this string encrString)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(encrString));
        }

        public static Stream ToStream(this string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        public static byte[] ToBytes(this string @string)
        {
            byte[] bytes = new byte[@string.Length * sizeof(char)];
            Buffer.BlockCopy(@string.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static bool IsMissing(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsPresent(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        public static string ExtractInteger(this string str)
        {
            string result = String.Empty;
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    result = result + c;
                }
            }
            return result;
        }
    }
}
