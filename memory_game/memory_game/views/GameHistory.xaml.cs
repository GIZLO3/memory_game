using memory_game.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace memory_game.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameHistory : ContentPage
    {
        public GameHistory()
        {
            InitializeComponent();
        }
        private void NavToMainPage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        void Sort(object sender, CheckedChangedEventArgs e)
        {
            Results.scrollView = scrollView;
            RadioButton radioButton = sender as RadioButton;
            if(radioButton.Content == "Punkty")
            {
                Results.GetResultsByPoints();
            }
            else if(radioButton.Content == "Data")
            {
                Results.GetResultsByDate();
            }
        }
    }
}