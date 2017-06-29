using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ZangForm
{
    public partial class MangaListPage : ContentPage
    {
        private IQueryable<MangaList> mangaList { get; set; }

        public MangaListPage()
        {
            InitializeComponent();

            this.mangaList = Manga.QueryMangaList();
            listView.ItemsSource = mangaList;
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

			if (mangaList.Count() == 0)
			{
                await Manga.FetchMangaList();
			}
		}

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new MangaItemPage(e.Item as MangaList));
        }
    }
}
