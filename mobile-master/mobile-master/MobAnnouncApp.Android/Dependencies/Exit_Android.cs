using System;
using Android.App;
using MobAnnouncApp.Dependencies;
using MobAnnouncApp.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(Exit_Android))]
namespace MobAnnouncApp.Droid.Dependencies
{
    public class Exit_Android : IExit
    {
        public void Finish()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
