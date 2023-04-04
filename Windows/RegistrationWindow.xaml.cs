using Cinema.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static Cinema.ClassHelper.EFClass;

namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TbLastName.Text) ||
                string.IsNullOrWhiteSpace(TbFirstName.Text) ||
                string.IsNullOrWhiteSpace(DpBirthDay.Text) ||
                string.IsNullOrWhiteSpace(TbPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(TbEmail.Text) ||
                string.IsNullOrWhiteSpace(PbPassword.Password))
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка");
                    return;
                }

                var authUser = Contextmy.User.ToList()
                    .Where(x => x.Email == TbEmail.Text).FirstOrDefault();
                if (authUser != null)
                {
                    MessageBox.Show("Такой логин уже занят", "Ошибка");
                }
                else
                {
                    User user = new User();
                    user.LastName = TbLastName.Text;
                    user.FirstName = TbFirstName.Text;
                    user.Birthday = DpBirthDay.SelectedDate.Value;
                    user.PhoneNumber = TbPhoneNumber.Text;
                    user.Email = TbEmail.Text;
                    user.Password = PbPassword.Password;
                    user.PersonalBonus = 0;
                    user.IdTag = 4;

                    Contextmy.User.Add(user);
                    Contextmy.SaveChanges();
                    MainWindow mainWindow = new MainWindow(user.IdUser);
                    mainWindow.Show();
                    this.Close();
                }

                
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void TbSignIn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }
    }
}
