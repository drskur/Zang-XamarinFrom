using System;

using Xamarin.Forms;

namespace ZangForm.Pages
{
    public class MangaPage : ContentPage
    {
        public MangaPage()
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

