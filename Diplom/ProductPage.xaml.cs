using Diplom.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        EditWind editWind;
        AddWind addWind;

        
        public static DB.DiplomEntities Connector = Class.Connector.GetDatabase();
        public static ObservableCollection<DB.Product> Products { get; set; }
        public static ObservableCollection<DB.ActionProduct> ActionProducts { get; set; }
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

        public ProductPage()
        {
            InitializeComponent();
            Products= new ObservableCollection<DB.Product>(Connector.Products.ToList());
            ActionProducts = new ObservableCollection<DB.ActionProduct>(Connector.ActionProducts.ToList());
            ProductList.ItemsSource = Products;
            SortCombo.SelectedIndex = 0;
            viewcount = Products.Count;
            totalcount= Products.Count;
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
                bool IsView = ((DB.Product)obj).NameProduct.ToLower().Contains(substring.ToLower());
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
            Product product = new Product();
            ActionProduct actionProduct = new ActionProduct()
            {
                artikul = product.Artikul,
                Name=product.NameProduct,
                Provide=product.Provider,
                ProductQuantity=product.quantity,
                ProductUnit=product.UnitProduct,
                DateAction=DateTime.Now,
                StatusProduct="Списано",
                
            };
            Connector.ActionProducts.Add(actionProduct);
            Connector.SaveChanges();
            NavigationService.Navigate(Class.NextPage.GetActionPage());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            ActionProduct actionProduct = new ActionProduct()
            {
                artikul = product.Artikul,
                Name = product.NameProduct,
                Provide = product.Provider,
                ProductQuantity = product.quantity,
                ProductUnit = product.UnitProduct,
                DateAction = DateTime.Now,
                StatusProduct = "Выдано",

            };
            Connector.ActionProducts.Add(actionProduct);
            Connector.SaveChanges();
            NavigationService.Navigate(Class.NextPage.GetActionPage2());


        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (editWind != null) return;
            editWind = new EditWind(ProductList.SelectedItem as DB.Product);
            editWind.Closed += EditWind_Closed;
            editWind.Topmost = true;
            editWind.Show();
            ProductList.SelectedIndex = -1;
        }
        private void EditWind_Closed(object sender, EventArgs e)
        {
            editWind = null;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (addWind == null)
            {
                addWind = new AddWind();
            }
            addWind.Closed += AddWind_Closed;
            addWind.ShowDialog();
        }

        private void AddWind_Closed(object sender, EventArgs e)
        {
            addWind = null;
        }

        private void crib(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(Class.NextPage.GetActionPage());

        }

        private void extradite(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Class.NextPage.GetActionPage2());
        }
        
    }
}
