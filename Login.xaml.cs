using R_III_WPF.Properties;
using R_III_WPF.Services.Interfaces;
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
using System.Windows.Shapes;
using Unity;

namespace R_III_WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly MainWindow mainWindow;
        private readonly IUsersService usersService;

        public Login()
        {
            InitializeComponent();
        }

        [InjectionConstructor]
        public Login(MainWindow mainWindow, IUsersService usersService)
        {
            this.usersService = usersService;
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = new Users()
            {
                UserName = loginLoginInput.Text.Replace(System.Environment.NewLine, "").Replace("\n", ""),
                Pasword = passwordLoginInput.Text.Replace(System.Environment.NewLine, "").Replace("\n", ""),
            };

            var result = usersService.Login(user);

            if (result == true)
            {
                Settings.Default.UserName = user.UserName;
                mainWindow.Show();
                Close();
            }
            else
            {
                statusLabel.Content = "Niepoprawny login lub hasło";
            }

        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var user = new Users()
            {
                UserName = loginRegInput.Text.Replace(System.Environment.NewLine, "").Replace("\n", ""),
                Pasword = passwordRegInput.Text.Replace(System.Environment.NewLine, "").Replace("\n", ""),
                PhoneNumber = Int32.Parse(telNumberRegInput.Text)
            };

            var result = usersService.Register(user);

            if (result == true)
            {
                Settings.Default.UserName = user.UserName;
                mainWindow.Show();
                Close();
            }
            else
            {
                statusLabelReg.Content = "Bład podczas rejestracj";
            }

            mainWindow.Show();
            Close();
        }
    }
}
