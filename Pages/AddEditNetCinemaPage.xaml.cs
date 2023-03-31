using Cinema.DB;
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
    /// Логика взаимодействия для AddEditNetCinemaPage.xaml
    /// </summary>
    public partial class AddEditNetCinemaPage : Page
    {
        private bool isEdit = false;
        private NetCinema editCinema;
        public AddEditNetCinemaPage()
        {
            InitializeComponent();

            CmbEmployee.ItemsSource = Contextmy.Employee.ToList();
            CmbEmployee.DisplayMemberPath = "LastName";
            CmbEmployee.SelectedIndex = 0;
        }

        public AddEditNetCinemaPage(NetCinema netCinema)
        {
            CmbEmployee.ItemsSource = Contextmy.Employee.ToList();
            CmbEmployee.DisplayMemberPath = "LastName";

            TbTitle.Text = netCinema.CinemaTitle;
            TbAddress.Text = netCinema.Address;
            TbPhone.Text = netCinema.PhoneNumber;
            TbEmail.Text = netCinema.CinemaEmail;
            CmbEmployee.SelectedItem = Contextmy.Employee.Where(i => i.IdEmployee == netCinema.IdEmployee).FirstOrDefault();

            isEdit = true;
            editCinema = netCinema;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                editCinema.CinemaTitle = TbTitle.Text;
                editCinema.Address = TbAddress.Text;
                editCinema.PhoneNumber = TbPhone.Text;
                editCinema.CinemaEmail = TbEmail.Text;
                editCinema.IdEmployee = (CmbEmployee.SelectedItem as Employee).IdEmployee;

                Contextmy.SaveChanges();
            }
            else
            {
                NetCinema netCinema = new NetCinema();
                netCinema.CinemaTitle = TbTitle.Text;
                netCinema.Address = TbAddress.Text;
                netCinema.PhoneNumber = TbPhone.Text;
                netCinema.CinemaEmail = TbEmail.Text;
                netCinema.IdEmployee = (CmbEmployee.SelectedItem as Employee).IdEmployee;

                Contextmy.NetCinema.Add(netCinema);
                Contextmy.SaveChanges();
            }
            frame.Navigate(new InfoCinemaPage());
        }
    }
}
