using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TreasureHuntDesktopApplication.FullClient.Messages;
using TreasureHuntDesktopApplication.FullClient.Project_Utilities;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{

    public class CreateHuntViewModel : ViewModelBase
    {
        #region Setup
        ITreasureHuntService serviceClient;
        public RelayCommand SaveHuntNameCommand { get; private set; }

        public CreateHuntViewModel(ITreasureHuntService _serviceClient)
        {
            serviceClient = _serviceClient;
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
                return 40;
            }
        }

        public bool IsValidHuntName()
        {
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
            this.serviceClient.SaveNewHunt(newHunt);

            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "SearchHuntViewModel" });
            Messenger.Default.Send<ViewUpdatedMessage>(new ViewUpdatedMessage() { UpdatedView = true});
        }

        #endregion
    }
}
