using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TreasureHuntDesktopApplication.Data;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    class CreateHuntViewModel : ViewModelBase
    {
        //Create initiation of the service here
        TreasureHuntServiceClient serviceClient;

        public RelayCommand SaveQuestionCommand { get; set; }
        public RelayCommand UpdateQuestionCommand { get; set; }
        List<string> listOfQuestions;

        #region Setup

        public CreateHuntViewModel()
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
            //Get the treasure hunts that have been saved to the database
            //this.serviceClient.GetTreasureHuntsAsync();
            //this.questions = this.serviceClient.GetTreasureHunts();
        }

        private void RefreshQuestions()
        {
            //Get the questions from the database
            //this.serviceClient.GetQuestionsAsync(this.CurrentTreasureHunt.TreasureHuntId);
            //this.Questions = this.serviceClient.GetQuestions(this.CurrentTreasureHunt.TreasureHuntId);

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

        //Excutes when the save button has been pressed so new question is saved to DB
        private void ExecuteSaveQuestionCommand()
        {
            //Save the question to the db
            question brandNewQuestion = new question();
            brandNewQuestion.Question1 = this.newQuestion;
            //brandNewQuestion.URL has not been generated yet!
            //int questionId = this.serviceClient.saveQuestion(brandNewQuestion); //return the new question's ID

            //Save to the hunt question table the new hunt question
            huntquestion newHuntQuestion = new huntquestion();
            //newHuntQuestion.QuestionId = questionId;
            newHuntQuestion.HuntId = currentTreasureHunt.HuntId; 
            //serviceClient.SaveNewHuntQuestion(newHuntQuestion);
        }

        //Will update the 
        private void ExecuteUpdateQuestionCommand()
        {
            //this.serviceClient.SaveQuestionAsync(this.CurrentQuestion);
            //refresh questions
        }


    
}
}
