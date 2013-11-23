using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;
using TreasureHuntDesktopApplication.FullClient.ViewModel;

namespace TreasureHuntDesktopApplication.Test
{
    [TestFixture]
    class ViewHuntViewModelTest
    {
        #region Setup

        public ViewHuntViewModel viewModel;
        Mock<ITreasureHuntService> serviceClient;
        
        public const string newQuestion = "My new treasure hunt question";

        hunt myFakeHunt;
        question myFakeQuestion;

        List<hunt> returnedHunts = new List<hunt>();
        List<long> returnedIds = new List<long>();
        List<question> huntQuestions = new List<question>();

        long one = 111;

        [SetUp]
        public void Setup()
        {
            viewModel = new ViewHuntViewModel();

            serviceClient = new Mock<ITreasureHuntService>();
            
            NewQuestion = newQuestion;

            myFakeHunt = new hunt();
            myFakeHunt.HuntName = "My Fake Hunt";
            myFakeQuestion = new question();
            myFakeQuestion.Question1 = "This is my question";
            myFakeQuestion.URL = "empty url";

            returnedHunts.Add(myFakeHunt);
            returnedIds.Add(one);

            CurrentTreasureHunt = myFakeHunt;
            CurrentQuestion = myFakeQuestion;

            huntQuestions.Add(myFakeQuestion);

            Questions = huntQuestions.AsEnumerable();



        }

        public String NewQuestion
        {
            get
            {
                return viewModel.NewQuestion;
            }
            set
            {
                viewModel.NewQuestion = value;
            }
        }

        public hunt CurrentTreasureHunt
        {
            get
            {
                return viewModel.CurrentTreasureHunt;
            }
            set
            {
                viewModel.CurrentTreasureHunt = value;
            }
        }

        public question CurrentQuestion
        {
            get
            {
                return viewModel.CurrentQuestion;
            }
            set
            {
                viewModel.CurrentQuestion = value;
            }
        }

        public IEnumerable<question> Questions
        {
            get { return viewModel.Questions; }
            set
            {
                viewModel.Questions = value;

            }
        }


        #endregion

        #region Validation Tests
        [Test]
        public void NewQuestionInvalidWhenNull()
        {
            String nullNewQuestion = null;
            NewQuestion = nullNewQuestion;
            Assert.False(viewModel.IsValidNewQuestion());
            Assert.False(viewModel.SaveQuestionCommand.CanExecute(""));
        }

      [Test]
        public void NewQuestionInvalidWhenWhitespace()
        {
            String WhitespaceNewQuestion = String.Empty;
            NewQuestion = WhitespaceNewQuestion;
            Assert.False(viewModel.IsValidNewQuestion());
            Assert.False(viewModel.SaveQuestionCommand.CanExecute(""));
        }

        [Test]
        public void NewQuestionValidWhenLessThanMaxLength()
        {
            String LongNewQuestion = "abcdefghijklmnopqrstuvwxyzabcdefg";
            NewQuestion = LongNewQuestion;
            Assert.True(viewModel.IsValidNewQuestion());
            Assert.True(viewModel.SaveQuestionCommand.CanExecute(""));
        }
        #endregion

        #region Service Call Tests

        [Test]
        public void ShouldRefreshQuestions()
        {
           serviceClient.Setup<long[]>(s => s.GetHuntQuestions(It.IsAny<hunt>())).Returns(returnedIds.ToArray);
           serviceClient.Setup<question>(s => s.GetQuestion(It.IsAny<long>())).Returns(myFakeQuestion);
           viewModel.RefreshQuestions();
           serviceClient.Verify(s => s.GetHuntQuestions(It.IsAny<hunt>()), Times.AtLeastOnce());
           serviceClient.Verify(s => s.GetQuestion(It.IsAny<long>()), Times.AtLeastOnce());

           Assert.AreEqual(returnedIds, this.Questions, "They are not the same list of questions."); 
        }

        [Test] //PROBLEM TEST HERE
        public void ShouldSaveNewQuestion()
        {
           serviceClient.Setup<long>(s => s.SaveQuestion(It.IsAny<question>())).Returns(2); //doesnt return 2
           serviceClient.Setup(s => s.SaveNewHuntQuestion(It.IsAny<huntquestion>())).Verifiable();

           viewModel.SaveQuestionCommand.Execute(new object());

          // huntquestion newHuntQuestion = new huntquestion();
          // newHuntQuestion.HuntId = this.CurrentTreasureHunt.HuntId;
          // newHuntQuestion.QuestionId = 2;

           serviceClient.Verify(s => s.SaveQuestion(It.IsAny<question>()), Times.Exactly(1));
           serviceClient.Verify(s => s.SaveNewHuntQuestion(It.IsAny<huntquestion>()), Times.Exactly(1));
           
        }

        [Test]
        public void ShouldCreateTreasureHuntDocument()
        {   
            viewModel.ExecutePrintQRCodesCommand();
            Assert.IsTrue(File.Exists("C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\Documents\\My Fake Hunt QR Codes Sheet.docx"), "The file does not exist");
        }

        [Test]
        public void ShouldCreateQRCodeImage()
        {   //The questions are empty
            viewModel.EncodeQRCode("C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\QRCodes\\My new treasure hunt question.png");
            Assert.IsTrue(File.Exists("C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\QRCodes\\My new treasure hunt question.png"), "The file does not exist");
        }
        #endregion
    }
}

