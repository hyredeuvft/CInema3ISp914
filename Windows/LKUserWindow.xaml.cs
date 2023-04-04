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
    /// Логика взаимодействия для LKUserWindow.xaml
    /// </summary>
    public partial class LKUserWindow : Window
    {
        int id;
        private User editUser;
        public LKUserWindow(int a)
        {
            try
            {
                InitializeComponent();
                id = a;
                var Auth = Contextmy.User.ToList().Where(i => i.IdUser == a).FirstOrDefault();
                TbLastName.Text = Auth.LastName;
                TbFirstName.Text = Auth.FirstName;
                TbPatronymic.Text = Auth.Patronymic;
                DpBirthDay.Text = Convert.ToString(Auth.Birthday);
                TbEmail.Text = Auth.Email;
                TbPhoneNumber.Text = Auth.PhoneNumber;
                TbPassword.Text = Auth.Password;
                editUser = Auth;
            }
                catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var DialogResult = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (DialogResult == MessageBoxResult.Yes)
            {
                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show();
                this.Close();
            }
            else{}
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(id);
            mainWindow.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                editUser.LastName = TbLastName.Text;
                editUser.FirstName = TbFirstName.Text;
                editUser.Patronymic = TbPatronymic.Text;
                editUser.Birthday = DpBirthDay.SelectedDate.Value;
                editUser.Email = TbEmail.Text;
                editUser.PhoneNumber = TbPhoneNumber.Text;
                editUser.Password = TbPassword.Text;

                Contextmy.SaveChanges();
                MessageBox.Show("Данные изменены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
