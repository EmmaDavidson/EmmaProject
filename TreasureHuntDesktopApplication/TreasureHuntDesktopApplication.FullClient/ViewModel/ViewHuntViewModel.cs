using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Novacode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
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
        public RelayCommand ViewQrCodeCommand { get; set; }
        public RelayCommand PrintQRCodesCommand { get; set; }
        private String myFileDirectory = "C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\";

        public ViewHuntViewModel()
        {
            SaveQuestionCommand = new RelayCommand(() => ExecuteSaveQuestionCommand(), ()=> IsValidNewQuestion());
            ViewQrCodeCommand = new RelayCommand(() => ExecuteViewQrCodeCommand(), () => IsSingleQuestionSelected());
            PrintQRCodesCommand = new RelayCommand(() => ExecutePrintQRCodesCommand(), () => IsValidListOfQuestions());
            
             Messenger.Default.Register<SelectedHuntMessage>
             (

             this,
             (action) => ReceiveSelectedHuntMessage(action.CurrentHunt)

             );

             RefreshQuestions();
        }

        #endregion

        #region Received Message Methods
        private void ReceiveSelectedHuntMessage(hunt currentHunt)
        {
            this.currentTreasureHunt = currentHunt;
            RefreshQuestions();
        }
        #endregion

        #region Validation

        public int NewQuestionMaxLength
        {
            get
            {
                return 100;

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

        public bool IsValidListOfQuestions()
        {
            if (this.questions != null)
            {
                return true;
            }
            return false;
        
        }

        public bool IsSingleQuestionSelected()
        {
            if (this.CurrentQuestion != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Refreshing Data

        //make internal
        public void RefreshQuestions()
        {
            if(this.currentTreasureHunt != null)
            {
                List<long> questionIds = this.serviceClient.GetHuntQuestions(this.currentTreasureHunt).ToList();
                List<question> listOfQuestionsFromHunt = new List<question>();

                using (var questionIdNumbers = questionIds.GetEnumerator())
                {
                    while (questionIdNumbers.MoveNext())
                    {
                        //this.serviceClient.GetQuestionAsync(questionIdNumbers.Current);
                        question currentQuestionInList = this.serviceClient.GetQuestion(questionIdNumbers.Current);
                        listOfQuestionsFromHunt.Add(currentQuestionInList);
                    }

                    this.Questions = listOfQuestionsFromHunt.AsEnumerable();
                }
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
       //make internal
        public void ExecuteSaveQuestionCommand()
        {
            String locationOfQrCodeImage = myFileDirectory + "QRCodes\\" + this.newQuestion + ".png";

            question brandNewQuestion = new question();
            brandNewQuestion.Question1 = this.newQuestion;
            brandNewQuestion.URL = locationOfQrCodeImage;
            long questionId = this.serviceClient.SaveQuestion(brandNewQuestion); //return the new question's ID

            SaveHuntQuestion(questionId);
            EncodeQRCode(locationOfQrCodeImage);
            
            this.NewQuestion = String.Empty;
        }

        //internal
        public void SaveHuntQuestion(long questionId)
        {
            huntquestion newHuntQuestion = new huntquestion();
            newHuntQuestion.QuestionId = questionId;
            newHuntQuestion.HuntId = currentTreasureHunt.HuntId;
            serviceClient.SaveNewHuntQuestion(newHuntQuestion);
        }

        //Make internal
        public void EncodeQRCode(String locationOfQrCodeImage)
        {
            //-http://www.youtube.com/watch?v=3CSifXK62Tk
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap generatedQrCodeImage = encoder.Encode(this.NewQuestion);
            generatedQrCodeImage.Save(locationOfQrCodeImage, ImageFormat.Jpeg);

            this.RefreshQuestions();
        }

        private void ExecuteViewQrCodeCommand()
        {
            Messenger.Default.Send<SelectedQuestionMessage>(new SelectedQuestionMessage() { SelectedQuestion = this.currentQuestion });
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "ViewQRCodeViewModel" });
        }

        //make internal
        public void ExecutePrintQRCodesCommand()
        {
            //-http://cathalscorner.blogspot.co.uk/2009/04/docx-version-1002-released.html
            String newDocumentFileLocation = myFileDirectory + "Documents\\" + this.currentTreasureHunt.HuntName + " QR Codes Sheet.docx";

            using (DocX documentOfQRCodes = DocX.Create(newDocumentFileLocation))
            {
                Novacode.Paragraph p = documentOfQRCodes.InsertParagraph(this.currentTreasureHunt.HuntName);
                
                using (var currentQuestionQRCode = this.questions.GetEnumerator())
                {
                    while (currentQuestionQRCode.MoveNext())
                    {
                        if (currentQuestionQRCode.Current.URL != null && currentQuestionQRCode.Current.URL != "empty URL")
                        {
                            Novacode.Paragraph q = documentOfQRCodes.InsertParagraph();
                            string locationOfImage = myFileDirectory + "QRCodes\\" + currentQuestionQRCode.Current.Question1 + ".png";
                            Novacode.Image img = documentOfQRCodes.AddImage(@locationOfImage);

                            Picture pic1 = img.CreatePicture();
                            q.InsertPicture(pic1, 0);
                            pic1.Width = 600; //smaller codes sizes
                            pic1.Height = 600;
                        }
                    }
                }
  
                documentOfQRCodes.Save(); 
            }

            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "PrintViewModel" });
            Messenger.Default.Send<PrintMessage>(new PrintMessage() { FileLocation = newDocumentFileLocation });

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
