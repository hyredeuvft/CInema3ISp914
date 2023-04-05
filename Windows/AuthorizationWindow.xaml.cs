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
using static Cinema.ClassHelper.EFClass;

namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        int id;
        public AuthorizationWindow()
        {
            try
            {
                InitializeComponent();
                string forCaptcha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                char[] stringChars = new char[8];
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = forCaptcha[rnd.Next(forCaptcha.Length)];
                }
                string finalString = new string(stringChars);
                Captcha.Text = finalString;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка генерации капчи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        Random rnd = new Random();
        

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Captcha.Text != TbCaptcha.Text)
                {
                    MessageBox.Show("Неверно введена капча", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if(TbLogin.Text.Substring(0, 3) == "EMP")
                    {
                        var authEmp = Contextmy.Employee.ToList()
                        .Where(i => i.Login == TbLogin.Text && i.Password == PbPassword.Password).FirstOrDefault();
                        if (authEmp != null)
                        {
                            NavigateWindow mainWindow = new NavigateWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Такой пользователь не найден", "Ошибка");
                        }
                    }
                    else
                    {
                        var authUser = Contextmy.User.ToList()
                        .Where(i => i.Email == TbLogin.Text && i.Password == PbPassword.Password).FirstOrDefault();
                        if (authUser != null)
                        {
                            MainWindow mainWindow = new MainWindow(authUser.IdUser);
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Такой пользователь не найден", "Ошибка");
                        }
                    }
                        
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show("Произошла ошибка \n " + ec, "Ошибка");
            }
        }

        private void TbReg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
