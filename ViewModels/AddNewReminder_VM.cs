using Memory_Refresher.Commands;
using Memory_Refresher.Models;
using Memory_Refresher.Services;
using Memory_Refresher.ViewModels.ViewModel_Base;
using System;
using System.Windows.Input;
using System.Linq;

namespace Memory_Refresher.ViewModels
{
    internal class AddNewReminder_VM : Base_VM
    {
        #region заголовок окна
        private string _Tittle = "Новое напоминание++";
        /// <summary>Заголовок окна </summary>
        public string Tittle
        {
            get => _Tittle;
            set => Set(ref _Tittle, value);
        }
        #endregion

        #region Свойства, привязанные к окну "Новое напоминание"

        #region Заголовок
        // Заголовок
        private string _tittleNewReminder;
        public string TittleNewReminder
        {
            get => _tittleNewReminder;
            set=> Set(ref _tittleNewReminder, value);
        }
        #endregion

        #region Текст напоминания
        //Содержание напоминания
        private string _contentNewReminder;
        public string ContentNewReminder
        {
            get => _contentNewReminder;
            set => Set(ref _contentNewReminder, value);
        }
        #endregion
                
        #region Дата и время напоминания

        private DateTime _dateTimeNewReminder = DateTime.Now;
        public DateTime DateTimeNewReminder
        {
            get => _dateTimeNewReminder;
            set => Set(ref _dateTimeNewReminder, value);
        }
        #endregion

        #region Интервал времени напоминания
        //Интервал времени (через сколько напомнить)
        private DateTime? _intervalTimeNewReminder = null;
        public DateTime? IntervalTimeNewReminder
        {
            get => _intervalTimeNewReminder;
            set => Set(ref _intervalTimeNewReminder, value);
        }
        #endregion

        #endregion

        #region Команда добавления нового напоминания

        public ICommand AddNewReminderCmd { get; }
        private bool CanAddNewReminderCmdExecute(object p) => true;
        private void OnAddNewReminderCmdExecuted(object p)
        {
            //Создание нового напоминания
            Reminder reminder = new Reminder();
            reminder.IndexReminder = Collections.Reminders.Count + 1;
            reminder.TittleReminder = TittleNewReminder;
            reminder.MessageReminder = ContentNewReminder;
            reminder.statusReminder = false;
            reminder.DateTimeReminder = DateTimeNewReminder;

            Collections.ListReminders = Collections.Reminders.ToList();
            Collections.ListReminders.Add(reminder);
            var sortcollections = Collections.ListReminders.OrderBy(r => r.DateTimeReminder).ToList();
            Collections.Reminders.Clear();
            foreach(var remind in sortcollections)
            {
                Collections.Reminders.Add(remind);
            }
        }

        #endregion

        #region Конструктор
        // Конструктор без парам

        public AddNewReminder_VM()
        {
            //Лямда-Команда создания нового напоминания
            AddNewReminderCmd = new LamdaCommand(OnAddNewReminderCmdExecuted, CanAddNewReminderCmdExecute);
            

        }

        #endregion
    }
}
