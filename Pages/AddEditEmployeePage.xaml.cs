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
    /// Логика взаимодействия для AddEditEmployeePage.xaml
    /// </summary>
    public partial class AddEditEmployeePage : Page
    {
        private bool isEdit = false;
        private Employee editEmployee;

        public AddEditEmployeePage()
        {
            InitializeComponent();

            CmbPosition.ItemsSource = Contextmy.EmployeePosition.ToList();
            CmbPosition.DisplayMemberPath = "PositionTitle";
            CmbPosition.SelectedIndex = 0;

            CmbGender.ItemsSource = Contextmy.Gender.ToList();
            CmbGender.DisplayMemberPath = "GenderTitle";
            CmbGender.SelectedIndex = 0;
        }

        public AddEditEmployeePage(Employee employee)
        {
            try
            {
                InitializeComponent();

                CmbPosition.ItemsSource = Contextmy.EmployeePosition.ToList();
                CmbPosition.DisplayMemberPath = "PositionTitle";
                CmbGender.ItemsSource = Contextmy.Gender.ToList();
                CmbGender.DisplayMemberPath = "GenderTitle";

                TbLName.Text = employee.LastName;
                TbFName.Text = employee.FirstName;
                TbP.Text = employee.Patronymic;
                TbDate.Text = Convert.ToString(employee.Birthday);
                CmbGender.SelectedItem = Contextmy.Gender.Where(i => i.IdGender == employee.IdGender).FirstOrDefault();
                CmbPosition.SelectedItem = Contextmy.EmployeePosition.Where(i => i.IdEmployeePosition == employee.IdEmployeePosition).FirstOrDefault();
                TbLogin.Text = employee.Login;
                TbPassword.Text = Convert.ToString(employee.Password);
                TbSeries.Text = employee.PassportSeries;
                TbNumber.Text = employee.PassportNumber;

                isEdit = true;
                editEmployee = employee;
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
                    editEmployee.LastName = TbLName.Text;
                    editEmployee.FirstName = TbFName.Text;
                    editEmployee.Patronymic = TbP.Text;
                    editEmployee.Birthday = TbDate.SelectedDate.Value;
                    editEmployee.IdGender = (CmbGender.SelectedItem as Gender).IdGender;
                    editEmployee.IdEmployeePosition = (CmbPosition.SelectedItem as EmployeePosition).IdEmployeePosition;
                    editEmployee.Login = TbLogin.Text;
                    editEmployee.Password = TbPassword.Text;
                    editEmployee.PassportSeries = TbSeries.Text;
                    editEmployee.PassportNumber = TbNumber.Text;

                    Contextmy.SaveChanges();
                }
                else
                {
                    Employee employee = new Employee();
                    employee.LastName = TbLName.Text;
                    employee.FirstName = TbFName.Text;
                    employee.Patronymic = TbP.Text;
                    employee.Birthday = TbDate.SelectedDate.Value;
                    employee.IdGender = (CmbGender.SelectedItem as Gender).IdGender;
                    employee.IdEmployeePosition = (CmbPosition.SelectedItem as EmployeePosition).IdEmployeePosition;
                    employee.Login = TbLogin.Text;
                    employee.Password = TbPassword.Text;
                    employee.PassportSeries = TbSeries.Text;
                    employee.PassportNumber = TbNumber.Text;

                    Contextmy.Employee.Add(employee);
                    Contextmy.SaveChanges();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            frame.Navigate(new InfoEmployeePage());
        }
    }
}
