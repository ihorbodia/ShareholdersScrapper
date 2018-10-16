using System;
using System.Windows.Input;
using SharehodlersScrapper.Models;
using SharehodlersScrapper.Common;
using SharehodlersScrapperLogic;

namespace SharehodlersScrapper.Commands
{
    public class ChooseCountryFolderCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        readonly NotificationModel parent;
        public ChooseCountryFolderCommand(NotificationModel parent)
        {
            this.parent = parent;
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }
        public bool CanExecute(object parameter)
        {
            return !parent.FileProcessingLabelData.Equals(StringConsts.FileProcessingLabelData_Processing);
        }

        public void Execute(object parameter)
        {
            string chosenPath = FilesHelper.SelectFolder();
            if (!string.IsNullOrEmpty(chosenPath.Trim()))
            {
                parent.CountryFolderPathLabelData = chosenPath;
                parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_CanProcess;
            }
        }
    }
}
