using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
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
        private string pathPhoto = null;

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
            try
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
                if (film.PhotoPath != null)
                {
                    using (MemoryStream stream = new MemoryStream(film.PhotoPath))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                        ImgFilm.Source = bitmapImage;

                    }
                }

                isEdit = true;
                editFilm = film;
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
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
                    if (pathPhoto != null)
                    {
                        editFilm.PhotoPath = File.ReadAllBytes(pathPhoto);
                    }
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
                    if (pathPhoto != null)
                    {
                        film.PhotoPath = File.ReadAllBytes(pathPhoto);
                    }

                    Contextmy.Film.Add(film);
                    Contextmy.SaveChanges();
                    MessageBox.Show("Фильм успешно добавлен");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

            frame.Navigate(new InfoFilmPage());
        }

        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    ImgFilm.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    pathPhoto = openFileDialog.FileName;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность выбранного фото", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
