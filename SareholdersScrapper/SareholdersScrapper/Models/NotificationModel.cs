using System.ComponentModel;
using System.Windows.Input;
using SharehodlersScrapper.Commands;
using SharehodlersScrapper.Common;

namespace SharehodlersScrapper.Models
{
    public class NotificationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _filePathLabel;
        private string _filePathLabelData;

        private string _fileProcessingLabel;
        private string _fileProcessingLabelData;

        public NotificationModel()
        {
            ProcessFileCommand = new ProcessFileCommand(this);
            ChooseFolderCommand = new ChooseFolderCommand(this);
            FilePathLabel = StringConsts.FilePathLabelConst;
            FileProcessingLabel = StringConsts.FileProcessingLabelConst;
        }
        public ICommand ProcessFileCommand { get; private set; }
        public ICommand ChooseFolderCommand { get; private set; }
        public string FilePathLabel
        {
            get
            {
                return _filePathLabel;
            }
            private set
            {
                if (_filePathLabel != value)
                {
                    _filePathLabel = value;
                    RaisePropertyChanged(nameof(FilePathLabel));
                }
            }
        }
        public string FilePathLabelData
        {
            get
            {
                return _filePathLabelData;
            }
            set
            {
                if (_filePathLabelData != value)
                {
                    _filePathLabelData = value;
                    RaisePropertyChanged(nameof(FilePathLabelData));
                }
            }
        }
        public string FileProcessingLabel
        {
            get
            {
                return _fileProcessingLabel;
            }
            private set
            {
                if (_fileProcessingLabel != value)
                {
                    _fileProcessingLabel = value;
                    RaisePropertyChanged(nameof(FileProcessingLabel));
                }
            }
        }
        public string FileProcessingLabelData
        {
            get
            {
                return _fileProcessingLabelData;
            }
            set
            {
                if (_fileProcessingLabelData != value)
                {
                    _fileProcessingLabelData = value;
                    RaisePropertyChanged(nameof(FileProcessingLabelData));
                }
            }
        }

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
