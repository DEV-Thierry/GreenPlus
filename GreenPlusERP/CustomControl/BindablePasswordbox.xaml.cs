using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace GreenPlusERP.CustomControl
{
    /// <summary>
    /// Interação lógica para BindablePasswordbox.xam
    /// </summary>
    public partial class BindablePasswordbox : UserControl
    {
        public static readonly DependencyProperty passwordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(BindablePasswordbox));

            public SecureString Password
        {
            get { return (SecureString)GetValue(passwordProperty); }
            set { SetValue(passwordProperty, value); }
        }
        public BindablePasswordbox()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += onPasswordChanged;
        }

        private void onPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = txtPassword.SecurePassword;
        }
    }
}
