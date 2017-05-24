using GuardianRP.Launcher.Configuration;
using GuardianRP.Service.SocketClient;
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

namespace GuardianRP.Launcher {

    public partial class MainWindow : Window {

        private SocketClient client = new SocketClient(Settings.Instance.ApiEndpoint);

        public MainWindow() {
            InitializeComponent();

            // Create bar at the top of the window that allows it to be dragable
            // It is declared here to prevent the bar getting in my way while using designer
            Rectangle TopBar = new Rectangle();
            TopBar.Width = Width - 64;
            TopBar.Height = 40;
            TopBar.Margin = new Thickness(8, 8, 0, 0);
            TopBar.HorizontalAlignment = HorizontalAlignment.Left;
            TopBar.VerticalAlignment = VerticalAlignment.Top;
            TopBar.Fill = Brushes.Transparent;
            TopBar.MouseDown += (sender, args) => {
                if(args.ChangedButton == MouseButton.Left)
                    DragMove();
            };
            InterGrid.Children.Add(TopBar);

            // Start the api client
            richTextBox.Document.Blocks.Clear();
            client.OnMessageReceived += (sender, args) => {
                richTextBox.Dispatcher.Invoke(() => {
                    richTextBox.Document.Blocks.Add(new Paragraph(new Run(args.Message)));
                });
            };

            client.Start();
        }

        private void OnExitButtonClicked(object sender, MouseButtonEventArgs e) {
            Application.Current.Shutdown();
        }

        private void OnHideClicked(object sender, MouseButtonEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            client.Send(Encoding.UTF8.GetBytes(textBox.Text));
            textBox.Text = string.Empty;
        }

    }

}
