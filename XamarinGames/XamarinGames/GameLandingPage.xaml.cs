using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinGames
{
    public partial class GameLandingPage : ContentPage
    {
        public GameLandingPage()
        {
            InitializeComponent();
 
        }

        async void btnClick(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            ContentPage page = new ContentPage();

            switch (btn.ClassId)
            {
                case "0":
                    page = new RockPaperScissorsPage();
                    break;
                case "1":
                    page = new TicTacToePage();
                    break;
                case "2":
                    page = new MemoryGame();
                    break;
                case "3":
                    page = new AnimationPractice();
                    break;
                case "4":
                    page = new PacmanGamePage();
                    break;
                default:
                    break;
            }

            await Navigation.PushAsync(page);
        }
    }
}
