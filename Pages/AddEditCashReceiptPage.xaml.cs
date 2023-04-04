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
    /// Логика взаимодействия для AddEditCashReceiptPage.xaml
    /// </summary>
    public partial class AddEditCashReceiptPage : Page
    {
        private bool isEdit = false;
        private CashReceipt editCashReceipt;

        public AddEditCashReceiptPage()
        {
            InitializeComponent();

            CmbUser.ItemsSource = Contextmy.User.ToList();
            CmbUser.DisplayMemberPath = "LastName";
            CmbUser.SelectedIndex = 0;

            CmbEmployee.ItemsSource = Contextmy.Employee.ToList();
            CmbEmployee.DisplayMemberPath = "LastName";
            CmbEmployee.SelectedIndex = 0;
        }

        public AddEditCashReceiptPage(CashReceipt cashReceipt)
        {
            try
            {
                InitializeComponent();
                CmbEmployee.ItemsSource = Contextmy.Employee.ToList();
                CmbEmployee.DisplayMemberPath = "LastName";
                CmbUser.ItemsSource = Contextmy.User.ToList();
                CmbUser.DisplayMemberPath = "LastName";

                CmbEmployee.SelectedItem = Contextmy.Employee.Where(i => i.IdEmployee == cashReceipt.IdEmployee).FirstOrDefault();
                CmbUser.SelectedItem = Contextmy.User.Where(i => i.IdUser == cashReceipt.IdUser).FirstOrDefault();
                TbDate.Text = Convert.ToString(cashReceipt.DateSale);
                TbCost.Text = Convert.ToString(cashReceipt.FullCost);

                isEdit = true;
                editCashReceipt = cashReceipt;
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                try
                {
                    editCashReceipt.IdUser = (CmbUser.SelectedItem as User).IdUser;
                    editCashReceipt.IdEmployee = (CmbEmployee.SelectedItem as Employee).IdEmployee;
                    editCashReceipt.DateSale = TbDate.SelectedDate.Value;
                    editCashReceipt.FullCost = Convert.ToDecimal(TbCost.Text);

                    Contextmy.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                try
                {
                    CashReceipt cashReceipt = new CashReceipt();
                    cashReceipt.IdUser = (CmbUser.SelectedItem as User).IdUser;
                    cashReceipt.IdEmployee = (CmbEmployee.SelectedItem as Employee).IdEmployee;
                    cashReceipt.DateSale = TbDate.SelectedDate.Value;
                    cashReceipt.FullCost = Convert.ToDecimal(TbCost.Text);

                    Contextmy.CashReceipt.Add(cashReceipt);
                    Contextmy.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }

            frame.Navigate(new InfoCashReceiptPage());
        }
    }
}
