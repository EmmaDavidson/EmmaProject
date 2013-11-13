using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TreasureHuntDesktopApplication.FullClient.Project_Utilities;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class CreateHuntViewModel : ViewModelBase
    {
        #region Setup
        TreasureHuntServiceClient serviceClient;
        public RelayCommand SaveHuntNameCommand { get; private set; }

        public CreateHuntViewModel()
        {
            serviceClient = new TreasureHuntServiceClient();
            SaveHuntNameCommand = new RelayCommand(() => ExecuteSaveHuntNameCommand(), () => IsValidHuntName());
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

        public int HuntNameMaxLength
        {
            get 
            {
                return 20;
            }
        }

        public bool IsValidHuntName()
        {
                //also checks that the new question field is not empty 
            if (!Validation.IsNullOrWhiteSpace(HuntName))
            {
                if (Validation.IsValidLength(HuntName, HuntNameMaxLength))
                {

                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Commands
        private void ExecuteSaveHuntNameCommand()
        {
            hunt newHunt = new hunt();
            newHunt.HuntName = this.huntName;
            this.serviceClient.SaveNewHuntAsync(newHunt);
            this.serviceClient.SaveNewHunt(newHunt);
        }

        #endregion
    }
}
