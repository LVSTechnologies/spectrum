using System;
namespace SignInUser.Common.Extensions
{
    public static class Constants
    {
        public const string AccountDoesNotExist = "Account does not exist";
        public const string TextFieldHasSpecialCharacters = "First and Last name Fields cannot have special characters";
        public const string PasswordMustBeBetween8And15 = "Password length must be between 8 and 15";
        public const string PasswordMustContainLowerCase = "Password must contain atleast one lower case letter";
        public const string PasswordMustContainUpperCase = "Password must contain atleast one upper case letter";
        public const string PasswordRepeatingCharacters = "Password must not have repeating sequence of characters";
        public const string TooEarlyToCreateAccount = "it is too early to create an account";
        public const string AccountDoesExist = "The account/user name does not exist";
        public const string PasswordIncorrect = "Password is incorrect";
        public const string LeftSquareBracket = "[";
        public const string RightSquareBracket = "]";
        public const string CurlyBrackets = "}{";
        public const string CurlyBracketsWithComma = "},{";
        public const string ReusableCellIdentifier = "UserCell";
        public const int MaxNumbers = 10;
        public const string PhoneNumberFormat = "(XXX)-XXX-XXXX";
        public const string ErrorSavingUsersToLocalStorage = "There was an error saving users to Local Storage: ";

    }
}
