using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using static System.Net.WebRequestMethods;
using static Cinema.ClassHelper.EFClass;

namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для InfoProductWindow.xaml
    /// </summary>
    public partial class InfoProductWindow : Window
    {
        List<Product> products = new List<Product>();
        int id;
        public InfoProductWindow()
        {
            InitializeComponent();
        }

        public InfoProductWindow(Product product, int a) 
        {
            InitializeComponent();
            GetInfo(product);
            id = a;
        }

        private void GetInfo(Product product) 
        {
            try
            {
                TbTitle.Text = product.ProductTitle;
                TbTitle2.Text = product.ProductTitle;
                TbCost.Text = Convert.ToString(product.Cost);
                TbCost2.Text = Convert.ToString(product.Cost);
                TbVolume.Text = Convert.ToString(product.Volume);
                TbVolume2.Text = Convert.ToString(product.Volume);
                TbCount.Text = Convert.ToString(product.Count);
                var Id = Contextmy.Category.ToList().Where(i => i.IdCategory == product.IdCategory).FirstOrDefault();
                TbCategory.Text = Id.CategoryTitle;
                if (product.PhotoPath != null)
                {
                    using (MemoryStream stream = new MemoryStream(product.PhotoPath))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                        ProductPhoto.Source = bitmapImage;
                    }
                }
                else
                {
                    ProductPhoto.Source = new BitmapImage(new Uri(@"/Res/noImage.png", UriKind.Relative));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось вывести информацию о продукте", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainProductWindow mainWindow = new MainProductWindow(id);
            mainWindow.Show();
            this.Close();
        }
    }
}
