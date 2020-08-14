using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TestApp
{
    public partial class RockPaperScissorsPage : ContentPage
    {
        //4.
        int wins = 0;
        int losses = 0;

        public RockPaperScissorsPage()
        {
            
            InitializeComponent();
        }

        void RPCButtonClicked(object sender, System.EventArgs e)
        {
            //1.
            var buttonClicked = sender as Button;
            player1.Text = buttonClicked.Text;

            HandlePlayer2Selection(); //Add Later, do logic here at first 
        }

        void HandlePlayer2Selection()
        {
            //2.
            //choose a random selection for player 2
            //set selection for player2
            //check for winner
            //print out the winner
            Random r = new Random();
            int randomInt = r.Next(3); //0,1,2
            //lblDisplay.Text = randomInt.ToString();

            if (randomInt == 0) player2.Text = "Rock";
            if (randomInt == 1) player2.Text = "Paper";
            if (randomInt == 2) player2.Text = "Scissors";

            //3.
            
            string result = "";

            if (player1.Text == "Rock")
            {
                if (player2.Text == "Scissors") result = "Win";
                else if (player2.Text == "Paper") result = "Lose";
                else if (player2.Text == "Rock") result = "Tied";
            }
            else if (player1.Text == "Paper") 
            {
                if (player2.Text == "Scissors") result = "Lose";
                else if (player2.Text == "Paper") result = "Tied";
                else if (player2.Text == "Rock") result = "Win";
            }
            else if (player1.Text == "Scissors")
            {
                if (player2.Text == "Scissors") result = "Tied";
                else if (player2.Text == "Paper") result = "Win";
                else if (player2.Text == "Rock") result = "Lose";
            }

            //lblDisplay.Text = $"You {result}";

            //4.
            //keep track of score
            if (result == "Win") wins++;
            if (result == "Lose") losses++;
            lblDisplay.Text = $"Wins: {wins} - Losses: {losses}";

        }
    }
}


//void ButtonClicked(object sender, System.EventArgs e)
//{
//    throw new NotImplementedException();
//}