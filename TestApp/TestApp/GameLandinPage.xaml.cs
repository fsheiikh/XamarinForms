using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TestApp
{
    public partial class GameLandinPage : ContentPage
    {

        public GameLandinPage()
        {
            InitializeComponent();

            
        }

        async void btnClicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;

            ContentPage page = new ContentPage();

            switch (btn.ClassId)
            { 
                case "0":
                    page = new RockPaperScissorsPage();
                    break;
                case "1":
                    page = new MemoryPuzzlePage();
                    break;
                case "2":
                    page = new TicTacToePage();
                    break;
                case "3":
                    page = new AnimationPage();
                    break;
                case "4":
                    page = new PacmanPage();
                    break;
                default:
                    break;
            }

            await Navigation.PushAsync(page);
        }
    }
}
