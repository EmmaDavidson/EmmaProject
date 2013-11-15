using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Windows.Input;
using TreasureHuntDesktopApplication.FullClient.Messages;
using TreasureHuntDesktopApplication.FullClient.Model;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //-http://www.codeproject.com/Articles/72724/Beginning-a-WPF-MVVM-application-Navigating-betwee

        private ViewModelBase currentViewModel;

        public RelayCommand NavigateToCreateHuntViewCommand { get; private set; }
        public RelayCommand NavigateToViewHuntsViewCommand { get; private set; }
        public RelayCommand NavigateToSearchHuntViewCommand { get; private set; }
        public RelayCommand NavigateToViewQRCodeCommand { get; private set; }

        readonly static CreateHuntViewModel createHuntViewModel = new CreateHuntViewModel();
        readonly static ViewHuntViewModel viewHuntViewModel = new ViewHuntViewModel();
        readonly static SearchHuntViewModel searchHuntViewModel = new SearchHuntViewModel();
        readonly static ViewQRCodeViewModel viewQRCodeViewModel = new ViewQRCodeViewModel();


        public ViewModelBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                if (currentViewModel == value)
                    return;
                currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public MainViewModel()
        {
            CurrentViewModel = MainViewModel.createHuntViewModel;
            NavigateToCreateHuntViewCommand = new RelayCommand(() => ExecuteNavigateToCreateHuntCommand());
            NavigateToViewHuntsViewCommand = new RelayCommand(() => ExecuteNavigateToViewHuntCommand());
            NavigateToSearchHuntViewCommand = new RelayCommand(() => ExecuteNavigateToSearchHuntCommand());
            NavigateToViewQRCodeCommand = new RelayCommand(() => ExecuteNavigateToQRCodeCommand());

            Messenger.Default.Register<UpdateViewMessage>
                (
                
                this,
                (action) => ReceiveViewMessage(action.UpdateViewTo)
                
                );
        }

        private void ReceiveViewMessage(String requestedUpdateViewModel)
        {
            if (requestedUpdateViewModel == "ViewHuntViewModel")
            {
                CurrentViewModel = MainViewModel.viewHuntViewModel;
            }
            else if (requestedUpdateViewModel == "ViewQRCodeViewModel")
            {
                CurrentViewModel = MainViewModel.viewQRCodeViewModel;
            }
            else if (requestedUpdateViewModel == "SearchHuntViewModel")
            {
                CurrentViewModel = MainViewModel.searchHuntViewModel;
            } 
            

        
        }

        private void ExecuteNavigateToQRCodeCommand()
        {
           // CurrentViewModel = MainViewModel.viewQRCodeViewModel;
        }

        private void ExecuteNavigateToCreateHuntCommand()
        {
            CurrentViewModel = MainViewModel.createHuntViewModel;
        }

        private void ExecuteNavigateToViewHuntCommand()
        {
            CurrentViewModel = MainViewModel.viewHuntViewModel;
        }

        private void ExecuteNavigateToSearchHuntCommand()
        {
            CurrentViewModel = MainViewModel.searchHuntViewModel;
        }
    }
}