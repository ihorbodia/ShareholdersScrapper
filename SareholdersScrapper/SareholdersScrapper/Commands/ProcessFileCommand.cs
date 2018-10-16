using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using SharehodlersScrapper.Common;
using SharehodlersScrapper.Models;
using SharehodlersScrapperLogic;

namespace SharehodlersScrapper.Commands
{
    class ProcessFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        NotificationModel parent;
        ShareholderAnalyzerLogic ms;
        public ProcessFileCommand(NotificationModel parent)
        {
            this.parent = parent;
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }
        public bool CanExecute(object parameter)
        {
            return parent.FileProcessingLabelData.Equals(StringConsts.FileProcessingLabelData_CanProcess);
        }

        public void Execute(object parameter)
        {
            string chosenPath = parent.FilePathLabelData;
            ms = new ShareholderAnalyzerLogic(chosenPath);
            if (string.IsNullOrEmpty(chosenPath.Trim()))
            {
                return;
            }
            parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_Processing;
            try
            {
                new Task(() =>
                {
                    Thread t = new Thread(()=>
                    {
                        ms.ProcessFile();
                        SaveFile();
                    });
                    t.Start();
                    t.Join();
                    parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_Finish;
                    Console.WriteLine(StringConsts.FileProcessingLabelData_Finish);
                }).Start();
            }
            catch (Exception)
            {
                parent.FileProcessingLabelData = StringConsts.FileProcessingLabelData_ErrorMessage;
            }
        }
        private void SaveFile()
        {
            try
            {
                ms.SaveFile();
            }
            catch (InvalidOperationException ex)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Try to close excel file and click OK.", "Error while saving file", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    SaveFile();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
