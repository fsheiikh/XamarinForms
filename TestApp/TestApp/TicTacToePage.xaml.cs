using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class TicTacToePage : ContentPage
    {
        //2
        private bool myTurn = false;

        //3
        private int[] boardElements = new int[9];

        //9
        int count = 0;

        public TicTacToePage()
        {
            InitializeComponent();
        }

        //1
        void clickedBtn(object sender, System.EventArgs e)
        {
            //1
            var btn = sender as Button;
            //btn.Text = "X";

            //9
            count++;
            
            //2
            myTurn = !myTurn; //if false, becomes true and vice versa

            if (String.IsNullOrEmpty(btn.Text))//so we dont change if aleady set 
            {
                if (myTurn == true) btn.Text = "X";
                else btn.Text = "O";
            }



            //4
            int boxNumber = Convert.ToInt32(btn.ClassId);

            if (CheckForWinner(boxNumber, btn.Text))
            {
                //5
                if (btn.Text == "X") DisplayAlert("Game Over", "Winner is X", "OK");
                else DisplayAlert("Game Over", "Winner is O", "OK");

                //6 challenge
                boardElements = new int[9];
                //after added x:Name to xml
                ResetButtonsText();

            }//under this after 9
            else
            {
                if (count == 9)
                    ResetButtonsText();
            }

        }

        //4
        bool CheckForWinner(int boxNumber, string btnText)
        {
            
            boardElements[boxNumber] = (btnText == "X") ? 1 : 2; //if X put 1, if O put 2

            if(CheckThreeElements(0,1,2) ||
               CheckThreeElements(3,4,5) ||
               CheckThreeElements(6,7,8) ||

               CheckThreeElements(0,3,6) ||
               CheckThreeElements(1,4,7) ||
               CheckThreeElements(2,5,8) ||

               CheckThreeElements(0,4,8) ||
               CheckThreeElements(2,4,6))
            {
                return true;
            }

            return false;
        }

        //4.5
        bool CheckThreeElements(int first, int second, int third)
        {
            //this is to ensure only sleected boxes are accounted for
            if (boardElements[first] == 0 || boardElements[second] == 0 || boardElements[third] == 0)
                return false;

            //we cant use == for three variables 
            if (boardElements[first].Equals(boardElements[second]) &&
               boardElements[first].Equals(boardElements[third]))
            {
                return true;
            }

            return false;
        }

        //6.5
        async void ResetButtonsText() //async later
        {
            await Task.Delay(2000);//add later

            Button[] buttonArray = { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9};

            foreach (var btn in buttonArray)
            {
                btn.Text = "";
            }
            //lblDisplay.Text = "";

            //7. add delay time and async method to top

            //await Task.Delay(5000);

            //9
            count = 0;
        }
    }
}

//[0,1,2,3,4,5,6,7,8]

//[0][1][2]
//[3][4][5]
//[6][7][8]

//0,1,2
//3,4,5
//6,7,8

//0,3,6
//1,4,7
//2,5,8

//0,4,8
//2,4,6



