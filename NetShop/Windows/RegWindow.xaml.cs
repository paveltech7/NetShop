using System.Linq;
using System.Windows;

namespace NetShop
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            string fio = "";
            string adress = "";
            string password = "";
            string telephone = "";

            if (tbFIO.Text != "")
            {
                fio = tbFIO.Text;
            }
            else
            {
                MessageBox.Show("Введите правильно ФИО");
            }

            if (pbPassword.Password != "")
            {
                password = pbPassword.Password;
            }
            else
            {
                MessageBox.Show("Введите правильно пароль");
            }

            if (tbAdress.Text != "")
            {
                adress = tbAdress.Text;
            }

            if (tbTelephone.Text != "")
            {
                telephone = tbTelephone.Text;
            }

            if ((tbFIO.Text != "") && (pbPassword.Password != ""))
            {
                Consumer consumer = new Consumer();

                consumer.Fio = fio;
                consumer.Adress = adress;
                consumer.Password = password;
                consumer.Telephone = telephone;
                using (ApplicationContext db = new ApplicationContext())
                {
                    // добавляем в бд
                    var user = db.Consumers.FirstOrDefault(p => p.Fio == consumer.Fio);
                    if (user != null)
                    {
                        MessageBox.Show("Такой пользователь уже есть");
                    }
                    else
                    {
                        consumer.Id = db.Consumers.Count() + 1;
                        db.Consumers.Add(consumer);
                        db.SaveChanges();
                        MessageBox.Show("Пользователь добавлен");
                        MainWindow newMainWindow = new MainWindow();
                        this.Close();
                        newMainWindow.Show();
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow();
            this.Close();
            newMainWindow.Show();
        }
    }
}