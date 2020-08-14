using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class AnimationPage : ContentPage
    {
        public AnimationPage()
        {
            InitializeComponent();


        }

        //31.2
        async void btnDown(object sender, System.EventArgs e)
        {
            await boxView.TranslateTo(0, 100, 500);
            await boxView.TranslateTo(0, 0); ///brings back to orginal spot
        }

        //32.2
        async void btnLeft(object sender, System.EventArgs e)
        {
            //await boxView.TranslateTo(-100, 0, 500, Easing.BounceOut);
            //await boxView.TranslateTo(0, 0); ///brings back to orginal spot

            //34
            await boxView.TranslateTo(-100, 0, 2000, Easing.BounceOut);
            await boxView.TranslateTo(0, 0); ///brings back to orginal spot
        }

        //35.2
        async void btnUp(object sender, System.EventArgs e)
        {
            await boxView.TranslateTo(0, -50, 500, Easing.SpringIn);
            await boxView.TranslateTo(0, 0); ///brings back to orginal spot
        }

        async void btnBig(object sender, System.EventArgs e)
        {
            await boxView.ScaleTo(3, 1000, Easing.SpringOut);
            await boxView.ScaleTo(1, 500);//1 = orignal position, 500 is time 
        }

        async void btnSmall(object sender, System.EventArgs e)
        {
            await boxView.ScaleTo(0.5, 1000);
            await boxView.ScaleTo(1, 500);

        }

        async void btnRotate(object sender, System.EventArgs e)
        {
            await boxView.RotateTo(180);
            await boxView.RotateTo(0, 1000);
        }

        async void btnFade(object sender, System.EventArgs e)
        {
            await boxView.FadeTo(0.3, 1000);//opacity
            await boxView.FadeTo(1, 500);
        }

        async void btnAdd(object sender, System.EventArgs e)
        {
            stackL.Children.Add(new BoxView { HeightRequest = 20,
                                              WidthRequest = 20,
                                              CornerRadius = 10,
                                              HorizontalOptions = LayoutOptions.Start,
                                              Color =Color.Red });
        }
        //38
        async void btnAnimate(object sender, System.EventArgs e)
        {
            //39.1
            List<BoxView> boxes = new List<BoxView>();

            foreach (var circle in stackL.Children)
            {
                if (circle.GetType() == boxView.GetType())
                {
                    //await circle.TranslateTo(100, 100, 1000);

                    //39.2
                    boxes.Add(circle as BoxView);
                }
            }

            //39.3 challenge
            //await Task.WhenAll( //assuming we make 4 boxes
            //    boxes[0].TranslateTo(100, 100, 2000),
            //    boxes[1].TranslateTo(50, 500, 2000),
            //    boxes[2].FadeTo(0.5, 2000),
            //    boxes[3].ScaleTo(4, 2000)  
            //);

            //await Task.WhenAll( //assuming we make 4 boxes
            //    boxes[0].TranslateTo(0,0, 1000),
            //    boxes[1].TranslateTo(0,0, 1000),
            //    boxes[2].FadeTo(1, 2000),
            //    boxes[3].ScaleTo(1, 1000)
            //);

            //40
            if (boxes[3] != null)
            {
                await Task.WhenAll( //assuming we make 4 boxes
                    boxes[0].TranslateTo(100, 100, 2000),
                    boxes[1].TranslateTo(50, 500, 2000),
                    boxes[2].FadeTo(0.5, 2000),
                    boxes[3].ScaleTo(4, 2000)
                );

                await Task.WhenAll( //assuming we make 4 boxes
                    boxes[0].TranslateTo(0, 0, 1000),
                    boxes[1].TranslateTo(0, 0, 1000),
                    boxes[2].FadeTo(1, 2000),
                    boxes[3].ScaleTo(1, 1000)
                );
            }


        }

    }
}
