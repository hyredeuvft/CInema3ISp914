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
    /// Логика взаимодействия для InfoEmployeePage.xaml
    /// </summary>
    public partial class InfoEmployeePage : Page
    {
        List<Employee> employee = new List<Employee>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По возрасту"
        };
        public InfoEmployeePage()
        {
            try
            {
                InitializeComponent();
                CmbSort.ItemsSource = sortList;
                CmbSort.SelectedIndex = 0;
                GetListEmployee();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
        }
        private void GetListEmployee()
        {
            try
            {
                employee = Contextmy.Employee.ToList();
                employee = employee.Where(i => i.LastName.Contains(TxbSearch.Text)
                || i.FirstName.Contains(TxbSearch.Text)
                || Convert.ToString(i.Birthday).Contains(TxbSearch.Text)).ToList();
                switch (CmbSort.SelectedIndex)
                {
                    case 0:
                        employee = employee.OrderBy(i => i.IdEmployee).ToList();
                        break;
                    case 1:
                        employee = employee.OrderBy(i => i.LastName).ToList();
                        break;
                    case 2:
                        employee = employee.OrderBy(i => i.FirstName).ToList();
                        break;
                    case 3:
                        employee = employee.OrderBy(i => i.Birthday).ToList();
                        break;
                    default:
                        break;
                }
                Dg.ItemsSource = employee;

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListEmployee();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListEmployee();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditEmployeePage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is Employee)
            {
                var employee = Dg.SelectedItem as Employee;
                frame.Navigate(new Pages.AddEditEmployeePage(employee));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is Employee)
            {
                var item = Dg.SelectedItem as Employee;
                GetListEmployee();
                var dialogResult = MessageBox.Show("Вы действительно хотите удалить сотрудника?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    Contextmy.Employee.Remove(item);
                    Contextmy.SaveChanges();
                    MessageBox.Show("Сотрудник успешно удален!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    frame.Navigate(new InfoFilmPage());
                }
                else
                { }
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
