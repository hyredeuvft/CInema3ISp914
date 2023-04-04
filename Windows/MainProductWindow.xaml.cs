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
    /// Логика взаимодействия для MainProductWindow.xaml
    /// </summary>
    public partial class MainProductWindow : Window
    {
        int id;
        List<Product> products = new List<Product>();
        List<string> sortList = new List<string>()
        {
            "По умолчанию",
            "По названию",
            "По цене"
        };
        public MainProductWindow()
        {
            try
            {
                InitializeComponent();

                CmbSort.ItemsSource = sortList;
                CmbSort.SelectedIndex = 0;

                GetListProduct();
                GetProduct();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public MainProductWindow(int a)
        {
            try
            {
                InitializeComponent();
                id = a;
                CmbSort.ItemsSource = sortList;
                CmbSort.SelectedIndex = 0;

                GetListProduct();
                GetProduct();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetListProduct()
        {
            try
            {
                products = Contextmy.Product.ToList();
                products = products.Where(x => x.ProductTitle.Contains(TxbSearch.Text)
                    || x.Cost.ToString().Contains(TxbSearch.Text)).ToList();
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
                LvFilmList.ItemsSource = products;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void GetProduct()
        {
            List<Product> products = new List<Product>();
            products = Contextmy.Product.ToList();
            LvFilmList.ItemsSource=products;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (LvFilmList.SelectedItem is Product)
            {
                Product product = LvFilmList.SelectedItem as Product;
                InfoProductWindow infoProductWindow = new InfoProductWindow(product);
                infoProductWindow.Show();
                this.Close();
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

        private void BtnFilm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(id);
            mainWindow.Show();
            this.Close();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            LKUserWindow lKUserWindow = new LKUserWindow(id);
            lKUserWindow.Show();
            this.Close();
        }
    }
}
