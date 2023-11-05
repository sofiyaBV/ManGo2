using ManGo.Data.Api;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ManGo
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private double previousWidth;


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

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            Image_Api_Client image_Api_Client = new Image_Api_Client();
            Image_Api_Client image_Api_Client2 = new Image_Api_Client();
            List<ImageSourse> result_popular_manga = await image_Api_Client.Loaded_NewAndUpdateManga(1);
            foreach (ImageSourse imageData in result_popular_manga)
            {
                LV_popular_manga.Items.Add(imageData);
            }
            List<ImageSourse> result_New = await image_Api_Client2.Loaded_NewAndUpdateManga(2);
            foreach (ImageSourse imageData2 in result_New)
            {
                LV_New.Items.Add(imageData2);
            }
        }

        private void I_list_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void I_User_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void I_conservation_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void LV_popular_manga_MouseEnter(object sender, MouseEventArgs e)
        {
            var stackPanel = sender as StackPanel; // Получаем StackPanel, на котором было событие
            if (stackPanel != null)
            {
                ImageSource imageSource = ((Image)stackPanel.Children[0]).Source; // Получаем изображение
                string text = ((TextBlock)stackPanel.Children[1]).Text; // Получаем текст

                ToolTip toolTip = new ToolTip();
                toolTip.Placement = PlacementMode.Right;

                Grid toolTipContentGrid = new Grid();
                toolTipContentGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // Первая колонка, размер задается автоматически
                toolTipContentGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); // Вторая колонка, размер определяется автоматически
                toolTipContentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                toolTipContentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                toolTipContentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });


                toolTipContentGrid.Children.Add(new Image { Source = imageSource, Width = 100, Height = 100 }); // Добавляем изображение в первую колонку

                TextBlock textBlock = new TextBlock { Text = text };
                Grid.SetColumn(textBlock, 1); // Устанавливаем колонку для TextBlock
                Grid.SetColumn(textBlock, 2);
                toolTipContentGrid.Children.Add(textBlock); // Добавляем TextBlock во вторую колонку

                toolTip.Content = toolTipContentGrid;

                stackPanel.ToolTip = toolTip; // Присваиваем ToolTip к StackPanel
            }
        }

        

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string href = null;
            if (sender is StackPanel stackPanel)
            {
                if (stackPanel.DataContext is ImageSourse imageSourse)
                {
                    href = imageSourse.Href;
                }

                TitleWindow titleWindow = new TitleWindow(href);
                if (this.WindowState == WindowState.Maximized)
                {
                    titleWindow.WindowState = WindowState.Maximized; // Установите новому окну состояние развернутого окна
                }
                else
                {
                    titleWindow.Width = this.Width;
                    titleWindow.Height = this.Height;
                    titleWindow.Left = this.Left;
                    titleWindow.Top = this.Top;
                }
                titleWindow.Show();
                Close();
            }
        }
    }
}
