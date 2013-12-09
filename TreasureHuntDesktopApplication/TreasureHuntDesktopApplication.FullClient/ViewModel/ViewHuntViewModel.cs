using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Novacode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using TreasureHuntDesktopApplication.FullClient.Messages;
using TreasureHuntDesktopApplication.FullClient.Project_Utilities;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class ViewHuntViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Setup
        ITreasureHuntService serviceClient;
        public RelayCommand SaveQuestionCommand { get; private set; }
        public RelayCommand ViewQrCodeCommand { get; private set; }
        public RelayCommand PrintQRCodesCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        private String myFileDirectory = "C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\";

        public ViewHuntViewModel(ITreasureHuntService _serviceClient)
        {
            serviceClient = _serviceClient;
            SaveQuestionCommand = new RelayCommand(() => ExecuteSaveQuestionCommand(), ()=> IsValidNewQuestion());
            ViewQrCodeCommand = new RelayCommand(() => ExecuteViewQrCodeCommand(), () => IsSingleQuestionSelected());
            PrintQRCodesCommand = new RelayCommand(() => ExecutePrintQRCodesCommand(), () => IsValidListOfQuestions());
            BackCommand = new RelayCommand(() => ExecuteBackCommand());
            
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
                return 150;

            }
        }

        public bool IsValidNewQuestion()
        {
            if (currentTreasureHunt != null)
            {
                if (!Validation.IsNullOrWhiteSpace(this.newQuestion))
                {
                    if (Validation.IsValidLength(newQuestion, NewQuestionMaxLength))
                    {
                        //-http://blog.magnusmontin.net/2013/08/26/data-validation-in-wpf/
                        if (Regex.IsMatch(this.newQuestion, @"^[a-zA-Z -]+$"))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsValidListOfQuestions()
        {
            if(Questions.Count() != 0)
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
        private void RefreshQuestions()
        {
            if(this.currentTreasureHunt != null)
            {
                List<long> questionIds = this.serviceClient.GetHuntQuestions(this.currentTreasureHunt).ToList();
                List<question> listOfQuestionsFromHunt = new List<question>();

                using (var questionIdNumbers = questionIds.GetEnumerator())
                {
                    while (questionIdNumbers.MoveNext())
                    {
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

        private void ExecuteSaveQuestionCommand()
        {
            String locationOfQrCodeImage = myFileDirectory + "QRCodes\\" + this.newQuestion + ".png";

            question brandNewQuestion = new question();
            brandNewQuestion.Question1 = this.newQuestion;
            brandNewQuestion.URL = locationOfQrCodeImage;
            long questionId = this.serviceClient.SaveQuestion(brandNewQuestion); 

            SaveHuntQuestion(questionId);
            EncodeQRCode(locationOfQrCodeImage);
            
            this.NewQuestion = String.Empty;
        }

        private void SaveHuntQuestion(long questionId)
        {
            huntquestion newHuntQuestion = new huntquestion();
            newHuntQuestion.QuestionId = questionId;
            newHuntQuestion.HuntId = currentTreasureHunt.HuntId;
            serviceClient.SaveNewHuntQuestion(newHuntQuestion);
        }

        private void EncodeQRCode(String locationOfQrCodeImage)
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
                documentOfQRCodes.InsertParagraph();
                using (var currentQuestionQRCode = this.questions.GetEnumerator())
                {
                    while (currentQuestionQRCode.MoveNext())
                    {
                        if (currentQuestionQRCode.Current.URL != null && currentQuestionQRCode.Current.URL != "empty URL")
                        {
                            documentOfQRCodes.InsertParagraph(currentQuestionQRCode.Current.Question1);
                            Novacode.Paragraph q = documentOfQRCodes.InsertParagraph();
                            
                            string locationOfImage = myFileDirectory + "QRCodes\\" + currentQuestionQRCode.Current.Question1 + ".png";
                            Novacode.Image img = documentOfQRCodes.AddImage(@locationOfImage);

                            Picture pic1 = img.CreatePicture();
                            q.InsertPicture(pic1, 0);
                            pic1.Width = 200; //smaller codes sizes
                            pic1.Height = 200;

                            documentOfQRCodes.InsertParagraph();

                        }
                    }
                }
  
                documentOfQRCodes.Save(); 
            }

            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "PrintViewModel" });
            Messenger.Default.Send<PrintMessage>(new PrintMessage() { FileLocation = newDocumentFileLocation });
            Messenger.Default.Send<SelectedHuntMessage>(new SelectedHuntMessage() { CurrentHunt = this.currentTreasureHunt });

         }

        private void ExecuteBackCommand()
        {
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "SearchHuntViewModel" });
            NewQuestion = null;
        }
        
        #endregion

        #region ETC
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get 
            {
                String result = null;
                if (!IsValidNewQuestion())
                {
                    result = "Question is invalid.";
                }

                return result;
                
            }
        }
    }
}
