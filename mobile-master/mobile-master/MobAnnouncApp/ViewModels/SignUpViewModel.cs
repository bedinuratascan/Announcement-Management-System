using MobAnnouncApp.Dependencies;
using MobAnnouncApp.Services;
using MobAnnouncApp.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobAnnouncApp.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public ICommand SignUpCommand { get; protected set; }
        public ICommand SignInCommand { get; protected set; }

        private readonly UserService UserService;


        public SignUpViewModel(INavigation navigation)
        {
            UserService = new UserService();
            Navigation = navigation;

            SignUpCommand = new Command<string>(async (key) =>
            {
                if (Email.Length < 3)
                {
                    string message = "Email adresinizi girmelisiniz.";
                    DependencyService.Get<IMessage>().ShowShortTime(message);
                }

                if (Password.Length < 1)
                {
                    string message = "Parolanızı girmelisiniz.";
                    DependencyService.Get<IMessage>().ShowShortTime(message);
                }

                if ( Name.Length < 3 )
                {
                    string message = "Adınızı girmelisiniz.";
                    DependencyService.Get<IMessage>().ShowShortTime(message);
                }

                if ( SurName.Length < 3)
                {
                    string message = "Soyadınızı girmelisiniz.";
                    DependencyService.Get<IMessage>().ShowShortTime(message);
                }

                if (Email.Length >= 3 && Password.Length >= 0 && Name.Length > 2 && SurName.Length > 2)
                {
                    bool result = await UserService.SignUpAsync(Email, Password, Name, SurName);

                    if (result)
                        await Navigation.PopModalAsync();
                }
            });

            SignInCommand = new Command<string>(async (key) =>
            {
                await Navigation.PopModalAsync();
            });
        }
    }
}
