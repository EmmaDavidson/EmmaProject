using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows;
using TreasureHuntDesktopApplication.FullClient.Messages;
using TreasureHuntDesktopApplication.FullClient.Project_Utilities;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class ViewHuntViewModel : ViewModelBase
    {
        #region Setup
        TreasureHuntServiceClient serviceClient = new TreasureHuntServiceClient();
        public RelayCommand SaveQuestionCommand { get; set; }
        //public RelayCommand UpdateQuestionCommand { get; set; }
        public RelayCommand ViewQrCodeCommand { get; set; }

        public ViewHuntViewModel()
        {
            SaveQuestionCommand = new RelayCommand(() => ExecuteSaveQuestionCommand(), ()=> IsValidNewQuestion());
            //UpdateQuestionCommand = new RelayCommand(()=> ExecuteUpdateQuestionCommand(), ()=> IsValidUpdateQuestion());
            ViewQrCodeCommand = new RelayCommand(() => ExecuteViewQrCodeCommand(), () => IsSingleQuestionSelected());
            this.RefreshQuestions();

             Messenger.Default.Register<SelectedHuntMessage>
             (

             this,
             (action) => ReceiveSelectedHuntMessage(action.CurrentHunt)

             );
        }

        #endregion

        #region Received Message Methods
        private void ReceiveSelectedHuntMessage(hunt currentHunt)
        {
            this.currentTreasureHunt = currentHunt;
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
            if (currentTreasureHunt != null)
            {
                if (!Validation.IsNullOrWhiteSpace(newQuestion))
                {
                    if (Validation.IsValidLength(newQuestion, NewQuestionMaxLength))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*public bool IsValidUpdateQuestion()
        {
            if (currentQuestion != null)
            {
                if (!Validation.IsNullOrWhiteSpace(CurrentQuestion.Question1))
                {
                    if (Validation.IsValidLength(CurrentQuestion.Question1, UpdateQuestionMaxLength))
                    {
                        return true;
                    }
                }
            }
            return false;
        }*/

        public bool IsSingleQuestionSelected()
        {
            if (currentQuestion != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Refreshing Data

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
            String locationOfQrCodeImage = "C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\" + this.newQuestion + ".jpg";

            question brandNewQuestion = new question();
            brandNewQuestion.Question1 = this.newQuestion;
            brandNewQuestion.URL = locationOfQrCodeImage;
            long questionId = this.serviceClient.SaveQuestion(brandNewQuestion); //return the new question's ID
            
            SaveHuntQuestion(questionId);

            //-http://www.youtube.com/watch?v=3CSifXK62Tk
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap generatedQrCodeImage = encoder.Encode(this.newQuestion);
            generatedQrCodeImage.Save(locationOfQrCodeImage, ImageFormat.Png);

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

        private void ExecuteViewQrCodeCommand()
        {
            Messenger.Default.Send<SelectedQuestionMessage>(new SelectedQuestionMessage() { SelectedQuestion = this.currentQuestion });
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "ViewQRCodeViewModel" });
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
