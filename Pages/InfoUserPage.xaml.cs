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
    /// Логика взаимодействия для InfoUserPage.xaml
    /// </summary>
    public partial class InfoUserPage : Page
    {

        List<User> users = new List<User>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По дате рождения"
        };
        public InfoUserPage()
        {
            InitializeComponent();
            CmbSort.ItemsSource = sortList;
            CmbSort.SelectedIndex = 0;
            GetListUser();
        }

        private void GetListUser()
        {

            users = Contextmy.User.ToList();
            users = users.Where(i => i.LastName.Contains(TxbSearch.Text)
            || i.FirstName.Contains(TxbSearch.Text)
            || Convert.ToString(i.Birthday).Contains(TxbSearch.Text)).ToList();
            switch (CmbSort.SelectedIndex)
            {
                case 0:
                    users = users.OrderBy(i => i.IdUser).ToList();
                    break;
                case 1:
                    users = users.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    users = users.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    users = users.OrderBy(i => i.Birthday).ToList();
                    break;
                default:
                    break;
            }
            Dg.ItemsSource = users;
        }
        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListUser();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListUser();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditUserPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is User)
            {
                var user = Dg.SelectedItem as User;
                frame.Navigate(new Pages.AddEditUserPage(user));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
