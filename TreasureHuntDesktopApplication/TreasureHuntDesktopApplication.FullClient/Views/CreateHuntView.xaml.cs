using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreasureHuntDesktopApplication.FullClient.ViewModel;

namespace TreasureHuntDesktopApplication.FullClient.Views
{
    /// <summary>
    /// Interaction logic for CreateHuntView.xaml
    /// </summary>
    public partial class CreateHuntView : UserControl
    {
        private CreateHuntViewModel  viewModel = new CreateHuntViewModel();
        public CreateHuntView()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };

        }
    }
}
