using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SignInUser.Models;

namespace SignInUser.Common.Extensions
{
    public static class FileExtensions
    {
        public static List<User> GetAccountsFromLocalStorage()
        {
            List<User> usersFromLocalFile = new List<User>();
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileName = Path.Combine(filePath, "accountslist.json");
            string jsonData = File.ReadAllText(fileName);

            if (string.IsNullOrEmpty(jsonData))
                return usersFromLocalFile;

            try
            {
                //Json data does not seem to be having the square brackets the first time, so just a work around for now
                jsonData = Constants.LeftSquareBracket
                            + jsonData.Replace(Constants.LeftSquareBracket, string.Empty).Replace(Constants.RightSquareBracket, string.Empty).Replace(Constants.CurlyBrackets, Constants.CurlyBracketsWithComma)
                            + Constants.RightSquareBracket;
                usersFromLocalFile = JsonConvert.DeserializeObject<List<User>>(jsonData);
            }
            catch (Exception ex)
            {
                //TODO: Need to do better error handling here
                var testMessage = ex.Message;
            }

            
            return usersFromLocalFile;

        }
    }
}
