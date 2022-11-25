using Memory_Refresher.Commands.Base_cmd;
using Memory_Refresher.Views.Windows;
using System;
using System.Windows;
namespace Memory_Refresher.Commands
{
    internal class AddNewReminderCommand : BaseCommand
    {
        private AddNewReminder _Window;
        public override bool CanExecute(object parameter) => _Window == null;
        public override void Execute(object parameter)
        {
            AddNewReminder window = new AddNewReminder
            {
                Owner = Application.Current.MainWindow
            };
            _Window = window;
            window.Closed += OnWindowClosed;
            window.ShowDialog();
        }

        private void OnWindowClosed(object Sender, EventArgs E)
        {
            ((Window)Sender).Closed -= OnWindowClosed;
            _Window = null;
        }
    }
}
