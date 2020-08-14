using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinGames
{
    public partial class PacmanGamePage : ContentPage
    {
        private int _rows; //rows in grid
        private int _columns; //cols in grid
        private Image pacmanSprite; //keep track of pacman sprite
        private int _score = 1; //score count
        private List<View> _ghosts; //list of ghosts

        //0 = WALL, 1 = COIN, 2 = PACMAN, 3 = GHOST
        private int[,] gridMap = { { 0,0,0,0,0,0,0,0,0,0},
                                   { 0,1,1,1,1,1,1,1,1,0},
                                   { 0,1,1,2,1,0,0,0,1,0},
                                   { 0,1,0,1,1,1,1,0,1,0},
                                   { 0,1,0,1,1,1,1,0,1,0},
                                   { 0,1,0,1,1,1,1,0,1,0},
                                   { 0,1,0,1,0,1,1,1,1,0},
                                   { 0,1,1,1,0,1,3,1,1,0},
                                   { 0,1,1,1,0,1,1,1,1,0},
                                   { 0,3,1,1,0,1,1,1,1,0},
                                   { 0,1,0,1,1,1,1,0,1,0},
                                   { 0,1,0,1,1,1,1,0,1,0},
                                   { 0,1,0,0,0,1,1,0,1,0},
                                   { 0,1,1,1,1,1,3,1,1,0},
                                   { 0,0,0,0,0,0,0,0,0,0}};

        public PacmanGamePage()
        {
            InitializeComponent();
            _ghosts = new List<View>();

            _rows = gridMap.GetLength(0); //# of rows
            _columns = gridMap.GetLength(1); //# of columns

            PopulateGrid(_rows, _columns);

            ChangeGhostPosition();
        }

        void PopulateGrid(int rows, int columns)
        {
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (gridMap[i, j] == 0)
                    {
                        SetWall(BaseImage(i,j));
                    }
                    else if (gridMap[i, j] == 1)
                    {
                        SetCoin(BaseImage(i, j));
                    }
                    else if (gridMap[i, j] == 2)
                    {
                        MakePacman(BaseImage(i,j));
                    }
                    else if (gridMap[i, j] == 3)
                    {
                        SetGhost(BaseImage(i,j));
                    }
                }
            }
        }
        
        Image BaseImage(int row, int col)
        {
            var image = new Image { HeightRequest = 30, ClassId = "BASE"};
            gameGrid.Children.Add(image, col, row);

            return gameGrid.Children.Where(i => Grid.GetRow(i) == row && Grid.GetColumn(i) == col).Single() as Image;
        }

        void SetWall(Image img)
        {
            img.BackgroundColor = Color.Blue;
            img.Scale = 1;
            img.ClassId = "WALL";
        }

        void SetCoin(Image img)
        {
            img.Source = ImageSource.FromResource("coin.png");
            img.Scale = 0.3;
            img.ClassId = "COIN";
        }

        void SetGhost(Image img)
        {
            img.Source = ImageSource.FromResource("ghost.png");
            img.Scale = 1;
            img.ClassId = "GHOST";
            _ghosts.Add(img as View);
        }

        void SetBlankBox(Image img)
        {
            img.Source = "";
            img.ClassId = "SPACE";
            img.Scale = 1;
            img.RotationY = 0;
            img.Rotation = 0;
        }

        void MakePacman(Image img)
        {
            img.Source = ImageSource.FromResource("pacman.png");
            img.ClassId = "PACMAN";
            img.Scale = 1;
            pacmanSprite = img;
        }

        void btnClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            MovePacman(button.ClassId);
        }

        void MovePacman(string direction)
        {
            int pacmanRow = Grid.GetRow(pacmanSprite), pacmanCol = Grid.GetColumn(pacmanSprite);
            int verticalDirection = 0, horizontalDirection = 0;

            if (direction == "UP") verticalDirection   = -1; //go up as row # decreases
            if (direction == "DOWN") verticalDirection  = 1;  //go down as row # increases
            if (direction == "LEFT") horizontalDirection = - 1; //go left as col # decreases
            if (direction == "RIGHT") horizontalDirection = 1; //go right as col # increases

            Image nextSpot = gameGrid.Children.Where(i => Grid.GetRow(i) == (pacmanRow + verticalDirection) &&
                                                        Grid.GetColumn(i) == (pacmanCol + horizontalDirection)).FirstOrDefault() as Image;

            if (nextSpot.ClassId != "WALL")
            {
                if (nextSpot.ClassId == "COIN") lblScore.Text = $"Score: {_score++}";
                if (nextSpot.ClassId == "GHOST") lblScore.Text = $"Score: {_score += 10}";

                MakePacman(nextSpot);
                RemoveOldPacman(pacmanRow, pacmanCol);
                
            }
            TurnPacMan(direction);

        }

        void RemoveOldPacman(int pacmanRowPrev, int pacmanColPrev)
        {
            SetBlankBox(gameGrid.Children.Where(i => Grid.GetRow(i) == pacmanRowPrev &&
                                                     Grid.GetColumn(i) == pacmanColPrev).FirstOrDefault() as Image);
        }

        void TurnPacMan(string direction)
        {
            if (direction == "UP")   pacmanSprite.Rotation = -90; //turn up
            if (direction == "DOWN") pacmanSprite.Rotation = 90; //turn down
            if (direction == "LEFT") pacmanSprite.RotationY = 180; //turn around
            if (direction == "RIGHT")pacmanSprite.RotationY = 0; //stay same 
        }

        async void ChangeGhostPosition()
        {
            try
            {
                do
                {
                    await Task.Delay(1000);

                    Random rnd = new Random();
                    int number = rnd.Next(0, 4);
                    string direction; //default directin

                    if (number == 0) direction = "UP";
                    else if (number == 1) direction = "DOWN";
                    else if (number == 2) direction = "LEFT";
                    else direction = "RIGHT";

                    _ghosts = gameGrid.Children.Where(i => i.ClassId == "GHOST").ToList();

                    Random rndGhost = new Random();
                    int ghostIdx = rndGhost.Next(_ghosts.Count);

                    MoveGhost(direction, ghostIdx);

                } while (_ghosts.Count > 0);
            }
            catch (Exception e)
            {
                //do nothing
            }

        }

        void MoveGhost(string direction, int ghostIdx)
        {
            int ghostRow = Grid.GetRow(_ghosts[ghostIdx]), ghostCol = Grid.GetColumn(_ghosts[ghostIdx]);
            int verticalDirection = 0, horizontalDirection = 0;

            if (direction == "UP") verticalDirection = -1; //go up as row # decreases
            if (direction == "DOWN") verticalDirection = 1;  //go down as row # increases
            if (direction == "LEFT") horizontalDirection = -1; //go left as col # decreases
            if (direction == "RIGHT") horizontalDirection = 1; //go right as col # increases

            Image nextSpot = gameGrid.Children.Where(i => Grid.GetRow(i) == (ghostRow + verticalDirection) &&
                                                        Grid.GetColumn(i) == (ghostCol + horizontalDirection)).FirstOrDefault() as Image;

            if (nextSpot.ClassId == "COIN" || nextSpot.ClassId == "SPACE")
            {
                RemoveOldGhost(ghostRow, ghostCol);
                SetGhost(nextSpot);
            }

        }

        void RemoveOldGhost(int row, int col)
        {
            SetCoin(gameGrid.Children.Where(i => Grid.GetRow(i) == row &&
                                                     Grid.GetColumn(i) == col).FirstOrDefault() as Image);
        }
    }
}

//Class Ids
//WALL ->Border
//SPACE -> clear space
//PACMAN -> pacman sprite
//COIN -> coin sprite
//GHOST -> ghost sprite
//BASE -> Blank base box
