using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CSharp2projwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool booksWindowClosed = true;
        private bool reservationsWindowClosed = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ManageBooks_Click(object sender, RoutedEventArgs e)
        {
            if (booksWindowClosed)
            {
                Thread thread = new Thread(() =>
                {
                    ManageBooksWindow manageBooksWindow = new ManageBooksWindow();
                    manageBooksWindow.Closing += (s, args) =>
                    {
                        booksWindowClosed = true;
                    };
                    manageBooksWindow.ShowDialog();
                });
                booksWindowClosed = false;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }
        private void ManageReservations_Click(object sender, RoutedEventArgs e)
        {
            if (reservationsWindowClosed)
            {
                Thread thread = new Thread(() =>
                {
                    ManageReservationsWindow manageReservationsWindow = new ManageReservationsWindow();
                    manageReservationsWindow.Closing += (s, args) =>
                    {
                        reservationsWindowClosed = true;
                    };
                    manageReservationsWindow.ShowDialog();
                });
                reservationsWindowClosed = false;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
