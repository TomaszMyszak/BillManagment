using R_III_WPF.Properties;
using R_III_WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace R_III_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IInfoService infoService;
        private readonly IBillsService billsService;
        private readonly IUsersService usersService;
        List<Bill> billsList = new List<Bill>();

        [InjectionConstructor]
        public MainWindow(IInfoService infoService, 
                           IBillsService billsService,
                           IUsersService usersService)
        {
            this.infoService = infoService;
            this.billsService = billsService;
            this.usersService = usersService;
            InitializeComponent();
            mainDataGrid.Visibility = Visibility.Hidden;
            welcomeLabel.Visibility = Visibility.Visible;
            addNewInfoBtn.Visibility = Visibility.Hidden;
            addNewBillBtn.Visibility = Visibility.Hidden;
            welcomeLabel.FontSize = 20;
            welcomeLabel.FontWeight = FontWeights.Bold;
            welcomeLabel.Content = $"Witamy w aplikacj Bill Manager, {Environment.NewLine}życzymy miłej pracy.";
            mainDataGrid.SelectionChanged += new SelectionChangedEventHandler(mainDataGrid_SelectionChanged);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            welcomeLabel.Visibility = Visibility.Visible;
            mainDataGrid.Visibility = Visibility.Hidden;
            addNewInfoBtn.Visibility = Visibility.Hidden;
            addNewBillBtn.Visibility = Visibility.Hidden;
            welcomeLabel.Content = $"Witamy w aplikacj Bill Manager, {Environment.NewLine}życzymy miłej pracy.";
        }

        private void BillsBtn_Click(object sender, RoutedEventArgs e)
        {
            mainDataGrid.Visibility = Visibility.Visible;
            addNewBillBtn.Visibility = Visibility.Visible;
            welcomeLabel.Visibility = Visibility.Hidden;
            addNewInfoBtn.Visibility = Visibility.Hidden;
            billsList = billsService.GetBillsForUser(Settings.Default.UserName).ToList();
            mainDataGrid.ItemsSource = billsList;
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            welcomeLabel.Visibility = Visibility.Visible;
            mainDataGrid.Visibility = Visibility.Hidden;
            addNewInfoBtn.Visibility = Visibility.Visible;
            welcomeLabel.Content = infoService.GetInformationInStringByUser(Settings.Default.UserName);
        }

        ContextMenu cxMenu = null;
        TextBox txtFN, txtLN, janTB, febTD, marTB, aprTB, mayTB, julTB, junTB, augTB, sepTB, octTB, novTB, decRB, nameTB;
        TextBlock txtId;

        private void AddNewBillBtn_Click(object sender, RoutedEventArgs e)
        {
            cxMenu = new ContextMenu();

            var bill = new Bill();
            txtId = new TextBlock();
            txtId.Text = bill.Id.ToString();
            cxMenu.Items.Add(txtId);

            nameTB = new TextBox();
            nameTB.Text = "Nazwa";
            cxMenu.Items.Add("NAZWA");
            cxMenu.Items.Add(nameTB);

            janTB = new TextBox();
            janTB.Text = "000";
            cxMenu.Items.Add("STYCZEN");
            cxMenu.Items.Add(janTB);

            febTD = new TextBox();
            febTD.Text = "000";
            cxMenu.Items.Add("LUTY");
            cxMenu.Items.Add(febTD);

            marTB = new TextBox();
            marTB.Text = "000";
            cxMenu.Items.Add("MARZEC");
            cxMenu.Items.Add(marTB);

            aprTB = new TextBox();
            aprTB.Text = "000";
            cxMenu.Items.Add("KWIECIEN");
            cxMenu.Items.Add(aprTB);

            mayTB = new TextBox();
            mayTB.Text = "000";
            cxMenu.Items.Add("MAJ");
            cxMenu.Items.Add(mayTB);

            julTB = new TextBox();
            julTB.Text = "000";
            cxMenu.Items.Add("CZERWIEC");
            cxMenu.Items.Add(julTB);

            junTB = new TextBox();
            junTB.Text = "000";
            cxMenu.Items.Add("LIPIEC");
            cxMenu.Items.Add(junTB);

            augTB = new TextBox();
            augTB.Text = "000";
            cxMenu.Items.Add("SIERPIEN");
            cxMenu.Items.Add(augTB);

            sepTB = new TextBox();
            sepTB.Text = "000";
            cxMenu.Items.Add("WRZESIEN");
            cxMenu.Items.Add(sepTB);

            octTB = new TextBox();
            octTB.Text = "000";
            cxMenu.Items.Add("PAŹDZIERNIK");
            cxMenu.Items.Add(octTB);

            novTB = new TextBox();
            novTB.Text = "000";
            cxMenu.Items.Add("LISTOPAD");
            cxMenu.Items.Add(novTB);

            decRB = new TextBox();
            decRB.Text = "000";
            cxMenu.Items.Add("GRUDZIEŃ");
            cxMenu.Items.Add(decRB);


            Button btnSaveNewBill = new Button();
            btnSaveNewBill.Content = "Zapisz";
            cxMenu.Items.Add(btnSaveNewBill);
            cxMenu.IsOpen = true;
            btnSaveNewBill.Click += new RoutedEventHandler(btnSaveNewBill_Click);
        }

        void btnSaveNewBill_Click(object sender, RoutedEventArgs e)
        {
            Bill bill = new Bill()
            {
                April = Double.Parse(aprTB.Text),
                August = Double.Parse(augTB.Text),
                BillName = Double.Parse(nameTB.Text),
                December = Double.Parse(decRB.Text),
                February = Double.Parse(febTD.Text),
                January = Double.Parse(janTB.Text),
                July = Double.Parse(julTB.Text),
                June = Double.Parse(junTB.Text),
                March = Double.Parse(marTB.Text),
                May = Double.Parse(mayTB.Text),
                November = Double.Parse(novTB.Text),
                October = Double.Parse(octTB.Text),
                September = Double.Parse(sepTB.Text),
                UserId = usersService.GetUserIdByName(Settings.Default.UserName)
            };
            billsService.Add(bill);
        }

        private void MainDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject depObj = (DependencyObject)e.OriginalSource;

            while (
                (depObj != null) &&
                !(depObj is DataGridCell) &&
                !(depObj is DataGridColumnHeader))
            {
                depObj = VisualTreeHelper.GetParent(depObj);
            }

            if (depObj == null)
            {
                return;
            }

            if (depObj is DataGridCell)
            {
                while ((depObj != null) && !(depObj is DataGridRow))
                {
                    depObj = VisualTreeHelper.GetParent(depObj);
                }

                DataGridRow dgRow = depObj as DataGridRow;
                dgRow.ContextMenu = cxMenu;
            }
        }

        Bill bill = null;
        private void mainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cxMenu = new ContextMenu();            

            foreach (Bill item in mainDataGrid.SelectedItems)
            {
                bill = new Bill
                {
                    Id = item.Id,
                    April = item.April,
                    August = item.August,
                    BillName = item.BillName,
                    BillsYear = item.BillsYear,
                    December = item.December,
                    February = item.February,
                    January = item.January,
                    July = item.July,
                    June = item.June,
                    March = item.March,
                    May = item.May,
                    November = item.November,
                    October = item.October,
                    September = item.September
                };
            }

            txtId = new TextBlock();
            txtId.Text = bill.Id.ToString();
            cxMenu.Items.Add(txtId);

            nameTB = new TextBox();
            nameTB.Text = bill.BillName.ToString();
            cxMenu.Items.Add("NAZWA");
            cxMenu.Items.Add(nameTB);

            janTB = new TextBox();
            janTB.Text = bill.January.ToString();
            cxMenu.Items.Add("STYCZEŃ");
            cxMenu.Items.Add(janTB);

            febTD = new TextBox();
            febTD.Text = bill.February.ToString();
            cxMenu.Items.Add("LUTY");
            cxMenu.Items.Add(febTD);

            marTB = new TextBox();
            marTB.Text = bill.March.ToString();
            cxMenu.Items.Add("MARZEC");
            cxMenu.Items.Add(marTB);

            aprTB = new TextBox();
            aprTB.Text = bill.April.ToString();
            cxMenu.Items.Add("KWIECIEŃ");
            cxMenu.Items.Add(aprTB);

            mayTB = new TextBox();
            mayTB.Text = bill.May.ToString();
            cxMenu.Items.Add("MAJ");
            cxMenu.Items.Add(mayTB);

            julTB = new TextBox();
            julTB.Text = bill.July.ToString();
            cxMenu.Items.Add("CZERWIEC");
            cxMenu.Items.Add(julTB);

            junTB = new TextBox();
            junTB.Text = bill.June.ToString();
            cxMenu.Items.Add("LIPIEC");
            cxMenu.Items.Add(junTB);

            augTB = new TextBox();
            augTB.Text = bill.August.ToString();
            cxMenu.Items.Add("SIERPIEN");
            cxMenu.Items.Add(augTB);

            sepTB = new TextBox();
            sepTB.Text = bill.September.ToString();
            cxMenu.Items.Add("WRZEŚIEŃ");
            cxMenu.Items.Add(sepTB);

            octTB = new TextBox();
            octTB.Text = bill.October.ToString();
            cxMenu.Items.Add("PAŹDZIERNIK");
            cxMenu.Items.Add(octTB);

            novTB = new TextBox();
            novTB.Text = bill.November.ToString();
            cxMenu.Items.Add("LISTOPAD");
            cxMenu.Items.Add(novTB);

            decRB = new TextBox();
            decRB.Text = bill.December.ToString();
            cxMenu.Items.Add("GRUDZIEŃ");
            cxMenu.Items.Add(decRB);

            Button btnSave = new Button();
            btnSave.Content = "Zapisz";
            cxMenu.Items.Add(btnSave);
            btnSave.Click += new RoutedEventHandler(btnSave_Click);
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Bill bill = new Bill();

            foreach (Bill item in billsList)
            {
                if (item.Id == Convert.ToInt32(txtId.Text))
                {
                    item.April = Double.Parse(aprTB.Text == "" ? "0" : aprTB.Text);
                    item.August = Double.Parse(augTB.Text == "" ? "0" : augTB.Text);
                    item.BillName = Double.Parse(nameTB.Text == "" ? "0" : nameTB.Text);
                    item.December = Double.Parse(decRB.Text == "" ? "0" : decRB.Text);
                    item.February = Double.Parse(febTD.Text == "" ? "0" : febTD.Text);
                    item.January = Double.Parse(janTB.Text == "" ? "0" : janTB.Text);
                    item.July = Double.Parse(julTB.Text == "" ? "0" : julTB.Text);
                    item.June = Double.Parse(junTB.Text == "" ? "0" : junTB.Text);
                    item.March = Double.Parse(marTB.Text == "" ? "0" : marTB.Text);
                    item.May = Double.Parse(mayTB.Text == "" ? "0" : mayTB.Text);
                    item.November = Double.Parse(novTB.Text == "" ? "0" : novTB.Text);
                    item.October = Double.Parse(octTB.Text == "" ? "0" : octTB.Text);
                    item.September = Double.Parse(sepTB.Text == "" ? "0" : sepTB.Text);

                    billsService.Edit(item);
                }
            }

            mainDataGrid.SelectionChanged -=
                new SelectionChangedEventHandler(mainDataGrid_SelectionChanged);

            mainDataGrid.ItemsSource = null;
            mainDataGrid.ItemsSource = billsList;
            mainDataGrid.SelectedIndex = -1;

            cxMenu.IsOpen = false;
            mainDataGrid.SelectionChanged +=
                new SelectionChangedEventHandler(mainDataGrid_SelectionChanged);
        }

        private void AddNewInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            cxMenu = new ContextMenu();
            DependencyObject depObj = (DependencyObject)e.OriginalSource;

            txtId = new TextBlock();
            txtId.Text = "Informacja:   ";
            cxMenu.Items.Add(txtId);

            txtFN = new TextBox();
            txtFN.Text = "Informacja   ";
            cxMenu.Items.Add(txtFN);

            txtId = new TextBlock();
            txtId.Text = "Treść:   ";
            cxMenu.Items.Add(txtId);

            txtLN = new TextBox();
            txtLN.Text = "Treść   ";
            cxMenu.Items.Add(txtLN);

            Button btnSave2 = new Button();
            btnSave2.Content = "Zapisz";
            cxMenu.Items.Add(btnSave2);
            cxMenu.IsOpen = true;
            btnSave2.Click += new RoutedEventHandler(btnSave2_Click);
        }

        void btnSave2_Click(object sender, RoutedEventArgs e)
        {
            Information info = new Information()
            {
                Content = txtFN.Text,
                InformationName = txtLN.Text,
                UserId = usersService.GetUserIdByName(Settings.Default.UserName)
        };

            infoService.AddInformation(info);

            cxMenu.IsOpen = false;
        }
    }
}