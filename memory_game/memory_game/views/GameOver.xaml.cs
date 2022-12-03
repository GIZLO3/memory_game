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
    public partial class GameOver : ContentPage
    {
        public GameOver()
        {
            InitializeComponent();
            SetUpPage();
        }

        private void SetUpPage()
        {
            Score.Text = $"{"Twój wynik: " + Questions.Points.Text}";
        }

        private void BackToMenu(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}