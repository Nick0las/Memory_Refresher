using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Refresher.Models
{
    internal class Reminder
    {
        public int IndexReminder { get; set; } //номер напоминания
        public string TittleReminder { get; set; } //заголовок
        public string MessageReminder { get; set; } //сообщение
        public DateTime DateTimeReminder { get; set; } //дата и время напоминания
        public bool statusReminder { get; set; } //статус выполнения напоминания
    }
}
