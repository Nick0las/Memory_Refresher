using Memory_Refresher.Models;
using System.Collections.ObjectModel;

namespace Memory_Refresher.Resources.Iterfaces
{
    internal interface IDownloadReminders
    {
        void ISaveReminders(ObservableCollection <Reminder> collection);
        void IDownloadReminders(ObservableCollection<Reminder> collection);
        void IReminderCompleted( Reminder SelectReminder , ObservableCollection<Reminder> collection);
        void IReminderDelete(Reminder SelectReminder, ObservableCollection<Reminder> collection);
    }
}
