using System;

using Xamarin.Forms;

namespace ZangForm
{
    public class MangaContentPage : ContentPage
    {
        public MangaContentPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

