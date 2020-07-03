using System.Threading.Tasks;
using MobAnnouncApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobAnnouncApp.Views
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();

            BindingContext = new SignInViewModel(Navigation);


            _ = RedirectIfSigned();
        }

        public async Task RedirectIfSigned()
        {
            var isSigned = await IsSignedAsync();
            if (isSigned)
            {
                await Navigation.PushAsync(new MainPage());
            }
        }

        public async Task<bool> IsSignedAsync()
        {
            var token = await SecureStorage.GetAsync("token");
            var full_name = await SecureStorage.GetAsync("full_name");

            if (token != null)
            {
                Application.Current.Properties["token"] = token;
                Application.Current.Properties["full_name"] = full_name;
            }

            return token != null;
        }
    }
}
