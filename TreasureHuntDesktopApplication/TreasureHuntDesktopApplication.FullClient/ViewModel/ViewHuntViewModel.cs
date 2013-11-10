using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TreasureHuntDesktopApplication.FullClient.Project_Utilities;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class ViewHuntViewModel : ViewModelBase
    {
        #region Setup
        TreasureHuntServiceClient serviceClient = new TreasureHuntServiceClient();
        public RelayCommand SaveQuestionCommand { get; set; }
        public RelayCommand UpdateQuestionCommand { get; set; }

        public ViewHuntViewModel()
        {
            SaveQuestionCommand = new RelayCommand(() => ExecuteSaveQuestionCommand(), ()=> IsValidNewQuestion());
            UpdateQuestionCommand = new RelayCommand(()=> ExecuteUpdateQuestionCommand(), ()=> IsValidUpdateQuestion());
            this.RefreshTreasureHunts();
        }

        #endregion

        #region Validation

        public int NewQuestionMaxLength
        {
            get
            {
                return 30;

            }
        }

        public int UpdateQuestionMaxLength
        {
            get
            {
                return 30;

            }
        }

        public bool IsValidNewQuestion()
        {
            if (currentTreasureHunt != null && Validation.IsValidLength(newQuestion, NewQuestionMaxLength))
            {
                if (!Validation.IsNullOrWhiteSpace(newQuestion))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValidUpdateQuestion()
        {
            if (currentQuestion != null && Validation.IsValidLength(CurrentQuestion.Question1, UpdateQuestionMaxLength))
            {
                if (!Validation.IsNullOrWhiteSpace(CurrentQuestion.Question1) && !Validation.IsNullOrEmpty(CurrentQuestion.Question1))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Refreshing Data

        private void RefreshTreasureHunts()
        {
            this.serviceClient.GetTreasureHuntsAsync();
            this.TreasureHunts = this.serviceClient.GetTreasureHunts();
        }

        private void RefreshQuestions()
        {
            this.serviceClient.GetHuntQuestionsAsync(currentTreasureHunt);
            List<long> questionIds = this.serviceClient.GetHuntQuestions(currentTreasureHunt).ToList();
            List<question> listOfQuestionsFromHunt = new List<question>();

            using (var questionIdNumbers = questionIds.GetEnumerator())
            {
                while (questionIdNumbers.MoveNext())
                {
                    this.serviceClient.GetQuestionAsync(questionIdNumbers.Current);
                    question currentQuestionInList = this.serviceClient.GetQuestion(questionIdNumbers.Current);
                    listOfQuestionsFromHunt.Add(currentQuestionInList);
                }

                this.Questions = listOfQuestionsFromHunt.AsEnumerable();
            }
        }

        #endregion

        #region Variable getters and setters

        private IEnumerable<question> questions;
        public IEnumerable<question> Questions
        {
            get { return this.questions; }
            set {              
                this.questions = value;
                RaisePropertyChanged("Questions");
            }       
        }

        private IEnumerable<hunt> treasureHunts;
        public IEnumerable<hunt> TreasureHunts
        {
            get { return this.treasureHunts; }
            set {
                
                this.treasureHunts = value;
                RaisePropertyChanged("TreasureHunts");
                //RefreshTreasureHunts(); - to refresh again the list of treasure hunts...not working!

            }        
        }

        private hunt currentTreasureHunt;
        public hunt CurrentTreasureHunt
        {
            get { return this.currentTreasureHunt; }
            set {                
                this.currentTreasureHunt = value;
                RaisePropertyChanged("CurrentTreasureHunt");
                RefreshQuestions();
            }        
        }

        public string newQuestion;
        public string NewQuestion
        {
            get { return this.newQuestion; }
            set
            {
                this.newQuestion = value;
                RaisePropertyChanged("NewQuestion");
            }
        }

        private question currentQuestion;
        public question CurrentQuestion
        {
            get { return this.currentQuestion; }
            set
            {

                this.currentQuestion = value;
                RaisePropertyChanged("CurrentQuestion");
            }
        }

        #endregion

        #region Commands
        //Excutes when the save button has been pressed so new question is saved to DB
        private void ExecuteSaveQuestionCommand()
        {
            question brandNewQuestion = new question();
            brandNewQuestion.Question1 = this.newQuestion;
            brandNewQuestion.URL = "empty URL";
            long questionId = this.serviceClient.SaveQuestion(brandNewQuestion); //return the new question's ID
            SaveHuntQuestion(questionId);
            
            this.RefreshQuestions();
        }

        private void SaveHuntQuestion(long questionId)
        {
            huntquestion newHuntQuestion = new huntquestion();
            newHuntQuestion.QuestionId = questionId;
            newHuntQuestion.HuntId = currentTreasureHunt.HuntId;
            serviceClient.SaveNewHuntQuestion(newHuntQuestion);
        }

        private void ExecuteUpdateQuestionCommand()
        {
            this.serviceClient.UpdateQuestion(this.currentQuestion);
            this.RefreshQuestions();
        }

        #endregion

        #region ETC
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
