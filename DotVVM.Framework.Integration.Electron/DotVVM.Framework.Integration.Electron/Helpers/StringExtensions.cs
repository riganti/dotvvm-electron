using System;
using System.Text;

namespace DotVVM.Electron.Helpers
{
    public static class StringExtensions
    {
        public static string FirstCharacterToLower(this string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
                return str;

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string InsertDashBeforeUpperCharacters(this string str)
        {
            StringBuilder newString = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsUpper(str[i]) && i != 0)
                {
                    newString.Append($"-{str[i]}");
                }
                else
                {
                    newString.Append(str[i]);
                }
            }
            return newString.ToString();
        }


    }

}