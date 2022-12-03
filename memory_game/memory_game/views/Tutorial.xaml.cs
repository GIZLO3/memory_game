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
    public partial class Tutorial : ContentPage
    {
        public Tutorial()
        {
            InitializeComponent();
        }

        private void NavToMainPage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}