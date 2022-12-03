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
            ISaveReminders(Collections.Reminders);
        }
        #endregion //Сохранение в файл

        #region Команда на статус "Напомнинание выполнено"
        public ICommand ReminderCompletedCmd { get; }
        private bool CanReminderCompletedCmdExecute(object p) => true;
        private void OnReminderCompletedCmdExecuted(object p)
        {
            IReminderCompleted(SelectedReminder, Collections.Reminders);
        }

        #endregion //Команда на статус "Напомнинание выполнено"

        #region Удаление напоминания из файла
        public ICommand ReminderDeleteCmd { get; }
        private bool CanReminderDeleteCmdExecute(object p) => true;
        private void OnReminderDeleteCmdExecuted(object p)
        {
            IReminderDelete(SelectedReminder, Collections.Reminders);
        }

        #endregion

        #region Команда сохранения и закрытия приложения
        public ICommand CloseApp { get; }
        private bool CanCloseAppExecute(object p) => true;
        private void OnCloseAppExecuted(object p)
        {
            ISaveReminders(Collections.Reminders);
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region Команда открытия окна по таймеру
        public ICommand ActivatingAlarmClockCmd { get; }
        private bool CanActivatingAlarmClockCmdExecute(object p) => true;
        private void OnnActivatingAlarmClockCmdExecuted (object p)
        {

        }




        #endregion


        #endregion //команды

        #region Конструктор
        public MainWindow_VM()
        {
            IDownloadReminders(Collections.Reminders);
            CloseApp = new LamdaCommand(OnCloseAppExecuted, CanCloseAppExecute);
            ReminderDeleteCmd = new LamdaCommand(OnReminderDeleteCmdExecuted, CanReminderDeleteCmdExecute);
            SaveRemindersCmd = new LamdaCommand(OnSaveRemindersCmdExecuted, CanSaveRemindersCmdExecute);
            ReminderCompletedCmd = new LamdaCommand(OnReminderCompletedCmdExecuted, CanReminderCompletedCmdExecute);
        }

        #endregion

        #region Реализация интерфейса IDownloadReminders

        //сохранение в файл
        public void ISaveReminders(ObservableCollection<Reminder> collections)
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
        //загрузка из файла
        public void IDownloadReminders(ObservableCollection<Reminder> collection)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(exePath + @"\..\..\Data\Reminders\");

            string jsonString = File.ReadAllText(path + @"reminders.json");
            try
            {
                var remiders = JsonSerializer.Deserialize<ObservableCollection<Reminder>>(jsonString);

                foreach (Reminder reminder in remiders)
                {
                    Reminder rem1 = new Reminder();
                    if(reminder.statusReminder == false)
                    {
                        rem1 = reminder;
                    }
                    else { break; }
                    
                    collection.Add(rem1);
                }
            }

            catch
            {
                return;
            }
        }

        //присваивание напоминанию статуса выполнено и исключение из коллекции

        public void IReminderCompleted(Reminder SelectReminder, ObservableCollection<Reminder> collection)
        {
            SelectedReminder.statusReminder = true;
            collection.Remove(SelectReminder);
        }

        //удаление напоминания

        public void IReminderDelete(Reminder SelectReminder, ObservableCollection<Reminder> collection)
        {
            collection.Remove(SelectReminder);
            //ISaveReminders(collection);
        }
    }
}
#endregion
