using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using TreasureHuntDesktopApplication.FullClient.Model;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        //-http://www.codeproject.com/Articles/72724/Beginning-a-WPF-MVVM-application-Navigating-betwee

        private ViewModelBase currentViewModel;
        public RelayCommand NavigateToCreateHuntViewCommand;
        private CreateHuntViewModel newViewModel = new CreateHuntViewModel();

        public MainViewModel()
        {
            NavigateToCreateHuntViewCommand = new RelayCommand(() => NavigateToView(newViewModel));
        }

        /// <summary>
        /// Gets the current view model that is being shown
        /// </summary>
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



     

        public void NavigateToView(ViewModelBase viewToNavigate)
        {
            CurrentViewModel = viewToNavigate;
        }
    }
}