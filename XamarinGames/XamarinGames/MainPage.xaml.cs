using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinGames
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            
            InitializeComponent();

            var label1 = new Label { Text = "xamarin forms",
                                        VerticalOptions = LayoutOptions.EndAndExpand,
                                        HorizontalOptions = LayoutOptions.Center };

            var button1 = new Button { Text = "Click me",
                                        VerticalOptions = LayoutOptions.StartAndExpand };

            button1.Clicked += ButtonClicked;

            var stack = new StackLayout();
            stack.Children.Add(label1);
            stack.Children.Add(button1);

            Content = stack;

        }

        void ButtonClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Clicked", "You clicked me", "OK");
        }

    }
}


//void ButtonClicked(object sender, System.EventArgs e)
//{
//    throw new NotImplementedException();
//}