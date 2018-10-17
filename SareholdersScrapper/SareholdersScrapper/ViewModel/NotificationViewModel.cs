using SharehodlersScrapper.Common;
using SharehodlersScrapper.Models;

namespace SharehodlersScrapper.ViewModel
{
    public class NotificationViewModel
    {
        public NotificationModel NotificationModelObject { get; set; }

        public NotificationViewModel()
        {
            NotificationModelObject = new NotificationModel()
            {
                FilePathLabelData = string.Empty,
                CountryFolderPathLabelData = string.Empty,
                FileProcessingLabelData = StringConsts.FileProcessingLabelData_ChooseFile
            };
        }
    }
}
