using System;
using System.Windows;
using EyesControllTV;

namespace EyeControllTV
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ArduinoConnection a;
        public static GazeDataStream stream;
        public static Point eyePoint;
        double eyeX, eyeY;
        public System.Windows.Threading.DispatcherTimer d_timer;

        public MainWindow()
        {
            InitializeComponent();
            a = new ArduinoConnection("COM3", 9600);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stream = new GazeDataStream();
            stream.startEyeTrack();

            d_timer = new System.Windows.Threading.DispatcherTimer();
            d_timer.Tick += new EventHandler(setTime);
            d_timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            d_timer.IsEnabled = true;
        }

        public void setTime(object sender, EventArgs e)
        {
            eyeX = stream.getEyeX();
            eyeY = stream.getEyeY();
            eyePoint = new Point(eyeX, eyeY);

            label1.Content = eyePoint;

            if (eyePoint.X == 10 && eyePoint.Y >= 9 && eyePoint.Y <= 10)
            {
                a.PowerON(1000);
            }
            else if (eyePoint.X == 9 && eyePoint.Y >= 9 && eyePoint.Y <= 10)
            {
                a.NextChannel(1000);
            }
            else if (eyePoint.X == 8 && eyePoint.Y >= 9 && eyePoint.Y <= 10)
            {
                a.PrevChannel(1000);
            }
        }
    }
}
