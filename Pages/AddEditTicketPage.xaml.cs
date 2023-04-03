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
    /// Логика взаимодействия для AddEditTicketPage.xaml
    /// </summary>
    public partial class AddEditTicketPage : Page
    {

        private bool isEdit = false;
        private Ticket editTicket;

        public AddEditTicketPage()
        {
            InitializeComponent();

            CmbSession.ItemsSource = Contextmy.SessionFilm.ToList();
            CmbSession.DisplayMemberPath = "DateTimeStart";
            CmbSession.SelectedIndex = 0;
        }

        public AddEditTicketPage(Ticket ticket) 
        {
            try
            {
                InitializeComponent();
                CmbSession.ItemsSource = Contextmy.SessionFilm.ToList();
                CmbSession.DisplayMemberPath = "DateTimeStart";

                CmbSession.SelectedItem = Contextmy.SessionFilm.Where(i => i.IdSessionFilm == ticket.IdSessionFilm).FirstOrDefault();
                TbRow.Text = Convert.ToString(ticket.Row);
                TbPlace.Text = Convert.ToString(ticket.Place);
                TbFPrice.Text = Convert.ToString(ticket.FinalPrice);

                isEdit = true;
                editTicket = ticket;
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isEdit)
                {
                    editTicket.Row = (byte)Convert.ToInt16(TbRow.Text);
                    editTicket.Place = (byte)Convert.ToInt16(TbPlace.Text);
                    editTicket.FinalPrice = Convert.ToDecimal(TbFPrice.Text);
                    editTicket.IdSessionFilm = (CmbSession.SelectedItem as SessionFilm).IdSessionFilm;

                    Contextmy.SaveChanges();
                }
                else
                {
                    Ticket ticket = new Ticket();
                    ticket.Row = (byte)Convert.ToInt16(TbRow.Text);
                    ticket.Place = (byte)Convert.ToInt16(TbPlace.Text);
                    ticket.FinalPrice = Convert.ToDecimal(TbFPrice.Text);
                    ticket.IdSessionFilm = (CmbSession.SelectedItem as SessionFilm).IdSessionFilm;

                    Contextmy.Ticket.Add(ticket);
                    Contextmy.SaveChanges();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
            frame.Navigate(new InfoTicketPage());
        }
    }
}
