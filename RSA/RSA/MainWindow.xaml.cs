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

namespace RSA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rBtnEncrypt.IsChecked = true;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {*/
                RSA_algorithm rsa = new RSA_algorithm();
                if(rBtnEncrypt.IsChecked==true)
                {
                    rsa.encrypt(Convert.ToInt32(tBxInputP.Text), Convert.ToInt32(tBxInputQ.Text),tBxInputText.Text);
                }
                else
                {
                    
                }
            /*}
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }*/
        }

        private void rBtnEncrypt_Checked(object sender, RoutedEventArgs e)
        {
            updateButton();
            gridEncrypt.Visibility = Visibility.Visible;
            gridDecrypt.Visibility = Visibility.Collapsed;
        }

        private void rBtnDecrypt_Checked(object sender, RoutedEventArgs e)
        {
            updateButton();
            gridEncrypt.Visibility = Visibility.Collapsed;
            gridDecrypt.Visibility = Visibility.Visible;
        }

        private void tBxInputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateButton();
        }

        private void tBxInputP_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateButton();
        }

        private void tBxInputQ_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateButton();
        }
        
        private void updateButton()
        {
            if (tBxInputText.Text.Equals("") || tBxInputP.Text.Equals("") || tBxInputQ.Text.Equals("") || (rBtnEncrypt.IsChecked == false && rBtnDecrypt.IsChecked == false))
                btnCalculate.IsEnabled = false;
            else
                btnCalculate.IsEnabled = true;
        }
    }
}
