using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobAnnouncApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobAnnouncApp.Views
{
    public partial class MainPage : MasterDetailPage
    {
        public Models.Menu Menu;


        public MainPage()
        {
            InitializeComponent();
            InitializeMenu();

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(AnnouncementListPage)));

            BindingContext = new { menu = Menu };
        }

        private void InitializeMenu()
        {
            UserService userService = new UserService();
            string userFullName = userService.CurrentUserName();

            Menu = new Models.Menu
            {
                Welcome = $"Hoş Geldiniz {userFullName}",
                GeneralItems = new List<Models.Menu.Item>(),
                ProfileItems = new List<Models.Menu.Item>(),
                ActionItems = new List<Models.Menu.Item>()
            };

            Menu.GeneralItems.Add(new Models.Menu.Item { Title = "Duyurular", TargetType = typeof(AnnouncementListPage), IsPage = false });

            Menu.ProfileItems.Add(new Models.Menu.Item { Title = "Duyurularınız", TargetType = typeof(ProfileAnnouncementListPage), IsPage = false });
            Menu.ProfileItems.Add(new Models.Menu.Item { Title = "Beğenileriniz", TargetType = typeof(ProfileLikeListPage), IsPage = false });
            Menu.ProfileItems.Add(new Models.Menu.Item { Title = "Önerileriniz", TargetType = typeof(ProfileRecommendationListPage), IsPage = false });

            Menu.ActionItems.Add(new Models.Menu.Item { Title = "Yeni Duyuru Ekle", TargetType = typeof(AnnouncementFormPage), IsPage = true });
            Menu.ActionItems.Add(new Models.Menu.Item { Title = "Arama", TargetType = typeof(SearchPage), IsPage = false });
        }

        private void MenuItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (Models.Menu.Item)e.Item;
            Type page = item.TargetType;

            if (item.IsPage)
                Navigation.PushAsync((Page)Activator.CreateInstance(page));
            else
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));

            IsPresented = false;
        }
    }
}
