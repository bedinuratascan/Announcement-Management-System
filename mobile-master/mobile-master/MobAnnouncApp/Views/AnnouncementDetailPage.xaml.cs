using System;
using MobAnnouncApp.Models.Announcement;
using MobAnnouncApp.Services;
using Xamarin.Forms;
using static MobAnnouncApp.Models.Announcement.ListAnnouncement;

namespace MobAnnouncApp.Views
{
    public partial class AnnouncementDetailPage : ContentPage
    {
        private int AnnouncementId { get; set; }
        private ShowAnnouncement Announcement { get; set; }
        private bool IsFirstLoad { get; set; } = true;
        private AnnouncementService AnnouncementService = new AnnouncementService();
        private ToolbarItem EditMenu { get; set; }
        private ToolbarItem DeleteMenu { get; set; }
        private ToolbarItem LikeMenu { get; set; }
        private bool IsLiked { get; set; }

        public AnnouncementDetailPage(int announcementId)
        {
            InitializeComponent();

            AnnouncementId = announcementId;

            LoadData();
        }

        protected async override void OnAppearing()
        {
            if (IsFirstLoad)
                return;

            LoadData();
        }

        async void LoadData()
        {

            var result = await AnnouncementService.FetchByIdAsync(AnnouncementId);

            if (!result.Data.HasValue)
                return;

            Announcement = result.Data.Value;
            IsLiked = Announcement.IsLiked;
            Title.Text = Announcement.Title;
            Contents.Text = Announcement.Contents;
            Author.Text = Announcement.User;
            LikeIcon.TextColor = Announcement.IsLiked ? Color.Red : Color.Gray;
            LikeIcon.FontFamily = Announcement.LikeStatus.FontFamily;
            Like.Text = Announcement.LikeCountText;

            PageTitle.Title = Announcement.Title;

            if (IsFirstLoad)
                InitialToolbar();
        }

        void InitialToolbar()
        {
            IsFirstLoad = false;

            if (Announcement.IsOwn)
            {
                EditMenu = new ToolbarItem {
                    Text = "Düzenle",
                    IconImageSource = "edit_icon.png",
                    Order = ToolbarItemOrder.Primary
                };
                EditMenu.Clicked += EditMenuClicked;
                ToolbarItems.Add(EditMenu);

                DeleteMenu = new ToolbarItem {
                    Text = "Sil",
                    IconImageSource = "delete_icon.png",
                    Order = ToolbarItemOrder.Primary
                };
                DeleteMenu.Clicked += DeleteMenuClicked;
                ToolbarItems.Add(DeleteMenu);
            }
            else
            {
                string message;
                string icon;
                if (Announcement.IsLiked)
                {
                    message = "Beğeniyi Çek";
                    icon = "favorite_icon.png";
                }
                else
                {
                    message = "Beğen";
                    icon = "unfavorite_icon.png";
                }

                LikeMenu = new ToolbarItem {
                    Text = message,
                    IconImageSource = icon,
                    Order = ToolbarItemOrder.Primary
                };
                LikeMenu.Clicked += LikeMenuClicked;
                ToolbarItems.Add(LikeMenu);
            }
        }

        void EditMenuClicked(object sender, EventArgs e)
        {
            var formAnnouncement = new FormAnnouncement()
            {
                Id = Announcement.Id,
                Title = Announcement.Title,
                Contents = Announcement.Contents
            };

            Navigation.PushAsync(new AnnouncementFormPage(formAnnouncement));
        }

        async void DeleteMenuClicked(object sender, EventArgs e)
        {
            bool result = await AnnouncementService.DeleteAsync(Announcement.Id);

            if (result)
                await Navigation.PopAsync();
        }

        async void LikeMenuClicked(object sender, EventArgs e)
        {
            bool result;

            if (IsLiked)
            {
                result = await AnnouncementService.UnLikeAsync(Announcement.Id);

                if (!result)
                    return;

                LikeMenu.Text = "Beğen";
                LikeMenu.IconImageSource = "unfavorite_icon.png";
                LikeIcon.TextColor = Color.Gray;
                LikeIcon.FontFamily = "fa-regular-400.ttf#Font Awesome 5 Free";
                IsLiked = false;

                string likeText = Like.Text;
                likeText = likeText.Replace("Beğeni", "");
                int likeCount = int.Parse(likeText);
                Like.Text = $"{likeCount - 1} Beğeni";
            }
            else
            {
                result = await AnnouncementService.LikeAsync(Announcement.Id);

                if (!result)
                    return;

                LikeMenu.Text = "Beğeniyi Çek";
                LikeMenu.IconImageSource = "favorite_icon.png";
                LikeIcon.TextColor = Color.Red;
                LikeIcon.FontFamily = "fa-solid-900.ttf#Font Awesome 5 Free";
                IsLiked = true;

                string likeText = Like.Text;
                likeText = likeText.Replace("Beğeni", "");
                int likeCount = int.Parse(likeText);
                Like.Text = $"{likeCount + 1} Beğeni";
            }
        }
    }
}
