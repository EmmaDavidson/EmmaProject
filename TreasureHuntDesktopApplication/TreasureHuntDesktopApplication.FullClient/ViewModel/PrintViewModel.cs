using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using TreasureHuntDesktopApplication.FullClient.Messages;
using Word = Microsoft.Office.Interop.Word;

namespace TreasureHuntDesktopApplication.FullClient.ViewModel
{
    public class PrintViewModel : ViewModelBase
    {
        public PrintViewModel()
        {
            Messenger.Default.Register<PrintMessage>
             (

             this,
             (action) => ReceivePrintMessage(action.FileLocation)
             );
        }


        private FixedDocumentSequence documentViewer;
        public FixedDocumentSequence DocumentViewer
        {
            get { return this.documentViewer; }
            set
            {
                this.documentViewer = value;
                RaisePropertyChanged("DocumentViewer");
            }
        }

        //-http://code.msdn.microsoft.com/office/CSVSTOViewWordInWPF-db347436
        private void ReceivePrintMessage(String fileLocation)
        {
            string convertedXpsDoc = string.Concat("C:\\Users\\Emma\\Documents\\GitHub\\EmmaProject\\TreasureHuntDesktopApplication\\tempDoc1.xps");

            XpsDocument xpsDocument = ConvertWordToXps(fileLocation, convertedXpsDoc);
            if (xpsDocument == null)
            {
                return;
            }

            DocumentViewer = xpsDocument.GetFixedDocumentSequence();
        }

        //-http://code.msdn.microsoft.com/office/CSVSTOViewWordInWPF-db347436
        private XpsDocument ConvertWordToXps(string wordFilename, string xpsFilename)
        {
            Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                wordApp.Documents.Open(wordFilename);
                wordApp.Application.Visible = false;
                wordApp.WindowState = WdWindowState.wdWindowStateMinimize;

                Document doc = wordApp.ActiveDocument;
                doc.SaveAs(xpsFilename, WdSaveFormat.wdFormatXPS);

                XpsDocument xpsDocument = new XpsDocument(xpsFilename, FileAccess.Read);
                return xpsDocument;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                wordApp.Documents.Close();
                ((_Application)wordApp).Quit(WdSaveOptions.wdDoNotSaveChanges);
            } 
           
        } 
    }
}
