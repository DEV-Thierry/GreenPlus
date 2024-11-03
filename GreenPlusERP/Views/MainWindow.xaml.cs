using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GreenPlusERP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sidebar_MouseEnter(object sender, MouseEventArgs e)
        {
            var animacaoLargura = new DoubleAnimation
            {
                From = 60,
                To = 200,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            sidebar.BeginAnimation(WidthProperty, animacaoLargura);

            txtHome.Visibility = Visibility.Visible;
            txtCadastro.Visibility = Visibility.Visible;
            txtProducao.Visibility = Visibility.Visible;
            txtEmpresa.Visibility = Visibility.Visible;
        }

        private void sidebar_MouseLeave(object sender, MouseEventArgs e)
        {
            var animacaoLargura = new DoubleAnimation
            {
                From = 200,
                To = 60,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            sidebar.BeginAnimation(WidthProperty, animacaoLargura);

            CadastroDrop.Visibility = Visibility.Collapsed;
            producaoDrop.Visibility = Visibility.Collapsed;

            txtHome.Visibility = Visibility.Collapsed;
            txtCadastro.Visibility = Visibility.Collapsed;
            txtProducao.Visibility= Visibility.Collapsed;
            txtEmpresa.Visibility = Visibility.Collapsed;
        }

        private void Cadastro_Click(object sender, RoutedEventArgs e)
        {
            if (CadastroDrop.Visibility == Visibility.Collapsed)
            {
                CadastroDrop.Visibility = Visibility.Visible;
                producaoDrop.Visibility = Visibility.Collapsed;
            }
            else
                CadastroDrop.Visibility = Visibility.Collapsed;
        }

        private void producao_Click(object sender, RoutedEventArgs e)
        {
            if (producaoDrop.Visibility == Visibility.Collapsed)
            {
                producaoDrop.Visibility = Visibility.Visible;
                CadastroDrop.Visibility = Visibility.Collapsed;
            }
            else
                producaoDrop.Visibility = Visibility.Collapsed;
        }
    }
}