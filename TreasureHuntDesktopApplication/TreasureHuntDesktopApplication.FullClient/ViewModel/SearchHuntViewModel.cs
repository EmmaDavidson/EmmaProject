using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureHuntDesktopApplication.FullClient.Messages;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class SearchHuntViewModel : ViewModelBase
    {
        #region Setup
        TreasureHuntServiceClient serviceClient = new TreasureHuntServiceClient();
        public RelayCommand SearchHuntCommand { get; set; }

        public SearchHuntViewModel()
        {
            SearchHuntCommand = new RelayCommand(() => ExecuteSearchHuntCommand(), () => IsValidHunt());
            RefreshTreasureHunts();

            Messenger.Default.Register<ViewUpdatedMessage>
             (

             this,
             (action) => ReceiveViewUpdatedMessage(action.UpdatedView)

             );
        }

        #endregion

        #region Received Message Methods
        private void ReceiveViewUpdatedMessage(bool updatedView)
        {
            if (updatedView)
            {
                RefreshTreasureHunts();
            }
        }
        #endregion

        #region Variables
        private IEnumerable<hunt> treasureHunts;
        public IEnumerable<hunt> TreasureHunts
        {
            get { return this.treasureHunts; }
            set
            {
                this.treasureHunts = value;
                RaisePropertyChanged("TreasureHunts");
            }
        }

        private hunt currentTreasureHunt;
        public hunt CurrentTreasureHunt
        {
            get { return this.currentTreasureHunt; }
            set
            {
                this.currentTreasureHunt = value;
                RaisePropertyChanged("CurrentTreasureHunt");
            }
        }
        #endregion

        #region Refreshing Data

        //make internal
        public void RefreshTreasureHunts()
        {
            this.TreasureHunts = this.serviceClient.GetTreasureHunts();
        }
        #endregion

        #region Validation

        //Make internal
        public bool IsValidHunt()
        {
            if (currentTreasureHunt != null)
            {
                return true;
            }
            return false;
        
        }
        #endregion

        #region Commands
        private void ExecuteSearchHuntCommand()
        {
            //Takes the user to the selected hunt page.
            Messenger.Default.Send<SelectedHuntMessage>(new SelectedHuntMessage() { CurrentHunt = this.currentTreasureHunt });
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "ViewHuntViewModel" });
        }

        #endregion
    }
}
