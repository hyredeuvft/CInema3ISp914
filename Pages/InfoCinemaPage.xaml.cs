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
    /// Логика взаимодействия для InfoCinemaPage.xaml
    /// </summary>
    public partial class InfoCinemaPage : Page
    {

        List<NetCinema> cinema = new List<NetCinema>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По названию кинотеатра",
            "По адресу"        
        };
        public InfoCinemaPage()
        {
            InitializeComponent();
            CmbSort.ItemsSource = sortList;
            CmbSort.SelectedIndex = 0;
            GetListCinema();
        }

        private void GetListCinema()
        {

            cinema = Contextmy.NetCinema.ToList();
            cinema = cinema.Where(i => i.CinemaTitle.Contains(TxbSearch.Text)
            || i.Address.Contains(TxbSearch.Text)
            || i.PhoneNumber.Contains(TxbSearch.Text)).ToList();
            switch (CmbSort.SelectedIndex)
            {
                case 0:
                    cinema = cinema.OrderBy(i => i.IdCinema).ToList();
                    break;
                case 1:
                    cinema = cinema.OrderBy(i => i.CinemaTitle).ToList();
                    break;
                case 2:
                    cinema = cinema.OrderBy(i => i.Address).ToList();
                    break;
                case 3:
                    cinema = cinema.OrderBy(i => i.PhoneNumber).ToList();
                    break;
                default:
                    break;
            }
            Dg.ItemsSource = cinema;
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListCinema();
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListCinema();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditNetCinemaPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is NetCinema)
            {
                var cinema = Dg.SelectedItem as NetCinema;
                frame.Navigate(new Pages.AddEditNetCinemaPage(cinema));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is NetCinema)
            {
                var item = Dg.SelectedItem as NetCinema;
                GetListCinema();
                var dialogResult = MessageBox.Show("Вы действительно хотите удалить кинотеатр?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    Contextmy.NetCinema.Remove(item);
                    Contextmy.SaveChanges();
                    MessageBox.Show("Кинотеатр успешно удален!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    frame.Navigate(new InfoFilmPage());
                }
                else
                { }
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
