using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinGames
{
    public partial class MemoryGame : ContentPage
    {

        private Image[] images; //images o
        private ImageSource[] imageSources;

        private Image firstSelectedImage;
        private Image secondSelectedImage;

        private int turnCount = 0;

        //opacity variables
        private double _visible = 1;
        private double _dull = 0.5;
        private double _invisible = 0.0100001;

        public MemoryGame()
        {
            InitializeComponent();

            images =  new Image[]{img1, img2, img3,img4,img5, img6, img7, img8,img9, img10, img11, img12,img13, img14, img15, img16};

            imageSources = GenerateImageSources();

            SetImages();
        }

        ImageSource[] GenerateImageSources()
        {
            ImageSource appleImage = ImageSource.FromResource("XamarinGames.Images.apple.png");
            ImageSource orangeImage = ImageSource.FromResource("XamarinGames.Images.orange.png");
            ImageSource bananaImage = ImageSource.FromResource("XamarinGames.Images.banana.png");
            ImageSource grapeImage = ImageSource.FromResource("XamarinGames.Images.grapes.png");
            ImageSource lemonImage = ImageSource.FromResource("XamarinGames.Images.lemon.png");
            ImageSource melonImage = ImageSource.FromResource("XamarinGames.Images.melon.png");
            ImageSource avacadoImage = ImageSource.FromResource("XamarinGames.Images.avocado.png");
            ImageSource berryImage = ImageSource.FromResource("XamarinGames.Images.berry.png");

            return new ImageSource[] { appleImage, orangeImage, bananaImage, grapeImage, lemonImage,
                                               melonImage, avacadoImage, berryImage};

        }

        void SetImages() //Randomize images 
        {
            Random rand = new Random();
            images = images.OrderBy(img1 => rand.Next()).ToArray();

            for (var i = 0; i < images.Length; i++)//iterate through randomized image objects
            {
                //set sources for images
                images[i].Source = imageSources[i/2]; //divide by two since source array is 8 and total images are 16.

                SetImageGesture(images[i]);

                images[i].Opacity = _visible;
            }
        }

        void OnSwitchToggle(object sender, System.EventArgs e)
        {
            if (switch1.IsToggled == false)
            {
                foreach (var i in images)
                {
                    i.Opacity = _invisible; //makes invisible
                }

                //switch1.IsEnabled = false;
            }
            else
            {
                foreach(var i in images)
                {
                    i.Opacity = _visible;
                }
            }


        }
        
        async void HandleImageSelection(Image selectedImage)
        {
            if (firstSelectedImage == null)
            {
                firstSelectedImage = selectedImage;

                firstSelectedImage.Opacity = _visible;
            }
            else if (secondSelectedImage == null)
            {
                secondSelectedImage = selectedImage;

                secondSelectedImage.Opacity = _visible;

                turnCount++;
                lblDisplay.Text = $"Turn: {turnCount}";
            }


            if (firstSelectedImage != null && secondSelectedImage != null)//check for match when two images picked
            {
                await CheckImageMatch();

            }

            CheckForGameEnd(); 
        }

        private async Task CheckImageMatch()
        {
            await Task.Delay(300); //wait .3 seconds before turning back to invisible

            if (firstSelectedImage.Source == secondSelectedImage.Source)//if match
            {
                firstSelectedImage.Opacity = _dull;
                secondSelectedImage.Opacity = _dull;
            }
            else //if no match
            {
                firstSelectedImage.Opacity = _invisible;
                secondSelectedImage.Opacity = _invisible;
            }

            //reset images, so two more can be picked
            firstSelectedImage = null;
            secondSelectedImage = null;
        }

        void SetImageGesture(Image image) //what to do when image is clicked
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                HandleImageSelection(image);
            };
            image.GestureRecognizers.Add(tapGesture);
        }

        void CheckForGameEnd()
        {
            if (images.All(img => img.Opacity == _dull))
            {
                ResetGame();
            }
        }

        void ResetGame()
        {
            DisplayAlert("Game Over", $"It Took {turnCount} turns", "Ok");

            turnCount = 0;
            switch1.IsEnabled = true;
            switch1.IsToggled = true;
            SetImages();
            lblDisplay.Text = "";
        }
    }
}


