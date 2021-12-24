using Beauty.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Beauty.Views
{
    public partial class MainWindow : Window
    {
        BeautyEntities beautyEntities;
        public MainWindow()
        {
            InitializeComponent();
            beautyEntities = new BeautyEntities();
            clientDataGrid.ItemsSource = beautyEntities.Client.ToList();

        }
        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var client = (Client)clientDataGrid.SelectedItem;
            ClientInfo clientInfo = new ClientInfo(beautyEntities, client, clientDataGrid);
            clientInfo.Show();
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var client = (Client)clientDataGrid.SelectedItem;
            beautyEntities.Client.Remove(client);
            beautyEntities.SaveChanges();
            clientDataGrid.ItemsSource = beautyEntities.Client.ToList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var newClient = new Client();
            beautyEntities.Client.Add(newClient);
            ClientInfo clientInfo = new ClientInfo(beautyEntities, newClient, clientDataGrid);
            clientInfo.Show();

        }
    }
}
