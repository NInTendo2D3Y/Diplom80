using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для ActionPage.xaml
    /// </summary>
    public partial class ActionPage : Page
    {

        public static DB.DiplomEntities Connector = Class.Connector.GetDatabase();
        public static ObservableCollection<DB.ActionProduct> productis;

        public class quantitySorts
        {
            public string DisplayName { get; set; }
            public string PropertyName { get; set; }
            public bool Ascending { get; set; }
        }
        int viewcount = 0;
        int totalcount = 0;
        public static List<quantitySorts> QuantitySorts { get; set; } = new List<quantitySorts>()
        {
            new quantitySorts(){DisplayName="Без сортировки", PropertyName=null, Ascending=true},
            new quantitySorts(){DisplayName="Возрастание количества", PropertyName="quantity", Ascending=true},
            new quantitySorts(){DisplayName="Убывание количества", PropertyName="quantity", Ascending=false},
        };
        
        public ActionPage()
        {
            InitializeComponent();
            productis = new ObservableCollection<DB.ActionProduct>(Connector.ActionProducts.ToList());
            
            ProductList.ItemsSource = productis;
            SortCombo.SelectedIndex = 0;
            viewcount = productis.Count;
            totalcount = productis.Count;
            LabelCount.Content = $"{viewcount}/{totalcount}";
            DataContext = this;
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search(SearchText.Text.Trim());
        }
        private void Search(string substring)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);
            if (view == null) return;
            viewcount = 0;
            view.Filter = new Predicate<object>(obj =>
            {
                bool IsView = ((DB.ActionProduct)obj).Name.ToLower().Contains(substring.ToLower());
                if (IsView) viewcount++;
                return IsView;
            });
            LabelCount.Content = $"{viewcount}/{totalcount}";
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            quantitySorts itemSorting = SortCombo.SelectedItem as quantitySorts;
            Sort(itemSorting.PropertyName, itemSorting.Ascending);
        }
        private void Sort(string property, bool asc)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);
            if (view == null) return;
            view.SortDescriptions.Clear();
            if (property != null)
            {
                view.SortDescriptions.Add(new SortDescription(property, asc ? ListSortDirection.Ascending : ListSortDirection.Descending));
            }
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
