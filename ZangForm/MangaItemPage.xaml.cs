using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ZangForm
{
    public partial class MangaItemPage : ContentPage
    {
        MangaList manga;
        IQueryable<MangaItem> mangas;

        public MangaItemPage(MangaList manga)
        {
            InitializeComponent();

            this.manga = manga;
            this.mangas = Manga.QueryMangaItem(manga);
            this.listView.ItemsSource = this.mangas;

            this.Title = manga.Title;
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.mangas.Count() == 0)
            {
                await Manga.FetchMangaItem(this.manga);
            }
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new MangaContentPage(e.SelectedItem as MangaItem));
        }
    }
}
