using System.Globalization;

namespace DemoProject.Helper
{
    public class TextHelper
    {
        public string NormalizeText(string name)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = name.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = textInfo.ToTitleCase(words[i]);
            }
            return string.Join(" ", words);
        }

    }
}
