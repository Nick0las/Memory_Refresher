using Memory_Refresher.Commands;
using Memory_Refresher.Models;
using Memory_Refresher.ViewModels.ViewModel_Base;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Memory_Refresher.ViewModels
{
    internal class MainWindow_VM : Base_VM
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
           
        }


        #endregion

        #region Конструктор
        public MainWindow_VM()
        {
            SaveRemindersCmd = new LamdaCommand(OnSaveRemindersCmdExecuted, CanSaveRemindersCmdExecute);
        }

        #endregion
    }
}
