using Othello.GameView;
using System.Windows;

namespace Othello
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void new_btn(object sender, RoutedEventArgs e)
        {           
            SetupGameDialog setupGameDialog = new SetupGameDialog();           
            setupGameDialog.ShowDialog();  
            this.Close();
        }

        private void Exit_btn (object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
