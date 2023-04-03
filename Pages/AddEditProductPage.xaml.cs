using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using static Cinema.ClassHelper.EFClass;
using static Cinema.ClassHelper.NavigateClass;

namespace Cinema.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        private bool isEdit = false;
        private Product editProduct;
        private string pathPhoto = null;
        public AddEditProductPage()
        {
            InitializeComponent();

            CmbCategory.ItemsSource = Contextmy.Category.ToList();
            CmbCategory.DisplayMemberPath = "CategoryTitle";
            CmbCategory.SelectedIndex = 0;
        }

        public AddEditProductPage(Product product)
        {
            try
            {
                InitializeComponent();

                CmbCategory.ItemsSource = Contextmy.Category.ToList();
                CmbCategory.DisplayMemberPath = "CategoryTitle";

                TbTitle.Text = product.ProductTitle;
                TbCost.Text = Convert.ToString(product.Cost);
                TbVolume.Text = Convert.ToString(product.Volume);
                TbCount.Text = Convert.ToString(product.Count);
                CmbCategory.SelectedItem = Contextmy.Category.Where(i => i.IdCategory == product.IdCategory).FirstOrDefault();
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
                        ImgProduct.Source = bitmapImage;

                    }
                }

                isEdit = true;
                editProduct = product;
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isEdit)
                {
                    editProduct.ProductTitle = TbTitle.Text;
                    editProduct.Cost = Convert.ToDecimal(TbCost.Text);
                    if (TbVolume.Text != "")
                    {
                        editProduct.Volume = Convert.ToDecimal(TbVolume.Text);
                    }

                    editProduct.Count = (byte?)Convert.ToInt16(TbCount.Text);
                    editProduct.IdCategory = (CmbCategory.SelectedItem as Category).IdCategory;

                    Contextmy.SaveChanges();
                }
                else
                {
                    Product product = new Product();
                    product.ProductTitle = TbTitle.Text;
                    product.Cost = Convert.ToDecimal(TbCost.Text);
                    if (TbVolume.Text != "")
                    {
                        product.Volume = Convert.ToDecimal(TbVolume.Text);
                    }
                    product.Count = (byte?)Convert.ToInt16(TbCount.Text);
                    product.IdCategory = (CmbCategory.SelectedItem as Category).IdCategory;
                    if (pathPhoto != null)
                    {
                        product.PhotoPath = File.ReadAllBytes(pathPhoto);
                    }

                    Contextmy.Product.Add(product);
                    Contextmy.SaveChanges();
                    MessageBox.Show("Товар успешно добавлен");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
            frame.Navigate(new InfoProductPage());
        }

        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    ImgProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    pathPhoto = openFileDialog.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте правильность выбранного фото", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
        }
    }
}
