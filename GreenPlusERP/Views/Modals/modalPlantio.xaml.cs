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

namespace GreenPlusERP.Views.Modals
{
    /// <summary>
    /// Lógica interna para modalPlantio.xaml
    /// </summary>
    public partial class modalPlantio : Window
    {
        public modalPlantio()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
