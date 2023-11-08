using ManGo.Data.Api;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace ManGo
{
    /// <summary>
    /// Логика взаимодействия для TitleWindow.xaml
    /// </summary>
    public partial class TitleWindow : Window
    {
        public TitleWindow(string url)
        {
            InitializeComponent();
            this.url = url;
        }
        private string url;
        Image_Api_Client client = new Image_Api_Client();
        private double previousWidth;
        List<string> list = new List<string>();

        private void ManGo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HomePage homePage = new HomePage();
            if (this.WindowState == WindowState.Maximized)
            {
                homePage.WindowState = WindowState.Maximized; // Установите новому окну состояние развернутого окна
            }
            else
            {
                homePage.Width = this.Width;
                homePage.Height = this.Height;
                homePage.Left = this.Left;
                homePage.Top = this.Top;
            }
            homePage.Show();
            Close();
        }

        private void I_list_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void I_conservation_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void I_User_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width != previousWidth)
            {
                double newWidth;
                if (I_User.Visibility != Visibility.Collapsed)
                { newWidth  = e.NewSize.Width - 380; }
                else { newWidth  = e.NewSize.Width - 410; }

                tb_search.Width = newWidth;
                previousWidth = e.NewSize.Width;

            }
        }

        private async void bt_reading_Click(object sender, RoutedEventArgs e)
        {
            string uri = client.ReturnUrlTitle(url);
            list = await client.ReturnImageUrls(uri);
            ReadingWindow readingWindow = new ReadingWindow(list);
            if (this.WindowState == WindowState.Maximized)
            {
                readingWindow.WindowState = WindowState.Maximized; // Установите новому окну состояние развернутого окна
            }
            else
            {
                readingWindow.Width = this.Width;
                readingWindow.Height = this.Height;
                readingWindow.Left = this.Left;
                readingWindow.Top = this.Top;
            }
            readingWindow.Show();

            Close();
        }

    }
}
