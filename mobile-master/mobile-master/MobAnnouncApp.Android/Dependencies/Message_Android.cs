using Android.Widget;
using MobAnnouncApp.Dependencies;
using MobAnnouncApp.Droid.Dependencies;
using Xamarin.Forms;

[assembly:Dependency(typeof(Message_Android))]
namespace MobAnnouncApp.Droid.Dependencies
{
    public class Message_Android : IMessage
    {
        public void ShowShortTime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }

        public void ShowLongTime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}
