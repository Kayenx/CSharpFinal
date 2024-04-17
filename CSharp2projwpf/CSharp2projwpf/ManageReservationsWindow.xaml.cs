using ORMLib;
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

namespace CSharp2projwpf
{
    /// <summary>
    /// Interaction logic for ManageReservationsWindow.xaml
    /// </summary>
    public partial class ManageReservationsWindow : Window
    {
        public ManageReservationsWindow()
        {
            InitializeComponent();
            List<Reservation> reservations = WPFService.GetReservations();
            ReservationsGrid.ItemsSource = reservations;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            int reservationId = (int)btn.Tag;
            Reservation reservation = WPFService.GetReservation(reservationId);
            WPFService.DeleteReservation(reservation);
            List<Reservation> reservations = WPFService.GetReservations();
            ReservationsGrid.ItemsSource = reservations;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations_new = ReservationsGrid.ItemsSource as List<Reservation>;
            foreach (var RN in reservations_new)
            {
                if (RN.Start >= RN.End)
                {
                    RN.Start = WPFService.GetReservation(RN.ID).Start;
                    RN.End = WPFService.GetReservation(RN.ID).End;
                }
                List<Reservation> reservations_old = WPFService.GetReservations().Where(x => x.Book_Id == RN.Book_Id).ToList();
                foreach (var RO in reservations_old)
                {
                    if (RO.User_Id == RN.User_Id)
                        continue;
                    if ((RO.Start <= RN.Start && RN.Start <= RO.End) || (RO.Start <= RN.End && RN.End <= RO.End))
                    {
                        RN.Start = WPFService.GetReservation(RN.ID).Start;
                        RN.End = WPFService.GetReservation(RN.ID).End;
                        break;
                    }
                }
                WPFService.UpdateReservation(RN);
                List<Reservation> reservations = WPFService.GetReservations();
                ReservationsGrid.ItemsSource = reservations;

            }
        }
    }
}
