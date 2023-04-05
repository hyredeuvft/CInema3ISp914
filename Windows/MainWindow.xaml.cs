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

using Cinema.ClassHelper;
using Cinema.DB;
using Cinema.Windows;
using static Cinema.ClassHelper.EFClass;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int id;
        List<Film> films = new List<Film>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По названию",
            "По рейтингу",
            "По дате премьеры"
        };
        public MainWindow()
        {
            InitializeComponent();
            CmbSort.ItemsSource = sortList;
            CmbSort.SelectedIndex = 0;
            GetListFilm();
            GetFilm();
        }
        public MainWindow(int a)
        {
            try
            {

                InitializeComponent();
                id = a;
                CmbSort.ItemsSource = sortList;
                CmbSort.SelectedIndex = 0;
                GetListFilm();
                GetFilm();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        films = films.OrderByDescending(i => i.Rating).ToList();
                        break;
                    case 3:
                        films = films.OrderBy(i => i.PremierDate).ToList();
                        break;
                    default:
                        break;
                }
                LvFilmList.ItemsSource = films;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void GetFilm()
        {
            List<Film> films = new List<Film>();
            films = Contextmy.Film.ToList();
            LvFilmList.ItemsSource = films;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (LvFilmList.SelectedItem is Film)
            {
                Film film = LvFilmList.SelectedItem as Film;
                InfoFilmWindow infoFilmWindow = new InfoFilmWindow(film, id);
                infoFilmWindow.Show();
                this.Close();
            }
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListFilm();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListFilm();
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            MainProductWindow productWindow = new MainProductWindow(id);
            productWindow.Show();
            this.Close();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            LKUserWindow lKUserWindow = new LKUserWindow(id);
            lKUserWindow.Show();
            this.Close();
        }
    }
}
