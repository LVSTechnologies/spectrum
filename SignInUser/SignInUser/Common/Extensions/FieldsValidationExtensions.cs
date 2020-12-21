using System;
using System.Text.RegularExpressions;

namespace SignInUser.Common.Extensions
{
    public static class FieldsValidationExtensions
    {
        /// <summary>
        /// Using this for Password field to have length between 8 and 15
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static bool IsBetweenEigthAndFifteen(this string fieldValue)
        {
            var isBetween8And15Chars = new Regex(@".{8,15}");

            return isBetween8And15Chars.IsMatch(fieldValue);

        }

        /// <summary>
        /// Using this for Password field to have atleast one lower case letter
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static bool HasAtleastOneLowerCaseLetter(this string fieldValue)
        {
            var hasAtleastOneLowerCaseLetter = new Regex(@"[a-z]+");

            return hasAtleastOneLowerCaseLetter.IsMatch(fieldValue);

        }

        /// <summary>
        /// Using this for Password field to have atleast one upper case letter
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static bool HasAtleastOneUpperCaseLetter(this string fieldValue)
        {
            var hasAtleastOneUpperCaseLetter = new Regex(@"[A-Z]+");

            return hasAtleastOneUpperCaseLetter.IsMatch(fieldValue);

        }

        /// <summary>
        /// Using this for special characters
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static bool HasSpecialCharaters(this string fieldValue)
        {
            var hasSpecialCharacters = new Regex(@"[!@#$%^&]");

            return hasSpecialCharacters.IsMatch(fieldValue);

        }

        /// <summary>
        /// Check if Characters are repeating for the Password field
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static bool DoCharactersRepeat(this string fieldValue)
        {
            var hasSpecialCharacters = new Regex(@"(.+)\1", RegexOptions.IgnoreCase);

            return hasSpecialCharacters.IsMatch(fieldValue);

        }

    }
}
