using System;
namespace MobAnnouncApp.Dependencies
{
    public interface IMessage
    {
        void ShowShortTime(string message);
        void ShowLongTime(string message);
    }
}
