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

        [SetUp]
        public void Setup()
        {
            viewModel = new ViewHuntViewModel();
            serviceClient = new Mock<ITreasureHuntService>();
            NewQuestion = newQuestion;
            UpdateQuestion = updateQuestion;
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

      /*  [Test]
        public void NewQuestionInvalidWhenWhitespace()
        {
            String WhitespaceHuntName = String.Empty;
            HuntName = WhitespaceHuntName;
            Assert.False(viewModel.IsValidHuntName());
            Assert.False(viewModel.SaveHuntNameCommand.CanExecute(""));
        }

        [Test]
        public void HuntNameInvalidWhenGreaterThanMaxLength()
        {
            String LongHuntName = "abcdefghijklmnopqrstuvwxyzabcdefg";
            HuntName = LongHuntName;
            Assert.False(viewModel.IsValidHuntName());
            Assert.False(viewModel.SaveHuntNameCommand.CanExecute(""));
        }*/

        #endregion

        #region Service Call Tests

        [Test]
        public void ShouldSaveNewTreasureHunt()
        {
            serviceClient.Setup(s => s.SaveNewHunt(It.IsAny<hunt>())).Verifiable();
            viewModel.SaveHuntNameCommand.Execute(new object()); 
            serviceClient.Verify(s => s.SaveNewHunt(It.IsAny<hunt>()), Times.Exactly(1));
        }

        #endregion
    }
    }
}
