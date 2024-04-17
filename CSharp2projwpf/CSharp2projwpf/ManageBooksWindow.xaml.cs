
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
using System.Windows.Shapes;
using ORMLib;

namespace CSharp2projwpf
{

    /// <summary>
    /// Interaction logic for ManageBooksWindow.xaml
    /// </summary>
    public partial class ManageBooksWindow : Window
    {
        private bool AddWindowClosed = true;
        public ManageBooksWindow()
        {
            InitializeComponent();
            List<Book> books = WPFService.GetBooks();
            BooksGrid.ItemsSource = books;
        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (AddWindowClosed)
            {
                Thread thread = new Thread(() =>
                {
                    AddBookWindow addBookWindow = new AddBookWindow();
                    addBookWindow.Closing += (s, args) =>
                    {
                        AddWindowClosed = true;
                        Dispatcher.Invoke(() =>
                        {
                            List<Book> books = WPFService.GetBooks();
                            BooksGrid.ItemsSource = books;
                        });
                    };
                    addBookWindow.ShowDialog();
                });
                AddWindowClosed = false;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            int bookId = (int)btn.Tag;
            Book book = WPFService.GetBook(bookId);
            WPFService.DeleteBook(book);
            List<Book> books = WPFService.GetBooks();
            BooksGrid.ItemsSource = books;
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            List<Book> books = BooksGrid.ItemsSource as List<Book>;
            foreach (Book book in books)
                WPFService.UpdateBook(book);
        }
    }
}
