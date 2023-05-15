using Diplom.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для EditWind.xaml
    /// </summary>
    public partial class EditWind : Window
    {
        public static DB.DiplomEntities Connection = Class.Connector.GetDatabase();

        
        public DB.Product SelectedProduct { get; set; }
        public List<DB.Providers> Providers { get; set; }
        public List<DB.Unit> Units { get; set; }   

        public EditWind(DB.Product product)
        {
            InitializeComponent();
            SelectedProduct = product;
            Providers= new List<DB.Providers>(Connection.Providers.ToList());
            ProviderCombo.ItemsSource = Providers;
            Units = new List<DB.Unit>(Connection.Units.ToList());
            UnitCombo.ItemsSource = Units;
            ProviderCombo.SelectedIndex= 0;
            UnitCombo.SelectedIndex= 0;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connection.SaveChanges();
                MessageBox.Show("Данные обновлены");
                EditGrid.GetBindingExpression(DataContextProperty).UpdateTarget();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
