using MahApps.Metro.Controls.Dialogs;
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

namespace ManGo
{
    /// <summary>
    /// Логика взаимодействия для ReadingWindow.xaml
    /// </summary>
    public partial class ReadingWindow : Window
    {
        private List<string> photo;
        private int currentPageIndex = 0;

        public ReadingWindow(List<string> photo)
        {
            InitializeComponent();
            this.photo = photo;
            DisplayPage(currentPageIndex);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Здесь n - количество страниц
            int n = photo.Distinct().Count();

            for (int i = 1; i <= n; i++)
            {
                PageComboBox.Items.Add(i);
            }
        }

        private void PageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageComboBox.SelectedItem != null)
            {
                int selectedPage = (int)PageComboBox.SelectedItem;
                // Перейдите к выбранной странице
                DisplayPage(selectedPage - 1);
            }
        }

        private void DisplayPage(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < photo.Count)
            {
                Image pageImage = new Image();
                pageImage.Source = new BitmapImage(new Uri(photo[pageIndex]));
                pageImage.Stretch = Stretch.Uniform;
                pageImage.HorizontalAlignment = HorizontalAlignment.Center;
                pageImage.VerticalAlignment = VerticalAlignment.Center;

                PageContainer.Children.Clear();
                PageContainer.Children.Add(pageImage);
            }
        }

        private void PreviousPage()
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                DisplayPage(currentPageIndex);
            }
        }

        private void NextPage()
        {
            if (currentPageIndex < photo.Count - 1)
            {
                currentPageIndex++;
                DisplayPage(currentPageIndex);
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            PreviousPage();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            NextPage();
        }

     
    }

}

