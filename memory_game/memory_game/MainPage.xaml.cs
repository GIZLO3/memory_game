using memory_game.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace memory_game
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavToTutorial(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Tutorial());
        }

        private void NavToGameBoard(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GameBoard());
        }

        private void NavToRanking(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GameHistory());
        }
    }
}