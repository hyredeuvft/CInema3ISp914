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
using System.Windows.Shapes;

namespace Cinema.Windows
{
    /// <summary>
    /// Логика взаимодействия для BuyWindow.xaml
    /// </summary>
    public partial class BuyWindow : Window
    {
        public BuyWindow()
        {
            InitializeComponent();
        }

        public BuyWindow(Film film)
        {
            InitializeComponent();
        }

        private void Buy(Film film)
        {
            Ticket ticket = new Ticket();
            SessionFilm sessionFilm = new SessionFilm();
            if (ticket.IdSessionFilm == sessionFilm.IdSessionFilm)
            {
                var Id = sessionFilm.IdFilm;
                if (Id == film.IdFilm)
                {
                    MessageBox.Show("qwwwww");
                }
            }
        }
    }
}
