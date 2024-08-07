using System.Windows;

namespace Othello.GameView
{
    /// <summary>
    /// Interaction logic for WinnerDialog.xaml
    /// </summary>
    public partial class WinnerDialog : Window
    {
        public WinnerDialog(string WinnerName)
        {
            InitializeComponent();
            TheWinner.Content = WinnerName;
        }

        private void Close_btn (object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
