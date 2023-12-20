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
using System.Windows.Shapes;

namespace Tic_Tac_Toe {
    public partial class ChooseDifficulty : Window {
        public ChooseDifficulty() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            Button button = sender as Button;
            mainWindow.Difficulty = Convert.ToInt32(button.Uid);
            mainWindow.Show();
            this.Close();
        }
    }
}
