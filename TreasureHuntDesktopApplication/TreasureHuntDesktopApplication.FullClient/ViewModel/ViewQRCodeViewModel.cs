using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TreasureHuntDesktopApplication.FullClient.Messages;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    class ViewQRCodeViewModel : ViewModelBase
    {
        TreasureHuntServiceClient serviceClient = new TreasureHuntServiceClient();
        //public RelayCommand GenerateQRCodeCommand { get; set; }

        public ViewQRCodeViewModel()
        {
            //GenerateQRCodeCommand = new RelayCommand(() => ExecuteGenerateQRCodeCommand());

            Messenger.Default.Register<SelectedQuestionMessage>
             (

             this,
             (action) => ReceiveSelectedQuestionMessage(action.SelectedQuestion)

             );

            GenerateQRCode();
        }

        private void ReceiveSelectedQuestionMessage(question selectedQuestion)
        {
            this.currentQuestion = selectedQuestion;
        }

        public question currentQuestion;
        public question CurrentQuestion
        {
            get { return this.currentQuestion; }
            set
            {
                this.currentQuestion = value;
                RaisePropertyChanged("CurrentQuestion");
            }
        }

        public ImageSource qrCode;
        public ImageSource QRCode
        {
            get { return this.qrCode; }
            set
            {
                this.qrCode = value;
                RaisePropertyChanged("QRCode");
            }
        }

        private void GenerateQRCode()
        {
            //-http://stackoverflow.com/questions/16246284/images-not-displaying-in-wpf-mvvm
            String locationOfQrCode = this.currentQuestion.URL;
            if (string.IsNullOrEmpty(locationOfQrCode)) return;
            Stream fileReader = File.OpenRead(locationOfQrCode);
            Image onScreenQrCode = Image.FromStream(fileReader);
            var finalStream = new MemoryStream();
            onScreenQrCode.Save(finalStream, ImageFormat.Png);
            var decoder = new PngBitmapDecoder(finalStream, BitmapCreateOptions.PreservePixelFormat,
                                               BitmapCacheOption.Default);

            this.QRCode = decoder.Frames[0];
            RaisePropertyChanged("QRCode");
            
        }

    }
}
