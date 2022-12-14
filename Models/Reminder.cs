using System;
namespace Memory_Refresher.Models
{
    [Serializable]
    internal class Reminder
    {
        public int IndexReminder { get; set; } //номер напоминания
        public string TittleReminder { get; set; } //заголовок
        public string MessageReminder { get; set; } //сообщение
        public DateTime DateTimeReminder { get; set; } //дата и время напоминания
        public bool statusReminder { get; set; } //статус выполнения напоминания
    }
}
