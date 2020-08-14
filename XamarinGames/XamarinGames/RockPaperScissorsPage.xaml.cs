using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinGames
{
    public partial class RockPaperScissorsPage : ContentPage
    {
        int wins = 0;
        int losses = 0;

        public RockPaperScissorsPage()
        {
            InitializeComponent();
        }

        void RPCButtonClicked(object sender, System.EventArgs e)
        {
            var buttonClicked = sender as Button;
            lblPlayer1.Text = buttonClicked.Text;

            HandlePlayer2Selection();
        }

        void HandlePlayer2Selection()
        {
            //choose a random selection for player2
            //set the selection for player2
            //check for winner
            //print out the winner

            Random r = new Random();
            int randomInt = r.Next(3); //returns 0,1,or 2

            if (randomInt == 0) lblPlayer2.Text = "Rock";
            if (randomInt == 1) lblPlayer2.Text = "Paper";
            if (randomInt == 2) lblPlayer2.Text = "Scissors";

            string result = "";

            if (lblPlayer1.Text == "Rock")
            {
                if (lblPlayer2.Text == "Scissors") result = "Win";
                else if(lblPlayer2.Text == "Paper") result = "Lose";
                else if(lblPlayer2.Text == "Rock") result = "Tied";
            }
            else if (lblPlayer1.Text == "Paper")
            {
                if (lblPlayer2.Text == "Scissors") result = "Lose";
                else if (lblPlayer2.Text == "Paper") result = "Tied";
                else if (lblPlayer2.Text == "Rock") result = "Win";
            }
            else if (lblPlayer1.Text == "Scissors")
            {
                if (lblPlayer2.Text == "Scissors") result = "Tied";
                else if (lblPlayer2.Text == "Paper") result = "Win";
                else if (lblPlayer2.Text == "Rock") result = "Lose";
            }

            if (result == "Win") wins++;//increments by 1
            if (result == "Lose") losses++;

            lblDisplay.Text = $"You {result} - Wins: {wins} | Losses: {losses}";
        }
    }
}
