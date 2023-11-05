using System.Windows;

namespace ManGo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HomePage homePage = new HomePage();
            homePage.Show();
            //Login_of_SignUpPage login_Of_SignUpPage = new Login_of_SignUpPage();
            //login_Of_SignUpPage.Show();
            Close();
        }
    }
}
