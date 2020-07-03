using System;
using MobAnnouncApp.Models.Announcement;
using MobAnnouncApp.Services;
using Xamarin.Forms;

namespace MobAnnouncApp.Views
{
    public partial class AnnouncementListPage : ContentPage
    {
        private AnnouncementService AnnouncementService = new AnnouncementService();
        private bool IsFirstLoad { get; set; } = true;
        private ListAnnouncement Response { get; set; }

        public AnnouncementListPage()
        {
            InitializeComponent();

            LoadData();
        }

        protected async override void OnAppearing()
        {
            if (IsFirstLoad)
                return;

            LoadData();
        }

        async void LoadData(int offset = 0, int length = 6)
        {
            var result = await AnnouncementService.FetchAllAsync(offset, length);

            if (!result.Data.HasValue)
                return;

            Response = result.Data.Value;

            AnnouncementList.ItemsSource = Response.Announcements;
            InitialPagination();

            if (IsFirstLoad)
                IsFirstLoad = false;
        }

        void AnnouncementTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (ListAnnouncement.Announcement)e.Item;

            Navigation.PushAsync(new AnnouncementDetailPage(item.Id));
        }

        void InitialPagination()
        {
            int started_at = Response.Offset;
            int length = Response.Length;
            int total = Response.Total;
            int ended_at = started_at + length;
            int range_end = total > ended_at ? ended_at : total;


            PaginationSummary.Text = $"{started_at} - {range_end} / {total}";
            PaginationBefore.IsEnabled = started_at > 0;
            PaginationAfter.IsEnabled = total > ended_at;                
        }

        void PaginationBeforeClicked(object sender, EventArgs e)
        {
            int started_at = Response.Offset;
            int length = Response.Length;
            int new_started_at = started_at - length;

            if (new_started_at < 0)
                new_started_at = 0;

            LoadData(new_started_at, length);
        }

        void PaginationAfterClicked(object sender, EventArgs e)
        {
            int started_at = Response.Offset;
            int length = Response.Length;
            int new_started_at = started_at + length;

            LoadData(new_started_at, length);
        }
    }
}
