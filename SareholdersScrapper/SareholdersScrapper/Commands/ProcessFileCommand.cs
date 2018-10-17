using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
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
                    !string.IsNullOrEmpty(parent.CountryFolderPathLabelData);
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
                    Thread t = new Thread(() =>
                    {
                        ShareholderAnalyzerLogic ms = new ShareholderAnalyzerLogic();
                        ms.ProcessFile(chosenPath, chosenFodlerPath);
                    });
                    t.Start();
                    t.Join();
                    parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_Finish;
                    Console.WriteLine(StringConsts.FileProcessingLabelData_Finish);
                });
            }
            catch (Exception)
            {
                parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_ErrorMessage;
            }
        }
        
    }
}
