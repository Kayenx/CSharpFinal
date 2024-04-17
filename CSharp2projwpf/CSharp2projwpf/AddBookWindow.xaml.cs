
using ORMLib;
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

namespace CSharp2projwpf
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string author = AuthorBox.Text;
            string genre = GenreBox.Text;
            if(title != null && title.Trim() != "" && author != null && author.Trim() != "" && genre != null && genre.Trim() != "")
            {
                Book newBook = new Book()
                {
                    Title = title,
                    Author = author,
                    Genre = genre
                };
                WPFService.InsertBook(newBook);
            }
            this.Close();
        }
    }
}
