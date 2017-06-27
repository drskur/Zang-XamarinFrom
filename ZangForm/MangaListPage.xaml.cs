using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ZangForm
{
    public partial class MangaListPage : ContentPage
    {
        IQueryable<MangaList> mangaList;

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

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new MangaItemPage(e.SelectedItem as MangaList));
        }
    }
}
