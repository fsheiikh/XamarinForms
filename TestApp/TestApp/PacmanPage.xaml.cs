using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class PacmanPage : ContentPage
    {

        private int _colummns;
        private int _rows;
        private Image pacmanSprite;

        private int _score = 1;

        //add after pacman can move everywhere. 0 = WALL, 1 = Coin, 2 = PACMAN, 3 = Ghost
        private int[,] gridMap = { {0,0,0,0,0,0,0,0,0,0},
                                   {0,1,1,1,3,1,1,1,1,0},
                                   {0,1,0,1,1,0,0,0,1,0},
                                   {0,1,0,1,1,1,1,1,1,0},
                                   {0,1,0,1,1,1,3,1,1,0},
                                   {0,1,0,1,1,1,1,1,1,0},
                                   {0,1,1,1,0,0,1,1,1,0},
                                   {0,1,1,1,0,0,1,1,1,0},
                                   {0,1,1,1,1,1,1,1,1,0},
                                   {0,1,0,1,1,3,1,1,1,0},
                                   {0,1,0,1,1,1,1,0,1,0},
                                   {0,1,0,1,1,1,1,0,1,0},
                                   {0,3,0,1,1,0,0,0,1,0},
                                   {0,1,1,2,1,1,1,1,1,0},
                                   {0,0,0,0,0,0,0,0,0,0}};

        private List<View> _ghosts = new List<View>();

        public PacmanPage()
        {
            InitializeComponent();

            //_rows = 15;
            //_colummns = 10;

            _rows = gridMap.GetLength(0);
            _colummns = gridMap.GetLength(1);

            PopulateGameGrid(_rows, _colummns);
            //SetWall(); //show at first remove in second implementation
            //PlaceSprites(); //firt time to show how to place sprite
            //ChangePosition(); //first time to show how position chnages
        }

        //Class ID
        //wall -> border/wall
        //space -> clear space
        //pacman -> pacman
        //coin-> coin

        //<Image Grid.Row="0" Grid.Column= "0" BackgroundColor= "yellow" HeightRequest= "25" />

        void PopulateGameGrid(int rows, int columns)
        {
           
            for (var i = 0; i < rows; i++)
            {
                for(var j = 0; j < columns; j++)
                {


                    if (gridMap[i, j] == 0)
                    {
                        SetWall(i, j);
                    }
                    else if (gridMap[i, j] == 2)
                    {
                        SetPacManSprite(i, j);
                    }
                    else if (gridMap[i, j] == 3)
                    {
                        SetGhost(i, j);
                    }
                    else if (j == 0)//not needed later
                    {
                        //SetFirstColumnBox(i, j); //set first box in first column 
                    }
                    else
                    {
                        SetBox(i, j);
                    }
                }
            }
        }

        //add after adding gamegrid array and code to function above, and then later not needed
        void SetFirstColumnBox(int i, int j)
        {
            gameGrid.Children.Add(new Image { BackgroundColor = Color.DimGray, HeightRequest = 30, ClassId = "space" }, j, i);
        }
        void SetBox(int i, int j)
        {
            //gameGrid.Children.Add(new Image { BackgroundColor = Color.DimGray, ClassId = "SPACE" }, j, i);
            //have gray dim color until added coin, no coin until pacman move around and all exceptions fixed.
            gameGrid.Children.Add(new Image { 
                                              ClassId = "COIN",
                                              Source=ImageSource.FromResource("coin.png"), Scale=0.3  }, j, i);
        }

        void SetGhost(int i, int j)
        {
            if (gameGrid.Children.Where(x => x.ClassId == "GHOST").Count() < 4)//add let in ghost movemet
            {
                var ghostImage = new Image() { ClassId = "GHOST", Source = ImageSource.FromResource("ghost.png") };

                gameGrid.Children.Add(ghostImage, j, i);
                //_ghosts = gameGrid.Children.Where(x => x.ClassId == "GHOST").ToList();
               
            }

        }

        void SetBlankBox(int i, int j)
        {
            //gameGrid.Children.Add(new Image { BackgroundColor = Color.DimGray, ClassId = "SPACE" }, j, i);
            //have gray dim color until added coin, no coin until pacman move around and all exceptions fixed.
            gameGrid.Children.Add(new Image { ClassId = "SPACE" }, j, i);
        }

        void SetWall(int i, int j)
        {
            //first implementation. + doesnt have any parameters
            //List<Image> imageList = new List<Image>();

            //foreach (var i in gameGrid.Children)
            //{
            //    if (Grid.GetRow(i) == 0 ||
            //        Grid.GetColumn(i) == _colummns-1 ||
            //        Grid.GetRow(i) == _rows-1 ||
            //        Grid.GetColumn(i) == 0)
            //    {
            //        i.BackgroundColor = Color.Blue;
            //        i.ClassId = "wall";
            //    }

            //    //imageList.Add(i as Image);
            //}

            //second implement with grid array numbers

            gameGrid.Children.Add(new Image { BackgroundColor = Color.Blue, ClassId = "WALL", HeightRequest = 30 }, j, i);

        }

        void SetPacManSprite(int i, int j)
        {
            if (pacmanSprite == null)//make sure only one is set
                gameGrid.Children.Add(MakePacMan(new Image()), j, i);
            else
                SetBox(i, j);
        }

        //void PlaceSprites()
        //{
        //    foreach (var i in gameGrid.Children)
        //    {
        //        var img = i as Image;
        //        if (Grid.GetRow(i) == 3 && Grid.GetColumn(i) != 0)
        //        {
        //            MakePacMan(img);
        //            return;
        //        }

        //        //imageList.Add(i as Image);
        //    }
        //}

        Image MakePacMan(Image img)
        {
            img.Source = ImageSource.FromResource("pacman.png");
            img.ClassId = "PACMAN";
            //img.BackgroundColor = Color.DimGray;
            img.Scale = 1;

            pacmanSprite = img;

            //after coin stuff, way after
            pacmanSprite.AnchorX = 0.5;
            pacmanSprite.AnchorY = 0.5;

            return pacmanSprite;
        }


        void btnClick(object sender, System.EventArgs e)
        {
            var btn = sender as Button;

            MovePacman(btn.ClassId.ToUpper());


            //switch (btn.ClassId)
            //{
            //    case "Up":
            //        MovePacmanUp();
            //        break;
            //    case "Down":
            //        break;
            //    case "Left":
            //        break;
            //    case "Right":
            //        break;
            //}
        }

        //make first and test, the subsequent functions can be implemented using the same code with tweaks
        //bool MovePacmanUp()
        //{
        //    int pacmanRow = Grid.GetRow(pacmanSprite);
        //    int pacmanColumn = Grid.GetColumn(pacmanSprite);

        //    Image rowAbove = gameGrid.Children.Where(newLocation => Grid.GetRow(newLocation) == (pacmanRow - 1) &&
        //                                                            Grid.GetColumn(newLocation) == (pacmanColumn)).Single() as Image;

        //    if (rowAbove.ClassId == "wall") //collison check
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        RemoveOldPacman();
        //        MakePacMan(rowAbove);
        //        return true;
        //    }
        //}

        //dont add this until youve done the up way with numbers and no direction variables
        void MovePacman(string direction)
        {
            //ChangeGhostPosition();
            int verticalDirection = 0, horizontalDirection = 0;

            int pacmanRow = Grid.GetRow(pacmanSprite), pacmanColumn = Grid.GetColumn(pacmanSprite);

            if (direction.ToUpper() == "UP") verticalDirection = -1;
            if (direction.ToUpper() == "DOWN") verticalDirection = 1;
            if (direction.ToUpper() == "LEFT") horizontalDirection = -1;
            if (direction.ToUpper() == "RIGHT") horizontalDirection = 1;

            //horizontalDirection = (direction.ToUpper() == "LEFT") ? -1 : horizontalDirection;
            //horizontalDirection = (direction.ToUpper() == "RIGHT") ? 1 : horizontalDirection;

            Image nextSpot = gameGrid.Children.Where(newLocation => Grid.GetRow(newLocation) == (pacmanRow + verticalDirection) &&
                             Grid.GetColumn(newLocation) == (pacmanColumn + horizontalDirection)).FirstOrDefault() as Image;

            
            if (nextSpot.ClassId.ToUpper() != "WALL") //collision check
            {


                if (nextSpot.ClassId.ToUpper() == "GHOST" || CheckForGhost(nextSpot))
                {
                    lblScore.Text = $"Score: {_score += 10}";
                    RemoveOldGhost(pacmanRow + verticalDirection, pacmanColumn + horizontalDirection);

                    _ghosts.Remove(_ghosts.Where(g => Grid.GetRow(g) == (pacmanRow + verticalDirection) &&
                                                 Grid.GetColumn(g) == (pacmanColumn + horizontalDirection)).FirstOrDefault() as View);
                }

                if (nextSpot.ClassId.ToUpper() == "COIN")
                    lblScore.Text = $"Score: {_score++}";


                MakePacMan(nextSpot);
                RemoveOldPacman(pacmanRow, pacmanColumn);
                TurnPacMan(direction);
            }
            ChangeGhostPosition();

        }

        void TurnPacMan(string direction)
        {
            if (direction == "UP") pacmanSprite.Rotation = -90;
            if (direction == "DOWN") pacmanSprite.Rotation = 90;
            if (direction == "RIGHT") pacmanSprite.RotationY = 0;
            if (direction == "LEFT") pacmanSprite.RotationY = 180;
        }


        //talk exception if prev isnt reset
        void RemoveOldPacman(int pacmanRowPrev, int pacmanColumnPrev)
        {
            gameGrid.Children.Remove(gameGrid.Children.Where(p => Grid.GetRow(p) == (pacmanRowPrev) &&
                                                Grid.GetColumn(p) == (pacmanColumnPrev)).FirstOrDefault());


            //dpont add yet
            //gameGrid.Children.Add(new Image { BackgroundColor = Color.Gray, ClassId = "space" }, pacmanColumnCurr, pacmanRowCurr);
            SetBlankBox(pacmanRowPrev, pacmanColumnPrev);
            
        }

        //show for example
        //async void ChangePosition()
        //{
        //    await Task.Delay(5000);

        //    foreach (var i in gameGrid.Children)
        //    {
        //        var img = i as Image;

        //        if (img.ClassId == "1")
        //        {
        //            img.Source = ImageSource.FromResource("");
        //            img.ClassId = "0";
        //        }

        //        if (Grid.GetRow(i) == 5 && Grid.GetColumn(i) != 0)
        //        {
        //            img.Source = ImageSource.FromResource("TestApp.Images.pacman.png");
        //            img.ClassId = "1";
        //            return;
        //        }
        //        //imageList.Add(i as Image);
        //    }

        //}

        void ChangeGhostPosition()
        {
            //do
            //{
            //    await Task.Delay(500);

                Random rnd = new Random();
                int number = rnd.Next(0, 4);
                string direction = "UP";

                if (number == 0) direction = "UP";
                if (number == 1) direction = "DOWN";
                if (number == 2) direction = "LEFT";
                if (number == 3) direction = "RIGHT";

                _ghosts = gameGrid.Children.Where(x => x.ClassId == "GHOST").ToList();

                //var ghostPosition = gameGrid.Children.Where(loc => Grid.GetRow(loc) == Grid.GetRow(_ghosts.First()) &&
                //                                                 Grid.GetColumn(loc) == Grid.GetColumn(_ghosts.First())).FirstOrDefault() as Image;



                MoveGhost(direction);

            //} while (!lblScore.Text.Contains("20"));
        }


        void MoveGhost(string direction)
        {
            int verticalDirection = 0, horizontalDirection = 0;

            int ghostRow = Grid.GetRow(_ghosts.First()), ghostColumn = Grid.GetColumn(_ghosts.First());

            if (direction.ToUpper() == "UP") verticalDirection = -1;
            if (direction.ToUpper() == "DOWN") verticalDirection = 1;
            if (direction.ToUpper() == "LEFT") horizontalDirection = -1;
            if (direction.ToUpper() == "RIGHT") horizontalDirection = 1;

            Image nextSpot = gameGrid.Children.Where(newLocation => Grid.GetRow(newLocation) == (ghostRow + verticalDirection) &&
                             Grid.GetColumn(newLocation) == (ghostColumn + horizontalDirection)).FirstOrDefault() as Image;

            if (nextSpot.ClassId.ToUpper() != "WALL") //collision check
            {
                RemoveOldGhost(ghostRow, ghostColumn);
                SetGhost(Grid.GetRow(nextSpot), Grid.GetColumn(nextSpot));
                _ghosts.Remove(_ghosts.First());
            }
        }

        void RemoveOldGhost(int ghostRowCurr, int ghostColumnCurr)
        {
            gameGrid.Children.Remove(gameGrid.Children.Where(p => Grid.GetRow(p) == (ghostRowCurr) &&
                                                Grid.GetColumn(p) == (ghostColumnCurr)).FirstOrDefault());

            SetBox(ghostRowCurr, ghostColumnCurr);
        }

        //add last after ghost bug
        bool CheckForGhost(Image nextSpot)
        {
           return  _ghosts.Any(g => Grid.GetRow(g) == Grid.GetRow(nextSpot) &&
                                    Grid.GetColumn(g) == Grid.GetColumn(nextSpot));
        }
    }
}


///
//assuming the image is in column 1
//var image = grid.Children.Where(c => Grid.GetRow(c) == row && Grid.GetColumn(c) == 1);

//ghost
//wait half second, check up, down, left, right, whichever is open, try 3 times to move in that direction
//check all four again, and repeat
//if hit a wall, trigger check
//0=left, 1=up, 2=right, 3=down