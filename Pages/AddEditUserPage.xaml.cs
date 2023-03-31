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
    /// Логика взаимодействия для AddEditUserPage.xaml
    /// </summary>
    public partial class AddEditUserPage : Page
    {

        private bool isEdit = false;
        private User editUser;
        public AddEditUserPage()
        {
            InitializeComponent();

            CmbTag.ItemsSource = Contextmy.Tag.ToList();
            CmbTag.DisplayMemberPath = "TagTitle";
            CmbTag.SelectedIndex = 0;
        }

        public AddEditUserPage(User user)
        {
            InitializeComponent();

            CmbTag.ItemsSource = Contextmy.Tag.ToList();
            CmbTag.DisplayMemberPath = "TagTitle";

            TbLName.Text = user.LastName;
            TbFName.Text = user.FirstName;
            TbP.Text = user.Patronymic;
            TbDate.Text = Convert.ToString(user.Birthday);
            TbEmail.Text = user.Email;
            TbPhNumber.Text = user.PhoneNumber;
            TbBonus.Text = Convert.ToString(user.PersonalBonus);
            CmbTag.SelectedItem = Contextmy.Tag.Where(i => i.IdTag == user.IdTag).FirstOrDefault();
            TbPassword.Text = Convert.ToString(user.Password);

            isEdit = true;
            editUser = user;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                editUser.LastName = TbLName.Text;
                editUser.FirstName = TbFName.Text;
                editUser.Patronymic = TbP.Text;
                editUser.Birthday = TbDate.SelectedDate.Value;
                editUser.Email = TbEmail.Text;
                editUser.PhoneNumber = TbPhNumber.Text;
                editUser.PersonalBonus = Convert.ToInt32(TbBonus.Text);
                editUser.IdTag = (CmbTag.SelectedItem as Tag).IdTag;
                editUser.Password = TbPassword.Text;

                Contextmy.SaveChanges();
            }
            else
            {
                User user = new User();
                user.LastName = TbLName.Text;
                user.FirstName = TbFName.Text;
                user.Patronymic = TbP.Text;
                user.Birthday = TbDate.SelectedDate.Value;
                user.Email = TbEmail.Text;
                user.PhoneNumber = TbPhNumber.Text;
                user.PersonalBonus = Convert.ToInt32(TbBonus.Text);
                user.IdTag = (CmbTag.SelectedItem as Tag).IdTag;
                user.Password = TbPassword.Text;

                Contextmy.User.Add(user);
                Contextmy.SaveChanges();
            }
            frame.Navigate(new InfoUserPage());
        }
    }
}
