﻿using System;
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
    /// Логика взаимодействия для InfoSessionPage.xaml
    /// </summary>
    public partial class InfoSessionPage : Page
    {

        List<SessionFilm> sessionFilms = new List<SessionFilm>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По началу сеанса",
            "По цене"
        };
        public InfoSessionPage()
        {
            InitializeComponent();
            CmbSort.ItemsSource = sortList;
            CmbSort.SelectedIndex = 0;
            GetListSession();
        }

        private void GetListSession()
        {

            sessionFilms = Contextmy.SessionFilm.ToList();
            sessionFilms = (List<SessionFilm>)sessionFilms.Where(i => Convert.ToString(i.DateTimeStart).Contains(TxbSearch.Text)
            || Convert.ToString(i.Price).Contains(TxbSearch.Text));
            switch (CmbSort.SelectedIndex)
            {
                case 0:
                    sessionFilms = sessionFilms.OrderBy(i => i.IdSessionFilm).ToList();
                    break;
                case 1:
                    sessionFilms = sessionFilms.OrderBy(i => i.DateTimeStart).ToList();
                    break;
                case 2:
                    sessionFilms = sessionFilms.OrderBy(i => i.Price).ToList();
                    break;
                default:
                    break;
            }
            Dg.ItemsSource = sessionFilms;
        }
        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListSession();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListSession();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditSessionPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is SessionFilm)
            {
                var session = Dg.SelectedItem as SessionFilm;
                frame.Navigate(new Pages.AddEditSessionPage(session));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
