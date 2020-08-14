using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class SliderGamePage : ContentPage
    {
        BoxView boxView;

        public SliderGamePage()
        {
            InitializeComponent();

            //addBoxView();
        }

        async void SliderChanged(object sender, System.EventArgs e)
        {
            //addBoxView();

            var slider = sender as Slider;
            lblDisplay.Text = slider.Value.ToString("0") + $"-dotView6.anchorY: {dotView6.AnchorY} - {dotView.Y}";
            //boxView.HeightRequest = slider.Value;
            //boxView.WidthRequest = slider.Value;
            //boxView.CornerRadius = slider.Value / 2;
           

            //await dotView.TranslateTo(0, -dotView6.TranslationY, 1000);

            await Task.WhenAll(
                dotView.TranslateTo(0, 40, 1000),
                dotView6.TranslateTo(0, 55, 1000));
            //RunAnimation(boxView);
        }

        async void RunAnimation(object obj)
        {
            var dot = obj as BoxView;

            await dot.TranslateTo(0, 100, 10000, Easing.BounceIn);
            //await dot.ScaleTo(0, 300, Easing.SinIn);
            
        }

        void addBoxView()
        {

            //boxView = new BoxView
            //{
            //    Color = Color.Accent,
            //    WidthRequest = 50,
            //    HeightRequest = 50,
            //    CornerRadius = HeightRequest / 2,
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center,
               
            //};

            ////boxView.SetValue(X:nameof, );

            //this.stackLayout.Children.Add(boxView);
        }
    }
}
