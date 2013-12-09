using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using TreasureHuntDesktopApplication.FullClient.Messages;
using TreasureHuntDesktopApplication.FullClient.Project_Utilities;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{

    public class CreateHuntViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Setup
        ITreasureHuntService serviceClient;
        public RelayCommand SaveHuntNameCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }

        public CreateHuntViewModel(ITreasureHuntService _serviceClient)
        {
            serviceClient = _serviceClient;
            SaveHuntNameCommand = new RelayCommand(() => ExecuteSaveHuntNameCommand(), () => IsValidHuntName());
            BackCommand = new RelayCommand(() => ExecuteBackCommand());
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
                return 100;
            }
        }

        public bool IsValidHuntName()
        {
            if (!Validation.IsNullOrWhiteSpace(HuntName))
            {
                if (Validation.IsValidLength(HuntName, HuntNameMaxLength))
                {
                    if (Regex.IsMatch(HuntName, @"^[a-zA-Z ]+$"))
                    {
                        return true;
                    }
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

            //Grabs the correct hunt's ID and passes it into the view hunt view.
            //Ensures that the hunt has been saved to the database before it goes and grab's it
            hunt huntToView = serviceClient.GetHuntBasedOnName(newHunt.HuntName);

            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "ViewHuntViewModel" });
            Messenger.Default.Send<SelectedHuntMessage>(new SelectedHuntMessage() { CurrentHunt = huntToView });
            Messenger.Default.Send<ViewUpdatedMessage>(new ViewUpdatedMessage() { UpdatedView = true});

            HuntName = null;
        }

        private void ExecuteBackCommand()
        {
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "SearchHuntViewModel" });
            HuntName = null;
        }

        #endregion

        //-http://codeblitz.wordpress.com/2009/05/08/wpf-validation-made-easy-with-idataerrorinfo/
        public string Error
        {
            get
            {
                return String.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                String result = null;
                if (!IsValidHuntName())
                {
                    result = "Hunt name is invalid.";
                }

                return result;
            }
        }
    }
}
