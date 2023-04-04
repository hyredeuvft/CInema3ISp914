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
    /// Логика взаимодействия для InfoFilmPage.xaml
    /// </summary>
    public partial class InfoFilmPage : Page
    {
        List<Film> films = new List<Film>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По названию",
            "По рейтингу",
            "По дате премьеры"
        };


        public InfoFilmPage()
        {
            try
            {
                InitializeComponent();
                CmbSort.ItemsSource = sortList;
                CmbSort.SelectedIndex = 0;
                GetListFilm();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void GetListFilm()
        {
            try
            {
                films = Contextmy.Film.ToList();
                films = films.Where(i => i.MovieTitle.Contains(TxbSearch.Text)
                || i.Description.Contains(TxbSearch.Text)
                || i.Director.Contains(TxbSearch.Text)).ToList();
                switch (CmbSort.SelectedIndex)
                {
                    case 0:
                        films = films.OrderBy(i => i.IdFilm).ToList();
                        break;
                    case 1:
                        films = films.OrderBy(i => i.MovieTitle).ToList();
                        break;
                    case 2:
                        films = films.OrderBy(i => i.Rating).ToList();
                        break;
                    case 3:
                        films = films.OrderBy(i => i.PremierDate).ToList();
                        break;
                    default:
                        break;
                }
                Dg.ItemsSource = films;
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListFilm();
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListFilm();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditFilmPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is Film)
            {
                var films = Dg.SelectedItem as Film;
                frame.Navigate(new Pages.AddEditFilmPage(films));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is Film)
            {
                var item = Dg.SelectedItem as Film;
                GetListFilm();
                var dialogResult = MessageBox.Show("Вы действительно хотите удалить фильм?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    Contextmy.Film.Remove(item);
                    Contextmy.SaveChanges();
                    MessageBox.Show("Фильм успешно удален!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
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
