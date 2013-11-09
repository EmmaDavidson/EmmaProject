using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class ViewHuntViewModel : ViewModelBase
    {
        #region Setup
        TreasureHuntServiceClient serviceClient;
        public RelayCommand SaveQuestionCommand { get; set; }
        public RelayCommand UpdateQuestionCommand { get; set; }

        public ViewHuntViewModel()
        {
            serviceClient = new TreasureHuntServiceClient();

            SaveQuestionCommand = new RelayCommand(() => ExecuteSaveQuestionCommand(), ()=> ValidQuestion());
            UpdateQuestionCommand = new RelayCommand(()=> ExecuteUpdateQuestionCommand(), ()=> ValidQuestion());
            RefreshTreasureHunts();
        }

        #endregion

        #region Validation

        private bool ValidQuestion()
        {
            //here will do validation to check on the boxes to ensure they're filled
            return true;
        }
        #endregion

        #region Refreshing Data

        private void RefreshTreasureHunts()
        {
            //Issue here
            //this.serviceClient.GetTreasureHuntsAsync();
            //this.treasureHunts = this.serviceClient.GetTreasureHunts();
        }

        private void RefreshQuestions()
        {
            List<long> questionIds = this.serviceClient.GetHuntQuestions(currentTreasureHunt).ToList();

            using (var questionIdNumbers = questionIds.GetEnumerator())
            {
                List<question> listOfQuestionsFromHunt = new List<question>();

                while (questionIdNumbers.MoveNext())
                {
                    question currentQuestionInList = serviceClient.GetQuestion(questionIdNumbers.Current);
                    listOfQuestionsFromHunt.Add(currentQuestionInList);
                }

                questions = listOfQuestionsFromHunt.AsEnumerable();
            }
        }

        #endregion

        #region Variable getters and setters
        //Questions from the DB
        private IEnumerable<question> questions;
        public IEnumerable<question> Questions
        {

            get { return this.questions; }
            set {

                this.questions = value;
                RaisePropertyChanged("Questions");
            }
        
        }

        //Treasure hunts from the DB
        private IEnumerable<hunt> treasureHunts;
        public IEnumerable<hunt> TreasureHunts
        {

            get { return this.treasureHunts; }
            set {
                
                this.treasureHunts = value;
                RaisePropertyChanged("TreasureHunts");
            }
        
        }

        //The current treasure hunt that has been selected on the dropdown
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

        //The question that is being added to the DB from the text box
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

        //Current question that has been selected on the data grid
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
            //brandNewQuestion.URL has not been generated yet!
            long questionId = this.serviceClient.SaveQuestion(brandNewQuestion); //return the new question's ID

            huntquestion newHuntQuestion = new huntquestion();
            newHuntQuestion.QuestionId = questionId;
            newHuntQuestion.HuntId = currentTreasureHunt.HuntId; 
            serviceClient.SaveNewHuntQuestion(newHuntQuestion);
        }

        private void ExecuteUpdateQuestionCommand()
        {
            this.serviceClient.SaveQuestionAsync(this.CurrentQuestion);
            RefreshQuestions();
            //object state manager to update to say that this has been modifiedc
        }

        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
