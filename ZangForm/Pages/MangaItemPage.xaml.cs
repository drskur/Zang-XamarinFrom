﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ZangForm
{
    public partial class MangaItemPage : ContentPage
    {
        private MangaList manga { get; set; }
        private IQueryable<MangaItem> mangas { get; set; }

        public MangaItemPage(MangaList manga)
        {
            this.manga = manga;
            this.mangas = Manga.QueryMangaItem(manga);

            InitializeComponent();

            this.BindingContext = this.manga;
            this.SetBinding(TitleProperty, "Title");
            this.listView.ItemsSource = this.mangas;
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.mangas.Count() == 0)
            {
                await Manga.FetchMangaItem(this.manga);
            }
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
			((ListView)sender).SelectedItem = null;
			await Navigation.PushAsync(new MangaContentPage(e.Item as MangaItem));
        }
    }
}
