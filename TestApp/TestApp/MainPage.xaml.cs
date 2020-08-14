using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //Label lblText;

        public MainPage()
        {
            InitializeComponent();

            //var button1 = new Button { Text = "Click Me",
            //                           VerticalOptions = LayoutOptions.Center,
            //                           HorizontalOptions = LayoutOptions.Center };
            //button1.Clicked += ButtonClicked;

            //lblText = new Label { Text = "Xamarin Forms",
            //                      VerticalOptions = LayoutOptions.Center,
            //                      HorizontalOptions = LayoutOptions.Center };

            //var stack = new StackLayout();
            //stack.Children.Add(button1);
            //stack.Children.Add(lblText);

            //Content = stack;
        }

        void ButtonClicked(object sender, System.EventArgs e)
        {
            //lblText.Text = "Button was clicked";
            DisplayAlert("Hello", "Hi", "OK");
        }
    }

   
}



//void ButtonClicked(object sender, System.EventArgs e)
//{
//    throw new NotImplementedException();
//}
