using Memory_Refresher.Models;
using System.Collections.ObjectModel;

namespace Memory_Refresher.Resources.Iterfaces
{
    internal interface IDownloadReminders
    {
        void SaveReminders(ObservableCollection <Reminder> collection);
        void DownloadReminders(ObservableCollection<Reminder> collection);
    }
}
