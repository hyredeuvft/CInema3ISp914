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

using Cinema.ClassHelper;
using Cinema.DB;
using static Cinema.ClassHelper.EFClass;
using static Cinema.ClassHelper.NavigateClass;

namespace Cinema.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoTicketPage.xaml
    /// </summary>
    public partial class InfoTicketPage : Page
    {
        List<Ticket> tickets = new List<Ticket>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По месту",
            "По ряду"
        };

        public InfoTicketPage()
        {
            try
            {
                InitializeComponent();
                CmbSort.ItemsSource = sortList;
                CmbSort.SelectedIndex = 0;
                GetListTicket();

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void GetListTicket()
        {
            try
            {
                tickets = Contextmy.Ticket.ToList();
                tickets = tickets.Where(i => Convert.ToString(i.Row).Contains(TxbSearch.Text)
                || Convert.ToString(i.Place).Contains(TxbSearch.Text)).ToList();
                switch (CmbSort.SelectedIndex)
                {
                    case 0:
                        tickets = tickets.OrderBy(i => i.IdTicket).ToList();
                        break;
                    case 1:
                        tickets = tickets.OrderBy(i => i.Row).ToList();
                        break;
                    case 2:
                        tickets = tickets.OrderBy(i => i.Place).ToList();
                        break;
                    default:
                        break;
                }
                Dg.ItemsSource = tickets;
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListTicket();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListTicket();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditTicketPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is Ticket)
            {
                var tickets = Dg.SelectedItem as Ticket;
                frame.Navigate(new Pages.AddEditTicketPage(tickets));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
