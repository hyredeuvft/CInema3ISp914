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

using Cinema.ClassHelper;
using Cinema.DB;
using Cinema.Pages;
using static Cinema.ClassHelper.EFClass;
using static Cinema.ClassHelper.NavigateClass;

namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для NavigateWindow.xaml
    /// </summary>
    public partial class NavigateWindow : Window
    {
        public NavigateWindow()
        {
            InitializeComponent();
            NavigateClass.frame = frameMenu;
        }
        
        private void btnForFilm_Click(object sender, RoutedEventArgs e)
        {
            InfoFilmPage infoFilmPage = new InfoFilmPage();
            frame.Navigate(infoFilmPage);
        }

        private void btnForCashReceipt_Click(object sender, RoutedEventArgs e)
        {
            InfoCashReceiptPage infoCashReceiptPage = new InfoCashReceiptPage();
            frame.Navigate(infoCashReceiptPage);
        }

        private void btnForCinema_Click(object sender, RoutedEventArgs e)
        {
            InfoCinemaPage infoCinemaPage = new InfoCinemaPage();
            frame.Navigate(infoCinemaPage);
        }

        private void btnForEmployee_Click(object sender, RoutedEventArgs e)
        {
            InfoEmployeePage infoEmployeePage = new InfoEmployeePage();
            frame.Navigate(infoEmployeePage);
        }

        private void btnForProduct_Click(object sender, RoutedEventArgs e)
        {
            InfoProductPage infoProductPage = new InfoProductPage();
            frame.Navigate(infoProductPage);
        }

        private void btnForSession_Click(object sender, RoutedEventArgs e)
        {
            InfoSessionPage infoSessionPage = new InfoSessionPage();
            frame.Navigate(infoSessionPage);
        }

        private void btnForTIcket_Click(object sender, RoutedEventArgs e)
        {
            InfoTicketPage infoTicketPage = new InfoTicketPage();
            frame.Navigate(infoTicketPage);
        }

        private void btnForUser_Click(object sender, RoutedEventArgs e)
        {
            InfoUserPage infoUserPage = new InfoUserPage();
            frame.Navigate(infoUserPage);
        }
    }
}