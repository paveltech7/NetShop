using System;
using System.Linq;
using System.Windows;

namespace NetShop
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

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            this.Close();
            regWindow.Show();
        }

        private void AthButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Consumers.Where(p => (p.Fio == tbLogin.Text && p.Password == tbPassword.Password));
                try
                {
                    var user = users.Single();
                    BuyWindow BuyWindow = new BuyWindow(user.Id);
                    this.Close();
                    BuyWindow.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Такого пользователя нет");
                }
            }
        }
    }
}