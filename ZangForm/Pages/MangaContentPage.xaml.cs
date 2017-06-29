using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ZangForm
{
    public partial class MangaContentPage : CarouselPage
    {
        private MangaItem mangaItem { get; set; }
        private IQueryable<MangaContent> contents { get; set; }

        public MangaContentPage(MangaItem item)
        {
            InitializeComponent();

            this.mangaItem = item;
            this.contents = Manga.QueryMangaContent(this.mangaItem);

            this.BindingContext = this.mangaItem;
            this.SetBinding(TitleProperty, "Title");
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

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            NavigationPage.SetHasNavigationBar(this, !NavigationPage.GetHasNavigationBar(this));
        }
    }
}
