using comon.tcp2;
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

namespace first.webBrowser
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            const string myIp = "127.0.0.1";
            const int portToListen = 8001;

            var tcpclient = TcpClientManager.Start(myIp, portToListen, "GET docOnServer.html");
            var response = Common.GetDataFromNetworkStream(tcpclient.GetStream());
            tcpclient.Close();

            htmlRenderer.Text = response;
        }
    }
}
