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
        //Команда сохранения в файл

        public ICommand SaveRemindersCmd { get; }
        private bool CanSaveRemindersCmdExecute(object p) => true;
        private void OnSaveRemindersCmdExecuted(object p)
        {
            SaveReminders(Collections.Reminders);
        }


        #endregion

        #region Конструктор
        public MainWindow_VM()
        {
            //TestOpenFile();
            SaveRemindersCmd = new LamdaCommand(OnSaveRemindersCmdExecuted, CanSaveRemindersCmdExecute);
        }

        #endregion

        private void TestOpenFile()
        {
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\Data\Reminders\");
            string[] nameFile = Directory.GetFiles(path);
            foreach (string paths in nameFile)
            {
                Process.Start(paths);
            }
        }

        public void SaveReminders(ObservableCollection<Reminder> collections)
        {
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\Data\Reminders\");
            string json = JsonSerializer.Serialize(collections);
            File.WriteAllText(path + @"reminders.json", json);
        }
    }
}
