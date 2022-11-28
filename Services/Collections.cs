using Memory_Refresher.Models;
using Memory_Refresher.Resources.Iterfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Memory_Refresher.Services
{
    internal class Collections
    {
        //Список List для возможности сортировки напоминаний по дате
        public static List<Reminder> ListReminders { get; set; } = new List<Reminder>();

        // Коллекция для хранения и отображения напоминаний
        public static ObservableCollection<Reminder> Reminders { get; /*set;*/ } = new ObservableCollection<Reminder>();
    }
}
