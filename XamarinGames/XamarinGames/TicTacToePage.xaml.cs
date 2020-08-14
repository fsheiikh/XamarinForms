using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinGames
{
    public partial class TicTacToePage : ContentPage
    {
        private bool myTurn = false;

        private int[] boardElements = new int[9];  //0 is default/blank, 1=X, 2=O

        int count = 0;

        public TicTacToePage()
        {
            InitializeComponent();
        }

        void clickedButton(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            count++;

            myTurn = !myTurn; //if false, make true and vice versa

            if (String.IsNullOrEmpty(btn.Text)) //so we dont change a btn that has already been set
            {
                if (myTurn == true) btn.Text = "X";
                else btn.Text = "O";
            }

            int boxNumber = Convert.ToInt32(btn.ClassId);

            if (CheckForWinner(boxNumber, btn.Text)) //if we get a winning combo
            {

                if (btn.Text == "X") DisplayAlert("Game Over", "Winner is X", "OK");
                else DisplayAlert("Game Over", "Winner is O", "OK");

                boardElements = new int[9];

                ResetButtonText();
            }
            else if (count == 9)
            {
                ResetButtonText();
            }

        }

        bool CheckForWinner(int boxNumber, string btnText)
        {
            boardElements[boxNumber] = (btnText == "X") ? 1 : 2; //if text is X, set 1 in array, else set 2(O)

            if (CheckThreeElements(0, 1, 2) ||
               CheckThreeElements(3, 4, 5) ||
               CheckThreeElements(6, 7, 8) ||

               CheckThreeElements(0, 3, 6) ||
               CheckThreeElements(1, 4, 7) ||
               CheckThreeElements(2, 5, 8) ||
               CheckThreeElements(0, 4, 8) ||
               CheckThreeElements(2, 4, 6))
            {
                return true;
            }

            return false;
        }

        bool CheckThreeElements(int first, int second, int third) 
        {
            //this ensures only selected boxes are accounted for
            if (boardElements[first] == 0 ||
                boardElements[second] == 0 ||
                boardElements[third] == 0)
            {
                return false;
            }

            if (boardElements[first].Equals(boardElements[second]) &&
                boardElements[first].Equals(boardElements[third]))
            {
                return true;
            }

            return false;
        }

        async void ResetButtonText()
        {
            await Task.Delay(2000);

            Button[] buttonArray = { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };

            foreach (var btn in buttonArray)
            {
                btn.Text = "";
            }

            count = 0;
        }
    }
}

//[0,1,2,3,4,5,6,7,8] => the classIds of the buttons

//[0][1][2]
//[3][4][5]
//[6][7][8]

//winning combos

//0,1,2
//3,4,5
//6,7,8

//0,3,6
//1,4,7
//2,5,8

//0,4,8
//2,4,6

