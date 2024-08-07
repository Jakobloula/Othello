using Othello.GameModel;
using System.Windows;
using System.Windows.Controls;

namespace Othello.GameView
{
    /// <summary>
    /// Interaction logic for SetupGameDialog.xaml
    /// </summary>

    public partial class SetupGameDialog : Window
    {
        public string PlayerOneName { get; private set; }
        public string PlayerTwoName { get; private set; }

        public PlayerType TypePlayerOne { get; private set; }
        public PlayerType TypePlayerTwo { get; private set; }

        public SetupGameDialog()
        {
            InitializeComponent();
        }

        private void PlayerTwoType(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Content.ToString()== "Human")
            {
                TypePlayerTwo= PlayerType.Human;
                
            }
            else
            {
                TypePlayerTwo= PlayerType.Computer;
                
            }

            
        }

        private void PlayerOneType (object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if ( radioButton.Content.ToString()=="Human")
            {
                TypePlayerOne= PlayerType.Human;
                

            }
            else
            {
                TypePlayerOne= PlayerType.Computer;
                
            }
        }

        private void start_btn (object sender, RoutedEventArgs e)
        {
            //Här inne ska du spara inmattingen av namn för både spelarna 
            //Kolla på den gamla versionen hur du skapade publica instanser av klasserna sen lägg in som name
            //dessa två ska då skickas till GameManager tillsammans med gameBoard och vilken typ av spelare där man skapar instanser av klasserna tror jag 
            
            if(string.IsNullOrEmpty(PlayerTextBox.Text) || string.IsNullOrEmpty(PlayerTextBox2.Text)) // DU kan  förbättre if satsen till att ifall det finns en elr två computer att det blir en forbestämd hät 
                //om inte spelaren har något annat 2
            {
                MessageBox.Show("Non of the two fields can be empty!");
            }
            else
            {
                PlayerOneName = PlayerTextBox.Text;
                PlayerTwoName = PlayerTextBox2.Text;
                this.Close();
                GameWindow gameWindow = new GameWindow(PlayerOneName, PlayerTwoName, TypePlayerOne, TypePlayerTwo);
                
         
                gameWindow.ShowDialog();
     
            }
            
            Opacity = 0.4;
            Opacity = 1;
        }





    }
}
