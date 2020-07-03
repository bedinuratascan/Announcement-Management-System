using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace MobAnnouncApp.ViewModels.Profile
{
    public class AnnouncementsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public List<Announcement> Announcements { get; private set; }

        public AnnouncementsViewModel()
        {
            Announcements = new List<Announcement>()
            {
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 },
                new Announcement { Title = "Duyuru #1", LikeCount = 5 }
            };
        }


        public struct Announcement
        {
            public string Title { get; set; }
            public int LikeCount { get; set; }
        }
    }
}
