using Memory_Refresher.Commands;
using Memory_Refresher.Models;
using Memory_Refresher.Services;
using Memory_Refresher.ViewModels.ViewModel_Base;
using System;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using Memory_Refresher.Views.Windows;

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
            reminder.IndexReminder = SerchMaxIndexInCollections(Collections.Reminders);                
            reminder.TittleReminder = TittleNewReminder;
            reminder.MessageReminder = ContentNewReminder;
            reminder.statusReminder = false;

            if(DateTimeNewReminder <= DateTime.Now)
            {
                MessageBoxResult result = MessageBox.Show("Невозможно создать напоминание раньше текущего времени", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            reminder.DateTimeReminder = DateTimeNewReminder;

            Collections.ListReminders = Collections.Reminders.ToList();
            Collections.ListReminders.Add(reminder);
            var sortcollections = Collections.ListReminders.OrderBy(r => r.DateTimeReminder).ToList();
            Collections.Reminders.Clear();
            foreach(var remind in sortcollections)
            {
                Collections.Reminders.Add(remind);
            }
            MessageBoxResult resultQuestion = MessageBox.Show("Напоминание добавлено. Добавить еще одно?", "Готово!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(resultQuestion == MessageBoxResult.No)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is AddNewReminder)
                    {
                        window.Close();
                        break;
                    }
                }
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

        //метод поиска  максимального значения Reminder.Index в коллекции
        private int SerchMaxIndexInCollections(ObservableCollection<Reminder> collectionReminders)
        {
            int maxIndex = 1;
            if (collectionReminders.Count != 0)
            {
                maxIndex = collectionReminders[0].IndexReminder;
                foreach(Reminder reminder in collectionReminders)
                {
                    maxIndex = maxIndex >= reminder.IndexReminder ? maxIndex : reminder.IndexReminder;
                }
                return maxIndex + 1;
            }
            return maxIndex;
        }
    }
}
