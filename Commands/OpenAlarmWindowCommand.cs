using Memory_Refresher.Commands.Base_cmd;
using Memory_Refresher.Views.Windows;
using System;
using System.Windows;




namespace Memory_Refresher.Commands
{
    internal class OpenAlarmWindowCommand : BaseCommand
    {
        double workHeight = SystemParameters.FullPrimaryScreenHeight;
        double workWidth = SystemParameters.FullPrimaryScreenWidth;

        private AlarmWindow _Window;
        public override bool CanExecute(object parameter) => _Window == null;
        public override void Execute(object parameter)
        {

            AlarmWindow window = new AlarmWindow();
            _Window = window;

            window.Top = workHeight-220;
            window.Left = workWidth-450;
            window.Owner = Application.Current.MainWindow;
            window.Closed += OnWindowClosed;
            window.Show();
        }

        private void OnWindowClosed(object Sender, EventArgs E)
        {
            ((Window)Sender).Closed -= OnWindowClosed;
            _Window = null;
        }
    }
}
