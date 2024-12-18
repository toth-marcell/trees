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
using System.Net.Http;
using Newtonsoft.Json;
namespace trees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient httpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        async void LoadData()
        {
            var response = await httpClient.GetAsync("http://localhost:3000/api");
            var jsonData = await response.Content.ReadAsStringAsync();
            var trees = JsonConvert.DeserializeObject<List<Tree>>(jsonData);
            DataContext = trees;
        }

        async private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var body = new StringContent(JsonConvert.SerializeObject(new Tree
            {
                size = int.Parse(sizeField.Text),
                type = typeField.Text
            }), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:3000/api", body);
            LoadData();
        }
    }
    class Tree
    {
        public int id { get; set; }
        public int size { get; set; }
        public string type { get; set; }
    }
}
