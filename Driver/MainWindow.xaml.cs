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
            Wifi wifi = new Wifi();
            IEnumerable<AccessPoint> accessPoints = wifi.GetAccessPoints();
            foreach (AccessPoint accessPoint in accessPoints)
            {
                String wifiConnectionName = accessPoint.Name;
                // debugger.Speak(@"Название: " + wifiConnectionName);
                StackPanel wifiConnection = new StackPanel();
                wifiConnection.Orientation = Orientation.Horizontal;
                TextBlock wifiConnectionNameLabel = new TextBlock();
                wifiConnectionNameLabel.Text = wifiConnectionName;
                wifiConnection.Children.Add(wifiConnectionNameLabel);
                wifiConnections.Children.Add(wifiConnection);
            }
        }

    }
}
