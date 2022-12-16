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

namespace Prb.FastFoodRestaurant.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> frietjes = new List<string>();
        List<string> burgers = new List<string>();
        string customerName;
        decimal burgerPrice;
        decimal friesPrice;
        decimal sauce;
        decimal total;
        string text;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CustomerName()
        {
            customerName = txtName.Text;

            if (customerName == "")
            {
                MessageBox.Show($"Beste klant, gelieve uw naam in te vullen !", "FOUT", MessageBoxButton.OK, MessageBoxImage.Error);
                txtName.Focus();
            }
        }

        private void FillCmbBurgers()
        {
            burgers.Add("Bicky Burger");
            burgers.Add("Bicky Cheese");
            burgers.Add("Bicky Rib");

            cmbBurger.ItemsSource = burgers;
            cmbBurger.SelectedIndex = 0;
        }

        private void FillCmbFrietjes()
        {
            frietjes.Add("Groot pak");
            frietjes.Add("Middel pak");
            frietjes.Add("Klein pak");

            cmbFries.ItemsSource = frietjes;
            cmbFries.SelectedIndex = 0;
        }

        private void GetSelectedBurger()
        {
            burgerPrice = 0;

            if (cmbBurger.SelectedIndex == 0)
            {
                //lstOverview.Items.Add(cmbBurger.SelectedItem.ToString());
                burgerPrice += (decimal)2.5;
                //lblTicket.Content = burgerPrice.ToString();
            }
            if (cmbBurger.SelectedIndex == 1)
            {
                //lstOverview.Items.Add(cmbBurger.SelectedItem.ToString());
                burgerPrice += (decimal)3;
                //lblTicket.Content = burgerPrice.ToString();
            }
            else if (cmbBurger.SelectedIndex == 2)
            {
                //lstOverview.Items.Add(cmbBurger.SelectedItem.ToString());
                burgerPrice += (decimal)4;
                //lblTicket.Content = burgerPrice.ToString();
            }
        }

        private void GetSelectedTypeOfFries()
        {
            friesPrice = 0;

            if (cmbFries.SelectedIndex == 0)
            {
                friesPrice = 4;
            }
            if (cmbFries.SelectedIndex == 1)
            {
                friesPrice = 3;
            }
            else if (cmbFries.SelectedIndex == 2)
            {
                friesPrice = 2;
            }
        }

        private void Calculation()
        {
            total = burgerPrice + friesPrice + sauce;    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Focus();
            FillCmbFrietjes();
            FillCmbBurgers();
            btnChange.Visibility = Visibility.Collapsed;
            btnConfirm.Visibility = Visibility.Collapsed;
            btnPay.Visibility = Visibility.Collapsed;
            lstOverview.Visibility = Visibility.Collapsed;
            lstPay.Visibility = Visibility.Collapsed;
            btnPrint.Visibility = Visibility.Collapsed;
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            CustomerName();
            GetSelectedBurger();
            GetSelectedTypeOfFries();
            Calculation();

            if (txtName.Text.Length == 0) //indien de naam uit minder dan 1 char bestaat dan :
            {
                lstOverview.Visibility = Visibility.Collapsed;
                txtName.Focus();
            }
            else if (txtName.Text.Length != 0) //anders als de naam meer dan 0 char telt dan :
            {
                lstOverview.Visibility = Visibility.Visible;
                text = $"Klant: {customerName}\nSnack: {cmbBurger.SelectedItem.ToString()}\nFriet :{cmbFries.SelectedItem.ToString()}\nTotaal te betalen: €{total}";
                lstOverview.Items.Add(text);
                btnOrder.Visibility = Visibility.Collapsed;
                btnConfirm.Visibility = Visibility.Visible;
                btnChange.Visibility = Visibility.Visible;
                txtName.Text = null;
                cmbBurger.SelectedIndex = 0;
                cmbFries.SelectedIndex = 0;
            }
        }

        private void CkbMayonaise_Checked(object sender, RoutedEventArgs e)
        {
            sauce = 0;
            if(ckbMayonaise.IsChecked == true)
            {
                sauce += (decimal)0.9;
            }
            if (ckbKetchup.IsChecked == true)
            {
                sauce += (decimal)0.9;
            }
            else if (ckbMayonaise.IsChecked == true && ckbKetchup.IsChecked == true)
            {
                sauce += (decimal)1.8;
            }
            
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = customerName;
            GetSelectedBurger();
            GetSelectedTypeOfFries();
            lstOverview.Items.Clear();
            btnOrder.Visibility = Visibility.Visible;
            ckbKetchup.IsChecked = false;
            ckbMayonaise.IsChecked = false;
            lstOverview.Visibility = Visibility.Collapsed;
            btnConfirm.Visibility = Visibility.Collapsed;
            btnChange.Visibility = Visibility.Collapsed;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lstOverview.Visibility = Visibility.Collapsed;
            btnConfirm.Visibility = Visibility.Collapsed;
            btnChange.Visibility= Visibility.Collapsed;
            lstPay.Visibility = Visibility.Visible;
            btnPay.Visibility = Visibility.Visible;
            lstPay.Items.Add($"Totaal te betalen:\n                           €{total}");

        }

        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            lstPay.Visibility = Visibility.Collapsed;
            btnPay.Visibility = Visibility.Collapsed;
            btnOrder.Visibility= Visibility.Visible;
            btnPrint.Visibility= Visibility.Visible;
            txtName.Text = null;
            lstOverview.Items.Clear();
            int customers = 0;
            lblTicket.Visibility = Visibility.Visible;
            lblTicket.Content = text;

            foreach(var item in lstPay.Items)
            {
                customers++;
            }
            tbkNumberOfCustomers.Text = customers.ToString();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{text}", "KASSA - TICKET" ,MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblTicket.Content = "";
            btnPrint.Visibility = Visibility.Collapsed;
        }
    }
}
