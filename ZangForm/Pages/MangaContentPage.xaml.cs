using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ZangForm
{
    public partial class MangaContentPage : CarouselPage
    {
        MangaItem mangaItem;
        IQueryable<MangaContent> contents;

        public MangaContentPage(MangaItem item)
        {
            InitializeComponent();

            this.mangaItem = item;
            this.contents = Manga.QueryMangaContent(this.mangaItem);

            this.Title = this.mangaItem.Title;

            this.ItemsSource = contents;
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.contents.Count() == 0) 
            {
                await Manga.FetchMangaContents(this.mangaItem);    
            }
        }
    }
}
