using memory_game.views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using static System.Net.Mime.MediaTypeNames;

namespace memory_game.models
{
    public class Results : Questions
    {
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "results.txt");
        //private static string path = "/storage/emulated/0/Android/data/com.companyname.memory_game/results.txt";
        private static StreamWriter sw;
        private static StreamReader sr;
        private static Dictionary<string, int> scores = new Dictionary<string, int>();
        public static Label Score { get; set; }
        public static ScrollView scrollView { get; set; }

        private static void ReadFile()
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] score = line.Split(';');
                scores.Add(score[1], int.Parse(score[0]));
            }
        }

        public static void GetResultsByPoints()
        {
            StackLayout content = new StackLayout();
            if(File.Exists(path))
            {
                scores.Clear();

                sr = new StreamReader(path);

                ReadFile();

                var sorted_scores = from pair in scores orderby pair.Value descending select pair;
                foreach (KeyValuePair<string, int> pair in sorted_scores)
                {
                    Label score = new Label();
                    score.Text = "Punkty: " + pair.Value + "pkt | Data: " + pair.Key;
                    score.FontSize = 16;
                    score.TextColor = Color.White;
                    content.Children.Add(score);
                }
                sr.Close();
            }
            else
            {
                Label score = new Label();
                score.Text = "Brak gier";
                score.FontSize = 16;
                score.TextColor = Color.White;
                content.Children.Add(score);
            }
            scrollView.Content = content;
        }
        public static void GetResultsByDate()
        {
            StackLayout content = new StackLayout();
            if(File.Exists(path))
            {
                scores.Clear();

                sr = new StreamReader(path);

                ReadFile();

                var keys = scores.Keys.ToList();
                keys.Reverse();


                foreach (var key in keys)
                {
                    Label score = new Label();
                    score.Text = "Punkty: " + scores[key] + "pkt | Data: " + key;
                    score.FontSize = 16;
                    score.TextColor = Color.White;
                    content.Children.Add(score);
                }
                sr.Close();
            }
            else
            {
                Label score = new Label();
                score.Text = "Brak gier";
                score.FontSize = 16;
                score.TextColor = Color.White;
                content.Children.Add(score);
            }
            scrollView.Content = content;
        }

        public static void AddResult()
        {
            if (File.Exists(path))
                sw = new StreamWriter(path, true);
            else
                sw = new StreamWriter(path);
                   
            sw.WriteLine("{0};{1}", Points.Text, DateTime.Now.ToString("MM.dd.yyyy HH:mm"));
            sw.Close();
        }
    }
}
