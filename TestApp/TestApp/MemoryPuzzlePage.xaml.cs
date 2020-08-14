using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class MemoryPuzzlePage : ContentPage
    {
        //3b
        //4. ImageSource appleImage = ImageSource.FromResource("TestApp.Images.apple.png"); //dont do until step 4
        ImageSource appleImage = new Uri("http://www.clker.com/cliparts/2/f/e/0/15168423841349514170free-school-apple-clipart.med.png");
        ImageSource orangeImage = new Uri("http://icons.iconarchive.com/icons/bingxueling/fruit-vegetables/256/orange-icon.png");
        ImageSource bananaImage = new Uri("https://images.vexels.com/media/users/3/143061/isolated/lists/aaf71ed4e387a6838e1c521fbecde77a-banana-icon-fruit.png");
        ImageSource grapeImage = new Uri("https://s3.amazonaws.com/pix.iemoji.com/images/emoji/apple/ios-12/256/grapes.png");
        ImageSource lemonImage = new Uri("https://s3.amazonaws.com/pix.iemoji.com/images/emoji/apple/ios-12/256/lemon.png");
        ImageSource melonImage = new Uri("https://s3.amazonaws.com/pix.iemoji.com/images/emoji/apple/ios-12/256/melon.png");
        ImageSource avacadoImage = new Uri("https://s3.amazonaws.com/pix.iemoji.com/images/emoji/apple/ios-12/256/avocado.png");
        ImageSource berryImage = new Uri("https://vignette.wikia.nocookie.net/pokemongo/images/d/db/Razz_Berry.png/revision/latest?cb=20160726023117");

        ImageSource[] imageSources;

        //6a
        Image firstSelectedImage;
        Image secondSelectedImage;

        //9b
        int turnCount = 0;

        public MemoryPuzzlePage()
        {
            InitializeComponent();

            //3b
            imageSources = new ImageSource[]{ appleImage, orangeImage, bananaImage,
                                               grapeImage, lemonImage, melonImage,
                                               avacadoImage, berryImage };
            //3b end

            //3b alternative
            //imageSources = new ImageSource[]{new Uri("https://tinyurl.com/y28yysm7"),
            //                                new Uri("https://tinyurl.com/y3rjcs75"),
            //                                new Uri("https://tinyurl.com/y5pxc7rp"),
            //                                new Uri("https://tinyurl.com/y3ue3ofc"),
            //                                new Uri("https://tinyurl.com/y3zl2bdy"),
            //                                new Uri("https://tinyurl.com/yyuv59fa"),
            //                                new Uri("https://tinyurl.com/y3cad9pt"),
            //                                new Uri("https://tinyurl.com/y365ugav"),};

            //1.
            //img1.Source = ImageSource.FromUri(new Uri("http://www.clker.com/cliparts/2/f/e/0/15168423841349514170free-school-apple-clipart.med.png"));




            SetImages();

        }

        //2.
        void SetImages()
        {
            Image[] images = { img1, img2, img3, img4,
                                         img5, img6, img7, img8,
                                         img9, img10,img11,img12,
                                         img13,img14,img15,img16};

            Random rand = new Random();
            images = images.OrderBy(img => rand.Next()).ToArray();
            //2 end

            //3a.
            for (var i = 0; i < images.Length; i++)
            {
                //images[i].Source = ??
                //3a end.

                //3c
                images[i].Source = imageSources[i / 2]; //i/2 since imagesource array is only 8, while the other is 16
                //3c end

                //7c
                setImageGesture(images[i]);
                //end 7c

            }

            //7c first //then remove
            //foreach (var img in images)
            //    setImageGesture(img);

        }

        //5b
        void OnToggle(object sender, System.EventArgs e)
        {
            Image[] images = { img1, img2, img3, img4,
                                         img5, img6, img7, img8,
                                         img9, img10,img11,img12,
                                         img13,img14,img15,img16};

            if (switch1.IsToggled == false)
            {
                foreach (var i in images)
                {
                    //i.IsVisible = false; //5b
                    i.Opacity = 0.0100001; //8b, say you can just use .11 but you want to be sure
                }

                //9a
                switch1.IsEnabled = false;
                //end 9a
            }
            else
            {
                foreach (var i in images)
                {
                    //i.IsVisible = true; //5b
                    i.Opacity = 1; //8b
                }
            }

            //end 5b
        }

        //6b . turn to async in step 8c
        async void handleImageSelection(Image selectedImage)
        {
            //7a
            if (firstSelectedImage == null)
            {
                firstSelectedImage = selectedImage;

                //7xa.
                firstSelectedImage.Opacity = 1;
            }
            else if (secondSelectedImage == null)
            {
                secondSelectedImage = selectedImage;

                //7xa.
                secondSelectedImage.Opacity = 1;

                //9b last
                turnCount++;
                lblDisplay.Text = $"Turns: {turnCount}";
                //end 9b
            }
            //end 7a


            //8a.
            if (firstSelectedImage != null && secondSelectedImage != null)//means that two images have been selected
            {

                //8c.
                await Task.Delay(1000); //wait one second before turning dull
                //end 8c

                //8a cont
                if (firstSelectedImage.Source == secondSelectedImage.Source)
                {
                    //8a con
                    firstSelectedImage.Opacity = 0.5;
                    secondSelectedImage.Opacity = 0.5;
                }
                else
                {
                    //8a cont
                    firstSelectedImage.Opacity = 0.0100001;
                    secondSelectedImage.Opacity = 0.0100001;
                }
                //end 8a

                //8d
                firstSelectedImage = null;
                secondSelectedImage = null;
                //end 8d
            }

            //10
            checkForGameEnd();
            //
        }

        //6c
        void setImageGesture(Image image)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                //last part of 6c
                handleImageSelection(image);

            };
            image.GestureRecognizers.Add(tapGesture);
            //end 6c
        }

        //10 challenge
        void checkForGameEnd()
        {
            Image[] images = { img1, img2, img3, img4,
                                         img5, img6, img7, img8,
                                         img9, img10,img11,img12,
                                         img13,img14,img15,img16};

            if (images.All(img => img.Opacity == 0.5))
            {
                //reset the game
                ResetGame();
            }
        }

        void ResetGame()
        {
            DisplayAlert("Game Over", $"It Took {turnCount} to Finish", "Restart Game");
            firstSelectedImage = null;//dont need 
            secondSelectedImage = null;//dont need

            turnCount = 0;
            switch1.IsEnabled = true;
            switch1.IsToggled = true;
            SetImages(); //so we have a set of random images
            lblDisplay.Text = "";
        }   
    }

}
