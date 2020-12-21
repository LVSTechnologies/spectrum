using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Foundation;
using Newtonsoft.Json;
using ReactiveUI;
using SignInUser.Common.Extensions;
using SignInUser.Models;
using Splat;
using UIKit;
using Xamarin.Essentials;

namespace SignInUser.ViewModel
{
    public class NewAccountViewModel: ReactiveObject
    {
        public List<User> Users;

        private readonly Interaction<string, bool> validationsAlert;
        private readonly Interaction<string, bool> nextSceneInteraction;

        public NewAccountViewModel()
        {
            CreateUserCommand = ReactiveCommand.CreateFromTask(CreateUserValidations, this.WhenAnyValue(x => x.CreateUserButtonEnabled));
            validationsAlert = new Interaction<string, bool>();
            nextSceneInteraction = new Interaction<string, bool>();
            Users = new List<User>();

        }

        private async Task CreateUserValidations()
        {
            //Check if FirstName and LastName has special characters
            bool hasSpecialCharacters = FirstName.HasSpecialCharaters() || LastName.HasSpecialCharaters();
            if (hasSpecialCharacters)
            {
                //Show an Alert message and return
                await validationsAlert.Handle(Constants.TextFieldHasSpecialCharacters);
                return;
            }

            //Check if Password is between 8 and 15 characters
            if (Password.Length < 8 || Password.Length > 15)
            {
                //Show an Alert message and return
                await validationsAlert.Handle(Constants.PasswordMustBeBetween8And15);
                return;
            }

            //Check if Password has atleast one lower case letter
            bool hasLowerCase = Password.HasAtleastOneLowerCaseLetter();
            if (!hasLowerCase)
            {
                await validationsAlert.Handle(Constants.PasswordMustContainLowerCase);
                return;
            }

            //Check if Password has atleast one upper case letter
            bool hasUpperCase = Password.HasAtleastOneUpperCaseLetter();
            if (!hasUpperCase)
            {
                //Show an Alert message and return
                await validationsAlert.Handle(Constants.PasswordMustContainUpperCase);
                return;
            }

            //Check if Password has repeating characters
            bool hasRpeatingCharacters = Password.DoCharactersRepeat();
            if (hasRpeatingCharacters)
            {
                //Show an Alert message and return
                await validationsAlert.Handle(Constants.PasswordRepeatingCharacters);
                return;
            }

            //Check if Service Date is more than 30 days
            var dateAheadThirtyDays = DateTime.Now.AddDays(30);
            var startDateTest = (DateTime)_startDateNSDate;
            if (startDateTest > dateAheadThirtyDays)
            {
                //Show an Alert message and return
                await validationsAlert.Handle(Constants.TooEarlyToCreateAccount);
                return;
            }

            //If All Validations Pass Add the User to Local Storage and Proceed to Next Scene
            await AddNewUser();

            try
            {
                await nextSceneInteraction.Handle("Launch success screen");
            }
            catch (Exception ex)
            {
                //TODO: Need to do better error handling
                var sometest = ex.Message;
            }


        }

        private void EnableCreateAccountButton()
        {
            //Enable the Create Account Button only when all fields are filled out
            CreateUserButtonEnabled = !string.IsNullOrEmpty(FirstName)
                                      && !string.IsNullOrEmpty(LastName)
                                      && !string.IsNullOrEmpty(UserName)
                                      && !string.IsNullOrEmpty(Password);

        }

        public async Task AddNewUser()
        {
            // First Create the new User to Add to Local Storage
            User user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Password = Password,
                PhoneNumber = PhoneNumber,
                ServiceStartDate = (DateTime)_startDateNSDate
            };

            //First Add the users to the list of users
            Users.Add(user);

            //Next Serialize the list of users
            var jsonUsersList = JsonConvert.SerializeObject(user);
            if (string.IsNullOrEmpty(jsonUsersList))
                return;

            try
            {
                //Save to local file in Json Format
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var fileName = Path.Combine(filePath, "accountslist.json");
                File.AppendAllText(fileName, jsonUsersList);
            }
            catch (Exception ex)
            {
                await validationsAlert.Handle(Constants.ErrorSavingUsersToLocalStorage + ex.Message);
                return;
            }

        }

        #region Properties Bound to Fields on Screen

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                this.RaiseAndSetIfChanged(ref _firstName, value);
                EnableCreateAccountButton();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                this.RaiseAndSetIfChanged(ref _lastName, value);
                EnableCreateAccountButton();
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                this.RaiseAndSetIfChanged(ref _userName, value);
                EnableCreateAccountButton();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                this.RaiseAndSetIfChanged(ref _password, value);
                EnableCreateAccountButton();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                this.RaiseAndSetIfChanged(ref _phoneNumber, value);
                EnableCreateAccountButton();
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                this.RaiseAndSetIfChanged(ref _startDate, value);
                EnableCreateAccountButton();
            }
        }

        private NSDate _startDateNSDate;
        public NSDate StartDateNSDate
        {
            get => (NSDate)DateTime.SpecifyKind(StartDate, DateTimeKind.Local);
            set
            {
                this.RaiseAndSetIfChanged(ref _startDateNSDate, value);
                EnableCreateAccountButton();
            }
        }

        private bool _createUserButtonEnabled;
        public bool CreateUserButtonEnabled
        {
            get => _createUserButtonEnabled;
            set
            {
                this.RaiseAndSetIfChanged(ref _createUserButtonEnabled, value);
            }
        }

        public Interaction<string, bool> ValidationsAlert => validationsAlert;

        public Interaction<string, bool> NextSceneInteraction => nextSceneInteraction;

        public ReactiveCommand<Unit, Unit> CreateUserCommand;

        #endregion

    }
}
