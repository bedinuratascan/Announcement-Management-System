using MobAnnouncApp.ViewModels;
using Xamarin.Forms;

namespace MobAnnouncApp.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();

            BindingContext = new SignUpViewModel(Navigation);
        }
    }
}
