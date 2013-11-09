using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using TreasureHuntDesktopApplication.FullClient.Model;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //-http://www.codeproject.com/Articles/72724/Beginning-a-WPF-MVVM-application-Navigating-betwee

        private ViewModelBase currentViewModel;

        public RelayCommand NavigateToCreateHuntViewCommand { get; private set; }
        public RelayCommand NavigateToViewHuntsViewCommand { get; private set; }

        readonly static CreateHuntViewModel createHuntViewModel = new CreateHuntViewModel();
        readonly static ViewHuntViewModel viewHuntViewModel = new ViewHuntViewModel();

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
        }


        private void ExecuteNavigateToCreateHuntCommand()
        {
            CurrentViewModel = MainViewModel.createHuntViewModel;
        }

        private void ExecuteNavigateToViewHuntCommand()
        {
            CurrentViewModel = MainViewModel.viewHuntViewModel;
        }

    }
}