using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NetShop
{
    /// <summary>
    /// Логика взаимодействия для Bill.xaml
    /// </summary>
    public partial class BillWindow : Window
    {
        int idLogin;
        string order;
        bool delivery = false;
        DateTime time;
        List<Item> listItems = new List<Item>();
        public BillWindow(int loginId, string orders)
        {
            idLogin = loginId;
            order = orders;
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            delivery = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            delivery = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bill bill = new Bill();
            bill.DateBill = time;
            bill.IsDelivery = delivery;
            bill.WhoOrdered = idLogin;
            bill.Orders = order;
            using (ApplicationContext db = new ApplicationContext())
            {
                bill.Id = db.Bills.Count() + 1;
                db.Bills.Add(bill);
                db.SaveChanges();
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> orders = new List<int>();
            string[] listOrder = null;
            time = DateTime.Now;
            tbDate.Text = "Дата создания чека: \n" + time.ToString();
            if (order.Length != 0)
            {
                listOrder = order.Split(' ');
            }
            if (listOrder != null)
            {
                foreach (var item in listOrder)
                {
                    int id;
                    Int32.TryParse(item, out id);
                    orders.Add(id);
                }
            }
            
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in orders)
                {
                    var item1 = db.Items.Find(item);
                    listItems.Add(item1);
                }
                dgOrders.ItemsSource = listItems;
            }
        }
    }
}
