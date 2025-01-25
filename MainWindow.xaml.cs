using System.Net.Http;
using System.Windows;
using System.Windows.Threading;

namespace heart_reate
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;


            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                    this.DragMove();
            };
        }

        DispatcherTimer timer = new DispatcherTimer();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Background = System.Windows.Media.Brushes.Transparent;
            this.Topmost = true;
            this.btnStart.Visibility = Visibility.Collapsed;

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private async void Timer_Tick(object? sender, EventArgs e)
        {
            try
            {
                var rate = await readHeartRate();
                this.NumberTextBlock.Text = rate.Trim(new char[] { '\n', ' ' });
            }
            catch
            {
                this.NumberTextBlock.Text = "N/A";
            }
        }

        HttpClient _client = new HttpClient();
        private async Task<string> readHeartRate()
        {
            var response = await _client.GetAsync("http://192.168.1.250:5000/read");
            return await response.Content.ReadAsStringAsync();
        }
    }
}