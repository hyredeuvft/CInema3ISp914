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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Cinema.ClassHelper;
using Cinema.DB;
using static Cinema.ClassHelper.EFClass;
using static Cinema.ClassHelper.NavigateClass;

namespace Cinema.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditSessionPage.xaml
    /// </summary>
    public partial class AddEditSessionPage : Page
    {
        private bool isEdit = false;
        private SessionFilm editSession;
        public AddEditSessionPage()
        {
            InitializeComponent();

            CmbHall.ItemsSource = Contextmy.MovieHall.ToList();
            CmbHall.DisplayMemberPath = "TypeHall";
            CmbHall.SelectedIndex = 0;

            CmbFilm.ItemsSource = Contextmy.Film.ToList();
            CmbFilm.DisplayMemberPath = "MovieTitle";
            CmbFilm.SelectedIndex = 0;
        }

        public AddEditSessionPage(SessionFilm sessionFilm)
        {
            try
            {
                InitializeComponent();
                CmbHall.ItemsSource = Contextmy.MovieHall.ToList();
                CmbHall.DisplayMemberPath = "TypeHall";
                CmbFilm.ItemsSource = Contextmy.Film.ToList();
                CmbFilm.DisplayMemberPath = "MovieTitle";

                TbDateStart.Text = Convert.ToString(sessionFilm.DateTimeStart);
                TbPrice.Text = Convert.ToString(sessionFilm.Price);
                CmbHall.SelectedItem = Contextmy.MovieHall.Where(i => i.IdMovieHall == sessionFilm.IdMovieHall).FirstOrDefault();
                CmbFilm.SelectedItem = Contextmy.Film.Where(i => i.IdFilm == sessionFilm.IdFilm).FirstOrDefault();

                isEdit = true;
                editSession = sessionFilm;
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
                    editSession.DateTimeStart = Convert.ToDateTime(TbDateStart.Text);
                    editSession.Price = Convert.ToDecimal(TbPrice.Text);
                    editSession.IdMovieHall = (CmbHall.SelectedItem as MovieHall).IdMovieHall;
                    editSession.IdFilm = (CmbFilm.SelectedItem as Film).IdFilm;

                    Contextmy.SaveChanges();
                }
                else
                {
                    SessionFilm sessionFilm = new SessionFilm();
                    sessionFilm.DateTimeStart = Convert.ToDateTime(TbDateStart.Text);
                    sessionFilm.Price = Convert.ToDecimal(TbPrice.Text);
                    sessionFilm.IdMovieHall = (CmbHall.SelectedItem as MovieHall).IdMovieHall;
                    sessionFilm.IdFilm = (CmbFilm.SelectedItem as Film).IdFilm;

                    Contextmy.SessionFilm.Add(sessionFilm);
                    Contextmy.SaveChanges();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
            frame.Navigate(new InfoSessionPage());
        }
    }
}
