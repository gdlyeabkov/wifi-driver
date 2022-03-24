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
using SimpleWifi;
using System.Speech.Synthesis;
using MaterialDesignThemes.Wpf;

namespace Driver
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public SpeechSynthesizer debugger;

        public MainWindow()
        {
            InitializeComponent();

            Initialize();

        }

        public void Initialize()
        {
            debugger = new SpeechSynthesizer();
            Rescan();
        }

        public void Rescan()
        {
            RowDefinitionCollection rows = wifiTable.RowDefinitions;
            int lastRowIndex = rows.Count;
            if (rows.Count >= 1)
            {
                wifiTable.RowDefinitions.Clear();
            }
            Wifi wifi = new Wifi();
            IEnumerable<AccessPoint> accessPoints = wifi.GetAccessPoints();
            foreach (AccessPoint accessPoint in accessPoints)
            {
                String wifiConnectionName = accessPoint.Name;
                bool wifiConnectionSecurity = accessPoint.IsSecure;
                uint wifiConnectionChannel = accessPoint.SignalStrength;
                uint wifiConnectionSignal = accessPoint.SignalStrength;
                // debugger.Speak(@"Название: " + wifiConnectionName);
                StackPanel wifiConnection = new StackPanel();
                wifiConnection.Orientation = Orientation.Horizontal;
                TextBlock wifiConnectionNameLabel = new TextBlock();
                wifiConnectionNameLabel.Text = wifiConnectionName;

                StackPanel wifiConnectionSecurityContainer = new StackPanel();
                wifiConnectionSecurityContainer.Orientation = Orientation.Horizontal;
                String rawWifiConnectionSecurity = "";
                if (wifiConnectionSecurity)
                {
                    rawWifiConnectionSecurity = "AES/TKIP";
                    PackIcon securityIcon = new PackIcon();
                    securityIcon.Kind = PackIconKind.Lock;
                    wifiConnectionSecurityContainer.Children.Add(securityIcon);
                }
                else
                {
                    rawWifiConnectionSecurity = "Не безопасное подключение";
                }
                TextBlock wifiConnectionSecurityLabel = new TextBlock();
                wifiConnectionSecurityLabel.Text = rawWifiConnectionSecurity;
                Thickness wifiConnectionSecurityLabelMargin = new Thickness();
                wifiConnectionSecurityLabelMargin.Left = 25;
                wifiConnectionSecurityLabel.Margin = wifiConnectionSecurityLabelMargin;
                wifiConnectionSecurityContainer.Children.Add(wifiConnectionSecurityLabel);

                String rawWifiConnectionChannel = wifiConnectionChannel.ToString();
                TextBlock wifiConnectionChannelLabel = new TextBlock();
                wifiConnectionChannelLabel.Text = rawWifiConnectionChannel;

                /*String rawWifiConnectionSingal = wifiConnectionSignal.ToString();
                TextBlock wifiConnectionSignalLabel = new TextBlock();
                wifiConnectionSignalLabel.Text = rawWifiConnectionSingal;*/
                String rawWifiConnectionSingal = wifiConnectionSignal.ToString();
                PackIcon wifiConnectionSignaIcon = new PackIcon();
                if (wifiConnectionSignal >= 0 && wifiConnectionSignal < 25)
                {
                    wifiConnectionSignaIcon.Kind = PackIconKind.WifiStrength0;
                }
                else if (wifiConnectionSignal >= 25 && wifiConnectionSignal < 50)
                {
                    wifiConnectionSignaIcon.Kind = PackIconKind.WifiStrength1;
                }
                else if (wifiConnectionSignal >= 50 && wifiConnectionSignal < 75)
                {
                    wifiConnectionSignaIcon.Kind = PackIconKind.WifiStrength2;
                }
                else if (wifiConnectionSignal >= 75 && wifiConnectionSignal < 100)
                {
                    wifiConnectionSignaIcon.Kind = PackIconKind.WifiStrength3;
                }
                else if (wifiConnectionSignal == 100)
                {
                    wifiConnectionSignaIcon.Kind = PackIconKind.WifiStrength4;
                }

                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(35);
                wifiTable.RowDefinitions.Add(rowDefinition);
                rows = wifiTable.RowDefinitions;
                lastRowIndex = rows.Count - 1;
                Border wifiConnectionNameBorder = new Border();
                wifiConnectionNameBorder.BorderBrush = System.Windows.Media.Brushes.Black;
                wifiConnectionNameBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                wifiConnectionNameBorder.Child = wifiConnectionNameLabel;
                Grid.SetRow(wifiConnectionNameBorder, lastRowIndex);
                Grid.SetColumn(wifiConnectionNameBorder, 0);
                wifiTable.Children.Add(wifiConnectionNameBorder);
                Border wifiConnectionSecurityContainerBorder = new Border();
                wifiConnectionSecurityContainerBorder.BorderBrush = System.Windows.Media.Brushes.Black;
                wifiConnectionSecurityContainerBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                wifiConnectionSecurityContainerBorder.Child = wifiConnectionSecurityContainer;
                Grid.SetRow(wifiConnectionSecurityContainerBorder, lastRowIndex);
                Grid.SetColumn(wifiConnectionSecurityContainerBorder, 1);
                wifiTable.Children.Add(wifiConnectionSecurityContainerBorder);
                Border wifiConnectionChannelBorder = new Border();
                wifiConnectionChannelBorder.BorderBrush = System.Windows.Media.Brushes.Black;
                wifiConnectionChannelBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                wifiConnectionChannelBorder.Child = wifiConnectionChannelLabel;
                Grid.SetRow(wifiConnectionChannelBorder, lastRowIndex);
                Grid.SetColumn(wifiConnectionChannelBorder, 2);
                wifiTable.Children.Add(wifiConnectionChannelBorder);
                Border wifiConnectionSignalBorder = new Border();
                wifiConnectionSignalBorder.BorderBrush = System.Windows.Media.Brushes.Black;
                wifiConnectionSignalBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                // wifiConnectionSignalBorder.Child = wifiConnectionSignalLabel;
                wifiConnectionSignalBorder.Child = wifiConnectionSignaIcon;
                Grid.SetRow(wifiConnectionSignalBorder, lastRowIndex);
                Grid.SetColumn(wifiConnectionSignalBorder, 3);
                wifiTable.Children.Add(wifiConnectionSignalBorder);
            }
        }

        private void RescanHandler(object sender, RoutedEventArgs e)
        {
            Rescan();
        }
    }
}
