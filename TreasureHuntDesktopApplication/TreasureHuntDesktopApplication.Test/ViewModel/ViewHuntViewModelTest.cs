using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;
using TreasureHuntDesktopApplication.FullClient.ViewModel;

namespace TreasureHuntDesktopApplication.Test
{
    class ViewHuntViewModelTest
    {
        #region Setup

        public ViewHuntViewModel viewModel;
        Mock<ITreasureHuntService> serviceClient;

        public const string newQuestion = "My new treasure hunt question";
        public const string updateQuestion = "My updated Question";
        hunt myFakeHunt;
        question myFakeQuestion;

        List<hunt> returnedHunts;
        List<long> returnedIds;

        long one = 111;

        [SetUp]
        public void Setup()
        {
            viewModel = new ViewHuntViewModel();
            serviceClient = new Mock<ITreasureHuntService>();
            NewQuestion = newQuestion;
            UpdateQuestion = updateQuestion;

            myFakeHunt = new hunt();
            myFakeHunt.HuntName = "My Fake Hunt";
            myFakeQuestion = new question();
            myFakeQuestion.Question1 = "This is my question";
            myFakeQuestion.URL = "empty url";

            returnedHunts.Add(myFakeHunt);
            returnedIds.Add(one);

            CurrentTreasureHunt = myFakeHunt;
            CurrentQuestion = myFakeQuestion;
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

        public String UpdateQuestion
        {
            get
            {
                return viewModel.CurrentQuestion.Question1;
            }
            set
            {
                viewModel.CurrentQuestion.Question1 = value;
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
        public void UpdateQuestionInvalidWhenNull()
        {
            String nullUpdateQuestion = null;
            UpdateQuestion = nullUpdateQuestion;
            Assert.False(viewModel.IsValidUpdateQuestion());
            Assert.False(viewModel.UpdateQuestionCommand.CanExecute(""));
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
        public void UpdateQuestionInvalidWhenWhitespace()
        {
            String WhitespaceUpdateQuestion = String.Empty;
            UpdateQuestion = WhitespaceUpdateQuestion;
            Assert.False(viewModel.IsValidNewQuestion());
            Assert.False(viewModel.SaveQuestionCommand.CanExecute(""));
        }

        [Test]
        public void NewQuestionInvalidWhenGreaterThanMaxLength()
        {
            String LongNewQuestion = "abcdefghijklmnopqrstuvwxyzabcdefg";
            NewQuestion = LongNewQuestion;
            Assert.False(viewModel.IsValidNewQuestion());
            Assert.False(viewModel.SaveQuestionCommand.CanExecute(""));
        }

        [Test]
        public void UpdateQuestionInvalidWhenGreaterThanMaxLength()
        {
            String LongUpdateQuestion = "abcdefghijklmnopqrstuvwxyzabcdefg";
            UpdateQuestion = LongUpdateQuestion;
            Assert.False(viewModel.IsValidUpdateQuestion());
            Assert.False(viewModel.UpdateQuestionCommand.CanExecute(""));
        }

        [Test]
        public void NewQuestionInvalidWhenCurrentTreasureHuntNull()
        {
             hunt nullHunt = null;
             CurrentTreasureHunt = nullHunt;
             Assert.False(viewModel.IsValidNewQuestion());
             Assert.False(viewModel.SaveQuestionCommand.CanExecute(""));
        }

        [Test]
        public void UpdateQuestionInvalidWhenCurrentQuestionNull()
        {
            question nullQuestion = null;
            CurrentQuestion = nullQuestion;
            Assert.False(viewModel.IsValidUpdateQuestion());
            Assert.False(viewModel.UpdateQuestionCommand.CanExecute(""));
        }

        #endregion

        #region Service Call Tests

        //Not called publically - so should they be ignored?
        [Ignore]
        [Test]
        public void ShouldRefreshTreasureHunts()
        {
           serviceClient.Setup<IEnumerable<hunt>>(s => s.GetTreasureHunts()).Returns(returnedHunts.AsEnumerable());
           
           //viewModel.SaveQuestionCommand.Execute(new object()); - not actually called anywhere
           serviceClient.Verify(s => s.GetTreasureHunts(), Times.Exactly(1));
        }

        //Only called privately through UpdateQuestionCommand and one other
        [Test]
        public void ShouldRefreshQuestions()
        {
           //serviceClient.Setup<List<int>>(s => s.GetHuntQuestions(It.IsAny<hunt>())).Returns(returnedIds.ToList());
           serviceClient.Setup<question>(s => s.GetQuestion(It.IsAny<long>())).Returns(myFakeQuestion);
           
           viewModel.UpdateQuestionCommand.Execute(new object()); 

           serviceClient.Verify(s => s.GetHuntQuestions(It.IsAny<hunt>()), Times.AtLeastOnce());
           serviceClient.Verify(s => s.GetQuestion(It.IsAny<long>()), Times.AtLeastOnce());
        }

        [Test]
        public void ShouldSaveNewQuestion()
        {
           serviceClient.Setup(s => s.SaveQuestion(It.IsAny<question>())).Verifiable();
           serviceClient.Setup(s => s.SaveNewHuntQuestion(It.IsAny<huntquestion>())).Verifiable();

           viewModel.SaveQuestionCommand.Execute(new object()); 

           serviceClient.Verify(s => s.SaveQuestion(It.IsAny<question>()), Times.Exactly(1));
           serviceClient.Verify(s => s.SaveNewHuntQuestion(It.IsAny<huntquestion>()), Times.Exactly(1));
        }

        [Test]
        public void ShouldUpdateQuestion()
        {
            serviceClient.Setup(s => s.UpdateQuestion(It.IsAny<question>())).Verifiable();
            viewModel.UpdateQuestionCommand.Execute(new object()); 
            serviceClient.Verify(s => s.UpdateQuestion(It.IsAny<question>()), Times.Exactly(1));      
        }

        #endregion
    }
}

