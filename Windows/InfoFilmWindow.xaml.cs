using Cinema.DB;
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
using Cinema.Windows;
using static Cinema.ClassHelper.EFClass;


namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для InfoFilmWindow.xaml
    /// </summary>
    public partial class InfoFilmWindow : Window
    {
        List<Film> films = new List<Film>();
        public InfoFilmWindow()
        {
            InitializeComponent();
        }

        public InfoFilmWindow(Film film)
        {
            InitializeComponent();
            GetInfo(film);
        }

        private void GetInfo(Film film)
        {
            TbTitle.Text = film.MovieTitle;
            TbRating.Text = Convert.ToString(film.Rating);
            TbRating20.Text = Convert.ToString(film.Rating);
            TbDuration.Text = Convert.ToString(film.DurationInMinute);
            TbTitle20.Text = film.MovieTitle;
            if (film.Description == "")
            {
                TbDescription.Text = "Описание отсутствует";
            }
            else
            {
                TbDescription.Text = film.Description;
            }
            TbDirector.Text = film.Director;
            TbPremierDate.Text = Convert.ToString(film.PremierDate);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();                
            this.Close();
        }
    }
}
