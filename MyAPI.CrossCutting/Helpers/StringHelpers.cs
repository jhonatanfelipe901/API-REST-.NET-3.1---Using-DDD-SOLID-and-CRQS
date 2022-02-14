using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MyAPI.CrossCutting.Helpers
{
    public class StringHelpers
    {
        public string RemoveSpecialCharacters(string text)
        {
            if (string.IsNullOrEmpty(text) == false)
            {
                text = RemoveAccents(text);

                var sb = new StringBuilder();
                foreach (char c in text)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                    {
                        sb.Append(c);
                    }
                }
                text = sb.ToString();

                text = RemoveEspassosDuplos(text);
            }
            return text;
        }

        public string RemoveAccents(string text)
        {
            if (String.IsNullOrEmpty(text) == false)
            {
                StringBuilder sbReturn = new StringBuilder();
                var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
                foreach (char letter in arrayText)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                        sbReturn.Append(letter);
                }
                text = sbReturn.ToString();
            }
            return text;
        }

        public string RemoveEspassosDuplos(string text)
        {
            if (String.IsNullOrEmpty(text) == false)
            {
                while (text.Contains("  "))
                    text = text.Replace("  ", " ");
            }
            return text;
        }
    }
}
