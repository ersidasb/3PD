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
        delegate void UpdateDelegate();
        UpdateDelegate update;
        delegate void ModelDelegate(EncryptedModel model);
        ModelDelegate _setModel;
        private EncryptedModel selectedModel = null;
        public MainWindow()
        {
            InitializeComponent();
            rBtnEncrypt.IsChecked = true;
            update = updateList;
            _setModel = setModel;
        }

        public void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RSA_algorithm rsa = new RSA_algorithm();
                if(rBtnEncrypt.IsChecked==true)
                {
                    if (Convert.ToInt32(tBxInputP.Text) * Convert.ToInt32(tBxInputQ.Text) < 127)
                        throw new Exception("p*q must be larger than 126");
                    if (!isPrime(Convert.ToInt32(tBxInputP.Text)))
                        throw new Exception("p is not prime");
                    if (!isPrime(Convert.ToInt32(tBxInputQ.Text)))
                        throw new Exception("q is not prime");
                    tBxOutput.Text = rsa.encrypt(Convert.ToInt32(tBxInputP.Text), Convert.ToInt32(tBxInputQ.Text),tBxInputText.Text);
                }
                else
                {
                    tBxOutput.Text = rsa.decrypt(selectedModel);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void rBtnEncrypt_Checked(object sender, RoutedEventArgs e)
        {
            updateButton();
            tBxInputP.Text = "";
            tBxInputQ.Text = "";
            tBxInputText.Text = "";
            selectedModel = null;
            gridEncrypt.Visibility = Visibility.Visible;
            gridDecrypt.Visibility = Visibility.Collapsed;
        }

        private void rBtnDecrypt_Checked(object sender, RoutedEventArgs e)
        {
            updateButton();
            updateList();
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
            if (rBtnEncrypt.IsChecked == true)
            {
                if(tBxInputP.Text.Equals("") || tBxInputQ.Text.Equals("") || tBxInputText.Text.Equals(""))
                    btnCalculate.IsEnabled = false;
                else
                    btnCalculate.IsEnabled = true;
            }
            else if (rBtnDecrypt.IsChecked == true)
            {
                if(selectedModel == null)
                    btnCalculate.IsEnabled = false;
                else
                    btnCalculate.IsEnabled = true;
            }
            else
                btnCalculate.IsEnabled = false;
        }

        private void updateList()
        {
            List<EncryptedModel> models = new List<EncryptedModel>();
            models = DataBase.GetAllEncrypted();
            stckEncrypted.Children.Clear();
            foreach(EncryptedModel m in models)
            {
                encryptedResult result = new encryptedResult();
                if (selectedModel != null)
                    if(m.e == selectedModel.e && m.n == selectedModel.n && m.y == selectedModel.y)
                    {
                        result.txte.Background = new SolidColorBrush(Color.FromRgb(100, 255, 100));
                        result.txtMessage.Background = new SolidColorBrush(Color.FromRgb(100, 255, 100));
                        result.txtn.Background = new SolidColorBrush(Color.FromRgb(100, 255, 100));
                    }
                result.txte.Text = m.e.ToString();
                result.txtn.Text = m.n.ToString();
                result.txtMessage.Text = m.y;
                result.model = m;
                result.update = update;
                result.sendData=_setModel;
                stckEncrypted.Children.Add(result);
            }
        }

        private void setModel(EncryptedModel model)
        {
            selectedModel = model;
            updateList();
            updateButton();
        }
        private bool isPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
