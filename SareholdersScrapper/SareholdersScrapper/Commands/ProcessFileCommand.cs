using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SharehodlersScrapper.Common;
using SharehodlersScrapper.Models;
using SharehodlersScrapperLogic;

namespace SharehodlersScrapper.Commands
{
    class ProcessFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        readonly NotificationModel parent;
        public ProcessFileCommand(NotificationModel parent)
        {
            this.parent = parent;
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }
        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(parent.FileProcessingLabelData) &&
                    !string.IsNullOrEmpty(parent.CountryFolderPathLabelData) &&
                    !parent.FileProcessingLabelData.Equals(StringConsts.FileProcessingLabelData_Processing);
        }

        public void Execute(object parameter)
        {
            string chosenPath = parent.FilePathLabelData;
            string chosenFodlerPath = parent.CountryFolderPathLabelData;
            
            if (string.IsNullOrEmpty(chosenPath.Trim()))
            {
                return;
            }
            parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_Processing;
            try
            {
                Task.Factory.StartNew(() =>
                {
                    ShareholderAnalyzerLogic ms = new ShareholderAnalyzerLogic();
                    ms.ProcessFile(chosenPath, chosenFodlerPath);
                })
                .ContinueWith((action) =>
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => {
                        parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_Finish;
                        Console.WriteLine(StringConsts.FileProcessingLabelData_Finish);
                    }));
                });
            }
            catch (Exception)
            {
                parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_ErrorMessage;
            }
        }
        
    }
}
