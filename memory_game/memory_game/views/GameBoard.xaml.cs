using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using memory_game.models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace memory_game.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameBoard : ContentPage
    {
        public GameBoard()
        {
            InitializeComponent();
            Questions.ProgressBar = Progres;
            Questions.Board = plansza;
            Questions.Points = punkty;
            Questions questions = new Questions();
            InGameTime(60);
        }
        //event 

        public void InGameTime(int timeCount)
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (Questions.PairCorrect)
                {
                    timeCount += 5;
                    Questions.PairCorrect = false;
                }
                timeCount--;
                czas.Text = timeCount.ToString();
                if (timeCount > 0)
                {
                    return true;
                }
                Navigation.PushAsync(new GameOver());
                Results.AddResult();
                return false;
            });
        }
    }
}