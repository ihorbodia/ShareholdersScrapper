using System.Windows.Controls;
using SharehodlersScrapper.ViewModel;

namespace ShareholderScrapper.Views
{
    /// <summary>
    /// Interaction logic for NotificationsView.xaml
    /// </summary>
    public partial class NotificationsView : UserControl
    {
        public NotificationsView()
        {
            InitializeComponent();
            DataContext = new NotificationViewModel();
        }
    }
}
