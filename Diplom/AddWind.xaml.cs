using Diplom.DB;
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
using System.ComponentModel;
using System.Runtime.Serialization;
using Diplom.Class;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для AddWind.xaml
    /// </summary>
    public partial class AddWind : Window
    {
        public static DB.DiplomEntities Connection= Class.Connector.GetDatabase();
       
        public DB.Product Products { get; set; }
        public List<DB.Providers> Providers { get; set; }
        public List<DB.Unit> Units { get; set; }
        
        public AddWind()
        {
            InitializeComponent();
            Products= new DB.Product();
            Providers = new List<DB.Providers>(Connection.Providers.ToList());
            ProviderCombo.ItemsSource= Providers;
            Units = new List<DB.Unit>(Connection.Units.ToList());
            UnitCombo.ItemsSource= Units;
            DataContext = this;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product()
            {
                Artikul = Convert.ToInt32(tbArtikul.Text.Trim()),
                NameProduct = tbNameProduct.Text.Trim(),
                Provider = ProviderCombo.Text.Trim(),
                DateOfDelivery=DateTime.UtcNow,
                ExpirationDate=ExpirationDate.SelectedDate.Value,
                quantity=Convert.ToInt32(tbQuantity.Text.Trim()),
                UnitProduct=UnitCombo.Text.Trim(),
            };
            Connection.Products.Add(product);
            Connection.SaveChanges();
            MessageBox.Show("Данные обновлены");
            this.Close();
        }
    }
}
