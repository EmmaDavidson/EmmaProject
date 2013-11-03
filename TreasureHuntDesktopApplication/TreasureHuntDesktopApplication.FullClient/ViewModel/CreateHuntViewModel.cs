using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    class CreateHuntViewModel : ViewModelBase
    {
        //Create initiation of the service here

        public RelayCommand SaveQuestionCommand { get; set; }
        public RelayCommand UpdateQuestionCommand { get; set; }

        public CreateHuntViewModel()
        {
            //set up the service
            //set up the relay command
            SaveQuestionCommand = new RelayCommand(() => ExecuteSaveQuestionCommand, ValidQuestion());
            UpdateQuestionCommand = new RelayCommand(()=> ExecuteUpdateQuestionCommand, ValidQuestion());
            RefreshTreasureHunts();
        
        }

        private void RefreshTreasureHunts()
        {
            //Get the treasure hunts that have been saved
            //this.serviceClient.GetTreasureHuntsAsync();
            //this.questions = this.serviceClient.GetTreasureHunts();
        }

        private void RefreshQuestions()
        {
            //Get the questions 
            //this.serviceClient.GetQuestionsAsync(this.CurrentTreasureHunt.TreasureHuntId);
            //this.Questions = this.serviceClient.GetQuestions(this.CurrentTreasureHunt.TreasureHuntId);

        }

        private IEnumerable<Question> questions;
        public IEnumerable<Question> Questions
        {

            get { return this.questions; }
            set {
                
                this.questions = value;
                RaisePropertyChanged("Questions");
            }
        
        }

        private IEnumerable<TreasureHunt> treasureHunts;
        public IEnumerable<TreasureHunt> TreasureHunts;
        {

            get { return this.treasureHunts; }
            set {
                
                this.treasureHunts = value;
                RaisePropertyChanged("TreasureHunts");
            }
        
        }

        private TreasureHunt currentTreasureHunt;
        public TreasureHunt CurrentTreasureHunt;
        {

            get { return this.currentTreasureHunt; }
            set {
                
                this.currentTreasureHunt = value;
                RaisePropertyChanged("CurrentTreasureHunt");
                RefreshQuestions();

            }
        
        }


        private Question currentQuestion;
        public Question CurrentQuestion
        {

            get { return this.currentQuestion; }
            set
            {

                this.currentQuestion = value;
                RaisePropertyChanged("CurrentQuestion");
            }

        

        private void ExecuteSaveQuestionCommand()
        {
            //Create a new instance of the question and add the appropriate variables
            //call the service here to save the question to the database
            //this.serviceClient.saveQuestion(Question);
        }

        private void ExecuteUpdateQuestionCommand()
        {
            //this.serviceClient.SaveQuestionAsync(this.CurrentQuestion);

        }


    }
}
