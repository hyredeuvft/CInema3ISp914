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
using Microsoft.Win32;
using static Cinema.ClassHelper.EFClass;

namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для InfoProductWindow.xaml
    /// </summary>
    public partial class InfoProductWindow : Window
    {
        List<Product> products = new List<Product>();
        private string pathPhoto = null;
        public InfoProductWindow()
        {
            InitializeComponent();
        }

        public InfoProductWindow(Product product) 
        {
            InitializeComponent();
            GetInfo(product);
        }

        private void GetInfo(Product product) 
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
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                ProductPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathPhoto = openFileDialog.FileName;
            }
        }
    }
}
