using MobAnnouncApp.Dependencies;
using MobAnnouncApp.Services;
using MobAnnouncApp.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobAnnouncApp.ViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ButtonText { get; private set; }

        public ICommand SignInCommand { get; protected set; }
        public ICommand SignUpCommand { get; protected set; }

        private readonly UserService UserService;


        public SignInViewModel(INavigation navigation)
        {
            UserService = new UserService();
            Navigation = navigation;

            SignInCommand = new Command<string>(async (key) =>
            {
                if ( Email.Length < 3 )
                {
                    string message = "Email adresinizi girmelisiniz.";
                    DependencyService.Get<IMessage>().ShowShortTime(message);
                }

                if ( Password.Length < 1 )
                {
                    string message = "Parolanızı girmelisiniz.";
                    DependencyService.Get<IMessage>().ShowShortTime(message);
                }

                if ( Email.Length >= 3 && Password.Length >= 0 ) {
                    bool result = await UserService.SignInAsync(Email, Password);

                    if ( result )
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                }
            });

            SignUpCommand = new Command<string>(async (key) =>
            {
                await Navigation.PushModalAsync(new SignUpPage());
            });
        }
    }
}
