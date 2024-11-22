using FinalUnoGame.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalUnoGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void bPlayGame_Click(object sender, RoutedEventArgs e)
        {
            PlayGame startGame = new PlayGame();

            this.Close();
            startGame.Show();
        }

        private void bHowToPlay_Click(object sender, RoutedEventArgs e)
        {
            UserGuide userGuide = new UserGuide();

            this.Close();
            userGuide.Show();
        }

        private void bExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Play game button

    }
}
