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
    /// Логика взаимодействия для InfoProductPage.xaml
    /// </summary>
    public partial class InfoProductPage : Page
    {
        List<Product> products = new List<Product>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По названию",
            "По стоимости"
        };
        public InfoProductPage()
        {
            try
            {
                InitializeComponent();
                CmbSort.ItemsSource = sortList;
                CmbSort.SelectedIndex = 0;
                GetListProduct();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void GetListProduct()
        {
            try
            {
                products = Contextmy.Product.ToList();
                products = products.Where(i => i.ProductTitle.Contains(TxbSearch.Text)
                || i.Cost.ToString().Contains(TxbSearch.Text)).ToList();
                switch (CmbSort.SelectedIndex)
                {
                    case 0:
                        products = products.OrderBy(i => i.IdProduct).ToList();
                        break;
                    case 1:
                        products = products.OrderBy(i => i.ProductTitle).ToList();
                        break;
                    case 2:
                        products = products.OrderBy(i => i.Cost).ToList();
                        break;
                    default:
                        break;
                }
                Dg.ItemsSource = products;
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListProduct();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListProduct();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddEditProductPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is Product)
            {
                var product = Dg.SelectedItem as Product;
                frame.Navigate(new Pages.AddEditProductPage(product));
            }
            else
            {
                MessageBox.Show("Запись не выбрана", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Dg.SelectedItem is Product)
            {
                var item = Dg.SelectedItem as Product;
                GetListProduct();
                var dialogResult = MessageBox.Show("Вы действительно хотите удалить фильм?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    Contextmy.Product.Remove(item);
                    Contextmy.SaveChanges();
                    MessageBox.Show("Фильм успешно удален!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
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
