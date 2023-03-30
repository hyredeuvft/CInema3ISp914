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
using Cinema.Pages;
using static Cinema.ClassHelper.EFClass;
using static Cinema.ClassHelper.NavigateClass;

namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для NavigateWindow.xaml
    /// </summary>
    public partial class NavigateWindow : Window
    {
        public NavigateWindow()
        {
            InitializeComponent();
            NavigateClass.frame = frameMenu;
        }
        
        private void btnForFilm_Click(object sender, RoutedEventArgs e)
        {
            InfoFilmPage infoFilmPage = new InfoFilmPage();
            frame.Navigate(infoFilmPage);
            this.Close();
        }

        private void btnForCashReceipt_Click(object sender, RoutedEventArgs e)
        {
            InfoCashReceiptPage infoCashReceiptPage = new InfoCashReceiptPage();
            frame.Navigate(infoCashReceiptPage);
            this.Close();
        }
    }
}