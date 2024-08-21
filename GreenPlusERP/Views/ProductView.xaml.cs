using Microsoft.Win32;
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

namespace GreenPlusERP.Views
{
    /// <summary>
    /// Interação lógica para ProductView.xam
    /// </summary>
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }

        private void RadioVenda_Checked(object sender, RoutedEventArgs e)
        {
            DetalhesVenda.Visibility = Visibility.Visible;
            painelVenda.Visibility = Visibility.Visible;
            PainelRequisitos.Visibility = Visibility.Visible;

            painelConserva.Visibility = Visibility.Collapsed;
            DetalhesInterno.Visibility = Visibility.Collapsed;
        }

        private void RadioInterno_Checked(object sender, RoutedEventArgs e)
        {
            DetalhesVenda.Visibility = Visibility.Collapsed;
            painelVenda.Visibility = Visibility.Collapsed;
            PainelRequisitos.Visibility = Visibility.Collapsed;

            painelConserva.Visibility = Visibility.Visible;
            DetalhesInterno.Visibility = Visibility.Visible;
        }

        private void ImgProduto_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif|Todos os Arquivos|*.*";

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(ofd.FileName);
                    image.EndInit();

                    Image image1 = new Image();
                    image1.Source = image;
                    image1.Stretch = System.Windows.Media.Stretch.Fill;
                    ImgProduto.Children.Clear();
                    ImgProduto.Children.Add(image1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
                }
            }
        }

        private void txtValor_GotFocus(object sender, RoutedEventArgs e)
        {
            txtValor.Text = string.Empty;
        }
    }
}
