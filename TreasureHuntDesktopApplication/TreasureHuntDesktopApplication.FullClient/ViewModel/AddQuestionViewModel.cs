using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureHuntDesktopApplication.FullClient.TreasureHuntService;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing.Imaging;
using System.Windows.Media;


namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    class AddQuestionViewModel : ViewModelBase
    {
        #region Setup
        TreasureHuntServiceClient serviceClient = new TreasureHuntServiceClient();
        public RelayCommand GenerateQRCodeCommand { get; set; }
        public RelayCommand SaveQRCodeCommand { get; set; }
        public RelayCommand PrintQRCodeCommand { get; set; }

        public AddQuestionViewModel()
        {
            GenerateQRCodeCommand = new RelayCommand(() => ExecuteGenerateQRCodeCommand());
            SaveQRCodeCommand = new RelayCommand(() => ExecuteSaveQuestionAndQRCodeCommand());
            PrintQRCodeCommand = new RelayCommand(() => ExecutePrintQRCodeCommand());
        }

        #endregion

        #region Variable Getters and Setters
        public string newQuestion;
        public string NewQuestion
        {
            get { return this.newQuestion; }
            set
            {
                this.newQuestion = value;
                RaisePropertyChanged("NewQuestion");
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
        #endregion

        #region Commands

        private void ExecuteGenerateQRCodeCommand()
        {
            //-http://www.youtube.com/watch?v=3CSifXK62Tk
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrCodeImage = encoder.Encode(this.newQuestion);
            String location ="C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\" + this.newQuestion + ".jpg";
            //Saves the image to the file system, allow the user to print it out.
            qrCodeImage.Save(location, ImageFormat.Png);

            //-http://stackoverflow.com/questions/16246284/images-not-displaying-in-wpf-mvvm
            if (string.IsNullOrEmpty(location)) return;
            Stream reader = File.OpenRead(location);
            Image photo = Image.FromStream(reader);
            var finalStream = new MemoryStream();
            photo.Save(finalStream, ImageFormat.Png);
            var decoder = new PngBitmapDecoder(finalStream, BitmapCreateOptions.PreservePixelFormat,
                                               BitmapCacheOption.Default);

            this.QRCode = decoder.Frames[0];
            RaisePropertyChanged("QRCode");
        }


        private void ExecutePrintQRCodeCommand()
        { }

        private void ExecuteSaveQuestionAndQRCodeCommand()
        { }
        #endregion

    }
}
