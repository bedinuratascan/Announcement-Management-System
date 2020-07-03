using System;
using MobAnnouncApp.Services;
using Xamarin.Forms;
using MobAnnouncApp.Models.Announcement;
using MobAnnouncApp.Dependencies;

namespace MobAnnouncApp.Views
{
    public partial class AnnouncementFormPage : ContentPage
    {
        private FormAnnouncement FormAnnouncement { get; set; }
        private AnnouncementService AnnouncementService = new AnnouncementService();

        public AnnouncementFormPage(FormAnnouncement formAnnouncement)
        {
            InitializeComponent();

            FormAnnouncement = formAnnouncement;

            InitializeForm();
        }

        public AnnouncementFormPage()
        {
            InitializeComponent();

            FormAnnouncement = new FormAnnouncement();

            InitializeForm();
        }

        void InitializeForm()
        {
            Title.Text = FormAnnouncement.Title;
            Contents.Text = FormAnnouncement.Contents;

            
            Save.Text = FormAnnouncement.Id.HasValue ? "Düzenle" : "Oluştur";
            PageTitle.Title = FormAnnouncement.Id.HasValue ? $"{FormAnnouncement.Title} Düzenle" : "Yeni Duyuru Oluştur";
        }

        public async void SaveClickedAsync(object sender, EventArgs e)
        {
            var form = new FormAnnouncement
            {
                Title = Title.Text,
                Contents = Contents.Text
            };

            if (form.Title.Length < 3)
                DependencyService.Get<IMessage>().ShowShortTime("Başlık en az 3 karakter olmalıdır.");

            if (form.Contents.Length < 25)
                DependencyService.Get<IMessage>().ShowShortTime("İçerik en az 25 karakter olmalıdır.");

            if (form.Title.Length < 3 || form.Contents.Length < 25)
                return;

            if (FormAnnouncement.Id.HasValue)
                form.Id = FormAnnouncement.Id.Value;

            var result = await AnnouncementService.CreateOrUpdateAsync(form);

            if (result.Data.HasValue)
                await Navigation.PopAsync();
        }
    }
}
