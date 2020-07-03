using System;
using System.Collections.Generic;
using MobAnnouncApp.Dependencies;
using MobAnnouncApp.Models.Announcement;
using MobAnnouncApp.Services;
using Xamarin.Forms;

namespace MobAnnouncApp.Views
{
    public partial class SearchPage : ContentPage
    {
        private AnnouncementService AnnouncementService = new AnnouncementService();
        

        public SearchPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            
        }

        async void SearchClicked(object sender, EventArgs e)
        {
            string search = SearchBar.Text;

            if (search.Length < 3)
            {
                string message = "Arama yapmak için en az 3 karakter yazmalısınız.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
                return;
            }

            PageTitle.Title = $"Ara: {search}";
            var result = await AnnouncementService.SearchAsync(search);

            if (!result.Data.HasValue)
                return;

            var response = result.Data.Value;

            AnnouncementList.ItemsSource = response.Announcements;            
        }

        void AnnouncementTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (ListAnnouncement.Announcement)e.Item;

            Navigation.PushAsync(new AnnouncementDetailPage(item.Id));
        }
    }
}
