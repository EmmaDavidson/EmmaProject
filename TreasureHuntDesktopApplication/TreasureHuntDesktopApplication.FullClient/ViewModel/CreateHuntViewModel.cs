using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class CreateHuntViewModel : ViewModelBase
    {
        #region Setup
        TreasureHuntServiceClient serviceClient;
        public RelayCommand SaveHuntNameCommand { get; set; }

        public CreateHuntViewModel()
        {
            serviceClient = new TreasureHuntServiceClient();
            SaveHuntNameCommand = new RelayCommand(() => ExecuteSaveHuntNameCommand(), ()=> IsValidName());
        }
        #endregion

        #region Variable getters and setters

        private string huntName;
        public string HuntName
        {
            get { return this.huntName; }
            set
            {

                this.huntName = value;
                RaisePropertyChanged("HuntName");
            }
        }
        #endregion

        #region Validation
        private bool IsValidName()
        {
            return true;
        
        }
        #endregion

        #region Commands
        private void ExecuteSaveHuntNameCommand()
        {
            hunt newHunt = new hunt();
            newHunt.HuntName = this.huntName;
            this.serviceClient.SaveNewHunt(newHunt);
        }

        #endregion
    }
}
