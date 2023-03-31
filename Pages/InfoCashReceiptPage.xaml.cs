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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Cinema.ClassHelper;
using Cinema.DB;
using static Cinema.ClassHelper.EFClass;
using static Cinema.ClassHelper.NavigateClass;

namespace Cinema.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoCashReceiptPage.xaml
    /// </summary>
    public partial class InfoCashReceiptPage : Page
    {

        List<CashReceipt> cashReceipt = new List<CashReceipt>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По дате продажи",
            "По стоимости"
        };
        public InfoCashReceiptPage()
        {
            InitializeComponent();
            CmbSort.ItemsSource = sortList;
            CmbSort.SelectedIndex = 0;
            GetListCashReceipt();
        }

        private void GetListCashReceipt()
        {

            cashReceipt = Contextmy.CashReceipt.ToList();
            switch (CmbSort.SelectedIndex)
            {
                case 0:
                    cashReceipt = cashReceipt.OrderBy(i => i.IdCashReceipt).ToList();
                    break;
                case 1:
                    cashReceipt = cashReceipt.OrderBy(i => i.DateSale).ToList();
                    break;
                case 2:
                    cashReceipt = cashReceipt.OrderBy(i => i.FullCost).ToList();
                    break;
                default:
                    break;
            }
            Dg.ItemsSource = cashReceipt;
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListCashReceipt();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListCashReceipt();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditCashReceiptPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is CashReceipt)
            {
                var cash = Dg.SelectedItem as CashReceipt;
                frame.Navigate(new Pages.AddEditCashReceiptPage(cash));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
