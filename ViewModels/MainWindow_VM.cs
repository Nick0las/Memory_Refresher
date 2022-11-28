using Memory_Refresher.Commands;
using Memory_Refresher.Models;
using Memory_Refresher.ViewModels.ViewModel_Base;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Text.Json;
using System.Text.Json.Serialization;
using Memory_Refresher.Services;
using Memory_Refresher.Resources.Iterfaces;
using System.Collections.ObjectModel;
using System;

namespace Memory_Refresher.ViewModels
{
    internal class MainWindow_VM : Base_VM, IDownloadReminders
    {
        #region заголовок окна
        private string _Tittle = "Memory Refresher++";
        /// <summary>Заголовок окна </summary>
        public string Tittle
        {
            get => _Tittle;
            set => Set(ref _Tittle, value);
        }
        #endregion

        #region Свойства, привязанные к онку
        public Reminder SelectedReminder { get; set; }

        #endregion

        #region Команды

        #region Сохранение в файл
        //Команда сохранения в файл

        public ICommand SaveRemindersCmd { get; }
        private bool CanSaveRemindersCmdExecute(object p) => true;
        private void OnSaveRemindersCmdExecuted(object p)
        {
            SaveReminders(Collections.Reminders);
        }
        #endregion //Сохранение в файл

        #region Команда на статус "Напомнинание выполнено"
        public ICommand ReminderCompletedCmd { get; }
        private bool CanReminderCompletedCmdExecute(object p) => true;
        private void OnReminderCompletedCmdExecuted(object p)
        {
            SelectedReminder.statusReminder = true;
        }

        #endregion //Команда на статус "Напомнинание выполнено"


        #endregion //команды

        #region Конструктор
        public MainWindow_VM()
        {
            DownloadReminders(Collections.Reminders);
            SaveRemindersCmd = new LamdaCommand(OnSaveRemindersCmdExecuted, CanSaveRemindersCmdExecute);
            ReminderCompletedCmd = new LamdaCommand(OnReminderCompletedCmdExecuted, CanReminderCompletedCmdExecute);
        }

        #endregion

        public void SaveReminders(ObservableCollection<Reminder> collections)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(exePath + @"\..\..\Data\Reminders\");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(collections, options);
            File.WriteAllText(path + @"reminders.json", json);
        }

        public void DownloadReminders(ObservableCollection<Reminder> collection)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(exePath + @"\..\..\Data\Reminders\");

            string jsonString = File.ReadAllText(path + @"reminders.json");

            var remiders = JsonSerializer.Deserialize<ObservableCollection<Reminder>>(jsonString);

            foreach(Reminder reminder in remiders)
            {
                Reminder rem1 = new Reminder();
                rem1 = reminder;
                collection.Add(rem1);
            }
        }
    }
}
