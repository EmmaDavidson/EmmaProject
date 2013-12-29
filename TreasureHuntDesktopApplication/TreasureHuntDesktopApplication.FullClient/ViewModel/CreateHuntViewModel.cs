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

            Messenger.Default.Register<CurrentUserMessage>
            (

            this,
            (action) => ReceiveCurrentUserMessage(action.CurrentUser)

            );
        }
        #endregion

        #region Receiving Messages

        private void ReceiveCurrentUserMessage(user currentUser)
        {
            CurrentUser = currentUser;
        }
        #endregion

        #region Variable getters and setters

        private user currentUser;
        public user CurrentUser
        {
            get { return this.currentUser; }
            set
            {

                this.currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

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
        public int HuntNameMinLength
        {
            get 
            {
                return 5;
            }
        }

        //-http://www.youtube.com/watch?v=OOHDie8BdGI
        private bool IsValidHuntName()
        {
            foreach(string property in ValidatedProperties)
                if(GetValidationMessage(property) != null)
                return false;
      
            return true;
        }
        #endregion

        #region Commands
        private void ExecuteSaveHuntNameCommand()
        {
            if (!DoesHuntAlreadyExist(HuntName))
            {
                hunt newHunt = new hunt();
                newHunt.HuntName = this.huntName;
                long huntId = this.serviceClient.SaveNewHunt(newHunt);

                userrole newUserRole = this.serviceClient.GetUserRole(this.currentUser);

                userhunt newUserHunt = new userhunt();
                newUserHunt.HuntId = huntId;
                newUserHunt.UserId = this.currentUser.UserId;
                newUserHunt.UserRoleId = newUserRole.UserRoleId;

                this.serviceClient.SaveUserHunt(newUserHunt);
                
                //Grabs the correct hunt's ID and passes it into the view hunt view.
                //Ensures that the hunt has been saved to the database before it goes and grab's it
                hunt huntToView = serviceClient.GetHuntBasedOnName(newHunt.HuntName);

                Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "ViewHuntViewModel" });
                Messenger.Default.Send<SelectedHuntMessage>(new SelectedHuntMessage() { CurrentHunt = huntToView });
                Messenger.Default.Send<ViewUpdatedMessage>(new ViewUpdatedMessage() { UpdatedView = true });

                HuntName = null;
            }
            else 
            {
                String messageBoxText = "This hunt already exists.";
                String caption = "Hunt Already Exists";
                MessageBoxResult box = MessageBox.Show(messageBoxText, caption);     
            }
        }

        private bool DoesHuntAlreadyExist(string newQuestion)
        {
            //GetHuntQuestions
            List<hunt> listOfHunts = serviceClient.GetTreasureHunts().ToList();

            using (var currentHunts = listOfHunts.GetEnumerator())
            {
                while (currentHunts.MoveNext())
                {
                    //-http://stackoverflow.com/questions/6371150/comparing-two-strings-ignoring-case-in-c-sharp
                    if(String.Equals(currentHunts.Current.HuntName, this.huntName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ExecuteBackCommand()
        {
            Messenger.Default.Send<UpdateViewMessage>(new UpdateViewMessage() { UpdateViewTo = "SearchHuntViewModel" });
            HuntName = null;
        }

        #endregion

        #region IDataErrorInfo
        //-http://codeblitz.wordpress.com/2009/05/08/wpf-validation-made-easy-with-idataerrorinfo/
        //-http://www.youtube.com/watch?v=OOHDie8BdGI 
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        //What properties I am validating.
        static readonly string[] ValidatedProperties = 
        { 
            "HuntName"
        };

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationMessage(propertyName);
            }
        }

        private string GetValidationMessage(string propertyName)
        {
            String result = null;

            switch (propertyName)
            {
                case "HuntName":
                    {
                        result = ValidateHuntName();
                        break;
                    }
            }

            return result;
        }

        private String ValidateHuntName()
        {
            if (Validation.IsNullOrEmpty(HuntName))
            {
                return "Hunt name cannot be empty!";
            }
            if (!Validation.IsValidCharacters(HuntName))
            {
                return "There are invalid characters";
            }
            if (!Validation.IsValidLength(HuntName, HuntNameMaxLength, HuntNameMinLength))
            {
                return "Hunt name is an invalid length!";
            }
            

            return null;
        }
        #endregion
    }
}
