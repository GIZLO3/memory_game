using memory_game.views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace memory_game.models
{
    public class Questions
    {
        private List<string> _inPolish = new List<string>() { "jablko", "drzwi", "banan", "okno", "śrubokręt", "długopis", "samochód", "gruszka", "owoc", "ksiazka", "telefon", "komputer", "klawiatura", "talerz" };
        private List<string> _inEnglish = new List<string>() { "apple", "door", "banana", "window", "screwdriver", "pen", "car", "pear", "fruit", "book", "phone", "computer", "keyboard", "plate" };
        private List<Button> _buttons = new List<Button>();
        private Random rand = new Random();

        private bool IsClicked = false;
        private Button ClickedButton = null;

        public static bool PairCorrect { get; set; }
        public static ProgressBar ProgressBar { get; set; }
        public static Grid Board { get; set; }
        public static Label Points { get; set; }

        public Questions()
        {
            AddButtons();
        }

        private void AddButtons()
        {
            _buttons.Clear();
            IsClicked = false;
            ClickedButton = null;

            for (int i = 0; i < 6; i++)
            {
                Board.Children.Add(CreateButton(0, i));
                Board.Children.Add(CreateButton(1, i));
            }
        }
        private Button CreateButton(int language, int i)
        {
            Button newBtn = new Button();
            if (language == 1)
            {
                //newBtn.Text = _inEnglish[i];
                newBtn.ClassId = "en";
            }
            else
            {
                //newBtn.Text = _inPolish[i];
                newBtn.ClassId = "pl";
            }

            newBtn.ImageSource = "card_back.png";
            newBtn.BorderWidth = 2;
            newBtn.BorderColor = Color.Black;
            newBtn.CornerRadius = 10;
            newBtn.TextColor = Color.Transparent;
            newBtn.BackgroundColor = Color.LightBlue;
            newBtn.RotationY = 180;
            newBtn.StyleId = i.ToString();

            int x = 0, y = 0;
            bool occupied = false;

            do
            {
                occupied = false;
                x = rand.Next(4);
                y = rand.Next(3);
                foreach (Button btn in _buttons)
                {
                    if (Grid.GetColumn(btn) == x && Grid.GetRow(btn) == y)
                        occupied = true;
                }
            } while (occupied);

            _buttons.Add(newBtn);
            Grid.SetColumn(newBtn, x);
            Grid.SetRow(newBtn, y);

            newBtn.Clicked += ButtonClicked;

            return newBtn;
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Clicked -= ButtonClicked;
            if (!IsClicked)
            {
                IsClicked = true;
                ClickedButton = btn;
                Reverse(btn);
            }
            else
            {
                Board.IsEnabled = false;
                IsClicked = false;
                Reverse(btn);
                Device.StartTimer(TimeSpan.FromMilliseconds(800), () =>
                {
                    if (btn.StyleId == ClickedButton.StyleId)
                    {
                        ProgressBar.Progress += 0.1;
                        Points.Text = (int.Parse(Points.Text) + 100).ToString();
                        Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                try
                                {
                                    btn.RotationY += 5;
                                    ClickedButton.RotationY += 5;
                                }
                                catch { }
                            });
                            if (btn.RotationY == 85)
                            {
                                Board.Children.Remove(btn);
                                Board.Children.Remove(ClickedButton);
                                PairCorrect = true;
                                _buttons.Remove(btn);
                                _buttons.Remove(ClickedButton);
                                Board.IsEnabled = true;
                                if (Board.Children.Count == 0)
                                {
                                    AddButtons();
                                    Points.Text = (int.Parse(Points.Text) + 300).ToString();
                                    ProgressBar.Progress = 0;
                                }
                                return false;
                            }
                            return true;
                        });
                    }
                    else
                    {
                        ReverseBack(ClickedButton);
                        ReverseBack(btn);
                    }
                    return false;
                });
            }
        }

        private void Reverse(Button btn)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        btn.RotationY -= 5;
                        if (btn.RotationY == 95)
                        {
                            btn.TextColor = Color.Black;
                            if (btn.ClassId == "en")
                            {
                                btn.Text = _inEnglish[int.Parse(btn.StyleId)];
                            }
                            else if (btn.ClassId == "pl")
                            {
                                btn.Text = _inPolish[int.Parse(btn.StyleId)];
                            }
                            btn.ImageSource = "";
                        }

                    }
                    catch { }
                });
                if (btn.RotationY == 5)
                {
                    return false;
                }
                return true;
            });
        }

        private void ReverseBack(Button btn)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        btn.RotationY += 5;
                        if (btn.RotationY == 85)
                        {
                            btn.Text = "";
                            btn.ImageSource = "card_back.png";
                        }

                    }
                    catch { }
                });
                if (btn.RotationY == 175)
                {
                    btn.Clicked += ButtonClicked;
                    Board.IsEnabled = true;
                    return false;
                }
                return true;
            });
        }
    }
}