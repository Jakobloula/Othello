using System.Windows;

namespace Othello.GameView
{
    /// <summary>
    /// Interaction logic for DrawTextBox.xaml
    /// </summary>
    public partial class DrawTextBox : Window
    {
        public DrawTextBox()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
