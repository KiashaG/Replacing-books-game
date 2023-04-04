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

namespace PROG7312POE_PARTONE
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

        private void Replacing_Books(object sender, RoutedEventArgs e)
        {
            ReplacingBooks RB = new ReplacingBooks();
            this.Close();
            RB.Show();
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            MainWindow MW= new MainWindow();
            this.Close();
            MW.Show();
        }
    }
}
