using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Coursework2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DeliverySystem deliverySystem = new DeliverySystem();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddCourier_Click(object sender, RoutedEventArgs e)
        {
            deliverySystem.CreateCourier(dlvryAreaTxt.Text, dlvryIDTxt.Text, courierTypeTxt.Text);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            deliverySystem.WriteToListBox(lstBox);
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            lstBox.Items.Clear();
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            string parcelPostcode = parcelPostcodeTxt.Text;
            string parcelAddresse = parcelAddresseeTxt.Text;
            string parcelId = parcelIDTxt.Text;
            deliverySystem.AddParcel(parcelPostcode, parcelId, parcelAddresse);
        }

        //TODO close files
        private void Serialize_to_CSV(object sender, RoutedEventArgs e)
        {
            deliverySystem.SerializeWrite();
        }

        private void Read_CSV(object sender, RoutedEventArgs e)
        {
            deliverySystem.SerializerRead();
        }
        private void CSV_Clear_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(@"..\..\..\Data\serialize.csv", "");
        }

        private void Swap_Parcel(object sender, RoutedEventArgs e)
        {
            deliverySystem.IdentifySwap(swapIdTxt.Text);
        }

        private void courierDeets_Click(object sender, RoutedEventArgs e)
        {
            deliverySystem.DetailsForCourier(courierIdForDetails.Text, lstBox);
        }
    }
}
