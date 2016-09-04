namespace RecipeMs.CrossCutting.Common
{
    public class StringManipulation
    {
        public static string CapitalizeName(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            text = text.Trim();
            string result = text.Substring(0, 1).ToUpper() + text.Substring(1, text.Length - 1).ToLower();

            return result;
        }
    }
}
