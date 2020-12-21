using System;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using UIKit;
using System.Collections.Generic;
using SignInUser.Models;
using SignInUser.Common.Extensions;
using System.Linq;
using Splat;

namespace SignInUser.ViewModel
{
    public class SignInViewModel : ReactiveObject
    {
        private readonly Interaction<string, bool> nextSceneInteraction;
        private readonly Interaction<string, bool> validationAccount;
        private readonly Interaction<string, bool> accountsSceneInteraction;

        public SignInViewModel()
        {
            SignInCommand = ReactiveCommand.CreateFromTask(AuthenticateUser, this.WhenAnyValue(x => x.SignButtonEnabled));
            nextSceneInteraction = new Interaction<string, bool>();
            validationAccount = new Interaction<string, bool>();
            accountsSceneInteraction = new Interaction<string, bool>();
        }

        private async Task AuthenticateUser()
        {
            List<User> users = FileExtensions.GetAccountsFromLocalStorage();

            var signedUser = users.FirstOrDefault(x => x.UserName == UserName);

            //User not Found
            if (signedUser == null)
            {
                await validationAccount.Handle(Constants.AccountDoesExist);
                return;
            }

            //Password does not match
            if (signedUser.Password != Password)
            {
                await validationAccount.Handle(Constants.PasswordIncorrect);
                return;
            }

            //If All Validations Pass Add the User to Local Storage and Proceed to Next Scene
            try
            {
                await nextSceneInteraction.Handle("Launch Accounts List screen");
            }
            catch (Exception ex)
            {
                //TODO: Need to do better error handling here.
                var sometest = ex.Message;
            }

        }

        private void EnableSignInButton()
        {
            //Enable the Sign in Button only when User Name and Password is filled out
            SignButtonEnabled = !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);

        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                this.RaiseAndSetIfChanged(ref _userName, value);
                EnableSignInButton();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                this.RaiseAndSetIfChanged(ref _password, value);
                EnableSignInButton();
            }
        }

        private bool _signButtonEnabled;
        public bool SignButtonEnabled
        {
            get => _signButtonEnabled;
            set
            {
                this.RaiseAndSetIfChanged(ref _signButtonEnabled, value);
            }
        }

        public ReactiveCommand<Unit, Unit> SignInCommand;
        public Interaction<string, bool> ValidationAccount => validationAccount;
        public Interaction<string, bool> NextSceneInteraction => nextSceneInteraction;
        public ReactiveCommand<Unit, IRoutableViewModel> AccountsSceneCommand { get; }

    }
}
