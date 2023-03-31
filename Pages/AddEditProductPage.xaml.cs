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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        private bool isEdit = false;
        private Product editProduct;
        public AddEditProductPage()
        {
            InitializeComponent();

            CmbCategory.ItemsSource = Contextmy.Category.ToList();
            CmbCategory.DisplayMemberPath = "CategoryTitle";
            CmbCategory.SelectedIndex = 0;
        }

        public AddEditProductPage(Product product)
        {
            InitializeComponent();

            CmbCategory.ItemsSource = Contextmy.Category.ToList();
            CmbCategory.DisplayMemberPath = "CategoryTitle";
            
            TbTitle.Text = product.ProductTitle;
            TbCost.Text = Convert.ToString(product.Cost);
            TbVolume.Text = Convert.ToString(product.Volume);
            TbCount.Text = Convert.ToString(product.Count);
            CmbCategory.SelectedItem = Contextmy.Category.Where(i => i.IdCategory == product.IdCategory).FirstOrDefault();

            isEdit = true;
            editProduct = product;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                editProduct.ProductTitle = TbTitle.Text;
                editProduct.Cost = Convert.ToDecimal(TbCost.Text);
                editProduct.Volume = Convert.ToDecimal(TbVolume.Text);
                editProduct.Count = (byte?)Convert.ToInt16(TbCount.Text);
                editProduct.IdCategory = (CmbCategory.SelectedItem as Category).IdCategory;

                Contextmy.SaveChanges();
            }
            else
            {
                Product product = new Product();
                product.ProductTitle = TbTitle.Text;
                product.Cost = Convert.ToDecimal(TbCost.Text);
                product.Volume = Convert.ToDecimal(TbVolume.Text);
                product.Count = (byte?)Convert.ToInt16(TbCount.Text);
                product.IdCategory = (CmbCategory.SelectedItem as Category).IdCategory;

                Contextmy.Product.Add(product);
                Contextmy.SaveChanges();
            }
            frame.Navigate(new InfoProductPage());
        }
    }
}
