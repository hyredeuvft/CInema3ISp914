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
    /// Логика взаимодействия для AddEditFilmPage.xaml
    /// </summary>
    public partial class AddEditFilmPage : Page
    {

        private bool isEdit = false;
        private Film editFilm;

        public AddEditFilmPage()
        {
            InitializeComponent();

            CmbAgeRestriction.ItemsSource = Contextmy.AgeRestriction.ToList();
            CmbAgeRestriction.DisplayMemberPath = "Age";
            CmbAgeRestriction.SelectedIndex = 0;

            CmbMovieStudio.ItemsSource = Contextmy.MovieStudio.ToList();
            CmbMovieStudio.DisplayMemberPath = "StudioTitle";
            CmbMovieStudio.SelectedIndex = 0;

            CmbMovieDistributor.ItemsSource = Contextmy.MovieDistributor.ToList();
            CmbMovieDistributor.DisplayMemberPath = "INN";
            CmbMovieDistributor.SelectedIndex = 0;
        }

        public AddEditFilmPage(Film film)
        {
            InitializeComponent();
            CmbAgeRestriction.ItemsSource = Contextmy.AgeRestriction.ToList();
            CmbAgeRestriction.DisplayMemberPath = "Age";
            CmbMovieStudio.ItemsSource = Contextmy.MovieStudio.ToList();
            CmbMovieStudio.DisplayMemberPath = "StudioTitle";
            CmbMovieDistributor.ItemsSource = Contextmy.MovieDistributor.ToList();
            CmbMovieDistributor.DisplayMemberPath = "INN";

            TbMovieTitle.Text = film.MovieTitle;
            TbDescription.Text = film.Description;
            TbDirector.Text = film.Director;
            TbDuration.Text = Convert.ToString(film.DurationInMinute);
            TbPremierDate.Text = Convert.ToString(film.PremierDate);
            CmbAgeRestriction.SelectedItem = Contextmy.AgeRestriction.Where(i => i.IdAgeRestriction == film.IdAgeRestriction).FirstOrDefault();
            TbMovieRating.Text = Convert.ToString(film.Rating);
            CmbMovieStudio.SelectedItem = Contextmy.MovieStudio.Where(i => i.IdMovieStudio == film.IdMovieStudio).FirstOrDefault();
            TbMovieBudget.Text = Convert.ToString(film.MovieBudget);
            CmbMovieDistributor.SelectedItem = Contextmy.MovieDistributor.Where(i => i.IdMovieDistributor == film.IdMovieDistributor).FirstOrDefault();

            isEdit = true;
            editFilm = film;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                editFilm.MovieTitle = TbMovieTitle.Text;
                editFilm.Description = TbDescription.Text;
                editFilm.Director = TbDirector.Text;
                editFilm.DurationInMinute = Convert.ToInt16(TbDuration.Text);
                editFilm.PremierDate = TbPremierDate.SelectedDate.Value;
                editFilm.IdAgeRestriction = (CmbAgeRestriction.SelectedItem as AgeRestriction).IdAgeRestriction;
                editFilm.Rating = Convert.ToDecimal(TbMovieRating.Text);
                editFilm.IdMovieStudio = (CmbMovieStudio.SelectedItem as MovieStudio).IdMovieStudio;
                editFilm.MovieBudget = Convert.ToDecimal(TbMovieBudget.Text);
                editFilm.IdMovieDistributor = (CmbMovieDistributor.SelectedItem as MovieDistributor).IdMovieDistributor;

                Contextmy.SaveChanges();
            }
            else
            {
                Film film = new Film();
                film.MovieTitle = TbMovieTitle.Text;
                film.Description = TbDescription.Text;
                film.Director = TbDirector.Text;
                film.DurationInMinute = Convert.ToInt16(TbDuration.Text);
                film.PremierDate = TbPremierDate.SelectedDate.Value;
                film.IdAgeRestriction = (CmbAgeRestriction.SelectedItem as AgeRestriction).IdAgeRestriction;
                film.Rating = Convert.ToDecimal(TbMovieRating.Text);
                film.IdMovieStudio = (CmbMovieStudio.SelectedItem as MovieStudio).IdMovieStudio;
                film.MovieBudget = Convert.ToDecimal(TbMovieBudget.Text);
                film.IdMovieDistributor = (CmbMovieDistributor.SelectedItem as MovieDistributor).IdMovieDistributor;

                Contextmy.Film.Add(film);
                Contextmy.SaveChanges();
            }

            frame.Navigate(new InfoFilmPage());
        }
    }
}
