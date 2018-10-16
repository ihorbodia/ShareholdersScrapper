using System;
using System.Windows.Input;
using SharehodlersScrapper.Models;
using SharehodlersScrapper.Common;
using SharehodlersScrapperLogic;

namespace SharehodlersScrapper.Commands
{
    class ChooseFolderCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        NotificationModel parent;
        public ChooseFolderCommand(NotificationModel parent)
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
            string chosenPath = FilesHelper.SelectFile();
            if (!string.IsNullOrEmpty(chosenPath.Trim()))
            {
                parent.FilePathLabelData = chosenPath;
                parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_CanProcess;
            }
        }
    }
}
