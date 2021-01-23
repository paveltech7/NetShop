using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;

namespace NetShop
{
    
    /// <summary>
    /// Логика взаимодействия для BuyWindow.xaml
    /// </summary>
    public partial class BuyWindow : Window
    {
        List<int> purchases = new List<int>();
        int selected;
        int loginId;
        public BuyWindow(int idLogin)
        {
            InitializeComponent();
            loginId = idLogin;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                TreeViewItem ClothesTreeView = new TreeViewItem();
                var clothes = from u in db.Clothes
                              join c in db.Items on u.ItemId equals c.Id
                              select c.Name;

                foreach (var item in clothes)
                {
                    ClothesTreeView.Items.Add(item);
                }
                ClothesTreeView.Selected += TreeViewItem_Selected;
                ClothesTreeView.Header = "Clothes";
                treeCategory.Items.Add(ClothesTreeView);
            }
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs args)
        {
            TreeViewItem tvItem = (TreeViewItem)sender;
            bAdding.IsEnabled = true;
            bBuy.IsEnabled = true;
            using (ApplicationContext db = new ApplicationContext())
            {
                var newItem = from u in db.Clothes
                              join c in db.Items on u.ItemId equals c.Id
                              where c.Name == treeCategory.SelectedItem.ToString()
                              select new { Name = c.Name, Price = c.Price, Description = c.Description, Size = u.Size, Id = c.Id };
                foreach (var el in newItem)
                {
                    tbItem.Text = "Товар: " + el.Name + "\nЦена: " + el.Price + "\nРазмер одежды: " + el.Size + "\nОписание: " + el.Description;
                    selected = el.Id;
                }
            }
        }

        private void Button_Click_Buy(object sender, RoutedEventArgs e)
        {
            if (purchases.Count != 0)
            {
                string orders = null;
                foreach (var item in purchases)
                {
                    orders = orders + item + ' ';
                }
                int removeSpace = orders.Length - 1;
                orders = orders.Remove(removeSpace);
                BillWindow BillWindow = new BillWindow(loginId, orders);
                this.Close();
                BillWindow.Show();
            }
        }

        private void Button_Click_Adding(object sender, RoutedEventArgs e)
        {
            purchases.Add(selected);
        }
    }
}