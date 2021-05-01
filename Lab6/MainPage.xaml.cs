using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab6
{
    public partial class MainPage : ContentPage
    {
        int CurrentQuestion;
        string[] QuestionList = {
                    "Bananas are part of your diet",
                    "Fruits are part of your diet",
                    "Legumes are part of your diet",
                    "Tomatoes are part of your diet",
                    "Vegetables are part of your diet" };
        string[] ImageList = { "banana.jpg", "fruits.jpg", "legumes.jpg", "tomato.png", "vegetables.jpg" };
        string[] OptionAList = { "True", "True", "True", "True", "True" };
        string[] OptionBList = { "False", "False", "False", "False", "False" };
        List<string> ResponseList = new List<string> { };

        public MainPage()
        {
            InitializeComponent();

            InitSurvey();
        }

        private void OnButtonClicked_OptionA(object sender, EventArgs e)
        {
            //await DisplayAlert("Alert", "Option A", "OK");
            SaveAnswerAsync("A");
        }
        private void OnButtonClicked_OptionB(object sender, EventArgs e)
        {
            //await DisplayAlert("Alert", "Option B", "OK");
            SaveAnswerAsync("B");
        }
        private void OnButtonClicked_Restart(object sender, EventArgs e)
        {
            InitSurvey();
        }

        void OnSwiped(object sender, SwipedEventArgs e)
        {
            string userResponse = $"You swiped: {e.Direction.ToString()}";
            if (SwipeDirection.Right == e.Direction)  // Right swipe
            {
                SaveAnswerAsync("A");
            } else if (SwipeDirection.Left == e.Direction)  //  Left swipe
            {
                SaveAnswerAsync("B");
            }  // else ignore
        }
        private void InitSurvey()
        {
            CurrentQuestion = 0;
            SetNewQuestion();
            // SurveyOptionA.IsVisible = true;
            // SurveyOptionB.IsVisible = true;
            // SurveyOptionA.IsEnabled = true;
            // SurveyOptionB.IsEnabled = true;
            SurveyRestart.IsVisible = false;
            SurveyRestart.IsEnabled = false;
            SurveyResult.IsVisible = false;
            SurveyQuestion.IsVisible = true;
            SurveyImage.IsVisible = true;
            ResponseList = new List<string> { };
        }

        private void SaveAnswerAsync(string answer)
        {
            ResponseList.Add(answer);
            CurrentQuestion = ResponseList.Count;
            if (CurrentQuestion >= QuestionList.Length)
            {
                string yourResult = "";
                int A_count = 0;
                int B_count = 0;
                for (int questioncount = 0; questioncount < QuestionList.Length; questioncount++)
                {
                    yourResult += $"Q{(questioncount + 1)}: {QuestionList[questioncount]} \n A{(questioncount + 1)}: ";
                    switch (ResponseList[questioncount])
                    {
                        case "A":
                            yourResult += $"{OptionAList[questioncount]}\n";
                            A_count++;
                            break;
                        default:
                            yourResult += $"{OptionBList[questioncount]}\n";
                            B_count++;
                            break;
                    }
                }
                SurveyImage.IsVisible = false;
                SurveyOptionA.IsVisible = false;
                SurveyOptionB.IsVisible = false;
                SurveyOptionA.IsEnabled = false;
                SurveyOptionB.IsEnabled = false;
                SurveyResult.IsVisible = true;
                SurveyQuestion.IsVisible = false;
                // Build personality result.
                if (A_count == QuestionList.Length)
                {
                    yourResult += $"\n You enjoy a lot of fruits and vegetables, and are probably eating healthy overall\n";
                }
                else if (B_count == QuestionList.Length)
                {
                    yourResult += $"\n You diet doesn't appear healthy\n";
                }
                else if (A_count >= B_count)
                {
                    yourResult += $"\n You like most fruits and vegetables\n";
                }
                else
                {
                    yourResult += $"\n You only like a few fruits and vegetables\n";
                }

                SurveyResult.Text = yourResult;

                SurveyRestart.IsVisible = true;
                SurveyRestart.IsEnabled = true;

            }
            else
            {
                SetNewQuestion();
            }
        }
        private void SetNewQuestion()
        {
            SurveyQuestion.Text = QuestionList[CurrentQuestion]; 
            SurveyImage.Source = ImageList[CurrentQuestion];
        }

    }
}
