using System;
using NUnit.Framework;
using TreasureHuntDesktopApplication.FullClient.ViewModel;
using Moq;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.Test
{
    [TestFixture]
    public class CreateHuntViewModelTest
    {
        #region Setup

        public CreateHuntViewModel viewModel;
        Mock<ITreasureHuntService> serviceClient;

        public const string huntName = "My New Treasure Hunt";

        [SetUp]
        public void Setup()
        {
            viewModel = new CreateHuntViewModel();
            serviceClient = new Mock<ITreasureHuntService>();
            HuntName = huntName;
        }

        public String HuntName
        {
            get
            {
                return viewModel.HuntName;
            }
            set
            {
                viewModel.HuntName = value;
            }
        }

        #endregion

        #region Validation Tests

        [Test]
        public void HuntNameInvalidWhenNull()
        {
            String nullHuntName = null;
            HuntName = nullHuntName;
            Assert.False(viewModel.IsValidHuntName());
            Assert.False(viewModel.SaveHuntNameCommand.CanExecute(""));
        }

        [Test]
        public void HuntNameInvalidWhenWhitespace()
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
        }

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