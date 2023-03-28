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
using Cinema.Windows;
using static Cinema.ClassHelper.EFClass;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GetFilm();
        }

        private void GetFilm()
        {
            List<Film> films = new List<Film>();
            films = Contextmy.Film.ToList();
            LvFilmList.ItemsSource = films;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            InfoFilmWindow infoFilmWindow = new InfoFilmWindow();
            infoFilmWindow.Show();
            this.Hide();
        }
    }
}
