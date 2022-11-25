using Memory_Refresher.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Memory_Refresher.Services
{
    internal class Collections
    {
        //Список List для возможности сортировки напоминаний по дате
        public static List<Reminder> ListReminders { get; set; } = new List<Reminder>();

        // Коллекция для хранения и отображения напоминаний
        public static ObservableCollection<Reminder> Reminders { get; set; } = new ObservableCollection<Reminder>();
    }
}
