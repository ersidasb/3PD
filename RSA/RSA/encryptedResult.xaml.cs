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
    /// Interaction logic for encryptedResult.xaml
    /// </summary>
    public partial class encryptedResult : UserControl
    {
        public encryptedResult()
        {
            InitializeComponent();
        }

        public EncryptedModel model = new EncryptedModel();
        public Delegate update;
        public Delegate sendData;

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            update.DynamicInvoke();
            sendData.DynamicInvoke(model);
        }
    }
}
