using ManGo.Data.Database;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using ManGo.Data.Database.Tebles;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManGo
{
    /// <summary>
    /// Логика взаимодействия для Login_of_SignUpPage.xaml
    /// </summary>
    public partial class Login_of_SignUpPage : Window
    {
        public Login_of_SignUpPage()
        {
            InitializeComponent();
        }
        private double previousWidth;
        private SnackbarMessageQueue messageQueue = new SnackbarMessageQueue();

        private void I_list_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void I_conservation_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void I_User_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width != previousWidth)
            {
                double newWidth;
                newWidth  = e.NewSize.Width - 360;

                tb_search.Width = newWidth;
                previousWidth = e.NewSize.Width;
            }
        }

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

        private void tb_Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string name = tb_Name.Text;
            if (name.Length < 5)
            {
                // Если имя короче 5 символов, обновите HelperText, чтобы сообщить об этом
                //.SetValue(materialDesign: HintAssist.HelperTextProperty, "Name should be at least 5 characters");
            }
            else
            {
                // Если имя удовлетворяет условиям, сбросьте HelperText
                //tb_Name.SetValue(materialDesign: HintAssist.HelperTextProperty, null);
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            sp_SidnUp.Visibility = Visibility.Visible;
            sp_login.Visibility = Visibility.Collapsed;
            Login.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x9E, 0xB3, 0x84));
            SignUp.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xCE, 0xDE, 0xBD));
            Create.Content = "Завершити реєстрацію";
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            sp_SidnUp.Visibility = Visibility.Collapsed;
            sp_login.Visibility = Visibility.Visible;
            SignUp.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x9E, 0xB3, 0x84));
            Login.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xCE, 0xDE, 0xBD));
            Create.Content = "Увійти";
        }

        private async void Login_SignUp_ClickAsync(object sender, RoutedEventArgs e)
        {
            switch (sp_SidnUp.Visibility)
            {
                case Visibility.Visible:
                    string login = tb_Name.Text;
                    if (PasswordBox1.Password == PasswordBox2.Password)
                    {
                        string password = PasswordBox1.Password;
                        if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                        {
                            using (var context = new ManGoBDContext())
                            {
                                var existingUser = context.User.FirstOrDefault(u => u.Name == login);
                                if (existingUser != null)
                                {
                                    ShowSnackbar("Це ім'я зайняте!");
                                }
                                else
                                {
                                    context.User.Add(new User(login, password));
                                    context.SaveChanges();
                                    I_User.Visibility = Visibility.Visible;
                                    ShowSnackbar("Ви успішно увійшли");
                                }
                            }
                        }
                    }
                    break;

                case Visibility.Collapsed:
                    string login2 = tb_Name2.Text;
                    string password2 = PasswordBox3.Password;
                    try
                    {
                        bool isUserAuthenticated = await Authenticate(login2, password2);

                        if (isUserAuthenticated)
                        {
                            ShowSnackbar(" Ви успішно увійшли");
                            I_User.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            ShowSnackbar("Нікого не знайдено");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowSnackbar("Произошла ошибка: " + ex.Message);
                    }
                    break;
            }
        }

        private async Task<bool> Authenticate(string login, string password)
        {
            using (var context = new ManGoBDContext())
            {
                var user = await context.User.SingleOrDefaultAsync(u => u.Name == login && u.Password == password);

                return user != null;
            }
        }
        private void ShowSnackbar(string message)
        {
            messageQueue.Enqueue(message);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

    }
}
